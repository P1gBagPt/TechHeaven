<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="TechHeaven.register" %>

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

        .log_in p, a {
            font-size: 15px;
        }



        .block-wrap {
            display: flex;
            justify-content: center;
            align-items: center;
            flex-wrap: wrap;
            height: 100%;
        }

            .block-wrap > div {
                width: 100%;
                text-align: center;
            }

        .btn-google, .btn-fb {
            display: inline-block;
            border-radius: 1px;
            text-decoration: none;
            box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.25);
            transition: background-color 0.218s, border-color 0.218s, box-shadow 0.218s;
            margin-top: 10px;
        }

            .btn-google .google-content, .btn-google .fb-content, .btn-fb .google-content, .btn-fb .fb-content {
                display: flex;
                align-items: center;
                width: 250px;
                height: 50px;
            }

                .btn-google .google-content .logo, .btn-google .fb-content .logo, .btn-fb .google-content .logo, .btn-fb .fb-content .logo {
                    padding: 15px;
                    height: inherit;
                }

                .btn-google .google-content svg, .btn-google .fb-content svg, .btn-fb .google-content svg, .btn-fb .fb-content svg {
                    width: 18px;
                    height: 18px;
                }

                .btn-google .google-content p, .btn-google .fb-content p, .btn-fb .google-content p, .btn-fb .fb-content p {
                    width: 100%;
                    line-height: 1;
                    letter-spacing: 0.21px;
                    text-align: center;
                    font-weight: 500;
                    font-family: "Roboto", sans-serif;
                }

        .btn-google {
            background: #FFF;
        }

            .btn-google:hover {
                box-shadow: 0 0 3px 3px rgba(66, 133, 244, 0.3);
            }

            .btn-google:active {
                background-color: #eee;
            }

            .btn-google .google-content p {
                color: #757575;
            }

        .btn-fb {
            padding-top: 1.5px;
            background: #4267b2;
            background-color: #3b5998;
        }

            .btn-fb:hover {
                box-shadow: 0 0 3px 3px rgba(59, 89, 152, 0.3);
            }

            .btn-fb .fb-content p {
                color: rgba(255, 255, 255, 0.87);
            }

        .social-message {
            display: flex;
            justify-content: center; /* Center horizontally */
            align-items: center; /* Center vertically */
            height: 100%; /* Adjust this value if necessary to control the vertical centering. */
        }

        .btn-google .google-content p {
    margin-top: 10px; /* Adjust the margin to align the text properly */
    color: #757575;
}

.btn-fb .fb-content p {
    margin-top: 10px; /* Adjust the margin to align the text properly */
    color: rgba(255, 255, 255, 0.87);
}

.btn-google .google-content .logo,
.btn-google .fb-content .logo,
.btn-fb .google-content .logo,
.btn-fb .fb-content .logo {
    padding: 5px; /* Adjust the padding to align the logo properly */
    height: inherit;
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
                            <div class="social-message">
                                <asp:Label ID="lbl_social" runat="server" Visible="False" Enabled="False"></asp:Label>
                            </div>
                            <div class="card-body p-5">
                                <h2 class="text-uppercase text-center mb-5">Create an account</h2>

                                <div class="form-group register_text">
                                    <asp:TextBox class="form-control form-control-lg" ID="register_email" runat="server" placeholder="Email" MaxLength="255" Style="font-size: 20px;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="register_email" ErrorMessage="Email Required!" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="error-message" runat="server" ControlToValidate="register_email" ErrorMessage="Invalid Email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group register_text">
                                            <asp:TextBox class="form-control form-control-lg" ID="register_firstName" runat="server" MaxLength="50" placeholder="First Name" Style="font-size: 20px;"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="error-message" runat="server" ControlToValidate="register_firstName" ErrorMessage="First Name Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group register_text">
                                            <asp:TextBox class="form-control form-control-lg" ID="register_lastName" runat="server" MaxLength="50" placeholder="Last Name" Style="font-size: 20px;"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="error-message" runat="server" ControlToValidate="register_lastName" ErrorMessage="Last Name Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group register_text">
                                    <asp:TextBox class="form-control form-control-lg" ID="register_username" runat="server" MaxLength="10" placeholder="Username" Style="font-size: 20px;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="error-message" runat="server" ControlToValidate="register_username" ErrorMessage="Username is Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group register_text">
                                    <asp:TextBox class="form-control form-control-lg" ID="register_password" runat="server" TextMode="Password" placeholder="Password" OnTextChanged="register_password_TextChanged" MaxLength="255" Style="font-size: 20px;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="error-message" runat="server" ControlToValidate="register_password" ErrorMessage="Password Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group register_text">
                                    <asp:TextBox class="form-control form-control-lg" ID="register_password_agn" runat="server" TextMode="Password" MaxLength="255" placeholder="Confirm Password" Style="font-size: 20px;"></asp:TextBox>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Label ID="lbl_mensagem" runat="server" ForeColor="Red"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div>
                                    <asp:Label ID="lbl_erro" runat="server"></asp:Label>
                                </div>

                                <div class="d-flex justify-content-center">
                                    <asp:LinkButton ID="SubmitRegister" class="btn btn-success btn-register btn-block btn-lg text-body" runat="server" OnClick="SubmitRegister_Click">Register</asp:LinkButton>
                                </div>


                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:LinkButton ID="btn_googleLogin" runat="server" OnClick="btn_googleLogin_Click" CausesValidation="False" CssClass="btn-google">
<div class="google-content">
    <div class="logo" style="margin-bottom: 8px;">
        <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 48 48">
            <defs>
                <path id="a" d="M44.5 20H24v8.5h11.8C34.7 33.9 30.1 37 24 37c-7.2 0-13-5.8-13-13s5.8-13 13-13c3.1 0 5.9 1.1 8.1 2.9l6.4-6.4C34.6 4.1 29.6 2 24 2 11.8 2 2 11.8 2 24s9.8 22 22 22c11 0 21-8 21-22 0-1.3-.2-2.7-.5-4z" />
            </defs>
            <clipPath id="b">
                <use xlink:href="#a" overflow="visible" />
            </clipPath>
            <path clip-path="url(#b)" fill="#FBBC05" d="M0 37V11l17 13z" />
            <path clip-path="url(#b)" fill="#EA4335" d="M0 11l17 13 7-6.1L48 14V0H0z" />
            <path clip-path="url(#b)" fill="#34A853" d="M0 37l30-23 7.9 1L48 0v48H0z" />
            <path clip-path="url(#b)" fill="#4285F4" d="M48 48L17 24l-4-3 35-10z" />
        </svg>
    </div>
    <p style="margin-top: 19px;">Register with Google</p>
</div>
                                            </asp:LinkButton>

                                        </div>
                                        <div class="col-md-6">
                                            <asp:LinkButton ID="btn_facebookRegisto" runat="server" OnClick="btn_facebookRegisto_Click" CausesValidation="False" CssClass="btn-fb">
<div class="fb-content">
    <div class="logo" style="margin-bottom: 8px;">
        <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" viewBox="0 0 32 32" version="1">
            <path fill="#FFFFFF" d="M32 30a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2V2a2 2 0 0 1 2-2h28a2 2 0 0 1 2 2v28z"/>
            <path fill="#4267b2" d="M22 32V20h4l1-5h-5v-2c0-2 1.002-3 3-3h2V5h-4c-3.675 0-6 2.881-6 7v3h-4v5h4v12h5z"/>
        </svg>
    </div>
    <p style="margin-top: 19px;">Register with Facebook</p>
</div>
                                            </asp:LinkButton>

                                        </div>
                                    </div>
                                </div>




                                <div class="log_in">
                                    <p class="text-center text-muted mt-2 mb-0">
                                        Have already an account? <a href="login.aspx" class="fw-bold text-body"><u>Login here</u></a>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>


</asp:Content>
