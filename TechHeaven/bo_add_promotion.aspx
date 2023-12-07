<%@ Page Title="" Language="C#" MasterPageFile="~/master_page_admin.Master" AutoEventWireup="true" CodeBehind="bo_add_promotion.aspx.cs" Inherits="TechHeaven.bo_add_promotion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main" class="main">
        <section class="section">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body text-center">
                            <h1 class="card-title">Add Promotion</h1>

                            <div class="row">
                                <div class="col-lg-4" style="text-align: left;">
                                    <h2>Product Image</h2>
                                    <asp:Image ID="img_product" runat="server" class="img-fluid" Style="width: 256px; height: 256px;" />

                                </div>
                                <div class="col-lg-4">
                                    <h2>Product Info</h2>
                                    <asp:Label ID="lbl_product_info" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <h2>Promotion %</h2>
                                    <div class="col-12">
                                        <div class="row">
                                        <div class="col-4">
                                            <p>%<asp:TextBox ID="tb_numero_artigo" class="form-control" runat="server" TextMode="Number" Min="1" Max="100" Value="1"></asp:TextBox></p>
                                            <asp:RequiredFieldValidator ID="rfv_tb_numero_artigo" runat="server" ControlToValidate="tb_numero_artigo" ErrorMessage="Percentage is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-4">
                                            Start
                                            <asp:TextBox ID="tb_start" class="form-control" runat="server" TextMode="Date"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv_start" runat="server" ControlToValidate="tb_start" ErrorMessage="Start date is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-4">
                                            End
                                            <asp:TextBox ID="tb_end" class="form-control" runat="server" TextMode="Date"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv_end" runat="server" ControlToValidate="tb_end" ErrorMessage="End date is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                            </div>

                                    </div>
                                    <div class="col-6" style="text-align: left;">
                                        <p>
                                            Current Price:
                                            <asp:Label ID="lb_current_price" runat="server"></asp:Label>
                                        </p>
                                        <asp:Panel ID="Panel1" runat="server" Visible="false">
                                            <p>
                                                New Price:
                                            <asp:Label ID="lb_new_price" runat="server"></asp:Label>
                                            </p>
                                        </asp:Panel>
                                        <asp:LinkButton ID="lb_preview" runat="server" OnCommand="lb_preview_Command" CommandName="Preview">Preview</asp:LinkButton>

                                    </div>

                                </div>

                                <hr />


                                <asp:Label ID="lbl_erro" runat="server"></asp:Label>

                                <asp:Button ID="btn_promocao" runat="server" class="btn btn-primary" Text="Promotion" OnClick="btn_promocao_Click" />


                            </div>
                        </div>
                        <!-- End Default Card -->
                    </div>
                </div>
            </div>
        </section>

    </main>

</asp:Content>
