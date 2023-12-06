<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="productpage.aspx.cs" Inherits="TechHeaven.productpage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <main class="main">
                <nav aria-label="breadcrumb" class="breadcrumb-nav border-0 mb-0">
                    <div class="container d-flex align-items-center">
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="main_page.aspx">Home</a></li>
                            <li class="breadcrumb-item"><a href="all_products.aspx">Products</a></li>
                        </ol>


                    </div>
                    <!-- End .container -->

                </nav>
                <!-- End .breadcrumb-nav -->

                <div class="page-content">
                    <div class="container">
                        <div class="product-details-top">
                            <asp:Label ID="lbl_erro" runat="server"></asp:Label>

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="product-gallery product-gallery-vertical">
                                        <div class="row">
                                            <figure class="product-main-image">
                                                <asp:Image ID="main_product_image" Width="100%" Height="100%" runat="server" />
                                            </figure>
                                            <!-- End .product-main-image -->

                                        </div>
                                        <!-- End .row -->
                                    </div>
                                    <!-- End .product-gallery -->
                                </div>
                                <!-- End .col-md-6 -->

                                <div class="col-md-6">
                                    <div class="product-details">
                                        <h1 class="product-title">
                                            <asp:Label ID="lbl_nome" runat="server"></asp:Label></h1>
                                        <!-- End .product-title -->

                                        <div class="ratings-container">
                                                <p>
                                                    <asp:Label ID="lbl_classificacao_media" runat="server" Text="0 stars &#9733;"></asp:Label>

                                                </p>
                                                <!-- End .ratings-val -->
                                            <a class="ratings-text" href="#product-review-link" id="review-link">
                                                <asp:Label ID="lbl_reviewsProd" runat="server"></asp:Label></a>
                                        </div>
                                        <!-- End .rating-container -->

                                        <div class="product-price">
                                            <asp:Label ID="lbl_preco" runat="server"></asp:Label>
                                        </div>
                                        <!-- End .product-price -->





                                        <div class="details-filter-row details-row-size">
                                            <label for="qty">Qty:</label>
                                            <div class="product-details-quantity">
                                                <asp:TextBox ID="tb_quantidade" runat="server" TextMode="Number" min="1"></asp:TextBox>
                                            </div>
                                            <!-- End .product-details-quantity -->
                                        </div>
                                        <!-- End .details-filter-row -->

                                        <div class="product-details-action">
                                            <asp:Button ID="btn_adicionar_carrinho" runat="server" Text="Add to cart" CssClass="btn-product btn-cart" OnClick="btn_adicionar_carrinho_Click" />


                                            <div class="details-action-wrapper">
                                                <asp:LinkButton ID="lb_add_wishlist" runat="server" CssClass="btn-product btn-wishlist" OnCommand="lb_add_wishlist_Command" CommandName="Wishlist"><span>Add to Wishlist</span></asp:LinkButton>

                                            </div>
                                            <!-- End .details-action-wrapper -->
                                        </div>
                                        <!-- End .product-details-action -->

                                        <div class="product-details-footer">
                                            <div class="product-cat">
                                                <span>Category:</span>
                                                <a>
                                                    <asp:LinkButton ID="lb_categoria" runat="server" OnCommand="lb_categoria_Command" CommandName="CategoriaMontra">LinkButton</asp:LinkButton></a>
                                            </div>
                                            <!-- End .product-cat -->

                                            <!-- End .product-cat -->

                                        </div>
                                        <div class="product-details-footer">

                                            <div class="product-cat">
                                                <span>Brand:</span>

                                                <b>
                                                    <asp:Label ID="lb_marca" runat="server"></asp:Label></b>
                                            </div>
                                        </div>

                                        <div class="product-details-footer">

                                            <div class="product-cat">
                                                <span>Product Code:</span>

                                                <b>
                                                    <asp:Label ID="lb_productCode" runat="server"></asp:Label></b>
                                            </div>
                                        </div>

                                        <!-- End .product-details-footer -->
                                    </div>
                                    <!-- End .product-details -->
                                </div>
                                <!-- End .col-md-6 -->
                            </div>
                            <!-- End .row -->
                        </div>
                        <!-- End .product-details-top -->

                        <div class="product-details-tab">
                            <ul class="nav nav-pills justify-content-center" role="tablist">
                                <li class="nav-item">
                                    <a class="nav-link active" id="product-desc-link" data-toggle="tab" href="#product-desc-tab" role="tab" aria-controls="product-desc-tab" aria-selected="true">Description</a>
                                </li>

                                <li class="nav-item">
                                    <a class="nav-link" id="product-review-link" data-toggle="tab" href="#product-review-tab" role="tab" aria-controls="product-review-tab" aria-selected="false">
                                        <asp:Label ID="lbl_reviews_nav_total" runat="server"></asp:Label>
                                    </a>

                                </li>
                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane fade show active" id="product-desc-tab" role="tabpanel" aria-labelledby="product-desc-link">
                                    <div class="product-desc-content">
                                        <h3>Product Description</h3>
                                        <p>
                                            <asp:Label ID="lbl_descricao" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                    <!-- End .product-desc-content -->
                                </div>
                                <!-- .End .tab-pane -->

                                <div class="tab-pane fade" id="product-review-tab" role="tabpanel" aria-labelledby="product-review-link">

                                    <div class="reviews">
                                        <h3>
                                            <asp:Label ID="lbl_total_reviews" runat="server"></asp:Label></h3>

                                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                            <ItemTemplate>
                                                <div class="review">
                                                    <div class="row no-gutters">
                                                        <div class="col-auto">
                                                            <h4><%# Eval("UserName") %></h4>
                                                            <div class="ratings-container">

                                                                    <p><%# Eval("Rating") %> stars &#9733;</p>

                                                                    <!-- End .ratings-val -->
                                                         
                                                                <!-- End .ratings -->
                                                            </div>
                                                            <!-- End .rating-container -->
                                                            <span class="review-date"><%# Eval("DaysAgo") %> days ago</span>
                                                        </div>
                                                        <!-- End .col -->
                                                        <div class="col">
                                                            <h4><%# Eval("Title") %></h4>

                                                            <div class="review-content">
                                                                <p><%# Eval("Review") %></p>
                                                            </div>
                                                            <!-- End .review-content -->
                                                        </div>
                                                        <!-- End .col-auto -->
                                                    </div>
                                                    <!-- End .row -->
                                                </div>
                                                <!-- End .review -->
                                            </ItemTemplate>
                                        </asp:Repeater>

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
                                    <!-- End .reviews -->
                                </div>
                                <!-- .End .tab-pane -->

                            </div>
                            <div class="product-details">

                                <asp:Panel ID="Panel2" runat="server">
                                    <asp:Label ID="lbl_erro_review" runat="server"></asp:Label>
                                    <div class="row">
                                        <div class="col-3">
                                            Title
                                            <asp:TextBox ID="tb_title_review" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-3">
                                            Classification
                                                <asp:TextBox ID="tb_classificarion" runat="server" TextMode="Number" class="form-control" min="1" max="5" value="1" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-6">
                                            Review
                                        <asp:TextBox ID="tb_review" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>


                                    <asp:LinkButton ID="lb_add" runat="server" OnCommand="lb_add_Command" CommandName="addReview">Add Review</asp:LinkButton>


                                </asp:Panel>
                            </div>
                            <!-- End .tab-content -->
                        </div>
                        <!-- End .product-details-tab -->
                    </div>
                    <!-- End .container -->
                </div>
                <!-- End .page-content -->

            </main>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        function isNumber(event) {
            var charCode = (event.which) ? event.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
