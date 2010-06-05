<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="PseudoMvc_WebAppSample.Views.Home.index" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="http://ajax.microsoft.com/ajax/jquery/jquery-1.4.2.min.js" />
        </Scripts>
    </asp:ScriptManagerProxy>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnSubmit").click(function () {
                alert();
            });
        });
    </script>
    

    <%= this.Model.SubmittedValue %>
    <%= Html.Form<PseudoMvc_WebAppSample.Controllers.HomeController>(c => c.Index(), FormActions.POST) %>
        <%= Html.TextBoxFor(m => m.HelloWorld) %>
        <%= Html.ValidationMessagefor(m => m.HelloWorld) %>
        <%= Html.SubmitButton("btnSubmit", "Submit") %>
    <%= Html.EndForm() %>


</asp:Content>
