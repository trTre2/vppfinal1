<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="CheckOut.aspx.cs" Inherits="FrontEnd_CheckOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
            <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/css/checkout.css") %>" />

    <main id="main">
        <h1>Thanh toán</h1>
        <div id="cart-items">
<asp:GridView  
    ID="gvCart"
    runat="server"
    AutoGenerateColumns="False"
    CssClass="cart-table"
    GridLines="None"
    ShowFooter="true">

    <Columns>
        <asp:BoundField DataField="TenSP" HeaderText="Tên sản phẩm" />

        <asp:BoundField 
            DataField="SoLuong" 
            HeaderText="Số lượng" />

        <asp:BoundField 
            DataField="Gia" 
            HeaderText="Đơn giá (VND)"
            DataFormatString="{0:N0}" />

        <asp:TemplateField HeaderText="Thành tiền (VND)">
            <ItemTemplate>
                <%#string.Format("{0:N0} ", Convert.ToInt32(Eval("SoLuong")) * Convert.ToDecimal(Eval("Gia"))) %>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
            </div>

<br />

<asp:Label CssClass="total" ID="lblTotal" runat="server" Font-Bold="true" />

<br /><br />

<asp:Button 
    ID="btnCheckout" 
    CssClass="checkout-btn"
    runat="server" 
    Text="Thanh toán"
    OnClick="btnCheckout_Click" />

    </main>
    <section id="services">
        <div class="service-item" onclick="selectProduct()">
            <i class="fas fa-shipping-fast"></i>
            <h3>&#128666; Miễn phí vận chuyển</h3>
            <p>Cho đơn hàng từ 300.000đ</p>
        </div>
        <div class="service-item" onclick="selectProduct()">
            <i class="fas fa-undo"></i>
            <h3>&#8635; Đổi trả 7 ngày</h3>
            <p>Thủ tục đơn giản</p>
        </div>
        <div class="service-item" onclick="selectProduct()">
            <i class="fas fa-lock"></i>
            <h3>&#x1F512; Thanh toán an toàn</h3>
            <p>Bảo mật 100%</p>
        </div>
    </section>
</asp:Content>

