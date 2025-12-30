<%@ Page Title="Quản lý sản phẩm" Language="C#" MasterPageFile="~/QuanTri.master" AutoEventWireup="true" CodeFile="QLSanPham.aspx.cs" Inherits="BackEnd_QLSanPham" %>
<%@ Register Assembly="FreeTextBox"
    Namespace="FreeTextBoxControls"
    TagPrefix="FTB" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/css/GridviewQL.css") %>" />

<div id="container">
    <div class="profile-box" id="left" runat="server" style="width: 350px">
        <h3>THÊM SẢN PHẨM MỚI</h3>

        <div class="row">
            <span class="title">Loại sản phẩm</span>
            <asp:DropDownList ID="ddlLoai" runat="server" CssClass="input" />
        </div>

        <div class="row">
            <span class="title">Tên sản phẩm</span>
            <asp:TextBox ID="txtTen" runat="server" CssClass="input" />
        </div>

        <div class="row">
            <span class="title">Giá</span>
            <asp:TextBox ID="txtGia" runat="server" CssClass="input" />
        </div>

        <div class="row">
            <span class="title">Tình trạng</span>
            <asp:TextBox ID="txtTinhTrang" runat="server" CssClass="input" />
        </div>

        <div class="row">
            <span class="title">Ảnh sản phẩm</span>
            <asp:FileUpload ID="fuAnh" runat="server" />
        </div>

        <div class="row">
            <span class="title">Mô tả</span>
            <FTB:FreeTextBox ID="ftbMoTa" runat="server"
                Height="150px" Width="100%" />
        </div>

        <div class="action">
            <asp:Button ID="btnThem" runat="server"
                Text="Thêm"
                CssClass="btn"
                OnClick="btnThem_Click" />
        </div>

        <asp:Label ID="lblMsg" runat="server" />
    </div>
    <div class="profile-box" id="right" runat="server">
        <h3>DANH SÁCH SẢN PHẨM</h3>
        <asp:DropDownList ID="ddlLoaiFilter" runat="server"
    AutoPostBack="true"
    OnSelectedIndexChanged="ddlLoaiFilter_SelectedIndexChanged"
    CssClass="input" />
        <asp:GridView ID="gvSanPham" runat="server"
    CssClass="table"
    AutoGenerateColumns="False"
    DataKeyNames="id"
    OnRowEditing="gv_RowEditing"
    OnRowUpdating="gv_RowUpdating"
    OnRowCancelingEdit="gv_RowCancelingEdit"
    OnRowDeleting="gv_RowDeleting"
    OnRowDataBound="gv_RowDataBound1">

    <Columns>

        <asp:BoundField DataField="MaSP" HeaderText="Mã SP" ReadOnly="true" />
        <asp:BoundField DataField="TenSP" HeaderText="Tên SP" />
        <asp:BoundField DataField="Gia" HeaderText="Giá" />
        <asp:TemplateField HeaderText="Loại">
            <ItemTemplate>
                <%# Eval("MaLoai") %>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList ID="ddlLoaiEdit" runat="server" CssClass="input" />
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="TinhTrang" HeaderText="Tình trạng" />
        <asp:TemplateField HeaderText="Ảnh">
            <ItemTemplate>
                <asp:Image runat="server"
                    ImageUrl='<%# Eval("AnhSP") %>'
                    Width="70" />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:FileUpload ID="fuEditAnh" runat="server" />
                <asp:HiddenField ID="hdAnhCu"
                    Value='<%# Eval("AnhSP") %>' runat="server" />
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
    </Columns>
</asp:GridView>
    </div>

</div>
</asp:Content>