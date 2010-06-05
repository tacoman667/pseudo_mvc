<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Baz.aspx.cs" Inherits="PseudoMvc_WebAppSample.Views.Home.Baz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        You saw me!
    </div>


    <%= this.SystemId %>
    </br/>
    <%= this.ClassId %>
    <br />
    <%= this.ObjectId %>

    <br /><br /><br />
    <% foreach (var item in Model) { %>
        
        <%= item.ToString() %>

    <% } %>

</asp:Content>