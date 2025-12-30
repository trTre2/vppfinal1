<%@ Page Title="Chi tiết đơn hàng" Language="C#" MasterPageFile="~/QuanTri.master" AutoEventWireup="true" CodeFile="ChiTietDonHang.aspx.cs" Inherits="BackEnd_ChiTietDonHang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/css/checkout.css") %>" />
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
        <asp:BoundField DataField="DonGia" HeaderText="Đơn giá" DataFormatString="{0:N0}" />
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
</asp:Content>