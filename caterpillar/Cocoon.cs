using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;


namespace Metamorph {
	public class ResultList {
		private ArrayList m_errorMessages = new ArrayList();
		private ArrayList m_urlsLoaded = new ArrayList();
		private int m_urlsSkipped = 0;
		private int m_recordsStored = 0;
		private string m_logfile;
		private DateTime m_startTime;
		private DateTime m_endTime;
		private int m_warningLevel;
		private int m_errorCount = 0;
		private int m_warningCount = 0;


		// Warning Level specifies how many warnings/errors you want logged
		// Warning Level 0 only prints fatal errors
		// Warning Level 1 prints all errors and warnings
		// Warning Level 2 prints all errors and warnings and extra information messages
		// Warning Level 3 prints all previous info plus additional verbose debugging details
		// Warning Level 4 is for special developer debug info
		public ResultList(int i_warningLevel, string i_logfile) {
			m_warningLevel = i_warningLevel;
			m_logfile = i_logfile;
		}

		public int Errors {
			get {
				return m_errorCount;
			}
		}

		public int Warnings {
			get {
				return m_warningCount;
			}
		}

		public int UrlsLoaded {
			get {
				return m_urlsLoaded.Count;
			}
		}

		public int UrlsSkipped {
			get {
				return m_urlsSkipped;
			}
			set {
				m_urlsSkipped = value;
			}
		}

		public int RecordsStored {
			get {
				return m_recordsStored;
			}
			set {
				m_recordsStored = value;
			}
		}

		public TimeSpan ExecutionTime {
			get {
				return m_endTime - m_startTime;
			}
		}

		public void Start() {
			m_startTime = DateTime.Now;
		}

		public void Stop() {
			m_endTime = DateTime.Now;
		}

		public string GetErrorMessage(int i) {
			return (string)m_errorMessages[i];
		}

		public void AppendUrlLoaded(string i_url) {
			m_urlsLoaded.Add(i_url);
		}

		public void AppendInformationMessage(string i_message) {
			File.AppendAllText(m_logfile, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ": " + i_message + "\r\n");
		}

		public void AppendErrorMessage(int i_warningLevel, string i_message) {
			if (i_warningLevel == 0) m_errorCount += 1;
			if (i_warningLevel == 1) m_warningCount += 1;
			if (m_warningLevel >= i_warningLevel) {
				File.AppendAllText(m_logfile, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ": " + i_message + "\r\n");
				m_errorMessages.Add(i_message);
			}
		}
	
	}

	public class Parameter {
		public string m_parameterName;
		public ParameterType m_parameterType;
	}
	
	public enum ParameterType {
		StringParameter,
		IntParameter,
		BoolParameter,
		DecimalParameter,
		DateParameter,
	}
	
	public class RecordType {
		public string m_typeName;
		public Parameter[] m_parameters;
	}

	public class Record {
		public string m_typeName;
		public object[] m_parameterValues;
	}



	public class Cocoon {
		private class Database {
			public SqlConnection m_connection;
			public SqlDataAdapter m_adapter;
			public string m_table;
		}
		private static Hashtable m_databases = new Hashtable();
		private static DataTable m_table;
		private static int m_pendingRecords = 0;


		// Converts null to DbNull.Value
		private static object ConvertNull(object i_input) {
			if (i_input == null) return DBNull.Value;
			else return i_input;
		}

		public static void AddDatabase(string i_databaseName, string i_connectionString, string i_tableName) {
			Database db = new Database();
			db.m_connection = new SqlConnection(i_connectionString);
			db.m_table = i_tableName;
			m_databases.Add(i_databaseName, db);

			// clear the table first
			db.m_connection.Open();
			SqlCommand cmd = new SqlCommand("DELETE FROM " + db.m_table, db.m_connection);
			cmd.ExecuteNonQuery();
		}

		// open the table for inserts
        public static void OpenDataTable(string i_databaseName, RecordType i_recType, Uri i_url, ref ResultList io_result)
        {
			Database db = (Database)m_databases[i_databaseName];
			int i;
			
			string sql = "SELECT url,";
			for (i = 0; i < i_recType.m_parameters.Length; i++) {
				if (i > 0) sql += ",";
				sql += "[" + i_recType.m_parameters[i].m_parameterName + "]";
			}
			sql += " FROM " + db.m_table;
			
			db.m_adapter = new SqlDataAdapter(sql, db.m_connection);
			m_table = new DataTable();

			// Try to fill the data up to 5 times in case there is a temporary network error
			i = 0;
			while (true) {
				i += 1;
				try {
					// Retrieve the table schema
					db.m_adapter.FillSchema(m_table, SchemaType.Mapped);
					break;  // Success
				} catch (Exception ex) {
					// Try 5 times, then log the error and exit
					if (i >= 5) {
						io_result.AppendErrorMessage(0, "ERROR: SQL Exception while processing url: " + i_url + "\r\n" + ex.Message + "\r\n" + ex.StackTrace + "\r\n");
						break;
					}
				}
			}
		}

        public static void UpdateDataTable(string i_databaseName, Uri i_url, ref ResultList io_result)
        {
			Database db = (Database)m_databases[i_databaseName];
			try {
				db.m_adapter.InsertCommand = new SqlCommandBuilder(db.m_adapter).GetInsertCommand();
				db.m_adapter.Update(m_table);
				io_result.RecordsStored += m_pendingRecords;
			} catch (Exception ex) {
				//throw new Exception("Could not update row.\r\n" + ex.Message);
				io_result.AppendErrorMessage(0, "ERROR: Could not update row. url: " + i_url + "\r\n" + ex.Message + "\r\n" + ex.StackTrace + "\r\n");
				string values = "";
				for (int i = m_table.Rows.Count - m_pendingRecords; i < m_table.Rows.Count; i++) {
					for (int j = 0; j < m_table.Columns.Count; j++) {
						values += "\r\n" + m_table.Rows[i][j];
					}
					values += "\r\n";
				}
				io_result.AppendErrorMessage(3, "VALUES: " + values);
			} finally {
				m_pendingRecords = 0;
				m_table.Clear();
			}
		}

		public static void StoreRecord(string i_databaseName, RecordType i_recType, Record i_rec, Uri i_url, ref ResultList io_result) {
			try {
				DataRow row = m_table.NewRow();

				// insert the new data into the table
				for (int i = 0; i < i_recType.m_parameters.Length; i++) {
					row[i_recType.m_parameters[i].m_parameterName] = ConvertNull(i_rec.m_parameterValues[i]);
				}
				row["url"] = i_url.AbsoluteUri;
				m_table.Rows.Add(row);
				m_pendingRecords += 1;
			} catch (Exception ex) {
				io_result.AppendErrorMessage(0, "ERROR: SQL Exception while trying to add row: " + i_url + "\r\n" + ex.Message + "\r\n" + ex.StackTrace + "\r\n");
			}
		}
	}
}
