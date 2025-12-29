<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="UserInfo.aspx.cs" Inherits="UserInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
                <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/css/profile.css") %>" />
   <div id="container">
    <div class="profile-box">

    <!-- TÊN -->
    <div class="row">
        <span class="title">Tên khách hàng</span>
        <asp:Label ID="lblTenKH" runat="server" />
        <asp:TextBox ID="txtTenKH" runat="server" CssClass="input" Visible="false" />
    </div>

    <!-- ĐIỆN THOẠI -->
    <div class="row">
        <span class="title">Số điện thoại</span>
        <asp:Label ID="lblPhone" runat="server" />
        <asp:TextBox ID="txtPhone" runat="server" CssClass="input" Visible="false" />
    </div>

    <!-- ĐỊA CHỈ -->
    <div class="row">
        <span class="title">Địa chỉ</span>
        <asp:Label ID="lblDiaChi" runat="server" />
        <asp:TextBox ID="txtDiaChi" runat="server" CssClass="input" Visible="false" />
    </div>

    <!-- EMAIL -->
    <div class="row">
        <span class="title">Email</span>
        <asp:Label ID="lblEmail" runat="server" />
        <asp:TextBox ID="txtEmail" runat="server" CssClass="input" Visible="false" />
    </div>

    <div class="action">
        <asp:Button ID="btnEdit" runat="server" Text="Sửa"
            CssClass="btn"
            OnClick="btnEdit_Click" />

        <asp:Button ID="btnSave" runat="server" Text="Lưu"
            CssClass="btn"
            Visible="false"
            OnClick="btnSave_Click" />

        <asp:Button ID="btnCancel" runat="server" Text="Huỷ"
            CssClass="btn cancel"
            Visible="false"
            OnClick="btnCancel_Click" />
    </div>

</div>
    <div class="profile-box">
        <div class="order-box">
    <h3>Đơn hàng đã mua</h3>

    <asp:Repeater ID="rpOrders" runat="server">
        <ItemTemplate>
                    <asp:HyperLink runat="server"
            CssClass="order-item link"
            NavigateUrl='<%# "~/FrontEnd/ChiTietDonHang.aspx?MaDH=" + Eval("MaDH") %>'>
            <div class="order-item">
                <div><b>Mã đơn:</b> <%# Eval("MaDH") %></div>
                <div><b>Ngày đặt:</b> <%# Eval("NgayDat", "{0:dd/MM/yyyy}") %></div>
                <div><b>Tổng tiền:</b> <%# Eval("TongTien", "{0:N0}") %> đ</div>
                <div><b>Trạng thái:</b> <%# Eval("TrangThai") %></div>
            </div>
                        </asp:HyperLink>
        </ItemTemplate>
    </asp:Repeater>

    <asp:Label ID="lblNoOrder" runat="server"
        Text="Chưa có đơn hàng nào."
        Visible="false"
        CssClass="empty" />
</div>

    </div>
    </div>
<script>
    function togglePasswordBox() {
        const box = document.getElementById("passwordBox");
        box.style.display = box.style.display === "none" || box.style.display === ""
            ? "block" : "none";
    }
</script>
</asp:Content>

