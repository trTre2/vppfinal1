<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="ProductDetail.aspx.cs" Inherits="FrontEnd_DetailProduct" MaintainScrollPositionOnPostBack="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/css/products.css") %>" />
    <div class="container">

    <div class="product-detail">

        <!-- LEFT -->
        <div class="product-detail-left">
            <asp:Image 
                ID="imgProduct"
                runat="server"
                CssClass="product-image"
                AlternateText="Ảnh sản phẩm" />
        </div>

        <!-- RIGHT -->
        <div class="product-detail-right">

            <asp:Label 
                ID="lblProductTitle"
                runat="server"
                CssClass="product-title"
                Text="Tên sản phẩm"
                Font-Size="X-Large"
                Font-Bold="true" />

            <asp:Label 
                ID="lblProductCode"
                runat="server"
                CssClass="product-code"
                Text="Mã sản phẩm: Đang cập nhật" />

            <asp:Label 
                ID="lblProductPrice"
                runat="server"
                CssClass="product-price"
                Text="Giá: 0đ" />

            <p class="product-status">
                Tình trạng:
                <span class="in-stock">Còn hàng</span>
            </p>

            <div class="product-actions">

                <asp:TextBox
                    ID="txtQuantity"
                    runat="server"
                    CssClass="quantity-input"
                    Text="1"
                    TextMode="Number" />

                <asp:Button
                    ID="btnAddToCart"
                    runat="server"
                    CssClass="add-to-cart-btn"
                    Text="Thêm vào giỏ hàng"
                    CommandArgument='<%# Eval("id") %>'
                    OnCommand="AddMainProductToCart" />
            </div>
        </div>

        <!-- POLICY -->
        <div class="policy-box">
            <div class="policy-section">
                <h4>BẢO HÀNH ĐỔI TRẢ</h4>
                <p>🏠 Chính hãng 100%</p>
                <p>👤 Đổi trả, khiếu nại sản phẩm</p>
            </div>
            <asp:Image ID="ads" CssClass="qc" runat="server" AlternateText="Quảng cáo" />
            <div class="policy-section">
                <h4>CHÍNH SÁCH KHÁC</h4>
                <p>🚚 Giao hàng tận nơi</p>
                <p>💳 Thanh toán linh hoạt</p>
                <p>🎁 Ưu đãi cho khách hàng thân thiết</p>
            </div>
        </div>
    </div>

    <!-- DESCRIPTION -->
    <div class="product-description">
        <h2>Thông tin sản phẩm</h2>
        <asp:Label
            ID="lblDescription"
            runat="server"
            Text="Đang tải mô tả..." />
    </div>

    <!-- SAME TYPE PRODUCTS -->
    <h3 style="text-align: center;">SẢN PHẨM TƯƠNG TỰ</h3>

    <asp:DataList
        ID="dlSameType"
        runat="server"
        RepeatLayout="Flow"
        RepeatDirection="Horizontal"
        CssClass="product-grid">

        <ItemTemplate>
            <div class="product-item" onclick="selectProduct(<%# Eval("id") %>)">

                <asp:Image
                    runat="server"
                    ImageUrl='<%# Eval("AnhSP") %>'
                    CssClass="product-image" />

                <h4><%# Eval("TenSP") %></h4>

                <p class="price">
                    <%# String.Format("{0:N0} đ", Eval("Gia")) %>
                </p>

                <asp:Button
                    runat="server"
                    CssClass="add-to-cart"
                    Text="Thêm vào giỏ"
                    CommandArgument='<%# Eval("id") %>'
                    OnCommand="AddToCartById" />
            </div>
        </ItemTemplate>
    </asp:DataList>

    <!-- SERVICES -->
    <section id="services">
        <div class="service-item">
            <h3>🚚 Miễn phí vận chuyển</h3>
            <p>Cho đơn hàng từ 300.000đ</p>
        </div>

        <div class="service-item">
            <h3>↺ Đổi trả 7 ngày</h3>
            <p>Thủ tục đơn giản</p>
        </div>

        <div class="service-item">
            <h3>🔒 Thanh toán an toàn</h3>
            <p>Bảo mật 100%</p>
        </div>
    </section>

</div>
<script type="text/javascript">
    function selectProduct(id) {
        window.location.href = '<%= ResolveUrl("~/FrontEnd/ProductDetail.aspx") %>?id=' + id;
    }
</script>
</asp:Content>

