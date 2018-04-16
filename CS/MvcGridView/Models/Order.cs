using DevExpress.Xpo;
using System;


public class Order : XPObject {
    public Order(Session session) : base(session) { }

    DateTime _orderDate;
    public DateTime OrderDate {
        get { return _orderDate; }
        set { SetPropertyValue("OrderDate", ref _orderDate, value); }
    }

    decimal _paymentAmount;
    public decimal PaymentAmount {
        get { return _paymentAmount; }
        set { SetPropertyValue("PaymentAmount", ref _paymentAmount, value); }
    }

    Customer _customer;
    [Association("Customer-Orders")]
    public Customer Customer {
        get { return _customer; }
        set { SetPropertyValue("Customer", ref _customer, value); }
    }
}
