using System;
using System.Activities.Statements;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class User : System.Web.UI.MasterPage
{
    int userID;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["vpp"].ConnectionString);


protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ID_KH"] != null)
        {
            userID = Convert.ToInt32(Session["ID_KH"]);
        }
            LoadCart();
            UpdateAuthLinks();
        }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        LoadCart();
    }


    // Load giỏ hàng (đổ ra Repeater rpCart)
    void LoadCart()
    {
        DataTable dt = GetProducts.GetCartById(userID);
        rpCart.DataSource = dt;
        rpCart.DataBind();

        decimal total = 0;
        foreach (DataRow r in dt.Rows)
        {
            total += Convert.ToDecimal(r["Gia"]) * Convert.ToInt32(r["SoLuong"]);
        }

        lblTotal.Text = total.ToString("#,##0");
    }

   
    private void UpdateAuthLinks()
    {
        if (Session["TenKH"] != null )
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
        Response.Redirect("~/Index.aspx");
    }
    protected void Category_Command(object sender, CommandEventArgs e)
    {
        Response.Redirect("~/FrontEnd/ProductsByType.aspx?type=" + e.CommandArgument);
    }
}
