<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Đăng nhập - Văn phòng phẩm ABC</title>
    <link rel="icon" href="../images/logo.png" type="image/png">
    <link href="../css/Login.css" rel="stylesheet" type="text/css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <div class="logo-section">
                <asp:HyperLink NavigateUrl="~/Index.aspx" class="logo" runat="server">
                    <img src="~/images/logo.png" alt="VPP Online Logo" runat="server" />
                </asp:HyperLink>
                <h1>Văn phòng phẩm VPP</h1>
            </div>
            <div class="form-group">
                <asp:Label ID="lblUsername" runat="server" Text="Tên đăng nhập" AssociatedControlID="txtUsername"></asp:Label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername" ErrorMessage="Tên đăng nhập bắt buộc" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group">
                <asp:Label ID="lblPassword" runat="server" Text="Mật khẩu" AssociatedControlID="txtPassword"></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Mật khẩu bắt buộc" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>

            <div class="remember-forgot">
                <div class="remember-me">
                    <asp:CheckBox ID="chkRemember" runat="server" Text=" Ghi nhớ tôi" />
                </div>
                <asp:HyperLink ID="lnkForgot" runat="server" NavigateUrl="~/Login/ForgotPassword.aspx">Quên mật khẩu?</asp:HyperLink>
            </div>

            <asp:Button ID="btnLogin" runat="server" Text="Đăng nhập" CssClass="btn-login" OnClick="btnLogin_Click" />

            <div class="signup-link">
                Chưa có tài khoản?
                <asp:HyperLink ID="lnkSignup" runat="server" NavigateUrl="~/Login/signup.aspx">Đăng ký ngay</asp:HyperLink>
            </div>
        </div>

    </form>
</body>
</html>
