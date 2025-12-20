using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Signup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
        protected void btnSignup_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;

        int result = GetUser.Signup(
            Email.Text.Trim(),
            TenKH.Text.Trim(),
            Phone.Text.Trim(),
            Address.Text.Trim(),
            Username.Text.Trim(),
            Password.Text.Trim()
        );

        if (result == -1)
            ClientScript.RegisterStartupScript(GetType(), "a", "alert('Tên đăng nhập đã tồn tại');", true);
        else if (result == -2)
            ClientScript.RegisterStartupScript(GetType(), "a", "alert('Email đã tồn tại');", true);
        else
            Response.Redirect("Login.aspx");
    }
}