using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;


namespace Metamorph {
	public class PageUtils {
		public static void AddDescription(Page i_page, string i_description) {
			HtmlMeta metaDescription = new HtmlMeta();
			metaDescription.Name = "description";
			metaDescription.Content = i_description;
			i_page.Header.Controls.Add(metaDescription);
		}
	}
}
