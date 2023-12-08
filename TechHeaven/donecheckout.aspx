<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="donecheckout.aspx.cs" Inherits="TechHeaven.donecheckout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section class="shopify-cart checkout-wrap padding-large">
    <div class="container">
        <div class="form-group">
            <div class="row d-flex flex-wrap">
                <h2 class="section-title" style="text-transform: none;">Thank you for your purchase, order number <asp:Label ID="lbl_num_encomenda" runat="server"></asp:Label></h2>
                <h3 style="text-transform: none;">A PDF with the order details has been sent to the email <asp:Label ID="lbl_email_utilizador" runat="server"></asp:Label></h3>
            </div>
        </div>
    </div>
</section>


</asp:Content>
