<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="inputnewpass.aspx.cs" Inherits="TechHeaven.inputnewpass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                                <h2 class="text-uppercase text-center mb-5">Reset password</h2>              

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
                                    <asp:LinkButton ID="SubmitNewPass" class="btn btn-success btn-register btn-block btn-lg text-body" runat="server" OnCliCK="SubmitNewPass_Click" style="background-color: #4C89E9; border: 1px #4C89E9;">New password</asp:LinkButton>
                                </div>





                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>


</asp:Content>
