<%@ Page Title="Quản lý đơn hàng" Language="C#" MasterPageFile="~/QuanTri.master" AutoEventWireup="true" CodeFile="QLDonHang.aspx.cs" Inherits="BackEnd_QLDonHang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/css/GridviewQL.css") %>" />

    <div id="container">
        <div class="profile-box">
            <h3>THÊM ĐƠN HÀNG</h3>

            <table class="form-table">
                <tr>
                    <td>Khách hàng</td>
                    <td>
                        <asp:DropDownList ID="ddlKhachHang" runat="server" CssClass="input" />
                    </td>
                </tr>
                <tr>
                    <td>Trạng thái</td>
                    <td>
                        <asp:DropDownList ID="ddlTrangThai" runat="server" CssClass="input">
                            <asp:ListItem>Chờ xử lý</asp:ListItem>
                            <asp:ListItem>Đang giao</asp:ListItem>
                            <asp:ListItem>Hoàn thành</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>

            <h4>Thêm sản phẩm</h4>
            <asp:TextBox ID="txtTimSP" runat="server" CssClass="input" />
            <asp:Button ID="btnTimSP" runat="server" Text="Tìm SP" CssClass="btn" OnClick="btnTimSP_Click" />

            <asp:GridView ID="gvTimSP" runat="server" AutoGenerateColumns="False" CssClass="table">
                <Columns>
                    <asp:BoundField DataField="TenSP" HeaderText="Tên SP" />
                    <asp:BoundField DataField="Gia" HeaderText="Giá" DataFormatString="{0:N0}" />
                    <asp:TemplateField HeaderText="Số lượng">
                        <ItemTemplate>
                            <asp:TextBox ID="txtSL" runat="server" Text="1" CssClass="input" TextMode="Number" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnThemSP" runat="server" Text="Thêm" CssClass="btn" OnClick="btnThemSP_Click" CommandArgument='<%# Eval("id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <h4>Sản phẩm đã chọn</h4>
            <asp:GridView ID="gvDonHangTemp" runat="server" AutoGenerateColumns="False" CssClass="table" HtmlEncode="false">
                <Columns>
                    <asp:TemplateField HeaderText="Tên SP">
                        <ItemTemplate>
                            <%# HttpUtility.HtmlDecode(Eval("TenSP").ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Gia" HeaderText="Giá" DataFormatString="{0:N0}" />
                    <asp:BoundField DataField="SoLuong" HeaderText="Số lượng" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnXoaSP" runat="server" Text="Xóa" CssClass="btn cancel" CommandArgument='<%# Eval("idSP") %>' OnClick="btnXoaSP_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <div class="action">
                <asp:Button ID="btnLuuDon" runat="server" Text="Lưu đơn hàng" CssClass="btn" OnClick="btnLuuDon_Click" />
            </div>
            <asp:Label ID="lblThongBao" runat="server" CssClass="msg" />
        </div>
        <div class="profile-box">
            <h3>QUẢN LÝ ĐƠN HÀNG</h3>
            <div class="row">
                <asp:DropDownList ID="ddlKhachHangFilter" runat="server"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="Filter_Changed" />

<asp:DropDownList ID="ddlTrangThaiFilter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Filter_Changed">
    <asp:ListItem Value="">Tất cả trạng thái</asp:ListItem>
    <asp:ListItem Value="Chờ xử lý">Chờ xử lý</asp:ListItem>
    <asp:ListItem Value="Đang giao">Đang giao</asp:ListItem>
    <asp:ListItem Value="Hoàn thành">Hoàn thành</asp:ListItem>
</asp:DropDownList>

            </div>
            <asp:GridView ID="gvDonHang" runat="server"
                AutoGenerateColumns="False"
                CssClass="table"
                DataKeyNames="MaDH"
                OnRowEditing="gvDonHang_RowEditing"
                OnRowUpdating="gvDonHang_RowUpdating"
                OnRowCancelingEdit="gvDonHang_RowCancelingEdit"
                OnRowDeleting="gvDonHang_RowDeleting"
                OnRowDataBound="gvDonHang_RowDataBound">

                <Columns>

                    <asp:BoundField DataField="MaDH" HeaderText="Mã đơn" ReadOnly="true" />
                    <asp:BoundField DataField="TenKH" HeaderText="Khách hàng" ReadOnly="true" />
                    <asp:BoundField DataField="NgayDat" HeaderText="Ngày đặt" ReadOnly="true" />

                    <asp:TemplateField HeaderText="Trạng thái">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlTrangThaiEdit" runat="server">
                                <asp:ListItem>Chờ xử lý</asp:ListItem>
                                <asp:ListItem>Đang giao</asp:ListItem>
                                <asp:ListItem>Hoàn thành</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <%# Eval("TrangThai") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>

