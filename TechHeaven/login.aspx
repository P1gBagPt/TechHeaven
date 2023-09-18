<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="TechHeaven.login" %>

<%@ MasterType VirtualPath="~/master_page.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        section {
            margin-top: -120px;
        }


        .btn-login {
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

            .btn-login:hover {
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

        .login_error_message {
            margin-top: -10px;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <section class="vh-100">
        <div class="mask d-flex align-items-center h-100">
            <div class="container h-100" style="max-width: 1100px;">
                <!-- Increase max-width for a bigger card -->
                <div class="row d-flex justify-content-center align-items-center h-100">
                    <div class="col-12 col-md-9 col-lg-7 col-xl-6">
                        <div class="card" style="border-radius: 20px; background-color: #f8f8f8; padding: 20px;">
                            <!-- Increase border-radius and padding for a bigger card -->
                            <div class="card-body p-5">

                                <div class="d-flex justify-content-center align-items-center mb-5">
                                    <img src="assets/images/icons/favicon-32x32-removebg.png" alt="Molla Logo" class="mr-3" style="width: 50px; height: 50px;">
                                    <!-- Increase width and height for a bigger logo -->
                                    <h2 class="text-uppercase mb-0" style="font-size: 30px;">Login</h2>
                                    <!-- Increase font-size for a bigger heading -->
                                </div>

                                <div class="form-group login_text">
                                    <asp:TextBox class="form-control form-control-lg" ID="login_email" placeholder="Insert the email" runat="server" MaxLength="255" Style="font-size: 20px;"></asp:TextBox>
                                    <!-- Increase font-size for a bigger textbox -->
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="error-message" runat="server" ErrorMessage="Please enter your email" ValidationGroup="log" ControlToValidate="login_email"> </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group login_text">
                                    <asp:TextBox class="form-control form-control-lg" ID="login_password" placeholder="Insert the password" runat="server" TextMode="Password" MaxLength="255" Style="font-size: 20px;"></asp:TextBox>
                                    <!-- Increase font-size for a bigger textbox -->
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="error-message" runat="server" ErrorMessage="Please enter your password" ValidationGroup="log" ControlToValidate="login_password"></asp:RequiredFieldValidator>
                                </div>

                                <div class="login_error_message">
                                    <asp:Label ID="lbl_erro" runat="server" Text="" CssClass="error-message" Style="font-size: 18px;"></asp:Label>
                                    <br />
                                    <asp:LinkButton ID="lbl_erro_enviar" runat="server" Text="" CssClass="error-message" Style="font-size: 18px;" OnClick="lbl_erro_enviar_Click"></asp:LinkButton>
                                </div>


                                <br />
                                <div>
                                    <asp:LinkButton class="btn-login btn-register btn-block btn-lg text-body" ID="btn_login" runat="server" OnClick="btn_login_Click" ValidationGroup="log" Style="font-size: 22.5px; padding: 21px 45px;"> <!-- Increase font-size and padding for a bigger button -->
                                    <i class="fa fa-sign-in"></i> LOGIN
                                    </asp:LinkButton>
                                </div>

                                <br />
                                <a href="register.aspx" style="font-size: 18px;">Not Registered yet? Click Here !</a>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>



</asp:Content>
