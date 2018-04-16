<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.1, Version=9.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>


<script runat="server">
    protected void ASPxGridView1_Init(object sender, EventArgs e) {
        ASPxGridView1.DataSource = ViewData.Model; 
        ASPxGridView1.DataBind();
    }
</script>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
    
    <form id="frm" runat="server" action="?">
        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server"
            OnInit="ASPxGridView1_Init">
        </dxwgv:ASPxGridView>
    </form>
</asp:Content>
