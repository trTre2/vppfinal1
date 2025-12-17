<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/home.css" rel="stylesheet" type="text/css">
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
                            <div class="product-item" onclick="selectProduct(<%# Eval("MaSP") %>)">
                                <img src='<%# Eval("AnhSP") %>' alt='<%# Eval("TenSP") %>' />
                                <h4><%# Eval("TenSP") %></h4>
                                <p class="price"><%# Eval("Gia") %>đ</p>
                                <button class="add-to-cart"
                                    onclick="addToCartById(<%# Eval("MaSP") %>, event)">
                                    Thêm vào giỏ
                                </button>
                            </div>
                        </ItemTemplate>


                    </asp:DataList>
                    <asp:DataList ID="paper" runat="server"
                        RepeatLayout="Flow"
                        RepeatDirection="Horizontal"
                        CssClass="product-grid">

                        <ItemTemplate>
                            <div class="product-item" onclick="selectProduct(<%# Eval("MaSP") %>)">
                                <img src='<%# Eval("AnhSP") %>' alt='<%# Eval("TenSP") %>' />
                                <h4><%# Eval("TenSP") %></h4>
                                <p class="price"><%# Eval("Gia") %>đ</p>
                                <button class="add-to-cart"
                                    onclick="addToCartById(<%# Eval("MaSP") %>, event)">
                                    Thêm vào giỏ
                                </button>
                            </div>
                        </ItemTemplate>


                    </asp:DataList>
                </div>
            </div>

        </div>

    </div>

</asp:Content>


