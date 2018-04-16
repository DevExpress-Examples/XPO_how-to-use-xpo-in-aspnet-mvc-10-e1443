Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing
Imports System.Data.SqlClient
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB

Namespace MvcGridView
	' Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	' visit http://go.microsoft.com/?LinkId=9394801

	Public Class MvcApplication
		Inherits System.Web.HttpApplication
		Public Shared Sub RegisterRoutes(ByVal routes As RouteCollection)
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}")

			routes.MapRoute("Default", "{controller}/{action}/{id}", New With {Key .controller = "Home", Key .action = "Index", Key .id = ""})

		End Sub

		Protected Sub Application_Start()
			RegisterRoutes(RouteTable.Routes)
			InitXpoDal()
		End Sub

		Private Sub InitXpoDal()
			' for SQL Express
			'string connStr = @"Data Source=.\sqlexpress;Integrated Security=true;AttachDbFilename=|DataDirectory|\MvcGridView.mdf;User Instance=true;";

			' for SQL Server
			Dim connStr As String = "Data Source=(local);Integrated Security=true;AttachDbFilename=|DataDirectory|\MvcGridView.mdf;"

			Dim conn As New SqlConnection(connStr)
			Dim dict As DevExpress.Xpo.Metadata.XPDictionary = New DevExpress.Xpo.Metadata.ReflectionDictionary()
			Dim store As DevExpress.Xpo.DB.IDataStore = DevExpress.Xpo.XpoDefault.GetConnectionProvider(conn, AutoCreateOption.SchemaAlreadyExists)
			dict.GetDataStoreSchema(GetType(Order).Assembly) ' <<< initialize the XPO dictionary
			XpoDefault.DataLayer = New ThreadSafeDataLayer(dict, store)
			XpoDefault.Session = Nothing
		End Sub
	End Class
End Namespace