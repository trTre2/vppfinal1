<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/home.css" rel="stylesheet" type="text/css">
    <div Class="banner">
    <asp:Image runat="server" ImageUrl="images/banner.png"/>
        </div>
    <div class="featured-products">
        <h2>Sản phẩm nổi bật</h2>
        <div id="noibat">
            <div class="scrolling-content">
                <div class="sanpham">
                    <asp:DataList ID="featured_products" runat="server"
                        RepeatLayout="Flow"
                        RepeatDirection="Horizontal"
                        CssClass="product-grid">
                        <ItemTemplate>
                            <div class="product-item" onclick="selectProduct(<%# Eval("id") %>)">
                                <img src='<%# Eval("AnhSP") %>' alt='<%# Eval("TenSP") %>' />
                                <h4><%# Eval("TenSP") %></h4>
                                <p class="price"><%# Eval("Gia") %>đ</p>
                                <button class="add-to-cart"
                                    onclick="addToCartById(<%# Eval("id") %>, event)">
                                    Thêm vào giỏ
                                </button>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
    </div>
    <div class="head-title" >
        <h3 class="section-title"><asp:LinkButton runat="server" CssClass="head-title" CommandArgument="G" OnCommand="Category_Command">Giấy</asp:LinkButton></h3>
    </div>
    <div class="product-grid">
        <asp:DataList ID="paper" runat="server"
            RepeatLayout="Flow"
            RepeatDirection="Horizontal"
            CssClass = "product-grid">

            <ItemTemplate>
                <div class="product-item" onclick="selectProduct(<%# Eval("id") %>)">
                    <img src='<%# Eval("AnhSP") %>' alt='<%# Eval("TenSP") %>' />
                    <h4><%# Eval("TenSP") %></h4>
                    <p class="price"><%# Eval("Gia") %>đ</p>
                    <button class="add-to-cart"
                        onclick="addToCartById(<%# Eval("id") %>, event)">
                        Thêm vào giỏ
                    </button>
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>
    <div class="head-title" onclick="redirectToCategory('giay')">
        <h3 class="section-title">Bút</h3>
    </div>
    <div class="product-grid">
        <asp:DataList ID="pen" runat="server"
            RepeatLayout="Flow"
            RepeatDirection="Horizontal"
            CssClass="product-grid">
            <ItemTemplate>
                <div class="product-item" onclick="selectProduct(<%# Eval("id") %>)">
                    <img src='<%# Eval("AnhSP") %>' alt='<%# Eval("TenSP") %>' />
                    <h4><%# Eval("TenSP") %></h4>
                    <p class="price"><%# Eval("Gia") %>đ</p>
                    <button class="add-to-cart"
                        onclick="addToCartById(<%# Eval("id") %>, event)">
                        Thêm vào giỏ
                    </button>
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>
    <div class="head-title" onclick="redirectToCategory('giay')">
        <h3 class="section-title">Các dụng cụ khác</h3>
    </div>
    <div class="product-grid">
        <asp:DataList ID="other" runat="server"
            RepeatLayout="Flow"
            RepeatDirection="Horizontal"
            CssClass="product-grid">
            <ItemTemplate>
                <div class="product-item" onclick="selectProduct(<%# Eval("id") %>)">
                    <img src='<%# Eval("AnhSP") %>' alt='<%# Eval("TenSP") %>' />
                    <h4><%# Eval("TenSP") %></h4>
                    <p class="price"><%# Eval("Gia") %>đ</p>
                    <button class="add-to-cart"
                        onclick="addToCartById(<%# Eval("id") %>, event)">
                        Thêm vào giỏ
                    </button>
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>
</asp:Content>


