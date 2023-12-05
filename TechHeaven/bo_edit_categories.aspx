<%@ Page Title="" Language="C#" MasterPageFile="~/master_page_admin.Master" AutoEventWireup="true" CodeBehind="bo_edit_categories.aspx.cs" Inherits="TechHeaven.bo_edit_categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main id="main" class="main">
        <section class="section">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body text-center">
                            <h1 class="card-title">Edit Category</h1>

                            <div class="row">
                                <div class="col-lg-4" style="text-align: left;">
                                    <p>Category Name</p>
                                    <asp:TextBox ID="tb_category_name" class="form-control" runat="server" MaxLength="255"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red;" runat="server" ErrorMessage="Category Name required!" ControlToValidate="tb_category_name"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <asp:Label ID="lbl_erro" runat="server"></asp:Label>

                            <asp:Button ID="btn_edit" runat="server" class="btn btn-primary" Text="Edit Category" OnClick="btn_edit_Click" />


                        </div>
                    </div>
                    <!-- End Default Card -->
                </div>
            </div>
        </section>

    </main>

</asp:Content>
