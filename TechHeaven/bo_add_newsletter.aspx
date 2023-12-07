<%@ Page Title="" Language="C#" MasterPageFile="~/master_page_admin.Master" AutoEventWireup="true" CodeBehind="bo_add_newsletter.aspx.cs" Inherits="TechHeaven.bo_add_newsletter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <main id="main" class="main">
    <section class="section">
        <div class="row">
            <div class="col-lg-12">
                <div class="card">
                    <div class="card-body text-center">
                        <h1 class="card-title">Add Category</h1>

                        
                        <div class="row">
                            <div class="col-lg-12">
                                <p style="text-align: left;">Newsletter Title</p>
                                <asp:TextBox ID="tb_title" class="form-control" runat="server" MaxLength="255"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" runat="server" ErrorMessage="Title is required!" ControlToValidate="tb_title"></asp:RequiredFieldValidator>
                            </div>
                            
                        </div>      
                        <div class="row">
                            <div class="col-lg-12">
                                <p style="text-align: left;">Newsletter body</p>
                                <asp:TextBox ID="tb_news" class="form-control" runat="server" MaxLength="255"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red;" runat="server" ErrorMessage="News is required!" ControlToValidate="tb_news"></asp:RequiredFieldValidator>
                            </div>
                            
                        </div>                     

                        <asp:Label ID="lbl_erro" runat="server"></asp:Label>

                        <asp:Button ID="btn_add_newsletter" runat="server" class="btn btn-primary" Text="Add News" OnClick="btn_add_newsletter_Click"/>


                    </div>
                </div>
                <!-- End Default Card -->
            </div>
        </div>
    </section>
</main>
</asp:Content>
