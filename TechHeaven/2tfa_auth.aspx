<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="2tfa_auth.aspx.cs" Inherits="TechHeaven._2tfa_auth" %>
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
                             <div class="social-message">
     <asp:Label ID="lbl_social" runat="server" Visible="False" Enabled="False"></asp:Label>
 </div>
                            <!-- Increase border-radius and padding for a bigger card -->
                            <div class="card-body p-5">

                                <div class="d-flex justify-content-center align-items-center mb-5">
                                    <img src="assets/images/icons/favicon-32x32-removebg.png" alt="Molla Logo" class="mr-3" style="width: 50px; height: 50px;">
                                    <!-- Increase width and height for a bigger logo -->
                                    <h2 class="text-uppercase mb-0" style="font-size: 30px;">2TFA</h2>
                                    <!-- Increase font-size for a bigger heading -->
                                </div>

                                <div class="form-group login_text">
                                    <asp:TextBox class="form-control form-control-lg" ID="tb_code" placeholder="Insert the Code" runat="server" MaxLength="6" Style="font-size: 20px;" onkeypress="return isNumber(event)"></asp:TextBox>
                                    <!-- Increase font-size for a bigger textbox -->
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="error-message" runat="server" ErrorMessage="Please the code!" ValidationGroup="log" ControlToValidate="tb_code"> </asp:RequiredFieldValidator>
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

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
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
