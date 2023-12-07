<%@ Page Title="" Language="C#" MasterPageFile="~/master_page_admin.Master" AutoEventWireup="true" CodeBehind="bo_newsletter.aspx.cs" Inherits="TechHeaven.bo_newsletter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <main id="main" class="main">

        <div class="pagetitle">
            <h1>Newsletters</h1>
            <nav>
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="main_page_admin.aspx">Home</a></li>
                    <li class="breadcrumb-item active">Newsletter</li>
                </ol>
            </nav>
        </div>
        <!-- End Page Title -->

        <section class="section">
            <div class="row">
                <div class="col-lg-12">

                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Newsletters</h5>
                            <div class="row">
                                <div class="col-6">
                                    <a href="bo_add_newsletter.aspx">Add newsletter</a>
                                </div>

                            </div>
                            <!--<a href="bo_adicionar_produto.aspx">Adicionar produto</a>-->

                            <!-- Primary Color Bordered Table -->
                            <table class="table table-bordered border-primary">
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <HeaderTemplate>
                                        <thead>
                                            <tr>
                                                <th scope="col">Send</th>
                                                <th scope="col">Created By</th>
                                                <th scope="col">Creation Date</th>
                                                <th scope="col">Title</th>
                                                <th scope="col">Newsletter</th>
                                                <th scope="col">Status</th>
                                                <th scope="col">Delete</th>

                                            </tr>
                                        </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tbody>
                                            <tr>
                                                <td scope="row">
                                                    <asp:LinkButton ID="lb_send_email" runat="server" OnCommand="lb_send_email_Command" CommandName="Send" CommandArgument='<%# Eval("id_newsletter") %>'>Send</asp:LinkButton>
                                                </td>
                                                <td scope="row"><%# Eval("fistNameUser") + " " + Eval("lastNameUser") %></td>
                                                <td scope="row"><%# Eval("creation_date", "{0:dd/MM/yyyy}") %></td>
                                                <td scope="row"><%# Eval("title")%></td>
                                                
                                                <td scope="row"><%# Eval("news")%></td>
                                                <td scope="row">
                                                    <asp:Image ID="imgEstado" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("status")) ? "admin_assets/img/sim.png" : "admin_assets/img/nao.png" %>' />
                                                </td>
                                                <td scope="row">
                                                    <asp:LinkButton ID="lb_activate_deactivate" runat="server" CssClass='<%# Convert.ToBoolean(Eval("status")) ? "btn btn-danger" : "btn btn-success" %>' CommandArgument='<%# Eval("id_newsletter") %>' OnCommand="lb_activate_deactivate_Command" CommandName="AtivarDesativar"><%# Convert.ToBoolean(Eval("status")) ? "Deactivate" : "Activate" %></asp:LinkButton>
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
