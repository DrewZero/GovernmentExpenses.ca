using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Metamorph {
	class Program {
	
		static void Main(string[] i_args) {
			Caterpillar caterpillar = new Caterpillar();
			ResultList result = null;
			
			try {
				// Optionally read the config file from the command line, otherwise use config.xml as default
				if (i_args.Length == 0) {
					result = caterpillar.ProcessConfig("config.xml");
				}
				for (int i = 0; i < i_args.Length; i++) {
					result = caterpillar.ProcessConfig(i_args[i]);
				}

				Console.WriteLine("FINISHED!  Total Time = " + result.ExecutionTime);
				Console.WriteLine(result.RecordsStored + " records stored");
				Console.WriteLine(result.Errors + " errors logged");
				Console.WriteLine(result.Warnings + " warnings logged");
				Console.WriteLine(result.UrlsLoaded + " URLs loaded");
				Console.WriteLine(result.UrlsSkipped + " URLs skipped");
			} catch (Exception ex) {
				Console.WriteLine("FATAL ERROR: " + ex.Message + "\n" + ex.StackTrace);
				if (result != null) result.AppendErrorMessage(0, "FATAL ERROR: " + ex.Message + "\n" + ex.StackTrace);
			}
			// Console.Read(); -- don't read a line because that prevents it from being easily used on automated command lines
		}

	}
}
