Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports DevExpress.Xpo

Namespace MvcGridView.Controllers
	<HandleError> _
	Public Class HomeController
		Inherits Controller
		Public Function Index() As ActionResult
			Dim customers As New XPView(New Session(), GetType(Customer))
			customers.AddProperty("Name")
			customers.AddProperty("CompanyName")

			Return View(customers)
		End Function

		Public Function About() As ActionResult
			Return View()
		End Function
	End Class
End Namespace
