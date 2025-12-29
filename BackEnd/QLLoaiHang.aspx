<%@ Page Title="" Language="C#" MasterPageFile="~/QuanTri.master" AutoEventWireup="true" CodeFile="QLLoaiHang.aspx.cs" Inherits="BackEnd_QLLoaiHang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/css/GridviewQL.css") %>" />

<div id="container">
    <div class="profile-box">
        <h3>QUẢN LÝ LOẠI SẢN PHẨM</h3>

        <div class="row">
            <span class="title">Mã loại (1 ký tự)</span>
            <asp:TextBox ID="txtMaLoai" runat="server"
                CssClass="input" MaxLength="1" />
        </div>

        <div class="row">
            <span class="title">Tên loại</span>
            <asp:TextBox ID="txtTenLoai" runat="server"
                CssClass="input" />
        </div>

        <div class="action">
            <asp:Button ID="btnThem" runat="server"
                Text="Thêm"
                CssClass="btn"
                OnClick="btnThem_Click" />
        </div>

        <asp:Label ID="lblThongBao" runat="server"
            CssClass="msg" />
    </div>
    <div class="profile-box" style="width:600px">
        <h3>DANH SÁCH LOẠI</h3>

        <asp:GridView ID="gvLoaiHang" runat="server"
            CssClass="table"
            AutoGenerateColumns="False"
            DataKeyNames="MaLoai"
            OnRowEditing="gv_RowEditing"
            OnRowUpdating="gv_RowUpdating"
            OnRowCancelingEdit="gv_RowCancelingEdit"
            OnRowDeleting="gv_RowDeleting">

            <Columns>
                <asp:BoundField DataField="MaLoai" HeaderText="Mã" ReadOnly="true" />
                <asp:BoundField DataField="TenLoai" HeaderText="Tên loại" />
                <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
            </Columns>
        </asp:GridView>
    </div>

</div>

</asp:Content>

