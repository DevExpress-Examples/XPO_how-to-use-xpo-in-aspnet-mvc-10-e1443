Imports Microsoft.VisualBasic
Imports DevExpress.Xpo
Imports System


Public Class Order
	Inherits XPObject
	Public Sub New(ByVal session As Session)
		MyBase.New(session)
	End Sub

	Private _orderDate As DateTime
	Public Property OrderDate() As DateTime
		Get
			Return _orderDate
		End Get
		Set(ByVal value As DateTime)
			SetPropertyValue("OrderDate", _orderDate, value)
		End Set
	End Property

	Private _paymentAmount As Decimal
	Public Property PaymentAmount() As Decimal
		Get
			Return _paymentAmount
		End Get
		Set(ByVal value As Decimal)
			SetPropertyValue("PaymentAmount", _paymentAmount, value)
		End Set
	End Property

	Private _customer As Customer
	<Association("Customer-Orders")> _
	Public Property Customer() As Customer
		Get
			Return _customer
		End Get
		Set(ByVal value As Customer)
			SetPropertyValue("Customer", _customer, value)
		End Set
	End Property
End Class
