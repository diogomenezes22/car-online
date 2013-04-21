using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;
using System.Web.WebPages;
using System.Web.Mvc;
namespace carOnline.Models
{
    public class BibliotecaHelpers : System.Web.WebPages.HelperPage
    {
        // Workaround - exposes the MVC HtmlHelper instead of the normal helper
        public static new System.Web.Mvc.HtmlHelper Html
        {
            get { return ((WebViewPage)WebPageContext.Current.Page).Html; }
        }
    }
}
