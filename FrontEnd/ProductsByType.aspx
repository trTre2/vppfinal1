<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="ProductsByType.aspx.cs" Inherits="FrontEnd_ProductsByType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/category.css" rel="stylesheet" type="text/css"/>
    <main id="main">
    <h2>CÁC SẢN PHẨM CÙNG LOẠI ĐÃ CHỌN</h2>
        <div id="filtered-products">
    <asp:Repeater ID="rpProducts" runat="server">
        <ItemTemplate>
            <div class="product-item">
                <img src='<%# Eval("AnhSP") %>' alt='<%# Eval("TenSP") %>' />
                <h3><%# Eval("TenSP") %></h3>
                <p class="price">
                    <%# string.Format("{0:N0} đ", Eval("Gia")) %>
                </p>

                <a class="btn-detail"
                   href='ProductDetail.aspx?id=<%# Eval("id") %>'>
                    Xem chi tiết
                </a>
            </div>
        </ItemTemplate>
    </asp:Repeater>
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

</asp:Content>

