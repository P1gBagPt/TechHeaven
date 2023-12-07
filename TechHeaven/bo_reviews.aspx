<%@ Page Title="" Language="C#" MasterPageFile="~/master_page_admin.Master" AutoEventWireup="true" CodeBehind="bo_reviews.aspx.cs" Inherits="TechHeaven.bo_reviews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main" class="main">

        <div class="pagetitle">
            <h1>Reviews</h1>
            <nav>
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="main_page_admin.aspx">Home</a></li>
                    <li class="breadcrumb-item active">Reviews</li>
                </ol>
            </nav>
        </div>
        <!-- End Page Title -->

        <section class="section">
            <div class="row">
                <div class="col-lg-12">

                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Reviews</h5>

                            <table class="table table-bordered border-primary">
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <HeaderTemplate>
                                        <thead>
                                            <tr>
                                                <th scope="col">User ID</th>
                                                <th scope="col">Product Reviewed</th>
                                                <th scope="col">Name</th>
                                                <th scope="col">Classification</th>
                                                <th scope="col" class="w-25">Title</th>
                                                <th scope="col" class="w-50">Review</th>

                                                <th scope="col">Status</th>
                                                <th scope="col">Activate/Deactivate</th>
                                            </tr>
                                        </thead>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <tbody>
                                            <tr>
                                                <th scope="row"><%# Eval("userID")%></th>
                                                <th scope="row">
                                                    <a href='<%# "productpage.aspx?productId=" + Eval("productID") %>'>

                                                        <%# Eval("productName")%>
                                                    </a>
                                                </th>
                                                <th scope="row"><%# String.Concat(Eval("firstName"), " ", Eval("lastName")) %></th>
                                                <th scope="row"><%# Eval("classification")%> stars &#9733;</th>
                                                <th scope="row"><%# Eval("title")%></th>
                                                <th scope="row"><%# Eval("review")%></th>

                                                <td scope="row">
                                                    <asp:Image ID="imgEstado" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("status")) ? "admin_assets/img/sim.png" : "admin_assets/img/nao.png" %>' />
                                                </td>
                                                <td scope="row">
                                                    <asp:LinkButton ID="lb_activate_deactivate" runat="server" CssClass='<%# Convert.ToBoolean(Eval("status")) ? "btn btn-danger" : "btn btn-success" %>' CommandArgument='<%# Eval("id_review") %>' OnCommand="lb_activate_deactivate_Command" CommandName="AtivarDesativar"><%# Convert.ToBoolean(Eval("status")) ? "Deactivate" : "Ativate" %></asp:LinkButton>
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
