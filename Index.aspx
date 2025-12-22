<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/home.css" rel="stylesheet" type="text/css">
    <div class="banner">
        <asp:Image runat="server" ImageUrl="images/banner.png" />
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
                                <asp:Image runat="server" ImageUrl='<%# Eval("AnhSP") %>' alt='<%# Eval("TenSP") %>' />
                                <asp:Label runat="server"><%# Eval("TenSP") %></asp:Label>
                                <asp:Label runat="server" CssClass="price"><%# string.Format("{0:N0} đ", Eval("Gia")) %></asp:Label>
                                <asp:Button runat="server" CssClass="add-to-cart" OnCommand="AddToCartById" CommandArgument='<%# Eval("id") %>' Text="Thêm vào giỏ" />
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
    </div>
    <div class="head-title">
        <asp:LinkButton runat="server" CssClass="head-title" CommandArgument="G" OnCommand="Category_Command"><h3 class="section-title">Giấy</h3></asp:LinkButton>
    </div>
    <asp:DataList ID="paper" runat="server"
        RepeatLayout="Flow"
        RepeatDirection="Horizontal"
        CssClass="product-grid">

        <ItemTemplate>
            <div class="product-item" onclick="selectProduct(<%# Eval("id") %>)">
                <asp:Image runat="server" ImageUrl='<%# Eval("AnhSP") %>' alt='<%# Eval("TenSP") %>' />
                <asp:Label runat="server"><%# Eval("TenSP") %></asp:Label>
                <asp:Label runat="server" CssClass="price"><%# string.Format("{0:N0} đ", Eval("Gia")) %></asp:Label>
                <asp:Button runat="server" CssClass="add-to-cart" OnCommand="AddToCartById" CommandArgument='<%# Eval("id") %>' Text="Thêm vào giỏ" />
            </div>
        </ItemTemplate>
    </asp:DataList>
    <div class="head-title">
        <asp:LinkButton runat="server" CssClass="head-title" CommandArgument="B" OnCommand="Category_Command"><h3 class="section-title">Bút</h3></asp:LinkButton>
    </div>
    <asp:DataList ID="pen" runat="server"
        RepeatLayout="Flow"
        RepeatDirection="Horizontal"
        CssClass="product-grid">
        <ItemTemplate>
            <div class="product-item" onclick="selectProduct(<%# Eval("id") %>)">
                <asp:Image runat="server" ImageUrl='<%# Eval("AnhSP") %>' alt='<%# Eval("TenSP") %>' />
                <asp:Label runat="server"><%# Eval("TenSP") %></asp:Label>
                <asp:Label runat="server" CssClass="price"><%# string.Format("{0:N0} đ", Eval("Gia")) %></asp:Label>
                <asp:Button runat="server" CssClass="add-to-cart" OnCommand="AddToCartById" CommandArgument='<%# Eval("id") %>' Text="Thêm vào giỏ" />
            </div>
        </ItemTemplate>
    </asp:DataList>
    <div class="head-title">
        <asp:LinkButton runat="server" CssClass="head-title" CommandArgument="K" OnCommand="Category_Command"><h3 class="section-title">Các dụng cụ khác</h3></asp:LinkButton>
    </div>
    <asp:DataList ID="other" runat="server"
        RepeatLayout="Flow"
        RepeatDirection="Horizontal"
        CssClass="product-grid">
        <ItemTemplate>
            <div class="product-item" onclick="selectProduct(<%# Eval("id") %>)">
                <asp:Image runat="server" ImageUrl='<%# Eval("AnhSP") %>' alt='<%# Eval("TenSP") %>' />
                <asp:Label runat="server"><%# Eval("TenSP") %></asp:Label>
                <asp:Label runat="server" CssClass="price"><%# string.Format("{0:N0} đ", Eval("Gia")) %></asp:Label>
                <asp:Button runat="server" CssClass="add-to-cart" OnCommand="AddToCartById" CommandArgument='<%# Eval("id") %>' Text="Thêm vào giỏ" />
            </div>
        </ItemTemplate>
    </asp:DataList>
    <script>
        function selectProduct(id) {
            window.location.href = 'FrontEnd/ProductDetail.aspx?id=' + id;
        }
    </script>
</asp:Content>


