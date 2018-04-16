using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.SqlClient;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

namespace MvcGridView {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );

        }

        protected void Application_Start() {
            RegisterRoutes(RouteTable.Routes);
            InitXpoDal();
        }

        private void InitXpoDal() {
            // for SQL Express
            //string connStr = @"Data Source=.\sqlexpress;Integrated Security=true;AttachDbFilename=|DataDirectory|\MvcGridView.mdf;User Instance=true;";

            // for SQL Server
            string connStr = @"Data Source=(local);Integrated Security=true;AttachDbFilename=|DataDirectory|\MvcGridView.mdf;";
            
            SqlConnection conn = new SqlConnection(connStr);
            DevExpress.Xpo.Metadata.XPDictionary dict = new DevExpress.Xpo.Metadata.ReflectionDictionary();
            DevExpress.Xpo.DB.IDataStore store = DevExpress.Xpo.XpoDefault.GetConnectionProvider(conn, AutoCreateOption.SchemaAlreadyExists);
            dict.GetDataStoreSchema(typeof(Order).Assembly);  // <<< initialize the XPO dictionary 
            XpoDefault.DataLayer = new ThreadSafeDataLayer(dict, store);
            XpoDefault.Session = null;
        }
    }
}