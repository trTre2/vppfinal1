using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class QuanTri : System.Web.UI.MasterPage
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["vpp"].ConnectionString);


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] != null)
        {
            UpdateAuthLinks();
        }
        else
                    {
            Response.Redirect("~/Index.aspx");
        }
    }


    private void UpdateAuthLinks()
    {
        if (Session["TenKH"] != null)
        {
            lblUser.Text = "Xin chào, " + Session["TenKH"];
            lblUser.Visible = true;

            btnLogout.Visible = true;

            lnkLogin.Visible = false;
            lnkRegister.Visible = false;
            sep.Visible = false;
        }
        else
        {
            lblUser.Visible = false;
            btnLogout.Visible = false;

            lnkLogin.Visible = true;
            lnkRegister.Visible = true;
            sep.Visible = true;
        }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("~/Login/Login.aspx");
    }
}
