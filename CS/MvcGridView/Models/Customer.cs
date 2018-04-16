using DevExpress.Xpo;
using System;

public class Customer : XPObject {
    public Customer(Session session) : base(session) { }

    string _name;
    public string Name {
        get { return _name; }
        set { SetPropertyValue("Name", ref _name, value); }
    }
    string _companyName;
    public string CompanyName {
        get { return _companyName; }
        set { SetPropertyValue("CompanyName", ref _companyName, value); }
    }

    [Association("Customer-Orders"), Aggregated]
    public XPCollection<Order> Orders { get { return GetCollection<Order>("Orders"); } }
}
