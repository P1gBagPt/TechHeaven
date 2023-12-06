<%@ Page Title="" Language="C#" MasterPageFile="~/master_page_admin.Master" AutoEventWireup="true" CodeBehind="bo_edit_users.aspx.cs" Inherits="TechHeaven.bo_edit_users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main id="main" class="main">
        <section class="section">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body text-center">
                            <h1 class="card-title">Edit User</h1>

                            <div class="row">
                                <div class="col-lg-4" style="text-align: left;">
                                    <p>First Name</p>
                                    <asp:TextBox ID="tb_firstName" class="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red;" runat="server" ErrorMessage="Name required!" ControlToValidate="tb_firstName"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-4" style="text-align: left;">
                                    <p>Last Name</p>
                                    <asp:TextBox ID="tb_lastName" class="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" runat="server" ErrorMessage="Name required!" ControlToValidate="tb_lastName"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-4" style="text-align: left;">
                                    <p>Username</p>
                                    <asp:TextBox ID="tb_username" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Style="color: red;" runat="server" ErrorMessage="Username required!" ControlToValidate="tb_username"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-4" style="text-align: left;">
                                    <p style="text-align: left;">Email</p>
                                    <asp:TextBox ID="tb_email" class="form-control" runat="server" MaxLength="255" TextMode="Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Style="color: red;" runat="server" ErrorMessage="Email required!" ControlToValidate="tb_email"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-4" style="text-align: left;">
                                    <p style="text-align: left;">Phone</p>
                                    <asp:TextBox ID="tb_phone" runat="server" class="form-control" MaxLength="100"></asp:TextBox>
                                </div>
                                <div class="col-lg-4">
                                    <p style="text-align: left;">Role</p>
                                    <asp:DropDownList ID="DropDownList1" runat="server" class="form-select" DataTextField="role_name" DataValueField="id_role" DataSourceID="SqlDataSource1"></asp:DropDownList><asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString="<%$ ConnectionStrings:techeavenConnectionString %>" SelectCommand="SELECT * FROM [roles]"></asp:SqlDataSource>
                                </div>
                            </div>



                            <div class="row">
                                <div class="col-lg-4">
                                    <p style="text-align: left;">NIF</p>
                                    <asp:TextBox ID="tb_nif" runat="server" class="form-control" MaxLength="100" onkeypress="return isNumber(event)"></asp:TextBox>
                                </div>
                                <div class="col-4">
                                    <p style="text-align: left;">Verify</p>
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1">Enable</asp:ListItem>
                                        <asp:ListItem Value="0">Disable</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-4">
                                    <p style="text-align: left;">Newsletter</p>
                                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1">Enable</asp:ListItem>
                                        <asp:ListItem Value="0">Disable</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-4">
                                    <p style="text-align: left;">Birthdate</p>
                                    <asp:TextBox ID="tb_birthdate" runat="server" class="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>


                            <asp:Label ID="lbl_erro" runat="server"></asp:Label>

                            <asp:Button ID="btn_edit" runat="server" class="btn btn-primary" Text="Edit User" OnClick="btn_edit_Click" />


                        </div>
                    </div>
                    <!-- End Default Card -->
                </div>
            </div>
        </section>

    </main>

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
