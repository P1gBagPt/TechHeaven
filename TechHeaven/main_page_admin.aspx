<%@ Page Title="" Language="C#" MasterPageFile="~/master_page_admin.Master" AutoEventWireup="true" CodeBehind="main_page_admin.aspx.cs" Inherits="TechHeaven.main_page_admin" %>

<%@ MasterType VirtualPath="~/master_page_admin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">




    <main id="main" class="main">

        <div class="pagetitle">
            <h1>Dashboard</h1>
            <nav>
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="main_page_admin.aspx">Home</a></li>
                    <li class="breadcrumb-item active">Dashboard</li>
                </ol>
            </nav>
        </div>
        <!-- End Page Title -->

        <section class="section dashboard">
            <div class="row">

                <!-- Left side columns -->
                <div class="col-lg-12">
                    <div class="row">
                        <asp:LinkButton ID="lb_pdf_generate" runat="server" OnCommand="lb_pdf_generate_Command" CommandName="GeneratePdf">Generate pdf</asp:LinkButton>
                        <!-- Sales Card -->
                        <div class="col-xxl-3 col-md-6">
                            <div class="card info-card sales-card">


                                <div class="card-body">
                                    <h5 class="card-title">Total sales</h5>

                                    <div class="d-flex align-items-center">
                                        <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                            <i class="bi bi-cart"></i>
                                        </div>
                                        <div class="ps-3">
                                            <h6 id="h6_TotalEncomendas" runat="server"></h6>

                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <!-- End Sales Card -->

                        <!-- Revenue Card -->
                        <div class="col-xxl-3 col-md-6">
                            <div class="card info-card revenue-card">


                                <div class="card-body">
                                    <h5 class="card-title">Profit</h5>

                                    <div class="d-flex align-items-center">
                                        <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                            <i class="bi bi-currency-dollar"></i>
                                        </div>
                                        <div class="ps-3">
                                            <h6 id="h6_TotalVendas" runat="server"></h6>

                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <!-- End Revenue Card -->

                        <!-- Customers Card -->
                        <!-- Customers Card -->
                        <div class="col-xxl-3 col-md-6">
                            <div class="card info-card customers-card">
                                <div class="card-body">
                                    <h5 class="card-title">Clients</h5>
                                    <div class="d-flex align-items-center">
                                        <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                            <i class="bi bi-people"></i>
                                        </div>
                                        <div class="ps-3">
                                            <h6 id="h6_TotalClientes" runat="server"></h6>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- End Customers Card -->

                        <!-- Products Card -->
                        <div class="col-xxl-3 col-md-6">
                            <div class="card info-card customers-card">
                                <div class="card-body">
                                    <h5 class="card-title">Products</h5>
                                    <div class="d-flex align-items-center">
                                        <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                            <i class="bi bi-box"></i>
                                            <!-- Assuming you want a box icon for Products -->
                                        </div>
                                        <div class="ps-3">
                                            <h6 id="h6_totalProducts" runat="server"></h6>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- End Products Card -->

                        <!-- End Customers Card -->

                        <!-- Categories Card -->
                        <div class="col-xxl-3 col-md-6">
                            <div class="card info-card customers-card">
                                <div class="card-body">
                                    <h5 class="card-title">Categories</h5>
                                    <div class="d-flex align-items-center">
                                        <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                            <i class="bi bi-tags"></i>
                                            <!-- Tags icon for Categories -->
                                        </div>
                                        <div class="ps-3">
                                            <h6 id="h6_total_categories" runat="server"></h6>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Brands Card -->
                        <div class="col-xxl-3 col-md-6">
                            <div class="card info-card customers-card">
                                <div class="card-body">
                                    <h5 class="card-title">Brands</h5>
                                    <div class="d-flex align-items-center">
                                        <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                            <i class="bi bi-gem"></i>
                                            <!-- Gem icon for Brands -->
                                        </div>
                                        <div class="ps-3">
                                            <h6 id="h6_totalBrands" runat="server"></h6>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Newsletter Register Card -->
                        <div class="col-xxl-3 col-md-6">
                            <div class="card info-card customers-card">
                                <div class="card-body">
                                    <h5 class="card-title">Newsletter Register</h5>
                                    <div class="d-flex align-items-center">
                                        <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                            <i class="bi bi-file-earmark-text"></i>
                                            <!-- File Text icon for Newsletter Register -->
                                        </div>
                                        <div class="ps-3">
                                            <h6 id="h6_totalNewsletter" runat="server"></h6>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <!-- Newsletter Register Card -->
                        <div class="col-xxl-3 col-md-6">
                            <div class="card info-card customers-card">
                                <div class="card-body">
                                    <h5 class="card-title">Total reviews</h5>
                                    <div class="d-flex align-items-center">
                                        <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                            <i class="bi bi-chat-left-dots"></i>
                                            <!-- File Text icon for Newsletter Register -->
                                        </div>
                                        <div class="ps-3">
                                            <h6 id="h6_totalReviews" runat="server"></h6>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                    <!-- End Right side columns -->
                </div>
            </div>
        </section>

    </main>







</asp:Content>
