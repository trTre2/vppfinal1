<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Signup.aspx.cs" Inherits="Signup" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Đăng ký tài khoản</title>
    <link rel="stylesheet" type="text/css" href="../css/signup.css" />
</head>

<body>
    <form id="signupForm" runat="server">
        <div class="signup-container">

            <div class="logo-section">
                <asp:HyperLink runat="server" NavigateUrl="Index.aspx" CssClass="logo">
                <img src="images/logo.png" alt="VPP Online Logo" />
                </asp:HyperLink>
                <h1>Văn phòng phẩm VPP</h1>
            </div>

            <div class="form-group">
                <asp:Label AssociatedControlID="Email" runat="server" Text="Email" />
                <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                    runat="server" ControlToValidate="Email"
                    ForeColor="Red" ErrorMessage="Email không được rỗng" />
            </div>

            <div class="form-group">
                <asp:Label AssociatedControlID="Username" runat="server" Text="Tên đăng nhập" />
                <asp:TextBox ID="Username" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                    runat="server" ControlToValidate="Username"
                    ForeColor="Red" ErrorMessage="Tên đăng nhập không được rỗng" />
            </div>

            <div class="form-group">
                <asp:Label AssociatedControlID="Password" runat="server" Text="Mật khẩu" />
                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                    runat="server" ControlToValidate="Password"
                    ForeColor="Red" ErrorMessage="Mật khẩu không được rỗng" />
            </div>

            <div class="form-group">
                <asp:Label AssociatedControlID="rePassword" runat="server" Text="Nhập lại mật khẩu" />
                <asp:TextBox ID="rePassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                    runat="server" ControlToValidate="rePassword"
                    ForeColor="Red" ErrorMessage="Không được rỗng" />
                <br />
                <asp:CompareValidator ID="CompareValidator2"
                    runat="server" ControlToValidate="rePassword"
                    ControlToCompare="Password" ForeColor="Red"
                    ErrorMessage="Mật khẩu gõ lại không khớp" />
            </div>

            <div class="remember-forgot">
                <div class="remember-me">
                    <input type="checkbox" id="remember" />
                    <label for="remember">Ghi nhớ tôi</label>
                </div>
                <a href="#">Quên mật khẩu?</a>
            </div>

            <button type="submit" class="btn-signup">Đăng ký</button>
        </div>
    </form>
</body>
</html>
