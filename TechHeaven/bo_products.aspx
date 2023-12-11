<%@ Page Title="" Language="C#" MasterPageFile="~/master_page_admin.Master" AutoEventWireup="true" CodeBehind="bo_products.aspx.cs" Inherits="TechHeaven.bo_produtos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .stock-red {
            color: red !important;
        }

        .stock-yellow {
            color: #ebc334 !important;
        }

        .stock-green {
            color: green !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main id="main" class="main">

        <div class="pagetitle">
            <h1>Products</h1>
            <nav>
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="main_page_admin.aspx">Home</a></li>
                    <li class="breadcrumb-item active">Products</li>
                </ol>
            </nav>
        </div>
        <!-- End Page Title -->

        <section class="section">
            <div class="row">
                <div class="col-lg-12">

                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Products</h5>
                            <div class="row">
                                <div class="col-6">
                                    <a href="bo_add_product.aspx">Add product</a> | To add stock or a promotion you need to edit the product
                                </div>
                                <div class="col-6 text-end">
                                    <asp:TextBox ID="tb_search" runat="server" placeholder="Search Product"></asp:TextBox>
                                    <asp:LinkButton ID="lb_search" runat="server" OnCommand="lb_search_Command" CommandName="search"><i class="bi bi-search"></i></asp:LinkButton>
                                </div>
                            </div>


                            <!-- Primary Color Bordered Table -->
                            <table class="table table-bordered border-primary">
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <HeaderTemplate>
                                        <thead>
                                            <tr>
                                                <th scope="col">Stock</th>
                                                <th scope="col">Brand</th>
                                                <th scope="col">Name</th>
                                                <th scope="col">Code</th>
                                                <th scope="col">Price</th>
                                                <th scope="col">Description</th>
                                                <th scope="col">Category</th>
                                                <th scope="col">Edit</th>
                                                <th scope="col">Status</th>
                                                <th scope="col">Activate/Deactivate</th>
                                                <th scope="col">Promo</th>
                                            </tr>
                                        </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tbody>
                                            <tr>
                                                <td scope="row" class='<%# GetStockColor(Eval("quantity")) %>'><%# Eval("quantity")%></td>
                                                <td scope="row"><%# Eval("brand")%></td>
                                                <td scope="row"><%# Eval("name")%></td>
                                                <td scope="row"><%# Eval("codigoArtigo")%></td>
                                                <td scope="row"><%# Eval("price")%> €</td>
                                                <td scope="row"><%# LimitDescription(Eval("description"), 50) %></td>
                                                <td scope="row"><%# Eval("category")%></td>
                                                <td scope="row">
                                                    <asp:LinkButton ID="edit_product" runat="server" OnCommand="edit_product_Command" CommandName="Edit" CommandArgument='<%# Eval("id_products") %>'>
                                                        <img src="admin_assets/img/editar.png" alt="Edit" />
                                                    </asp:LinkButton>
                                                </td>
                                                <td scope="row">
                                                    <asp:Image ID="imgEstado" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("status")) ? "admin_assets/img/sim.png" : "admin_assets/img/nao.png" %>' />
                                                </td>
                                                <td scope="row">
                                                    <asp:LinkButton ID="lb_activate_deactivate" runat="server" CssClass='<%# Convert.ToBoolean(Eval("status")) ? "btn btn-danger" : "btn btn-success" %>' CommandArgument='<%# Eval("id_products") %>' OnCommand="lb_activate_deactivate_Command" CommandName="AtivarDesativar"><%# Convert.ToBoolean(Eval("status")) ? "Deactivate" : "Active" %></asp:LinkButton>
                                                </td>
                                                <td scope="row">
                                                    <asp:LinkButton ID="lb_remove_promo" runat="server" OnCommand="lb_remove_promo_Command" CommandName="RemovePromo" CommandArgument='<%# Eval("id_products") %>' Visible='<%# HasPromo(Eval("discounted_price")) %>'>
                        Remove Promo
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </table>
                            <nav aria-label="...">
                                <ul class="pagination">
                                    <li class="page-item">
                                        <asp:LinkButton ID="lbPrevious" runat="server" OnClick="lbPrevious_Click" CssClass="page-link" Text="Previous"></asp:LinkButton>
                                    </li>
                                    <asp:DataList ID="rptPaging" runat="server" OnItemCommand="rptPaging_ItemCommand" OnItemDataBound="rptPaging_ItemDataBound" RepeatDirection="Horizontal">
                                        <ItemTemplate>
                                            <li class="page-item">
                                                <asp:LinkButton ID="lbPaging" runat="server" CommandArgument='<%# Eval("PageIndex") %>' CommandName="newPage" CssClass="page-link"><%# Eval("PageText") %></asp:LinkButton>
                                            </li>
                                        </ItemTemplate>
                                    </asp:DataList>
                                    <li class="page-item">
                                        <asp:LinkButton ID="lbNext" runat="server" OnClick="lbNext_Click" CssClass="page-link" Text="Next"></asp:LinkButton>
                                    </li>
                                </ul>
                            </nav>


                            <!-- End Primary Color Bordered Table -->
                        </div>
                    </div>

                </div>
            </div>
        </section>

    </main>
    <!-- End #main -->

</asp:Content>
