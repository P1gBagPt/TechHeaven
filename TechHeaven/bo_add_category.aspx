<%@ Page Title="" Language="C#" MasterPageFile="~/master_page_admin.Master" AutoEventWireup="true" CodeBehind="bo_add_category.aspx.cs" Inherits="TechHeaven.bo_add_category" %>
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
                                <p style="text-align: left;">Category Name</p>
                                <asp:TextBox ID="tb_nome" class="form-control" runat="server" MaxLength="255"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red;" runat="server" ErrorMessage="Categoty name required!" ControlToValidate="tb_nome"></asp:RequiredFieldValidator>
                            </div>
                            
                        </div>                     

                        <asp:Label ID="lbl_erro" runat="server"></asp:Label>

                        <asp:Button ID="btn_add_category" runat="server" class="btn btn-primary" Text="add Category" OnClick="btn_add_category_Click"/>


                    </div>
                </div>
                <!-- End Default Card -->
            </div>
        </div>
    </section>
</main>

</asp:Content>
