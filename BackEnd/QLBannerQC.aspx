<%@ Page Title="" Language="C#" MasterPageFile="~/QuanTri.master" AutoEventWireup="true" CodeFile="QLBannerQC.aspx.cs" Inherits="BackEnd_QLBannerQC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/css/GridviewQL.css") %>" />

<div id="container">
    <div class="profile-box" id="left">
        <h3>THÊM MỚI QUẢNG CÁO</h3>

        <div class="row">
            <span class="title">Đối tác</span>
            <asp:TextBox ID="txtDoiTac" runat="server"></asp:TextBox>
        </div>

        <div class="row">
            <span class="title">Ngày hết hạn</span>
            <asp:TextBox ID="txtNgayHetHan" runat="server" TextMode="Date"></asp:TextBox>
        </div>

        <div class="row">
            <span class="title">Hiển thị</span>
            <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" />
        </div>

        <div class="row">
            <span class="title">Ảnh 16:9</span>
            <asp:FileUpload ID="fuAds" runat="server" />
        </div>

        <div class="action">
            <asp:Button ID="btnThem" runat="server" Text="Thêm" CssClass="btn" OnClick="btnThem_Click" />
        </div>

        <asp:Label ID="lblMsg" runat="server" CssClass="msg"></asp:Label>
    </div>

    <div class="profile-box">
        <h3>DANH SÁCH QUẢNG CÁO</h3>
        <asp:DropDownList ID="ddlTrangThaiFilter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTrangThaiFilter_SelectedIndexChanged">
            <asp:ListItem Text="Tất cả" Value="" />
            <asp:ListItem Text="Hiển thị" Value="1" />
            <asp:ListItem Text="Ẩn" Value="0" />
        </asp:DropDownList>

    <asp:GridView ID="gvAds" runat="server"
        AutoGenerateColumns="False"
        CssClass="table"
        DataKeyNames="id"
        OnRowEditing="gvAds_RowEditing"
        OnRowUpdating="gvAds_RowUpdating"
        OnRowCancelingEdit="gvAds_RowCancelingEdit"
        OnRowDeleting="gvAds_RowDeleting">

        <Columns>
            <asp:TemplateField HeaderText="Ảnh">
                <ItemTemplate>
                    <a href='<%# Eval("Link_Ads") %>' target="_blank">
                        <img src='<%# Eval("Link_Ads") %>' width="160" />
                    </a>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:FileUpload ID="fuAnhEdit" runat="server" />
                    <asp:HiddenField ID="hdAnhCu" Value='<%# Eval("Link_Ads") %>' runat="server" />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Đối tác">
                <ItemTemplate><%# Eval("TenDoiTac") %></ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDoiTacEdit" runat="server" Text='<%# Eval("TenDoiTac") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Ngày hết hạn">
                <ItemTemplate><%# Eval("NgayHetHan", "{0:yyyy-MM-dd}") %></ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtNgayHetHanEdit" runat="server" Text='<%# Eval("NgayHetHan", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Hiển thị">
                <ItemTemplate>
                    <%# (bool)Eval("IsActive") ? "Có" : "Không" %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="chkActiveEdit" runat="server" Checked='<%# Eval("IsActive") %>' />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>
    </div>    </div>

</asp:Content>

