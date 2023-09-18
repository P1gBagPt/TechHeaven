﻿<%@ Page Title="" Language="C#" MasterPageFile="~/master_page.Master" AutoEventWireup="true" CodeBehind="account.aspx.cs" Inherits="TechHeaven.account" %>

<%@ MasterType VirtualPath="~/master_page.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        body {
            background: #f8f8f8;
        }

        .emp-profile {
            padding: 3%;
            margin-top: 3%;
            margin-bottom: 3%;
            border-radius: 0.5rem;
            background: #fff;
        }

        .profile-img {
            text-align: center;
        }

            .profile-img img {
                width: 70%;
                height: 100%;
            }

            .profile-img .file {
                position: relative;
                overflow: hidden;
                margin-top: -20%;
                width: 70%;
                border: none;
                border-radius: 0;
                font-size: 15px;
                background: #212529b8;
            }

                .profile-img .file input {
                    position: absolute;
                    opacity: 0;
                    right: 0;
                    top: 0;
                }

        .profile-head h5 {
            color: #333;
        }

        .profile-head h6 {
            color: #0062cc;
        }

        .profile-edit-btn {
            border: none;
            border-radius: 1.5rem;
            width: 70%;
            padding: 2%;
            font-weight: 600;
            color: #6c757d;
            cursor: pointer;
        }

        .proile-rating {
            font-size: 12px;
            color: #818182;
            margin-top: 5%;
        }

            .proile-rating span {
                color: #495057;
                font-size: 15px;
                font-weight: 600;
            }

        .profile-head .nav-tabs {
            margin-bottom: 5%;
        }

            .profile-head .nav-tabs .nav-link {
                font-weight: 600;
                border: none;
            }

                .profile-head .nav-tabs .nav-link.active {
                    border: none;
                    border-bottom: 2px solid #0062cc;
                }

        .profile-work {
            /*padding: 14%;*/
            margin-top: -15%;
        }

            .profile-work p {
                font-size: 12px;
                color: #818182;
                font-weight: 600;
                margin-top: 10%;
            }

        .label-user-email {
            text-decoration: none;
            color: #495057;
            font-weight: 600;
            font-size: 14px;
        }

        .profile-work ul {
            list-style: none;
        }

        .profile-tab label {
            font-weight: 600;
        }

        .profile-tab p, .lbl-user-info {
            font-weight: 600;
            color: #0062cc;
        }

        .tb-user-info {
            max-width: 220px;
        }

        .top {
            margin-bottom: 0%;
        }

        .btn-save {
            appearance: none;
            backface-visibility: hidden;
            background-color: #5cb85c;
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

            .btn-save:hover {
                color: black;
                background-color: #51a651;
                border: none;
                box-shadow: rgba(0, 0, 0, .05) 0 5px 30px, rgba(0, 0, 0, .05) 0 1px 4px;
                opacity: 1;
                transform: translateY(0);
                transition-duration: .35s;
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container emp-profile">
        <div>
            <div class="row top">
                <div class="col-md-4">
                    <div class="profile-img">
                        <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS52y5aInsxSm31CvHOFHWujqUx_wWTS9iM6s7BAm21oEN_RiGoog" alt="" />
                        <br />
                    </div>
                </div>
                <div class="col-md-8">
                    <div class="profile-head">
                        <h5>
                            <asp:Label runat="server" ID="lbl_nameTop">-</asp:Label>
                            <br />
                            <br />

                        </h5>
                        <ul class="nav nav-tabs" id="myTab" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" id="home-tab" data-toggle="tab" href="#home" role="tab" aria-controls="home" aria-selected="true">About</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" id="profile-tab" data-toggle="tab" href="#profile" role="tab" aria-controls="profile" aria-selected="false">Addresses</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <div class="profile-work">
                        <p>Username</p>
                        <asp:Label ID="lbl_username" runat="server" class="label-user-email"></asp:Label>
                        <p>Email</p>
                        <asp:Label ID="lbl_email" runat="server" class="label-user-email"></asp:Label>

                        <p>Newsletter Settings</p>

                        <asp:RadioButtonList ID="rbl_newsletter" runat="server">
                            <asp:ListItem>Enable</asp:ListItem>
                            <asp:ListItem>Disabled</asp:ListItem>
                        </asp:RadioButtonList><asp:SqlDataSource runat="server" ID="SqlDataSource1"></asp:SqlDataSource>



                        <asp:Button ID="btn_save_newsletter" runat="server" class="btn-save" Text="Save" OnClick="btn_save_newsletter_Click" CausesValidation="False" />
                        <asp:Label ID="lbl_news" runat="server" Visible="False" Enabled="False"></asp:Label>




                    </div>
                </div>
                <div class="col-md-8">
                    <div class="tab-content profile-tab" id="myTabContent">
                        <div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab">
                            <div class="row">
                                <div class="col-md-2">
                                    <label>First Name</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="tb_first_name" runat="server" class="tb-user-info" MaxLength="50"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label>Last Name</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="tb_last_name" runat="server" class="tb-user-info" MaxLength="50"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label>NIF</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="tb_NIF" runat="server" class="tb-user-info" MaxLength="9" TextMode="SingleLine" onkeypress="return isNumber(event)"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label>Phone Number</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="tb_phoneNumber" runat="server" class="tb-user-info" MaxLength="100" TextMode="Phone"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                </div>
                                <div class="col-md-2">
                                    <label>2FA</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:Button ID="btn_2fa" runat="server" Text="Setup" OnClick="btn_2fa_Click" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:LinkButton ID="btn_save" class="btn-save" runat="server" OnClick="btn_save_Click">Save</asp:LinkButton>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:Label ID="lbl_sucesso" runat="server" Enabled="False" Visible="False"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lbl_erro" runat="server" Enabled="False" Visible="False"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="profile" role="tabpanel" aria-labelledby="profile-tab">
                            <div class="row">
                                <div class="col-md-6">
                                    <label>Experience</label>
                                </div>
                                <div class="col-md-6">
                                    <p>Expert</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label>Hourly Rate</label>
                                </div>
                                <div class="col-md-6">
                                    <p>10$/hr</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label>Total Projects</label>
                                </div>
                                <div class="col-md-6">
                                    <p>230</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label>English Level</label>
                                </div>
                                <div class="col-md-6">
                                    <p>Expert</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label>Availability</label>
                                </div>
                                <div class="col-md-6">
                                    <p>6 months</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <label>Your Bio</label><br />
                                    <p>Your detail description</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

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
