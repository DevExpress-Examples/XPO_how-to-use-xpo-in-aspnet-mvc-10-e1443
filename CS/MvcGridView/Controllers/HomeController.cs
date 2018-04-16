using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Xpo;

namespace MvcGridView.Controllers {
    [HandleError]
    public class HomeController : Controller {
        public ActionResult Index() {
            XPView customers = new XPView(new Session(), typeof(Customer));
            customers.AddProperty("Name");
            customers.AddProperty("CompanyName");

            return View(customers);
        }

        public ActionResult About() {
            return View();
        }
    }
}
