using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Configuration;
using DataAccess;

public partial class Login : BasePages
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text.Trim();
        //string sql = "SELECT * FROM Users WHERE Username=@Username AND Password=@Password";
        SqlCommand cmd = new SqlCommand(
            "SELECT Role FROM Users WHERE Username=@u AND Password=@p", conn);

        cmd.Parameters.AddWithValue("@u", txtUsername.Text);
        cmd.Parameters.AddWithValue("@p", txtPassword.Text);

        conn.Open();
        var role = cmd.ExecuteScalar();
        conn.Close();

        if (role != null)
        {
            Session["User"] = txtUsername.Text;
            Session["Role"] = role.ToString();
        }
        if ((role as string) == "admin" || (role as string) == "limited")
        {
            Response.Redirect("Index.aspx");
        } 
        else if((role as string)=="customer")
            Response.Redirect("Index.aspx");
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Tên đăng nhập hoặc mật khẩu không đúng!');", true);
        }
    }
}
