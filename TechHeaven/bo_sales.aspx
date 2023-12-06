<%@ Page Title="" Language="C#" MasterPageFile="~/master_page_admin.Master" AutoEventWireup="true" CodeBehind="bo_sales.aspx.cs" Inherits="TechHeaven.bo_sales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main" class="main">

        <div class="pagetitle">
            <h1>Encomendas</h1>
            <nav>
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="index_admin.aspx">Inicio</a></li>
                    <li class="breadcrumb-item active">Encomendas</li>
                </ol>
            </nav>
        </div>


        <section class="section">
            <div class="row">
                <div class="col-lg-12">

                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Encomendas</h5>



                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                <ItemTemplate>
                                    <div class="accordion" id="accordionExample">
                                        <div class="accordion-item">
                                            <h2 class="accordion-header" id='<%# "heading" + Container.ItemIndex %>'>
                                                <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target='<%# "#collapse" + Container.ItemIndex %>' aria-expanded="false" aria-controls='<%# "collapse" + Container.ItemIndex %>'>
                                                    Order #<%# Eval("id_order") %>
                                                </button>

                                            </h2>
                                            <div id='<%# "collapse" + Container.ItemIndex %>' class="accordion-collapse collapse" aria-labelledby='<%# "heading" + Container.ItemIndex %>' data-bs-parent="#accordionExample">
                                                <div class="accordion-body">
                                                    <h3>Order Info</h3>
                                                    <p>ID: <%# Eval("id_order") %></p>
                                                    <p>Order Date: <%# Eval("order_date", "{0:MM/dd/yyyy}") %></p>
                                                    <p>Total: <%# Eval("total") %> €</p>
                                                    <p>Status: <%# Eval("status") %></p>
                                                    <p>Payment Method: <%# Eval("payment_methodID") %></p>

                                                    <h3>Products</h3>

                                                    <asp:Repeater ID="Repeater2" runat="server">
                                                        <ItemTemplate>
                                                            <p>
                                                                Product Name: <%# Eval("name") %><br />
                                                                Subtotal: <%# Eval("subtotal") %> €<br />
                                                                Brand: <%# Eval("marca_nome") %>
                                                            </p>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <!-- End Default Accordion Example -->

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
