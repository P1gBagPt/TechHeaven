<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="forgotpassword.aspx.cs" Inherits="TechHeaven.forgotpassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                                    <h2 class="text-uppercase mb-0" style="font-size: 30px;">Reset Password</h2>
                                    <!-- Increase font-size for a bigger heading -->
                                </div>

                                <div class="form-group login_text">
                                    <asp:TextBox class="form-control form-control-lg" ID="tb_email" placeholder="Insert the email" runat="server" MaxLength="255" Style="font-size: 20px;"></asp:TextBox>

                                    <asp:RegularExpressionValidator ID="regexEmail" runat="server" ControlToValidate="tb_email"
                                        ErrorMessage="Invalid email address" ValidationExpression="\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b"
                                        Display="Dynamic" ForeColor="Red" />
                                </div>


                                <div class="login_error_message">
                                    <asp:Label ID="lbl_erro" runat="server" Text="" CssClass="error-message" Style="font-size: 18px;"></asp:Label>
                                </div>




                                <br />
                                <div>
                                    <asp:LinkButton class="btn btn-primary btn-register btn-block btn-lg text-body" ID="btn_forgot_password" runat="server" OnClick="btn_forgot_password_Click" ValidationGroup="log" Style="font-size: 22.5px; padding: 21px 45px;"> <!-- Increase font-size and padding for a bigger button -->
                                    Reset Password
                                    </asp:LinkButton>
                                </div>


                                <br />
                                <a href="register.aspx" style="font-size: 18px;">Not Registered yet? Click Here !</a>
                                <br />
                                <a href="login.aspx" style="font-size: 18px;">Login</a>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
    </section>


</asp:Content>
