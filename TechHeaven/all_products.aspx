﻿<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="all_products.aspx.cs" Inherits="TechHeaven.all_products" %>

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
                                                                    <%# Eval("price")%> €
                                                                </div>
                                                                <!-- End .product-price -->
                                                                <div class="ratings-container">
                                                                    <div class="ratings">
                                                                        <div class="ratings-val" style="width: 20%;"></div>
                                                                        <!-- End .ratings-val -->
                                                                    </div>
                                                                    <!-- End .ratings -->
                                                                    <span class="ratings-text">( 2 Reviews )</span>
                                                                </div>
                                                                <!-- End .rating-container -->


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





                                        <div class="widget widget-collapsible">
                                            <h3 class="widget-title">
                                                <a data-toggle="collapse" href="#widget-5" role="button" aria-expanded="true" aria-controls="widget-5">Price
                                                </a>
                                            </h3>
                                            <!-- End .widget-title -->

                                            <div class="collapse show" id="widget-5">
                                                <div class="widget-body">
                                                    <div class="filter-price">
                                                        <div class="filter-price-text">
                                                            Price Range:
                                                   
                                                    <span id="filter-price-range"></span>
                                                        </div>
                                                        <!-- End .filter-price-text -->

                                                        <div id="price-slider"></div>
                                                        <!-- End #price-slider -->
                                                    </div>
                                                    <!-- End .filter-price -->
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

    <!-- Mobile Menu -->
    <div class="mobile-menu-overlay"></div>
    <!-- End .mobil-menu-overlay -->

    <div class="mobile-menu-container">
        <div class="mobile-menu-wrapper">
            <span class="mobile-menu-close"><i class="icon-close"></i></span>

            <div action="#" method="get" class="mobile-search">
                <label for="mobile-search" class="sr-only">Search</label>
                <input type="search" class="form-control" name="mobile-search" id="mobile-search" placeholder="Search in..." required>
                <button class="btn btn-primary" type="submit"><i class="icon-search"></i></button>
            </div>

            <nav class="mobile-nav">
                <ul class="mobile-menu">
                    <li class="active">
                        <a href="index.html">Home</a>

                        <ul>
                            <li><a href="index-1.html">01 - furniture store</a></li>
                            <li><a href="index-2.html">02 - furniture store</a></li>
                            <li><a href="index-3.html">03 - electronic store</a></li>
                            <li><a href="index-4.html">04 - electronic store</a></li>
                            <li><a href="index-5.html">05 - fashion store</a></li>
                            <li><a href="index-6.html">06 - fashion store</a></li>
                            <li><a href="index-7.html">07 - fashion store</a></li>
                            <li><a href="index-8.html">08 - fashion store</a></li>
                            <li><a href="index-9.html">09 - fashion store</a></li>
                            <li><a href="index-10.html">10 - shoes store</a></li>
                            <li><a href="index-11.html">11 - furniture simple store</a></li>
                            <li><a href="index-12.html">12 - fashion simple store</a></li>
                            <li><a href="index-13.html">13 - market</a></li>
                            <li><a href="index-14.html">14 - market fullwidth</a></li>
                            <li><a href="index-15.html">15 - lookbook 1</a></li>
                            <li><a href="index-16.html">16 - lookbook 2</a></li>
                            <li><a href="index-17.html">17 - fashion store</a></li>
                            <li><a href="index-18.html">18 - fashion store (with sidebar)</a></li>
                            <li><a href="index-19.html">19 - games store</a></li>
                            <li><a href="index-20.html">20 - book store</a></li>
                            <li><a href="index-21.html">21 - sport store</a></li>
                            <li><a href="index-22.html">22 - tools store</a></li>
                            <li><a href="index-23.html">23 - fashion left navigation store</a></li>
                            <li><a href="index-24.html">24 - extreme sport store</a></li>
                        </ul>
                    </li>
                    <li>
                        <a href="category.html">Shop</a>
                        <ul>
                            <li><a href="category-list.html">Shop List</a></li>
                            <li><a href="category-2cols.html">Shop Grid 2 Columns</a></li>
                            <li><a href="category.html">Shop Grid 3 Columns</a></li>
                            <li><a href="category-4cols.html">Shop Grid 4 Columns</a></li>
                            <li><a href="category-boxed.html"><span>Shop Boxed No Sidebar<span class="tip tip-hot">Hot</span></span></a></li>
                            <li><a href="category-fullwidth.html">Shop Fullwidth No Sidebar</a></li>
                            <li><a href="product-category-boxed.html">Product Category Boxed</a></li>
                            <li><a href="product-category-fullwidth.html"><span>Product Category Fullwidth<span class="tip tip-new">New</span></span></a></li>
                            <li><a href="cart.html">Cart</a></li>
                            <li><a href="checkout.html">Checkout</a></li>
                            <li><a href="wishlist.html">Wishlist</a></li>
                            <li><a href="#">Lookbook</a></li>
                        </ul>
                    </li>
                    <li>
                        <a href="product.html" class="sf-with-ul">Product</a>
                        <ul>
                            <li><a href="product.html">Default</a></li>
                            <li><a href="product-centered.html">Centered</a></li>
                            <li><a href="product-extended.html"><span>Extended Info<span class="tip tip-new">New</span></span></a></li>
                            <li><a href="product-gallery.html">Gallery</a></li>
                            <li><a href="product-sticky.html">Sticky Info</a></li>
                            <li><a href="product-sidebar.html">Boxed With Sidebar</a></li>
                            <li><a href="product-fullwidth.html">Full Width</a></li>
                            <li><a href="product-masonry.html">Masonry Sticky Info</a></li>
                        </ul>
                    </li>
                    <li>
                        <a href="#">Pages</a>
                        <ul>
                            <li>
                                <a href="about.html">About</a>

                                <ul>
                                    <li><a href="about.html">About 01</a></li>
                                    <li><a href="about-2.html">About 02</a></li>
                                </ul>
                            </li>
                            <li>
                                <a href="contact.html">Contact</a>

                                <ul>
                                    <li><a href="contact.html">Contact 01</a></li>
                                    <li><a href="contact-2.html">Contact 02</a></li>
                                </ul>
                            </li>
                            <li><a href="login.html">Login</a></li>
                            <li><a href="faq.html">FAQs</a></li>
                            <li><a href="404.html">Error 404</a></li>
                            <li><a href="coming-soon.html">Coming Soon</a></li>
                        </ul>
                    </li>
                    <li>
                        <a href="blog.html">Blog</a>

                        <ul>
                            <li><a href="blog.html">Classic</a></li>
                            <li><a href="blog-listing.html">Listing</a></li>
                            <li>
                                <a href="#">Grid</a>
                                <ul>
                                    <li><a href="blog-grid-2cols.html">Grid 2 columns</a></li>
                                    <li><a href="blog-grid-3cols.html">Grid 3 columns</a></li>
                                    <li><a href="blog-grid-4cols.html">Grid 4 columns</a></li>
                                    <li><a href="blog-grid-sidebar.html">Grid sidebar</a></li>
                                </ul>
                            </li>
                            <li>
                                <a href="#">Masonry</a>
                                <ul>
                                    <li><a href="blog-masonry-2cols.html">Masonry 2 columns</a></li>
                                    <li><a href="blog-masonry-3cols.html">Masonry 3 columns</a></li>
                                    <li><a href="blog-masonry-4cols.html">Masonry 4 columns</a></li>
                                    <li><a href="blog-masonry-sidebar.html">Masonry sidebar</a></li>
                                </ul>
                            </li>
                            <li>
                                <a href="#">Mask</a>
                                <ul>
                                    <li><a href="blog-mask-grid.html">Blog mask grid</a></li>
                                    <li><a href="blog-mask-masonry.html">Blog mask masonry</a></li>
                                </ul>
                            </li>
                            <li>
                                <a href="#">Single Post</a>
                                <ul>
                                    <li><a href="single.html">Default with sidebar</a></li>
                                    <li><a href="single-fullwidth.html">Fullwidth no sidebar</a></li>
                                    <li><a href="single-fullwidth-sidebar.html">Fullwidth with sidebar</a></li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                    <li>
                        <a href="elements-list.html">Elements</a>
                        <ul>
                            <li><a href="elements-products.html">Products</a></li>
                            <li><a href="elements-typography.html">Typography</a></li>
                            <li><a href="elements-titles.html">Titles</a></li>
                            <li><a href="elements-banners.html">Banners</a></li>
                            <li><a href="elements-product-category.html">Product Category</a></li>
                            <li><a href="elements-video-banners.html">Video Banners</a></li>
                            <li><a href="elements-buttons.html">Buttons</a></li>
                            <li><a href="elements-accordions.html">Accordions</a></li>
                            <li><a href="elements-tabs.html">Tabs</a></li>
                            <li><a href="elements-testimonials.html">Testimonials</a></li>
                            <li><a href="elements-blog-posts.html">Blog Posts</a></li>
                            <li><a href="elements-portfolio.html">Portfolio</a></li>
                            <li><a href="elements-cta.html">Call to Action</a></li>
                            <li><a href="elements-icon-boxes.html">Icon Boxes</a></li>
                        </ul>
                    </li>
                </ul>
            </nav>
            <!-- End .mobile-nav -->

            <div class="social-icons">
                <a href="#" class="social-icon" target="_blank" title="Facebook"><i class="icon-facebook-f"></i></a>
                <a href="#" class="social-icon" target="_blank" title="Twitter"><i class="icon-twitter"></i></a>
                <a href="#" class="social-icon" target="_blank" title="Instagram"><i class="icon-instagram"></i></a>
                <a href="#" class="social-icon" target="_blank" title="Youtube"><i class="icon-youtube"></i></a>
            </div>
            <!-- End .social-icons -->
        </div>
        <!-- End .mobile-menu-wrapper -->
    </div>
    <!-- End .mobile-menu-container -->

    <!-- Sign in / Register Modal -->
    <div class="modal fade" id="signin-modal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true"><i class="icon-close"></i></span>
                    </button>

                    <div class="form-box">
                        <div class="form-tab">
                            <ul class="nav nav-pills nav-fill" role="tablist">
                                <li class="nav-item">
                                    <a class="nav-link active" id="signin-tab" data-toggle="tab" href="#signin" role="tab" aria-controls="signin" aria-selected="true">Sign In</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" id="register-tab" data-toggle="tab" href="#register" role="tab" aria-controls="register" aria-selected="false">Register</a>
                                </li>
                            </ul>
                            <div class="tab-content" id="tab-content-5">
                                <div class="tab-pane fade show active" id="signin" role="tabpanel" aria-labelledby="signin-tab">
                                    <div>
                                        <div class="form-group">
                                            <label for="singin-email">Username or email address *</label>
                                            <input type="text" class="form-control" id="singin-email" name="singin-email" required>
                                        </div>
                                        <!-- End .form-group -->

                                        <div class="form-group">
                                            <label for="singin-password">Password *</label>
                                            <input type="password" class="form-control" id="singin-password" name="singin-password" required>
                                        </div>
                                        <!-- End .form-group -->

                                        <div class="form-footer">
                                            <button type="submit" class="btn btn-outline-primary-2">
                                                <span>LOG IN</span>
                                                <i class="icon-long-arrow-right"></i>
                                            </button>

                                            <div class="custom-control custom-checkbox">
                                                <input type="checkbox" class="custom-control-input" id="signin-remember">
                                                <label class="custom-control-label" for="signin-remember">Remember Me</label>
                                            </div>
                                            <!-- End .custom-checkbox -->

                                            <a href="#" class="forgot-link">Forgot Your Password?</a>
                                        </div>
                                        <!-- End .form-footer -->
                                    </div>
                                    <div class="form-choice">
                                        <p class="text-center">or sign in with</p>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <a href="#" class="btn btn-login btn-g">
                                                    <i class="icon-google"></i>
                                                    Login With Google
                                                </a>
                                            </div>
                                            <!-- End .col-6 -->
                                            <div class="col-sm-6">
                                                <a href="#" class="btn btn-login btn-f">
                                                    <i class="icon-facebook-f"></i>
                                                    Login With Facebook
                                                </a>
                                            </div>
                                            <!-- End .col-6 -->
                                        </div>
                                        <!-- End .row -->
                                    </div>
                                    <!-- End .form-choice -->
                                </div>
                                <!-- .End .tab-pane -->
                                <div class="tab-pane fade" id="register" role="tabpanel" aria-labelledby="register-tab">
                                    <div>
                                        <div class="form-group">
                                            <label for="register-email">Your email address *</label>
                                            <input type="email" class="form-control" id="register-email" name="register-email" required>
                                        </div>
                                        <!-- End .form-group -->

                                        <div class="form-group">
                                            <label for="register-password">Password *</label>
                                            <input type="password" class="form-control" id="register-password" name="register-password" required>
                                        </div>
                                        <!-- End .form-group -->

                                        <div class="form-footer">
                                            <button type="submit" class="btn btn-outline-primary-2">
                                                <span>SIGN UP</span>
                                                <i class="icon-long-arrow-right"></i>
                                            </button>

                                            <div class="custom-control custom-checkbox">
                                                <input type="checkbox" class="custom-control-input" id="register-policy" required>
                                                <label class="custom-control-label" for="register-policy">I agree to the <a href="#">privacy policy</a> *</label>
                                            </div>
                                            <!-- End .custom-checkbox -->
                                        </div>
                                        <!-- End .form-footer -->
                                    </div>
                                    <div class="form-choice">
                                        <p class="text-center">or sign in with</p>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <a href="#" class="btn btn-login btn-g">
                                                    <i class="icon-google"></i>
                                                    Login With Google
                                                </a>
                                            </div>
                                            <!-- End .col-6 -->
                                            <div class="col-sm-6">
                                                <a href="#" class="btn btn-login  btn-f">
                                                    <i class="icon-facebook-f"></i>
                                                    Login With Facebook
                                                </a>
                                            </div>
                                            <!-- End .col-6 -->
                                        </div>
                                        <!-- End .row -->
                                    </div>
                                    <!-- End .form-choice -->
                                </div>
                                <!-- .End .tab-pane -->
                            </div>
                            <!-- End .tab-content -->
                        </div>
                        <!-- End .form-tab -->
                    </div>
                    <!-- End .form-box -->
                </div>
                <!-- End .modal-body -->
            </div>
            <!-- End .modal-content -->
        </div>
        <!-- End .modal-dialog -->
    </div>
    <!-- End .modal -->
</asp:Content>