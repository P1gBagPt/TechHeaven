<%@ Page Title="" Language="C#" MasterPageFile="~/master_page_admin.Master" AutoEventWireup="true" CodeBehind="bo_edit_product.aspx.cs" Inherits="TechHeaven.bo_edit_product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main id="main" class="main">
        <section class="section">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body text-center">
                            <h1 class="card-title">Edit Product</h1>
                            <div class="row">
                                <asp:LinkButton ID="lb_add_promo" runat="server" OnCommand="lb_add_promo_Command" CommandName="Promo">Add Promotion</asp:LinkButton>
                            </div>
                            <div class="row">
                                <div class="col-lg-4" style="text-align: left;">
                                    <p>Name</p>
                                    <asp:TextBox ID="tb_nome" class="form-control" runat="server" MaxLength="255"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red;" runat="server" ErrorMessage="Name is required!" ControlToValidate="tb_nome"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-4" style="text-align: left;">
                                    <p>Product Code</p>
                                    <asp:TextBox ID="tb_numero_artigo" class="form-control" runat="server" MaxLength="255"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" runat="server" ErrorMessage="Product code is required!" ControlToValidate="tb_numero_artigo"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-4" style="text-align: left;">
                                    <p>Price</p>
                                    <asp:TextBox ID="tb_preco" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Style="color: red;" runat="server" ErrorMessage="Price is required!" ControlToValidate="tb_preco"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-4" style="text-align: left;">
                                    <p style="text-align: left;">Stock</p>
                                    <asp:TextBox ID="tb_stock" class="form-control" runat="server" MaxLength="5" onkeypress="return isNumber(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Style="color: red;" runat="server" ErrorMessage="Stock is required!" ControlToValidate="tb_stock"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-4">
                                    <p style="text-align: left;">Category</p>
                                    <asp:DropDownList ID="ddl_categoria" class="form-select" runat="server" DataTextField="category_name" DataValueField="id_category" DataSourceID="SqlDataSource1"></asp:DropDownList><asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString="<%$ ConnectionStrings:techeavenConnectionString %>" SelectCommand="SELECT * FROM [categories]"></asp:SqlDataSource>
                                </div>
                                <div class="col-lg-4">
                                    <p style="text-align: left;">Image</p>
                                    <asp:FileUpload class="form-control" ID="fu_imagem" runat="server" />
                                </div>
                            </div>

                            <div class="row">

                                <div class="col-lg-4" style="text-align: left;">
                                    <asp:DropDownList ID="ddl_marca" class="form-select" runat="server" DataTextField="brand_name" DataValueField="id_brand" DataSourceID="SqlDataSource2"></asp:DropDownList><asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString="<%$ ConnectionStrings:techeavenConnectionString %>" SelectCommand="SELECT * FROM [brands]"></asp:SqlDataSource>
                                </div>
                                <div class="col-lg-4">
                                    <p style="text-align: left;">Description</p>
                                    <asp:TextBox ID="tb_descricao" class="form-control" runat="server" TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                                </div>

                                <div class="col-lg-4">
                                    <p style="text-align: center;">Current Image</p>
                                    <p>
                                        <asp:Label ID="lbl_nao_imagem" runat="server" Text="(No Image)" Enabled="False" Visible="False"></asp:Label>
                                    </p>
                                    <asp:Image ID="productImage" Style="border: 1px solid black; width: 128px; height: 128px;" runat="server" />
                                </div>
                            </div>

                            <asp:Label ID="lbl_erro" runat="server"></asp:Label>

                            <asp:Button ID="btn_editar" runat="server" class="btn btn-primary" Text="Edit product" OnClick="btn_editar_Click" />


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
