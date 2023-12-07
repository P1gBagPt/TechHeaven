<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="all_products.aspx.cs" Inherits="TechHeaven.all_products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-wrapper">
        <main class="main">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>


                    <div class="page-header text-center" style="background-image: url('assets/images/page-header-bg.jpg')">
                        <div class="container">
                            <h1 class="page-title">Techeaven<span>Shop</span></h1>
                        </div>
                        <!-- End .container -->
                    </div>
                    <!-- End .page-header -->
                    <nav aria-label="breadcrumb" class="breadcrumb-nav mb-2">
                        <div class="container">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="main_page.aspx">Home</a></li>
                                <li class="breadcrumb-item"><a href="all_products.aspx">Shop</a></li>
                                <li class="breadcrumb-item active" aria-current="page">List</li>
                            </ol>
                        </div>
                        <!-- End .container -->
                    </nav>
                    <!-- End .breadcrumb-nav -->

                    <div class="page-content">
                        <div class="container">
                            <asp:Label ID="lbl_erro" runat="server" Visible="False" Enabled="False"></asp:Label>

                            <div class="row">
                                <div class="col-lg-9">
                                    <div class="toolbox">

                                        <%--filtros--%>
                                        <div class="toolbox-right">
                                            <div class="toolbox-sort">
                                                <label for="sortby">Sort by:</label>
                                                <div class="select-custom">
                                                    <asp:DropDownList ID="sortby" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="sortby_SelectedIndexChanged">
                                                        <asp:ListItem Text="Ascending Name" Value="name_asc" />
                                                        <asp:ListItem Text="Descending Name" Value="name_desc" />
                                                        <asp:ListItem Text="Ascending Price" Value="price_asc" />
                                                        <asp:ListItem Text="Descending Price" Value="price_desc" />

                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                            <!-- End .toolbox-sort -->
                                        </div>
                                        <!-- End .toolbox-right -->
                                    </div>
                                    <!-- End .toolbox -->

                                    <div class="products mb-3">

                                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                            <ItemTemplate>

                                                <div class="product product-list">
                                                    <div class="row">
                                                        <div class="col-6 col-lg-3">
                                                            <figure class="product-media">
                                                                <asp:Panel ID="Panel1" runat="server">
                                                                    <span class="product-label label-new">New</span>
                                                                </asp:Panel>
                                                                <a href='<%# "productpage.aspx?productId=" + Eval("id_products") %>'>
                                                                    <asp:Image ID="img_produto" runat="server" class="product-image" Style="width: 202.75px; height: 202.75px" />
                                                                </a>
                                                            </figure>
                                                            <!-- End .product-media -->
                                                        </div>
                                                        <!-- End .col-sm-6 col-lg-3 -->

                                                        <div class="col-6 col-lg-3 order-lg-last">
                                                            <div class="product-list-action">
                                                                <div class="product-price">
                                                                    <asp:Label ID="lblPrice" runat="server"></asp:Label>
                                                                    <asp:Label ID="lblDiscountedPrice" runat="server" Visible="false"></asp:Label>
                                                                </div>


                                                                <!-- End .product-action -->

                                                                <asp:LinkButton ID="lb_add_cart" runat="server" class="btn-product btn-cart" CommandName="add_cart" CommandArgument='<%# Eval("id_products") %>' OnCommand="lb_add_cart_Command">add to cart</asp:LinkButton>
                                                            </div>
                                                            <!-- End .product-list-action -->
                                                        </div>
                                                        <!-- End .col-sm-6 col-lg-3 -->

                                                        <div class="col-lg-6">
                                                            <div class="product-body product-action-inner">
                                                                <asp:LinkButton ID="lb_wishlist" runat="server" OnCommand="lb_wishlist_Command" CommandName="Wishlist" CommandArgument='<%# Eval("id_products") %>' CssClass="btn-product btn-wishlist" title="Add to wishlist"></asp:LinkButton>
                                                                <div class="product-cat">
                                                                    <a href="#"><%# Eval("category")%></a>
                                                                </div>
                                                                <!-- End .product-cat -->
                                                                <h3 class="product-title"><a href='<%# "productpage.aspx?productId=" + Eval("id_products") %>'><%# Eval("name")%></a></h3>
                                                                <!-- End .product-title -->

                                                                <div class="product-content">
                                                                    <p><%# Eval("description")%> </p>
                                                                </div>
                                                                <!-- End .product-content -->


                                                                <!-- End .product-nav -->
                                                            </div>
                                                            <!-- End .product-body -->
                                                        </div>
                                                        <!-- End .col-lg-6 -->
                                                    </div>
                                                    <!-- End .row -->
                                                </div>
                                                <!-- End .product -->

                                            </ItemTemplate>
                                        </asp:Repeater>


                                    </div>
                                    <!-- End .products -->

                                    <nav aria-label="Page navigation">
                                        <ul class="pagination">
                                            <li class="page-item">
                                                <asp:LinkButton ID="lbPrevious" runat="server" class="page-link page-link-prev" OnClick="lbPrevious_Click" aria-label="Previous" TabIndex="-1" aria-disabled="true">
                                                    <span aria-hidden="true"><i class="icon-long-arrow-left"></i></span>Prev
                                                </asp:LinkButton>
                                            </li>


                                            <asp:DataList ID="rptPaging" runat="server" OnItemCommand="rptPaging_ItemCommand" OnItemDataBound="rptPaging_ItemDataBound" RepeatDirection="Horizontal">
                                                <ItemTemplate>
                                                    <li class="page-item">
                                                        <asp:LinkButton ID="lbPaging" runat="server" CommandArgument='<%# Eval("PageIndex") %>' CommandName="newPage" CssClass="page-link"><%# Eval("PageText") %></asp:LinkButton>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:DataList>


                                            <li class="page-item">
                                                <asp:LinkButton ID="lbNext" runat="server" OnClick="lbNext_Click" class="page-link page-link-next" aria-label="Next">
    Next <span aria-hidden="true"><i class="icon-long-arrow-right"></i></span>
                                                </asp:LinkButton>
                                            </li>
                                        </ul>
                                    </nav>
                                </div>
                                <!-- End .col-lg-9 -->
                                <aside class="col-lg-3 order-lg-first">
                                    <div class="sidebar sidebar-shop">
                                        <div class="widget widget-clean">
                                            <label>Filters:</label>
                                            <asp:LinkButton ID="lb_clean_filters" runat="server" class="sidebar-filter-clear" OnCommand="lb_clean_filters_Command" CommandName="Clear">Clean All</asp:LinkButton>
                                        </div>
                                        <!-- End .widget widget-clean -->

                                        <div class="widget widget-collapsible">
                                            <h3 class="widget-title">
                                                <a data-toggle="collapse" href="#widget-1" role="button" aria-expanded="true" aria-controls="widget-1">Category
                                                </a>
                                            </h3>
                                            <!-- End .widget-title -->

                                            <div class="collapse show" id="widget-1">
                                                <div class="widget-body">
                                                    <div class="filter-items filter-items-count">
                                                        <asp:Repeater ID="Repeater2" runat="server">
                                                            <ItemTemplate>
                                                                <div class="filter-item">
                                                                    <asp:LinkButton ID="lb_category" runat="server" Text='<%# Eval("category_name") %>' OnCommand="lb_category_Command" CommandName="Category" CommandArgument='<%# Eval("id_category") %>'></asp:LinkButton>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <!-- End .filter-item -->


                                                    </div>
                                                    <!-- End .filter-items -->
                                                </div>
                                                <!-- End .widget-body -->
                                            </div>
                                            <!-- End .collapse -->
                                        </div>
                                        <!-- End .widget -->

                                        <div class="widget widget-collapsible">
                                            <h3 class="widget-title">
                                                <a data-toggle="collapse" href="#widget-2" role="button" aria-expanded="true" aria-controls="widget-2">Some Brands
                                                </a>
                                            </h3>
                                            <!-- End .widget-title -->

                                            <div class="collapse show" id="widget-2">
                                                <div class="widget-body">
                                                    <div class="filter-items">
                                                        <asp:Repeater ID="Repeater3" runat="server">
                                                            <ItemTemplate>
                                                                <div class="filter-item">
                                                                    <asp:LinkButton ID="lb_brand" runat="server" Text='<%# Eval("brand_name") %>' OnCommand="lb_brand_Command" CommandName="Brand" CommandArgument='<%# Eval("id_brand") %>'></asp:LinkButton>

                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>

                                                        <!-- End .filter-item -->

                                                    </div>
                                                    <!-- End .filter-items -->
                                                </div>
                                                <!-- End .widget-body -->
                                            </div>
                                            <!-- End .collapse -->
                                        </div>
                                        <!-- End .widget -->





                                    </div>
                                    <!-- End .sidebar sidebar-shop -->
                                </aside>
                                <!-- End .col-lg-3 -->
                            </div>
                            <!-- End .row -->
                        </div>
                        <!-- End .container -->
                    </div>
                    <!-- End .page-content -->
                </ContentTemplate>
            </asp:UpdatePanel>
        </main>
        <!-- End .main -->


    </div>
    <!-- End .page-wrapper -->
    <button id="scroll-top" title="Back to Top"><i class="icon-arrow-up"></i></button>

</asp:Content>
