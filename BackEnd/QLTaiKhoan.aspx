<%@ Page Title="Quản lý tài khoản" Language="C#" MasterPageFile="~/QuanTri.master" AutoEventWireup="true" CodeFile="QLTaiKhoan.aspx.cs" Inherits="BackEnd_QLTaiKhoan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/css/GridviewQL.css") %>" />
    <div id="container">

        <div class="profile-box" id="left" style="min-width: 300px" runat="server">
            <h3>THÊM MỚI TÀI KHOẢN</h3>

            <div class="row">
                <span class="title">Role</span>
                <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                    <asp:ListItem Text="Customer" Value="customer"></asp:ListItem>
                    <asp:ListItem Text="Admin" Value="admin"></asp:ListItem>
                    <asp:ListItem Text="Limited" Value="limited"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <asp:Panel ID="pnlCustomer" runat="server">
                <div class="row">
                    <span class="title">Họ và tên</span>
                    <asp:TextBox ID="txtTenKH" runat="server"></asp:TextBox>
                </div>
                <div class="row">
                    <span class="title">Email</span>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </div>
                <div class="row">
                    <span class="title">Số điện thoại</span>
                    <asp:TextBox ID="txtPhone" textMode="Number" runat="server"></asp:TextBox>
                </div>
                <div class="row">
                    <span class="title">Địa chỉ</span>
                    <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                </div>
            </asp:Panel>

            <div class="row">
                <span class="title">Tên đăng nhập</span>
                <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
            </div>

            <div class="row">
                <span class="title">Mật khẩu</span>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            </div>

            <div class="action">
                <asp:Button ID="btnThem" runat="server" Text="Thêm" CssClass="btn" OnClick="btnThem_Click" />
            </div>

            <asp:Label ID="lblMsg" runat="server" CssClass="msg"></asp:Label>
        </div>

        <div class="profile-box" id="right">
            <h3>DANH SÁCH TÀI KHOẢN</h3>
            <asp:DropDownList ID="ddlRoleFilter" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlRoleFilter_SelectedIndexChanged" CssClass="input">
                <asp:ListItem Text="Tất cả" Value="" />
                <asp:ListItem Text="Admin" Value="admin" />
                <asp:ListItem Text="Limited" Value="limited" />
                <asp:ListItem Text="Customer" Value="customer" />
            </asp:DropDownList>
            <asp:GridView ID="gvUsers" runat="server"
    AutoGenerateColumns="False"
    DataKeyNames="ID,Role"
    OnRowEditing="gv_RowEditing"
    OnRowUpdating="gv_RowUpdating"
    OnRowCancelingEdit="gv_RowCancelingEdit"
    OnRowDeleting="gv_RowDeleting"
    CssClass="table">

                <Columns>
                    <asp:BoundField DataField="Username" HeaderText="Username" ReadOnly="true" />

                    <asp:TemplateField HeaderText="Role">
                        <ItemTemplate><%# Eval("Role") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlRoleEdit" runat="server">
                                <asp:ListItem Value="admin">admin</asp:ListItem>
                                <asp:ListItem Value="customer">customer</asp:ListItem>
                                <asp:ListItem Value="limited">limited</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Mật khẩu">
                        <ItemTemplate><%# Eval("Password") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPasswordEdit" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Họ tên">
                        <ItemTemplate><%# Eval("TenKH") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTenKHEdit" runat="server" Text='<%# Bind("TenKH") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate><%# Eval("Email") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEmailEdit" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Phone">
                        <ItemTemplate><%# Eval("PhoneNumber") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPhoneEdit" runat="server" Text='<%# Bind("PhoneNumber") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Địa chỉ">
                        <ItemTemplate><%# Eval("DiaChi") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAddressEdit" runat="server" Text='<%# Bind("DiaChi") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

