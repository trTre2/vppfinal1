<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="ProductsByType.aspx.cs" Inherits="FrontEnd_ProductsByType" MaintainScrollPositionOnPostBack="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="<%= ResolveUrl("~/css/category.css") %>" rel="stylesheet" type="text/css"/>
    <main id="main">
        <h2 style="margin: 30px 0;">CÁC SẢN PHẨM CÙNG LOẠI ĐÃ CHỌN</h2>
        <div class="sanpham">
            <asp:DataList ID="bytype" runat="server"
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
        <!-- giữ nguyên phần dịch vụ -->
        <section id="services">
            <div class="service-item">
                <h3>🚚 Miễn phí vận chuyển</h3>
                <p>Cho đơn hàng từ 300.000đ</p>
            </div>
            <div class="service-item">
                <h3>↩️ Đổi trả 7 ngày</h3>
                <p>Thủ tục đơn giản</p>
            </div>
            <div class="service-item">
                <h3>🔒 Thanh toán an toàn</h3>
                <p>Bảo mật 100%</p>
            </div>
        </section>
    </main>
    <script>
        function selectProduct(id) {
            window.location.href = '<%= ResolveUrl("~/FrontEnd/ProductDetail.aspx") %>?id=' + id;
        }
    </script>
</asp:Content>

