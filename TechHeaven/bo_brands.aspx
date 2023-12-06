<%@ Page Title="" Language="C#" MasterPageFile="~/master_page_admin.Master" AutoEventWireup="true" CodeBehind="bo_brands.aspx.cs" Inherits="TechHeaven.bo_brands" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main id="main" class="main">

        <div class="pagetitle">
            <h1>Brands</h1>
            <nav>
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="main_page_main.aspx">Home</a></li>
                    <li class="breadcrumb-item active">Brands</li>
                </ol>
            </nav>
        </div>
        <!-- End Page Title -->

        <section class="section">
            <div class="row">
                <div class="col-lg-12">

                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Brands</h5>

                            <a href="bo_add_brand.aspx">Add brand</a>

                            <table class="table table-bordered border-primary">
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <HeaderTemplate>
                                        <thead>
                                            <tr>
                                                <th scope="col" style="color: blue;">Brand name</th>
                                                <th scope="col" style="color: blue;">Total of products linked to that brand</th>
                                                <th scope="col" style="color: blue;">Edit</th>
                                            </tr>
                                        </thead>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <tbody>
                                            <tr>
                                                <th scope="row"><%# Eval("nome")%></th>
                                                <th scope="row"><%# Eval("totalMarcasProdutos")%></th>
                                                <td scope="row">
                                                    <asp:LinkButton ID="edit_product" runat="server" OnCommand="edit_product_Command" CommandName="Edit" CommandArgument='<%# Eval("id_brand") %>'>
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
