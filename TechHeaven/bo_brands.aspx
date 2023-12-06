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
                                                <th scope="row"><%# Eval("brand_name")%></th>
                                                <th scope="row"><%# Eval("total_products")%></th>
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
                        </div>
                    </div>

                </div>
            </div>
        </section>

    </main>

</asp:Content>
