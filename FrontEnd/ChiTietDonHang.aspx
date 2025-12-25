<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChiTietDonHang.aspx.cs" Inherits="FrontEnd_ChiTietDonHang" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chi tiết đơn hàng</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

<h1>Chi tiết đơn hàng</h1>

<div id="order-info">
    <asp:Label ID="lblOrderInfo" runat="server" />
</div>

<asp:GridView ID="gvOrderDetail" runat="server"
    AutoGenerateColumns="False"
    CssClass="cart-table">
    <Columns>
        <asp:BoundField DataField="TenSP" HeaderText="Sản phẩm" />
        <asp:BoundField DataField="SoLuong" HeaderText="SL" />
        <asp:BoundField DataField="Gia" HeaderText="Đơn giá" DataFormatString="{0:N0}" />
        <asp:TemplateField HeaderText="Thành tiền">
            <ItemTemplate>
                <%# Eval("ThanhTien", "{0:N0}") %>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

<div id="total">
    <h2>
        Tổng tiền:
        <asp:Label ID="lblTotal" runat="server" />
    </h2>
</div>

        </div>
    </form>
</body>
</html>
