<%@ Page Title="" Language="C#" MasterPageFile="~/master_page_admin.Master" AutoEventWireup="true" CodeBehind="bo_users.aspx.cs" Inherits="TechHeaven.bo_users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main id="main" class="main">

        <div class="pagetitle">
            <h1>Clients</h1>
            <nav>
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="index_admin.aspx">Home</a></li>
                    <li class="breadcrumb-item active">Clients</li>
                </ol>
            </nav>
        </div>
        <!-- End Page Title -->

        <section class="section">
            <div class="row">
                <div class="col-lg-12">

                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Clients</h5>

                            <!--<a href="bo_adicionar_produto.aspx">Adicionar produto</a>-->

                            <!-- Primary Color Bordered Table -->
                            <table class="table table-bordered border-primary">
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <HeaderTemplate>
                                        <thead>
                                            <tr>
                                                <th scope="col">Name</th>
                                                <th scope="col">Email</th>
                                                <th scope="col">Username</th>
                                                <th scope="col">Verified</th>
                                                <th scope="col">Role</th>
                                                <th scope="col">NIF</th>
                                                <th scope="col">FTA</th>
                                                <th scope="col">Newsletter</th>
                                                <th scope="col">Purchases</th>
                                                <th scope="col">Editar</th>

                                            </tr>
                                        </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tbody>
                                            <tr>
                                                <th scope="row">
                                                    <%# Eval("firstName") != null ? Eval("firstName") : "" %>
                                                    <%# Eval("lastName") != null ? " " + Eval("lastName") : "" %>
                                                </th>
                                                <td scope="row"><%# Eval("email")%></td>
                                                <td scope="row"><%# Eval("username")%></td>
                                                <td scope="row"><%# Convert.ToBoolean(Eval("verify")) ? "Yes" : "No" %></td>
                                                <td scope="row"><%# Eval("role")%></td>
                                                <td scope="row">
                                                    <%# Eval("NIF") != DBNull.Value && Eval("NIF") != null ? Eval("NIF").ToString() : "N/A" %>
                                                </td>
                                                <td scope="row">
                                                    <%# Eval("tfa") != DBNull.Value 
        ? (Eval("tfa") != null && Convert.ToBoolean(Eval("tfa")) ? "Yes" : "No") 
        : "No" %>
                                                </td>
                                                <td scope="row">
                                                    <%# Eval("newsletter") != DBNull.Value 
        ? (Eval("newsletter") != null && Convert.ToBoolean(Eval("newsletter")) ? "Yes" : "No") 
        : "No" %>
                                                </td>

                                                <td scope="row">
                                                    <%# Eval("totalOrders") %>
                                                </td>

                                                <td scope="row">
                                                    <asp:LinkButton ID="edit_user" runat="server" OnCommand="edit_user_Command" CommandName="Edit" CommandArgument='<%# Eval("id") %>'>
                                                <img src="admin_assets/img/editar.png" alt="Editar" />
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

</asp:Content>
