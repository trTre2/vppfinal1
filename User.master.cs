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
        if (Session["User"] != null)
        {
            string user = Session["User"].ToString();
            SqlDataAdapter da = new SqlDataAdapter("SELECT a.ID_KH, b.TenKH FROM Users a JOIN KhachHang b ON a.ID_KH = b.id WHERE Username = @username", conn);
            da.SelectCommand.Parameters.AddWithValue("@username",user);
            DataTable dt = new DataTable();
            da.Fill(dt);
            string uid = dt.Rows[0]["ID_KH"].ToString();
        userID = Convert.ToInt32(uid);
        }
        if (!IsPostBack)
        {
            LoadCart();
            UpdateAuthLinks();
        }
    }

    
    // Load giỏ hàng (đổ ra Repeater rpCart)
    void LoadCart()
    {
        string sql = @"
            SELECT 
                c.SoLuong, 
                s.TenSP, 
                s.Gia, 
                s.AnhSP,
                s.id AS idSP
            FROM Cart c 
            JOIN San_Pham s ON c.idSP = s.id
            WHERE c.idKH = @uid";

        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
        da.SelectCommand.Parameters.AddWithValue("@uid", userID);

        DataTable dt = new DataTable();
        da.Fill(dt);

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
        if (Session["User"] != null)
        {
            lblUser.Text = "Xin chào, " + Session["User"];
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
        Response.Redirect("Index.aspx");
    }
}
