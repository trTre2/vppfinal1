using System;
public partial class Login : BasePages
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        var user = GetUser.Login(txtUsername.Text, txtPassword.Text);

        if (user != null)
        {
            Session["TenKH"] = user["TenKH"];
            Session["Role"] = user["Role"];
            string role = user["Role"].ToString();
            if (role == "admin" || role == "limited")
            {
                Response.Redirect("IndexQL.aspx");
            }
            Session["ID_KH"] = user["ID_KH"];
            Response.Redirect("Index.aspx");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Tên đăng nhập hoặc mật khẩu không đúng!');", true);
        }
    }
}