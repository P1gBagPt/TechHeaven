<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="billing.aspx.cs" Inherits="TechHeaven.billing" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-lg-9">
                <h2 class="checkout-title">Billing and Shipping Details</h2>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/add_address.aspx">Add address</asp:HyperLink>
                <asp:DropDownList ID="ddl_address" class="form-select" CssClass="form-control" runat="server" DataTextField="address" DataValueField="id_addresses" DataSourceID="SqlDataSource2"></asp:DropDownList>



                <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString="<%$ ConnectionStrings:techeavenConnectionString %>" SelectCommand="SELECT * FROM [addresses] WHERE [user_id] = @user_id AND status = 1">
                    <SelectParameters>
                        <asp:SessionParameter Name="user_id" SessionField="UserId" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>




                <!-- End .checkout-title -->
                <asp:Panel ID="Panel2" runat="server">
                </asp:Panel>
                <asp:Panel ID="Panel1" runat="server">
                    <div class="row">
                        <div class="col-sm-6">
                            <label>First Name *</label>
                            <asp:TextBox ID="tb_firstname" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="First name is required!" ControlToValidate="tb_firstname" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <!-- End .col-sm-6 -->

                        <div class="col-sm-6">
                            <label>Last Name *</label>
                            <asp:TextBox ID="tb_lastname" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="First name is required!" ControlToValidate="tb_lastname" ForeColor="Red"></asp:RequiredFieldValidator>

                        </div>
                        <!-- End .col-sm-6 -->
                    </div>
                    <!-- End .row -->


                    <label>Street address *</label>
                    <asp:TextBox ID="tb_address" runat="server" CssClass="form-control" MaxLength="60" placeholder="Street address or P.O Box"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Address is required!" ControlToValidate="tb_address" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="tb_address_floor" runat="server" CssClass="form-control" MaxLength="60" placeholder="Apt, suite, unit, building, florr, etc"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Floor is required!" ControlToValidate="tb_address_floor" ForeColor="Red"></asp:RequiredFieldValidator>



                    <div class="row">
                        <div class="col-sm-6">
                            <label>City *</label>
                            <asp:TextBox ID="tb_city" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="City is required!" ControlToValidate="tb_city" ForeColor="Red"></asp:RequiredFieldValidator>

                        </div>
                        <!-- End .col-sm-6 -->

                        <div class="col-sm-6">
                            <label>State / Province / Region *</label>
                            <asp:TextBox ID="tb_state" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="City is required!" ControlToValidate="tb_state" ForeColor="Red"></asp:RequiredFieldValidator>

                        </div>
                        <!-- End .col-sm-6 -->
                    </div>
                    <!-- End .row -->

                    <div class="row">
                        <div class="col-sm-6">
                            <label>Zip code *</label>
                            <asp:TextBox ID="tb_zipcode" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Zipcode is required!" ControlToValidate="tb_zipcode" ForeColor="Red"></asp:RequiredFieldValidator>

                        </div>
                        <!-- End .col-sm-6 -->

                        <div class="col-sm-6">
                            <label>Phone *</label>
                            <asp:TextBox ID="tb_phonenumber" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Zipcode is required!" ControlToValidate="tb_phonenumber" ForeColor="Red"></asp:RequiredFieldValidator>

                        </div>
                        <!-- End .col-sm-6 -->
                    </div>
                    <!-- End .row -->

                </asp:Panel>
            </div>
            <!-- End .col-lg-9 -->
            <aside class="col-lg-3">
                <div class="summary">
                    <h3 class="summary-title">Your Order</h3>
                    <!-- End .summary-title -->

                    <table class="table table-summary">
                        <thead>
                            <tr>
                                <th>Product</th>
                                <th>Total</th>
                            </tr>
                        </thead>

                        <tbody>
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><a href='<%# "productpage.aspx?productId=" + Eval("id_products") %>'><%# Eval("name") %></a></td>
                                        <td><%# Eval("price") %> €</td>

                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>



                            <tr>
                                <td>Shipping:</td>
                                <td>
                                    <asp:Literal ID="lbShipping" runat="server"></asp:Literal>
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

                    <div class="accordion-summary" id="accordion-payment">
                        <div class="card">
                            <div class="card-header" id="heading-5">
                                <h2 class="card-title">
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="shipping-options" RepeatDirection="Vertical"
                                        OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="Paypal" Value="1" Selected="True" />
                                        <asp:ListItem Text="Credit Card" Value="2" />
                                    </asp:RadioButtonList>
                                </h2>
                            </div>
                            <!-- End .card-header -->

                            <asp:Panel ID="Panel3" runat="server" Visible="False">

                                <div class="card">

                                    <asp:DropDownList ID="DropDownListCards" runat="server" CssClass="form-control" DataTextField="ConcatenatedField" DataValueField="id_card" DataSourceID="SqlDataSource1"></asp:DropDownList>

                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:techeavenConnectionString %>"
                                        SelectCommand="SELECT id_card, CONCAT(name, ' - ', number) AS ConcatenatedField FROM [cards] WHERE [userId] = @userId AND status = 1">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="userId" SessionField="UserId" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>

                            </asp:Panel>
                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/add_card.aspx">Add card</asp:HyperLink>

                            <!-- End .collapse -->
                        </div>

                        <!-- End .card -->
                    </div>

                    <asp:Panel ID="Panel4" runat="server">

                        <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true"/>

                    </asp:Panel>


                </div>
                <!-- End .accordion -->

                <asp:Button ID="btn_submit" runat="server" CssClass="btn btn-outline-primary-2 btn-order btn-block" Text="Place Order" OnClick="btn_submit_Click" />
                <!-- End .summary -->
            </aside>
            <!-- End .col-lg-3 -->
        </div>
        <!-- End .row -->
    </div>
</asp:Content>
