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

        .log_in p, a{
            font-size: 15px;
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
