<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="activation.aspx.cs" Inherits="TechHeaven.activation" %>

<%@ MasterType VirtualPath="~/master_page.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <br /><br /><br />
    <div>
    <center> <h2 style="color:green">Account activated! <asp:Label ID="lbl_nome" runat="server"></asp:Label></h2></center>
    </div>
     <br /> <br /><br />
</asp:Content>
