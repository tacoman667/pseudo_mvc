<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="bar.aspx.cs" Inherits="PseudoMvc_WebAppSample.Views.Home.bar" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%= Model.SubmittedValue %>
    <input type="text" name="HelloMvc" value="Hello Mvc!" />
    <input type="text" name="Id" value="1" />
    <input type="submit" value="Submit" />

</asp:Content>
