<%@ Page Title="" Language="C#" MasterPageFile="~/master_page_admin.Master" AutoEventWireup="true" CodeBehind="bo_categories.aspx.cs" Inherits="TechHeaven.bo_categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main" class="main">

        <div class="pagetitle">
            <h1>Categories</h1>
            <nav>
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="main_page_admin.aspx">Home</a></li>
                    <li class="breadcrumb-item active">Categories</li>
                </ol>
            </nav>
        </div>
        <!-- End Page Title -->

        <section class="section">
            <div class="row">
                <div class="col-lg-12">

                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Categories</h5>

                            <a href="bo_add_category.aspx">Add category</a>

                            <table class="table table-bordered border-primary">
                                <asp:Repeater ID="Repeater1" runat="server">

                                    <HeaderTemplate>

                                        <thead>
                                            <tr>
                                                <th scope="col" style="color: grey;">Category Name</th>
                                                <th scope="col" style="color: grey;">Total of products associated</th>
                                                <th scope="col" style="color: grey;">Edit</th>
                                            </tr>
                                        </thead>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <tbody>
                                            <tr>
                                                <td scope="row"><%# Eval("nome") %></td>
                                                <td scope="row"><%# Eval("totalProdutos") %></td>
                                                <td scope="row">
                                                    <asp:LinkButton ID="edit_product" runat="server" OnCommand="edit_product_Command" CommandName="Edit" CommandArgument='<%# Eval("id_category") %>'>
        <img src="admin_assets/img/editar.png" alt="Edit" />
                                                    </asp:LinkButton>
                                                </td>


                                            </tr>

                                        </tbody>

                                    </ItemTemplate>

                                </asp:Repeater>
                            </table>
                        </div>
                    </div>

                </div>
            </div>
        </section>

    </main>
</asp:Content>
