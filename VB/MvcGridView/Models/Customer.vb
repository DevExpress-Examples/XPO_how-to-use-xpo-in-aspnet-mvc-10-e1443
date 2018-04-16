Imports Microsoft.VisualBasic
Imports DevExpress.Xpo
Imports System

Public Class Customer
	Inherits XPObject
	Public Sub New(ByVal session As Session)
		MyBase.New(session)
	End Sub

	Private _name As String
	Public Property Name() As String
		Get
			Return _name
		End Get
		Set(ByVal value As String)
			SetPropertyValue("Name", _name, value)
		End Set
	End Property
	Private _companyName As String
	Public Property CompanyName() As String
		Get
			Return _companyName
		End Get
		Set(ByVal value As String)
			SetPropertyValue("CompanyName", _companyName, value)
		End Set
	End Property

	<Association("Customer-Orders"), Aggregated> _
	Public ReadOnly Property Orders() As XPCollection(Of Order)
		Get
			Return GetCollection(Of Order)("Orders")
		End Get
	End Property
End Class
