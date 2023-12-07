<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="account.aspx.cs" Inherits="TechHeaven.account" %>

<%@ MasterType VirtualPath="~/master_page.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        body {
            background: #f8f8f8;
        }

        .emp-profile {
            padding: 3%;
            margin-top: 3%;
            margin-bottom: 3%;
            border-radius: 0.5rem;
            background: #fff;
        }

        .profile-img {
            text-align: center;
        }

            .profile-img img {
                width: 70%;
                height: 100%;
            }

            .profile-img .file {
                position: relative;
                overflow: hidden;
                margin-top: -20%;
                width: 70%;
                border: none;
                border-radius: 0;
                font-size: 15px;
                background: #212529b8;
            }

                .profile-img .file input {
                    position: absolute;
                    opacity: 0;
                    right: 0;
                    top: 0;
                }

        .profile-head h5 {
            color: #333;
        }

        .profile-head h6 {
            color: #0062cc;
        }

        .profile-edit-btn {
            border: none;
            border-radius: 1.5rem;
            width: 70%;
            padding: 2%;
            font-weight: 600;
            color: #6c757d;
            cursor: pointer;
        }

        .proile-rating {
            font-size: 12px;
            color: #818182;
            margin-top: 5%;
        }

            .proile-rating span {
                color: #495057;
                font-size: 15px;
                font-weight: 600;
            }

        .profile-head .nav-tabs {
            margin-bottom: 5%;
        }

            .profile-head .nav-tabs .nav-link {
                font-weight: 600;
                border: none;
            }

                .profile-head .nav-tabs .nav-link.active {
                    border: none;
                    border-bottom: 2px solid #0062cc;
                }

        .profile-work {
            /*padding: 14%;*/
            margin-top: -15%;
        }

            .profile-work p {
                font-size: 12px;
                color: #818182;
                font-weight: 600;
                margin-top: 10%;
            }

        .label-user-email {
            text-decoration: none;
            color: #495057;
            font-weight: 600;
            font-size: 14px;
        }

        .profile-work ul {
            list-style: none;
        }

        .profile-tab label {
            font-weight: 600;
        }

        .profile-tab p, .lbl-user-info {
            font-weight: 600;
            color: #0062cc;
        }

        .tb-user-info {
            max-width: 220px;
        }

        .top {
            margin-bottom: 0%;
        }

        .btn-save {
            appearance: none;
            backface-visibility: hidden;
            background-color: #5cb85c;
            border-style: none;
            box-shadow: none;
            box-sizing: border-box;
            border: 1px solid #f8f6f6;
            color: #fff;
            cursor: pointer;
            font-size: 15px;
            font-weight: 500;
            height: 50px;
            letter-spacing: normal;
            line-height: 1.5;
            outline: none;
            overflow: hidden;
            padding: 14px 30px;
            position: relative;
            text-align: center;
            text-decoration: none;
            transform: translate3d(0, 0, 0);
            transition: all .3s;
            user-select: none;
            -webkit-user-select: none;
            touch-action: manipulation;
            vertical-align: top;
            white-space: nowrap;
            display: flex;
            align-items: center;
            justify-content: center;
        }

            .btn-save:hover {
                color: black;
                background-color: #51a651;
                border: none;
                box-shadow: rgba(0, 0, 0, .05) 0 5px 30px, rgba(0, 0, 0, .05) 0 1px 4px;
                opacity: 1;
                transform: translateY(0);
                transition-duration: .35s;
            }


        .icon {
            font-size: 2.8rem;
        }

        .card h2 {
            font-size: 20px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container emp-profile">
                <div>
                    <div class="row top">

                        <div class="col-md-12">
                            <div class="profile-head">
                                <h5>
                                    <asp:Label runat="server" ID="lbl_nameTop">-</asp:Label>
                                    <br />
                                    <br />

                                </h5>
                                <ul class="nav nav-tabs" id="myTab" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" id="home-tab" data-toggle="tab" href="#home" role="tab" aria-controls="home" aria-selected="true">About</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" id="profile-tab" data-toggle="tab" href="#profile" role="tab" aria-controls="profile" aria-selected="false">Addresses</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" id="cards-tab" data-toggle="tab" href="#cards" role="tab" aria-controls="cards" aria-selected="false">Cards</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" id="orders-tab" data-toggle="tab" href="#orders" role="tab" aria-controls="orders" aria-selected="false">Orders</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" id="wishlist-tab" data-toggle="tab" href="#wishlist" role="tab" aria-controls="wishlist" aria-selected="false">Wishlist</a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>



                    <div class="row">
                        <div class="col-md-4">
                            <div class="profile-work">
                                <p>Username</p>
                                <asp:Label ID="lbl_username" runat="server" class="label-user-email"></asp:Label>
                                <p>Email</p>
                                <asp:Label ID="lbl_email" runat="server" class="label-user-email"></asp:Label>
                                <p>Balance</p>
                                <asp:Label ID="lbl_balance" runat="server" class="label-user-email"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-8">
                            <div class="tab-content profile-tab" id="myTabContent">
                                <div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label>First Name</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="tb_first_name" runat="server" class="tb-user-info" MaxLength="50"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label>Last Name</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="tb_last_name" runat="server" class="tb-user-info" MaxLength="50"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label>NIF</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="tb_NIF" runat="server" class="tb-user-info" MaxLength="9" TextMode="SingleLine" onkeypress="return isNumber(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label>Phone Number</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="tb_phoneNumber" runat="server" class="tb-user-info" MaxLength="100" TextMode="Phone"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label>Birthdate</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="tb_birthdate" runat="server" class="tb-user-info" TextMode="Date"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label>Newsletter</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:LinkButton ID="lb_save_news" runat="server" OnCommand="lb_save_news_Command" CommandName="news" class="btn-save"></asp:LinkButton>
                                        </div>
                                        <div class="col-md-2">
                                            <label>
                                                <asp:Label ID="lbl_2fa" runat="server" Text="2FA"></asp:Label></label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:LinkButton ID="lb_save_tfa" runat="server" OnCommand="lb_save_tfa_Command" CommandName="tfa" class="btn-save"></asp:LinkButton>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:LinkButton ID="btn_save" class="btn-save" runat="server" OnClick="btn_save_Click">Save</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:Label ID="lbl_sucesso" runat="server" Enabled="False" Visible="False"></asp:Label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="lbl_erro" runat="server" Enabled="False" Visible="False"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="profile" role="tabpanel" aria-labelledby="profile-tab">
                                    <!--ADDRESSES-->
                                    <div class="row">
                                        <asp:Panel ID="panel_add_address" runat="server" CssClass="col-md-6">
                                            <div class="col-md-12">
                                                <div class="card text-center">
                                                    <a href="add_address.aspx">
                                                        <div class="card-body">
                                                            <div class="icon">
                                                                <i class="fa-solid fa-plus"></i>
                                                            </div>
                                                            <h2>Add Address</h2>
                                                        </div>
                                                    </a>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Repeater ID="Repeater1" runat="server">
                                            <ItemTemplate>
                                                <div class="col-md-6">
                                                    <div class="card">
                                                        <div class="card-body">
                                                            <b>Name: </b>
                                                            <asp:Label ID="lbl_address_name" runat="server"><%# Eval("name") %></asp:Label>
                                                            <br />
                                                            <b>Address: </b>
                                                            <asp:Label ID="lbl_address_address" runat="server"><%# Eval("address") %></asp:Label>
                                                            <br />
                                                            <b>Floor: </b>
                                                            <asp:Label ID="lbl_address_floor" runat="server"><%# Eval("floor") %></asp:Label>
                                                            <br />
                                                            <b>Zipcode: </b>
                                                            <asp:Label ID="lbl_address_zipcode" runat="server"><%# Eval("zipcode") %></asp:Label>
                                                            <br />
                                                            <b>Location: </b>
                                                            <asp:Label ID="lbl_address_location" runat="server"><%# Eval("location") %></asp:Label>
                                                            <br />
                                                            <b>City: </b>
                                                            <asp:Label ID="lbl_address_city" runat="server"><%# Eval("city") %></asp:Label>
                                                            <br />
                                                            <b>Phone: </b>
                                                            <asp:Label ID="lbl_address_phone" runat="server"><%# Eval("phone") %></asp:Label>

                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <asp:LinkButton ID="lb_edit_address" runat="server" OnCommand="lb_edit_address_Command" CommandName="edit_address" CommandArgument='<%# Eval("id") %>'>Edit</asp:LinkButton>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:LinkButton ID="lb_delete_address" runat="server" OnCommand="lb_delete_address_Command" CommandName="delete_address" CommandArgument='<%# Eval("id") %>'>Delete</asp:LinkButton>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>

                                </div>



                                <div class="tab-pane fade" id="cards" role="tabpanel" aria-labelledby="cards-tab">
                                    <!--CARDS-->
                                    <div class="row">
                                        <asp:Panel ID="panel2" runat="server" CssClass="col-md-6">
                                            <div class="col-md-12">
                                                <div class="card text-center">
                                                    <a href="add_card.aspx">
                                                        <div class="card-body">
                                                            <div class="icon">
                                                                <i class="fa-solid fa-plus"></i>
                                                            </div>
                                                            <h2>Add Cards</h2>
                                                        </div>
                                                    </a>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Repeater ID="Repeater2" runat="server">
                                            <ItemTemplate>
                                                <div class="col-md-6">
                                                    <div class="card">
                                                        <div class="card-body">
                                                            <b>Name: </b>
                                                            <asp:Label ID="lbl_name" runat="server"><%# Eval("name") %></asp:Label>
                                                            <br />
                                                            <b>Number: </b>
                                                            <asp:Label ID="lbl_number" runat="server"><%# Eval("number") %></asp:Label>
                                                            <br />
                                                            <b>CVV: </b>
                                                            <asp:Label ID="lbl_cvv" runat="server"><%# Eval("cvv") %></asp:Label>
                                                            <br />
                                                            <b>Valid: </b>
                                                            <asp:Label ID="lbl_valid" runat="server"><%# Eval("valid") %></asp:Label>
                                                            <br />
                                                            <b>Card type: </b>
                                                            <asp:Label ID="lbl_cardType" runat="server"><%# Eval("cardTypeName") %></asp:Label>

                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <asp:LinkButton ID="lb_edit_card" runat="server" OnCommand="lb_edit_card_Command" CommandName="edit_card" CommandArgument='<%# Eval("id") %>'>Edit</asp:LinkButton>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:LinkButton ID="lb_delete_card" runat="server" OnCommand="lb_delete_card_Command" CommandName="delete_card" CommandArgument='<%# Eval("id") %>'>Delete</asp:LinkButton>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>


                                    </div>
                                </div>


                                <div class="tab-pane fade" id="orders" role="tabpanel" aria-labelledby="orders-tab">
                                    <!--ORDERS-->
                                    <div class="page-content">
                                        <div class="container">
                                            <h2 class="title mb-3">Orders</h2>
                                            <asp:Panel ID="Panel4" runat="server" Visible="False">
                                                <h2 class="title mb-3">No orders :(</h2>

                                            </asp:Panel>
                                            <!-- End .title -->
                                            <asp:Repeater ID="Repeater4" runat="server" OnItemDataBound="Repeater4_ItemDataBound">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="accordion accordion-rounded" id='<%# "accordion-" + Container.ItemIndex %>'>
                                                                <div class="card card-box card-sm bg-light">
                                                                    <div class="card-header" id='<%# "heading-" + Container.ItemIndex %>'>
                                                                        <h2 class="card-title">
                                                                            <a class="collapsed" role="button" data-toggle="collapse"
                                                                                href='<%# "#collapse-" + Container.ItemIndex %>'
                                                                                aria-expanded="false" aria-controls='<%# "collapse-" + Container.ItemIndex %>'>Order #<%# Eval("id_order") %>
                                                                            </a>
                                                                        </h2>

                                                                    </div>
                                                                    <!-- End .card-header -->
                                                                    <div id='<%# "collapse-" + Container.ItemIndex %>' class="collapse"
                                                                        aria-labelledby='<%# "heading-" + Container.ItemIndex %>'
                                                                        data-parent='<%# "#accordion-" + Container.ItemIndex %>'>
                                                                        <div class="card-body">
                                                                            <p>Order number: <%# Eval("id_order") %></p>
                                                                            <p>Date: <%# Eval("order_date", "{0:MM/dd/yyyy}") %></p>
                                                                            <p>Total: <%# Eval("total") %> €</p>
                                                                            <p>Payment Method: <%# Eval("pagamento") %></p>

                                                                            <h3>Products</h3>

                                                                            <asp:Repeater ID="Repeater5" runat="server">
                                                                                <ItemTemplate>
                                                                                    <p>
                                                                                        Product Name: <%# Eval("name") %><br />
                                                                                        Subtotal: <%# Eval("subtotal") %> €<br />
                                                                                        Brand: <%# Eval("marca_nome") %>
                                                                                    </p>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                        <!-- End .card-body -->
                                                                    </div>
                                                                    <!-- End .collapse -->
                                                                </div>
                                                                <!-- End .card -->
                                                            </div>
                                                            <!-- End .accordion -->
                                                        </div>
                                                        <!-- End .col-md-6 -->
                                                    </div>
                                                    <!-- End .row -->
                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </div>
                                        <!-- End .container -->


                                    </div>
                                    <!-- End .page-content -->

                                </div>


                                <div class="tab-pane fade" id="wishlist" role="tabpanel" aria-labelledby="wishlist-tab">
                                    <!--WISHLIST-->
                                    <div class="row">
                                        <asp:Panel ID="Panel3" runat="server" CssClass="col-md-12" Visible="False">
                                            <h2 class="title mb-3">No items on your wishlist</h2>
                                        </asp:Panel>

                                        <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater3_ItemDataBound">
                                            <ItemTemplate>
                                                <div class="col-md-12">
                                                    <div class="card">
                                                        <div class="card-body">
                                                            <div class="row d-flex align-items-center">
                                                                <div class="col-md-3">
                                                                    <a href='<%# "productpage.aspx?productId=" + Eval("id_products") %>'>
                                                                        <img src='data:<%# Eval("productImageContentType") %>;base64,<%# Convert.ToBase64String((byte[])Eval("productImage")) %>' alt='<%# Eval("productName") %>' class="img-fluid" style="height: 128px; width: 128px;">
                                                                    </a>
                                                                </div>

                                                                <div class="col-md-5">
                                                                    <b>Name: </b>
                                                                    <asp:Label ID="lbl_productName" runat="server"><%# Eval("ProductName") %></asp:Label>
                                                                    <br />
                                                                    <b>Price: </b>
                                                                    <div class="product-price">
                                                                        <asp:Label ID="lbl_productPrice" runat="server"><%# Eval("ProductPrice", "{0:C}") %></asp:Label>
                                                                        <asp:Label ID="lblDiscountedPrice" runat="server" Visible="false"></asp:Label>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-3">
                                                                    <asp:LinkButton ID="lb_add_cart" runat="server" class="btn-product btn-cart" CommandName="add_cart" CommandArgument='<%# Eval("id_products") %>' OnCommand="lb_add_cart_Command">add to cart</asp:LinkButton>
                                                                </div>
                                                                <div class="col-1">
                                                                    <asp:LinkButton ID="lb_remover_wish" runat="server" CssClass="btn-remove" CommandArgument='<%# Eval("id_wish") %>' OnCommand="lb_remover_wish_Command" CommandName="Remover"><i class="icon-close"></i></asp:LinkButton>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </div>
                                    <asp:Panel ID="Panel1" runat="server">
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
                                    </asp:Panel>
                                </div>





                            </div>
                        </div>
                    </div>
                </div>
            </div>
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
