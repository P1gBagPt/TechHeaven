<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="add_address.aspx.cs" Inherits="TechHeaven.add_address" %>
<%@ MasterType VirtualPath="~/master_page.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /*#4C89E9*/
        .btn-register {
            appearance: none;
            backface-visibility: hidden;
            background-color: #4C89E9;
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

            .btn-register:hover {
                color: black;
                background-color: #366aba;
                border: none;
                box-shadow: rgba(0, 0, 0, .05) 0 5px 30px, rgba(0, 0, 0, .05) 0 1px 4px;
                opacity: 1;
                transform: translateY(0);
                transition-duration: .35s;
            }

        .error-message {
            color: red;
            font-size: 14px;
            margin-top: 5px;
        }

        .form-group.has-error input {
            border-color: red;
        }

        .register_error_message {
            margin-top: -10px;
        }

        .footer {
            margin-top: 5%;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">





    <section class="vh-100">
        <div class="mask d-flex align-items-center h-100">
            <div class="container h-100">
                <div class="row d-flex justify-content-center align-items-center h-100">
                    <div class="col-12 col-md-9 col-lg-7 col-xl-6">
                        <div class="card" style="border-radius: 15px; background-color: #f8f8f8;">
                            <div class="card-body p-5">
                                <h2 class="text-uppercase text-center mb-5">Add address <i class="fa-regular fa-house"></i></h2>

                                <div class="form-group">
                                    <h5>Full name (First and Last)</h5>
                                    <asp:TextBox class="form-control form-control-lg" ID="address_name" runat="server" MaxLength="50" Style="font-size: 15px;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="address_name" ForeColor="Red" Display="Dynamic">
                                        <span>
                                            <asp:Label ID="Label1" runat="server" Text="Please enter a name"></asp:Label>
                                            <i class="fa-solid fa-circle-exclamation" style="color: #8c1818;"></i>
                                        </span>
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <h5>Phone Number</h5>
                                    <asp:TextBox class="form-control form-control-lg" ID="address_phone" runat="server" MaxLength="20" Style="font-size: 15px;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="address_phone" ForeColor="Red" Display="Dynamic">
                                        <span>
                                            <asp:Label ID="Label2" runat="server" Text="Please enter a phone number"></asp:Label>
                                            <i class="fa-solid fa-circle-exclamation" style="color: #8c1818;"></i>
                                        </span>
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <h5>Address</h5>
                                    <asp:TextBox class="form-control form-control-lg" ID="address_address" placeholder="Street address or P.O Box" runat="server" MaxLength="60" Style="font-size: 15px; margin-bottom: 3px;"></asp:TextBox>
                                    <asp:TextBox class="form-control form-control-lg" ID="address_floor" placeholder="Apt, suite, unit, building, florr, etc" runat="server" MaxLength="60" Style="font-size: 15px;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="address_address" ForeColor="Red" Display="Dynamic">
                                        <span>
                                            <asp:Label ID="Label3" runat="server" Text="Please enter an address"></asp:Label>
                                            <i class="fa-solid fa-circle-exclamation" style="color: #8c1818;"></i>
                                        </span>
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <h5>City</h5>
                                            <asp:TextBox class="form-control form-control-lg" ID="address_city" runat="server" MaxLength="50" Style="font-size: 15px;"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="error-message" runat="server" ControlToValidate="address_city" ForeColor="Red" Display="Dynamic">
                                                <span>
                                                    <asp:Label ID="Label4" runat="server" Text="Please enter city name"></asp:Label>
                                                    <i class="fa-solid fa-circle-exclamation" style="color: #8c1818;"></i>
                                                </span>
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <h5>State / Province / Region</h5>
                                            <asp:TextBox class="form-control form-control-lg" ID="address_location" runat="server" MaxLength="50" Style="font-size: 15px;"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <h5>Zip Code</h5>
                                    <asp:TextBox class="form-control form-control-lg" ID="address_zipcode" runat="server" MaxLength="20" Style="font-size: 15px;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="address_zipcode" ErrorMessage="Zip Code is Required!" ForeColor="Red">
                                        <span>
                                            <asp:Label ID="Label5" runat="server" Text="Please enter a zipcode"></asp:Label>
                                            <i class="fa-solid fa-circle-exclamation" style="color: #8c1818;"></i>
                                        </span>
                                    </asp:RequiredFieldValidator>
                                </div>


                                <div>
                                    <asp:Label ID="lbl_erro" runat="server" Visible="False"></asp:Label>
                                </div>

                                <div class="d-flex justify-content-center">
                                    <asp:LinkButton ID="submit_address" class="btn btn-success btn-register btn-block btn-lg text-body" runat="server" OnClick="submit_address_Click">Add Address <i class="fa-solid fa-plus"></i></asp:LinkButton>
                                </div>

                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>





</asp:Content>
