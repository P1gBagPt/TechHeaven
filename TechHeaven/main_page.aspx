<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="main_page.aspx.cs" Inherits="TechHeaven.main_page" %>

<%@ MasterType VirtualPath="~/master_page.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main class="main">
        <div class="intro-slider-container mb-5">
            <div class="intro-slider owl-carousel owl-theme owl-nav-inside owl-light" data-toggle="owl"
                data-owl-options='{
                        "dots": true,
                        "nav": false, 
                        "responsive": {
                            "1200": {
                                "nav": true,
                                "dots": false
                            }
                        }
                    }'>
                <div class="intro-slide" style="background-image: url(assets/images/demos/demo-4/slider/slide-2.png);">
                    <div class="container intro-content">
                        <div class="row justify-content-end">
                            <div class="col-auto col-sm-7 col-md-6 col-lg-5">
                                <h3 class="intro-subtitle text-third">Beats</h3>
                                <!-- End .h3 intro-subtitle -->
                                <h1 class="intro-title">Beats by</h1>
                                <h1 class="intro-title">Dre Studio 3</h1>
                                <!-- End .intro-title -->

                                <div class="intro-price">
                                    <span class="text-primary">325<sup>.00</sup> €
                                    </span>
                                </div>
                                <!-- End .intro-price -->

                                <a href="all_products.aspx?brandID=1" class="btn btn-primary btn-round">
                                    <span>Shop More</span>
                                    <i class="icon-long-arrow-right"></i>
                                </a>
                            </div>
                            <!-- End .col-lg-11 offset-lg-1 -->
                        </div>
                        <!-- End .row -->
                    </div>
                    <!-- End .intro-content -->
                </div>
                <!-- End .intro-slide -->

                <div class="intro-slide" style="background-image: url(assets/images/demos/demo-4/slider/slide-1.png);">
                    <div class="container intro-content">
                        <div class="row justify-content-end">
                            <div class="col-auto col-sm-7 col-md-6 col-lg-5">
                                <h3 class="intro-subtitle text-primary">Apple Products</h3>
                                <!-- End .h3 intro-subtitle -->
                                <h1 class="intro-title">Apple 15 Pro
                                    <br>
                                    6.1´, 128GB </h1>
                                <!-- End .intro-title -->

                                <div class="intro-price">
                                    <span class="text-primary">1249<sup>.00</sup> €
                                    </span>
                                </div>
                                <!-- End .intro-price -->

                                <a href="category.html" class="btn btn-primary btn-round">
                                    <span>Shop Now</span>
                                    <i class="icon-long-arrow-right"></i>
                                </a>
                            </div>
                            <!-- End .col-md-6 offset-md-6 -->
                        </div>
                        <!-- End .row -->
                    </div>
                    <!-- End .intro-content -->
                </div>
                <!-- End .intro-slide -->
            </div>
            <!-- End .intro-slider owl-carousel owl-simple -->

            <span class="slider-loader"></span>
            <!-- End .slider-loader -->
        </div>
        <!-- End .intro-slider-container -->

        <div class="container">
            <h2 class="title text-center mb-4">Explore Some Categories</h2>
            <!-- End .title text-center -->

            <div class="cat-blocks-container">
                <div class="row">

                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <div class="col-6 col-sm-4 col-lg-2">
                                <a href='<%# "all_products.aspx?categoryID=" + Eval("id_category") %>' class="cat-block">

                                    <h1 class="cat-block-title"><%# Eval("category_name") %></h1>
                                    <!-- End .cat-block-title -->
                                </a>
                            </div>
                            <!-- End .col-sm-4 col-lg-2 -->
                        </ItemTemplate>
                    </asp:Repeater>



                </div>
                <!-- End .row -->
            </div>
            <!-- End .cat-blocks-container -->
        </div>
        <!-- End .container -->

        <div class="mb-4"></div>
        <!-- End .mb-4 -->

        <hr />

        <div class="mb-3"></div>
        <!-- End .mb-5 -->

        <div class="container new-arrivals">
            <div class="heading heading-flex mb-3">
                <div class="heading-left">
                    <h2 class="title">New Arrivals</h2>
                    <!-- End .title -->
                </div>
                <!-- End .heading-left -->


            </div>
            <!-- End .heading -->

            <div class="tab-content tab-content-carousel just-action-icons-sm">
                <div class="tab-pane p-0 fade show active" id="new-all-tab" role="tabpanel" aria-labelledby="new-all-link">
                    <div class="owl-carousel owl-full carousel-equal-height carousel-with-shadow" data-toggle="owl"
                        data-owl-options='{
                                "nav": true, 
                                "dots": true,
                                "margin": 20,
                                "loop": false,
                                "responsive": {
                                    "0": {
                                        "items":2
                                    },
                                    "480": {
                                        "items":2
                                    },
                                    "768": {
                                        "items":3
                                    },
                                    "992": {
                                        "items":4
                                    },
                                    "1200": {
                                        "items":5
                                    }
                                }
                            }'>
                        <asp:Repeater ID="Repeater2" runat="server">
                            <ItemTemplate>
                                <div class="product product-2">
                                    <figure class="product-media">
                                        <a href='<%# "productpage.aspx?productId=" + Eval("id_products") %>'>
                                            <asp:Image ID="img_produto_carrousel" runat="server" CssClass="product-image" Style="width: 279.41px; height: 279.41px;" ImageUrl='<%# GetBase64Image(Eval("image"), Eval("contenttype")) %>' />
                                        </a>
                                    </figure>
                                    <!-- End .product-media -->

                                    <div class="product-body">
                                        <div class="product-cat">
                                            <%--CATEGORY--%>
                                            <%# Eval("category") %>
                                        </div>
                                        <!-- End .product-cat -->
                                        <%--NAME--%>
                                        <h3 class="product-title"><a href='<%# "productpage.aspx?productId=" + Eval("id_products") %>'><%# Eval("name") %></a></h3>
                                        <!-- End .product-title -->
                                        <div class="product-price">
                                            <%--PRICE--%>
                                            <asp:Label ID="lblPrice" runat="server" Text='<%# string.Format("{0:C}", Eval("price")) %>' CssClass='<%# ShowDiscountedPrice(Eval("discounted_price")) ? "old-price" : "" %>'></asp:Label>

                                            <asp:Label ID="lblDiscountedPrice" runat="server" Visible='<%# ShowDiscountedPrice(Eval("discounted_price")) %>'>
                        <%# string.Format("{0:C}", Eval("discounted_price")) %>
                                            </asp:Label>
                                        </div>
                                        <!-- End .product-price -->
                                    </div>
                                    <!-- End .product-body -->
                                </div>
                                <!-- End .product -->
                            </ItemTemplate>
                        </asp:Repeater>


                    </div>
                    <!-- End .owl-carousel -->
                </div>
                <!-- .End .tab-pane -->

                <!-- .End .tab-pane -->










                <!-- End .product -->
            </div>

            <!-- End .tab-content -->
        </div>
        <!-- End .container -->



        <div class="mb-6"></div>
        <!-- End .mb-6 -->



        <hr />




        <div class="container">
            <h2 class="title text-center mb-4">Some Brands</h2>
            <!-- End .title text-center -->

            <div class="cat-blocks-container">
                <div class="row">

                    <asp:Repeater ID="Repeater3" runat="server">
                        <ItemTemplate>
                            <div class="col-6 col-sm-4 col-lg-2">
                                <a href='<%# "all_products.aspx?brandID=" + Eval("id_brand") %>' class="cat-block">

                                    <h1 class="cat-block-title"><%# Eval("brand_name") %></h1>
                                    <!-- End .cat-block-title -->
                                </a>
                            </div>
                            <!-- End .col-sm-4 col-lg-2 -->
                        </ItemTemplate>
                    </asp:Repeater>



                </div>
                <!-- End .row -->
            </div>
            <!-- End .cat-blocks-container -->
        </div>
        <!-- End .container -->




        <div class="mb-5"></div>

        <hr />
        <!-- End .mb-5 -->

        <div class="container for-you">
            <div class="heading heading-flex mb-3">
                <div class="heading-left">
                    <h2 class="title">Some Products</h2>
                    <!-- End .title -->
                </div>
                <!-- End .heading-left -->

                <div class="heading-right">
                    <a href="all_products.aspx" class="title-link">View All Recommendadion <i class="icon-long-arrow-right"></i></a>
                </div>
                <!-- End .heading-right -->
            </div>
            <!-- End .heading -->

            <div class="products">
                <div class="row justify-content-center">

                    <asp:Repeater ID="Repeater4" runat="server">
                        <ItemTemplate>
                            <div class="col-6 col-md-4 col-lg-3">
                                <div class="product product-2">
                                    <figure class="product-media">
                                        <a href='<%# "productpage.aspx?productId=" + Eval("id_products") %>'>
                                            <asp:Image ID="img_produto" runat="server" CssClass="product-image" Style="width: 279.41px; height: 279.41px;" ImageUrl='<%# GetBase64Image(Eval("image"), Eval("contenttype")) %>' />
                                        </a>



                                        <div class="product-action">
                                            <a href="#" class="btn-product btn-cart" title="Add to cart"><span>add to cart</span></a>
                                        </div>
                                        <!-- End .product-action -->
                                    </figure>
                                    <!-- End .product-media -->

                                    <div class="product-body">
                                        <div class="product-cat">
                                            <%# Eval("category") %>
                                        </div>
                                        <!-- End .product-cat -->
                                        <h3 class="product-title"><a href='<%# "productpage.aspx?productId=" + Eval("id_products") %>'><%# Eval("name") %></a></h3>
                                        </a></h3>
                                            <!-- End .product-title -->
                                        <div class="product-price">
                                            <%--PRICE--%>
                                            <asp:Label ID="lblPrice" runat="server" Text='<%# string.Format("{0:C}", Eval("price")) %>' CssClass='<%# ShowDiscountedPrice(Eval("discounted_price")) ? "old-price" : "" %>'></asp:Label>

                                            <asp:Label ID="lblDiscountedPrice" runat="server" Visible='<%# ShowDiscountedPrice(Eval("discounted_price")) %>'>
        <%# string.Format("{0:C}", Eval("discounted_price")) %>
                                            </asp:Label>
                                        </div>
                                        <!-- End .product-price -->
                                    </div>
                                    <!-- End .product-body -->
                                </div>
                                <!-- End .product -->
                            </div>
                            <!-- End .col-sm-6 col-md-4 col-lg-3 -->
                        </ItemTemplate>
                    </asp:Repeater>




                </div>
                <!-- End .row -->
            </div>
            <!-- End .products -->
        </div>
        <!-- End .container -->

        <div class="mb-4"></div>
        <!-- End .mb-4 -->

        <div class="container">
            <hr class="mb-0">
        </div>
        <!-- End .container -->

        <div class="icon-boxes-container bg-transparent">
            <div class="container">
                <div class="row">
                    <div class="col-sm-6 col-lg-3">
                        <div class="icon-box icon-box-side">
                            <span class="icon-box-icon text-dark">
                                <i class="icon-rocket"></i>
                            </span>
                            <div class="icon-box-content">
                                <h3 class="icon-box-title">Free Shipping</h3>
                            </div>
                            <!-- End .icon-box-content -->
                        </div>
                        <!-- End .icon-box -->
                    </div>
                    <!-- End .col-sm-6 col-lg-3 -->

                    <div class="col-sm-6 col-lg-3">
                        <div class="icon-box icon-box-side">
                            <span class="icon-box-icon text-dark">
                                <i class="icon-rotate-left"></i>
                            </span>

                            <div class="icon-box-content">
                                <h3 class="icon-box-title">Free Returns</h3>
                                <!-- End .icon-box-title -->
                                <p>Within 30 days</p>
                            </div>
                            <!-- End .icon-box-content -->
                        </div>
                        <!-- End .icon-box -->
                    </div>
                    <!-- End .col-sm-6 col-lg-3 -->



                    <div class="col-sm-6 col-lg-3">
                        <div class="icon-box icon-box-side">
                            <span class="icon-box-icon text-dark">
                                <i class="icon-life-ring"></i>
                            </span>

                            <div class="icon-box-content">
                                <h3 class="icon-box-title">We Support</h3>
                                <!-- End .icon-box-title -->
                                <p>24/7 amazing services</p>
                            </div>
                            <!-- End .icon-box-content -->
                        </div>
                        <!-- End .icon-box -->
                    </div>
                    <!-- End .col-sm-6 col-lg-3 -->
                </div>
                <!-- End .row -->
            </div>
            <!-- End .container -->
        </div>
        <!-- End .icon-boxes-container -->
    </main>
    <!-- End .main -->

</asp:Content>
