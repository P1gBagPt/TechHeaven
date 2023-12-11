<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="add_card.aspx.cs" Inherits="TechHeaven.add_card" %>

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
                                <h2 class="text-uppercase text-center mb-5">Add Card <i class="fa-regular fa-credit-card"></i></h2>

                                <div class="form-group">
                                    <h5>Name in the card</h5>
                                    <asp:TextBox class="form-control form-control-lg" ID="card_name" runat="server" MaxLength="50" Style="font-size: 15px;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="card_name" ForeColor="Red" Display="Dynamic">
                                        <span>
                                            <asp:Label ID="Label1" runat="server" Text="Please enter a name"></asp:Label>
                                            <i class="fa-solid fa-circle-exclamation" style="color: #8c1818;"></i>
                                        </span>
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <h5>Card Number</h5>
                                    <asp:TextBox class="form-control form-control-lg" ID="card_number" runat="server" Style="font-size: 15px;" MaxLength="16" onkeypress="return isNumber(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="card_number" ForeColor="Red" Display="Dynamic">
                                        <span>
                                            <asp:Label ID="Label2" runat="server" Text="Please enter a card number"></asp:Label>
                                            <i class="fa-solid fa-circle-exclamation" style="color: #8c1818;"></i>
                                        </span>
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <h5>CVV and Expiration</h5>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:TextBox class="form-control form-control-lg" ID="cvv" runat="server" MaxLength="3" Style="font-size: 15px;" onkeypress="return isNumber(event)"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cvv" ForeColor="Red" Display="Dynamic">
                                                <span>
                                                    <asp:Label ID="Label3" runat="server" Text="Please enter a cvv"></asp:Label>
                                                    <i class="fa-solid fa-circle-exclamation" style="color: #8c1818;"></i>
                                                </span>
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-6">                              
                                            <asp:TextBox class="form-control form-control-lg" ID="expiration" runat="server" Style="font-size: 15px;" TextMode="Month"></asp:TextBox>
                                          
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="expiration" ForeColor="Red" Display="Dynamic">
                                                <span>
                                                    <asp:Label ID="Label6" runat="server" Text="Please enter a expiration date"></asp:Label>
                                                    <i class="fa-solid fa-circle-exclamation" style="color: #8c1818;"></i>
                                                </span>
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>



                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <h5>Card type</h5>
                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control form-control-lg" DataTextField="name" DataValueField="id_type_card" DataSourceID="SqlDataSource1">

                                            </asp:DropDownList><asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString="<%$ ConnectionStrings:techeavenConnectionString %>" SelectCommand="SELECT * FROM [card_type]"></asp:SqlDataSource>

                                        </div>
                                    </div>
                                    
                                </div>
                             
                                <div>
                                    <asp:Label ID="lbl_erro" runat="server" Visible="False"></asp:Label>
                                </div>

                                <div class="d-flex justify-content-center">
                                    <asp:LinkButton ID="submit_card" class="btn btn-success btn-register btn-block btn-lg text-body" runat="server" OnClick="submit_card_Click">Add Card <i class="fa-solid fa-plus"></i></asp:LinkButton>
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
