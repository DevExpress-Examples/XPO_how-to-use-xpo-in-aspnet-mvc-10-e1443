<!-- default badges list -->
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1443)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [HomeController.cs](./CS/MvcGridView/Controllers/HomeController.cs) (VB: [HomeController.vb](./VB/MvcGridView/Controllers/HomeController.vb))
* [Default.aspx.cs](./CS/MvcGridView/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/MvcGridView/Default.aspx.vb))
* [Global.asax.cs](./CS/MvcGridView/Global.asax.cs) (VB: [Global.asax.vb](./VB/MvcGridView/Global.asax.vb))
* [Customer.cs](./CS/MvcGridView/Models/Customer.cs) (VB: [Customer.vb](./VB/MvcGridView/Models/Customer.vb))
* [Order.cs](./CS/MvcGridView/Models/Order.cs) (VB: [Order.vb](./VB/MvcGridView/Models/Order.vb))
* [Index.aspx](./CS/MvcGridView/Views/Home/Index.aspx) (VB: [Index.aspx](./VB/MvcGridView/Views/Home/Index.aspx))
* [Site.Master](./CS/MvcGridView/Views/Shared/Site.Master) (VB: [Site.Master](./VB/MvcGridView/Views/Shared/Site.Master))
<!-- default file list end -->
# How to use XPO in ASP.NET MVC 1.0


<p>This example is out-of-date, since it relies on ASP.NET MVC 1. If you are using ASP.NET MVC 2 or later, please refer to <br />
<a href="https://www.devexpress.com/Support/Center/p/E2300">E2300</a></p><p>This example demonstrates how to use eXpress Persistent Objects (<a href="http://www.devexpress.com/xpo"><u>XPO</u></a>) in an <a href="http://www.asp.net/mvc/"><u>ASP.NET MVC 1</u></a> application.</p><p>XPO persistent classes are implemented in a project's Models subfolder.</p><p>Database connection is defined within the Application_Start event handler in Global.asax.</p><p>A data list (XPView) is created in the Controllers/HomeController module.</p><p>The <a href="http://www.devexpress.com/aspxgridview"><u>ASPxGridView</u></a> represents a list view. The ASPxGridView is put within a <form> tag, and the form's action parameter is set to "?" to enable correct callback processing and ViewState restoration.</p>

```aspx
<newline/>
<form id="frm" runat="server" action="/Home/Index"><newline/>
   <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server"<newline/>
       OnInit="ASPxGridView1_Init"><newline/>
   </dxwgv:ASPxGridView><newline/>
</form><newline/>

```

<p><i>(It still doesn't work and throws a </i><a href="https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=393619"><i><u>Validation of viewstate MAC failed</u></i></a><i> error unless Html.RenderPartial is removed from Site.Master.)</i></p><p>To run this example on your system, you should install <a href="http://www.asp.net/mvc/download/"><u>ASP.NET MVC 1.0</u></a>.</p><p><strong>See Also:</strong><strong><br />
</strong><a href="https://www.devexpress.com/Support/Center/p/K18525">How to use XPO in an ASP.NET MVC application</a><br />
<a href="https://www.devexpress.com/Support/Center/p/E3434">How to use XPO in ASP.NET MVC3 application (Razor)</a><strong><br />
</strong><a href="https://www.devexpress.com/Support/Center/p/K18061">K18061</a><br />
<a href="https://www.devexpress.com/Support/Center/p/E2300">E2300</a></p>

<br/>


