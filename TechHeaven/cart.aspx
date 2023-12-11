<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="cart.aspx.cs" Inherits="TechHeaven.cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .cart-product-quantity {
            display: flex;
            align-items: center;
        }

        .quantity-button {
            padding: 5px 10px;
            cursor: pointer;
            border: 1px solid #ccc;
            background-color: #f9f9f9;
            text-align: center;
            text-decoration: none;
            display: inline-block;
        }

        .decrement-button {
            border-radius: 4px 0 0 4px;
        }

        .increment-button {
            border-radius: 0 4px 4px 0;
        }

        .form-control {
            text-align: center;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main class="main">
        <div class="page-header text-center" style="background-image: url('assets/images/page-header-bg.jpg')">
            <div class="container">
                <h1 class="page-title">Shopping Cart<span>Shop</span></h1>
            </div>
            <!-- End .container -->
        </div>
        <!-- End .page-header -->
        <nav aria-label="breadcrumb" class="breadcrumb-nav">
            <div class="container">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="main_page.aspx">Home</a></li>
                    <li class="breadcrumb-item"><a href="all_products.aspx">Shop</a></li>
                    <li class="breadcrumb-item active" aria-current="page">Shopping Cart</li>
                </ol>
            </div>
            <!-- End .container -->
        </nav>
        <!-- End .breadcrumb-nav -->

        <div class="page-content">
            <div class="cart">
                <div class="container">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <div class="row">
                                <div class="col-lg-9">
                                    <table class="table table-cart table-mobile">

                                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                            <HeaderTemplate>
                                                <thead>
                                                    <tr>
                                                        <th>Product</th>
                                                        <th>Price</th>
                                                        <th>Quantity</th>
                                                        <th>Total</th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tbody>
                                                    <tr>
                                                        <td class="product-col">
                                                            <div class="product">
                                                                <figure class="product-media">
                                                                    <a href='<%# "productpage.aspx?productId=" + Eval("id_products") %>'>
                                                                        <img src='data:<%# Eval("contenttype") %>;base64,<%# Convert.ToBase64String((byte[])Eval("image")) %>' alt='<%# Eval("name") %>' class="img-fluid" style="width: 60px; height: 68.06px;">
                                                                    </a>
                                                                </figure>

                                                                <h3 class="product-title">
                                                                    <a href='<%# "productpage.aspx?productId=" + Eval("id_products") %>'><%# Eval("name") %></a>
                                                                </h3>
                                                                <!-- End .product-title -->
                                                            </div>
                                                            <!-- End .product -->
                                                        </td>

                                                        <td class="price-col">
                                                            <div class="product-price">
                                                                <asp:Label ID="lbl_productPrice" runat="server"></asp:Label>
                                                                <asp:Label ID="lblDiscountedPrice" runat="server" Visible="false"></asp:Label>
                                                            </div>
                                                        </td>

                                                        <td class="quantity-col">
                                                            <div class="cart-product-quantity">
                                                                <asp:LinkButton ID="lb_diminuir" runat="server" class="quantity-button decrement-button" CommandName="Diminuir" CommandArgument='<%# Eval("id_products") + "," + Eval("id_cart") %>' OnCommand="lb_diminuir_Command">-</asp:LinkButton>
                                                                <asp:TextBox ID="tb_quantidade" runat="server" CssClass="form-control" Text='<%# Eval("quantity") %>' ReadOnly="true"></asp:TextBox>
                                                                <asp:LinkButton ID="lb_increase" runat="server" CommandName="Aumentar" CommandArgument='<%# Eval("id_products") + "," + Eval("id_cart") %>' OnCommand="lb_increase_Command" class="quantity-button increment-button">+</asp:LinkButton>

                                                            </div>

                                                            <!-- End .cart-product-quantity -->
                                                        </td>
                                                        <td class="total-col">
                                                            <asp:Label ID="lbl_subtotal" runat="server"></asp:Label></td>

                                                        <td class="remove-col">
                                                            <asp:LinkButton ID="lb_remover" runat="server" CssClass="btn-remove" CommandArgument='<%# Eval("id_cart") %>' OnCommand="lb_remover_Command" CommandName="Remover"><i class="icon-close"></i></asp:LinkButton>

                                                        </td>
                                                    </tr>
                                                </tbody>

                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                    <!-- End .table table-wishlist -->

                                    <asp:Label ID="lbl_vazio" runat="server" Visible="False" Enabled="False"></asp:Label></h2>

                                    <div class="cart-bottom">
                                        <h2 class="section-title">


                                            <asp:Button ID="btn_esvaziar" runat="server" class="btn btn-outline-dark-2" Text="Empty Cart" OnClick="btn_esvaziar_Click" />

                                            <asp:Button ID="btn_continuar_comprar" runat="server" class="btn btn-outline-dark-2" Text="Continue shopping" OnClick="btn_continuar_comprar_Click" />
                                        </a>



                                    </div>
                                    <!-- End .cart-bottom -->
                                </div>
                                <!-- End .col-lg-9 -->

                                <aside class="col-lg-3">
                                    <asp:Panel ID="checkout_panel" runat="server">

                                        <div class="summary summary-cart">
                                            <h3 class="summary-title">Cart Total</h3>
                                            <!-- End .summary-title -->

                                            <table class="table table-summary">
                                                <tbody>
                                                    <tr class="summary-shipping">
                                                        <td>Shipping:</td>
                                                        <td>&nbsp;</td>
                                                    </tr>

                                                    <tr class="summary-shipping-row">
                                                        <td>
                                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="shipping-options" RepeatDirection="Vertical"
                                                                OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="Free Shipping" Value="1" Selected="True" />
                                                                <asp:ListItem Text="Standard" Value="2" />
                                                                <asp:ListItem Text="Express" Value="3" />
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <!-- End .summary-shipping-row -->


                                                    <!-- End .summary-shipping-row -->
                                                    <tr class="summary-total">
                                                        <td>Shipping cost:</td>
                                                        <td>
                                                            <asp:Literal ID="ltShipping" runat="server" Text="FREE"></asp:Literal>€
                                                        </td>
                                                    </tr>

                                                    <tr class="summary-total">
                                                        <td>Total:</td>
                                                        <td>
                                                            <asp:Literal ID="ltTotal" runat="server"></asp:Literal>€</td>
                                                    </tr>
                                                    <!-- End .summary-total -->
                                                </tbody>
                                            </table>
                                            <!-- End .table table-summary -->

                                            <asp:Button ID="btn_checkout" runat="server" class="btn btn-outline-primary-2 btn-order btn-block" Text="PROCEED TO CHECKOUT" OnClick="btn_checkout_Click" /></a>

                                        </div>
                                        <!-- End .summary -->
                                    </asp:Panel>

                                </aside>

                                <!-- End .col-lg-3 -->
                            </div>
                            <!-- End .row -->
                            </div>
                <!-- End .container -->
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <!-- End .cart -->
            </div>
        </div>
        <!-- End .page-content -->
    </main>
    <!-- End .main -->
</asp:Content>
