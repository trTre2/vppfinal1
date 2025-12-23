using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Index : BasePages
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadProducts();
        }
    }
    private void LoadProducts()
    {
        DataRow dr = GetProducts.GetAds();
        ads.ImageUrl = $"~/images/ads/{dr["Link_ads"].ToString()}";
        DataTable dt = new DataTable();
        dt = GetProducts.GetTopProduct(10);
        featured_products.DataSource = dt;
        featured_products.DataBind();
        DataTable dt1 = new DataTable();
        dt1 = GetProducts.GetTopByType('G', 5,0);
        paper.DataSource = dt1;
        paper.DataBind();
        DataTable dt2 = new DataTable();
        dt2 = GetProducts.GetTopByType('B', 5,0);
        pen.DataSource = dt2;
        pen.DataBind();
        DataTable dt3 = new DataTable();
        dt3 = GetProducts.GetTopByType('K', 5, 0);
        other.DataSource = dt3;
        other.DataBind();
    }
    protected void Detail_Command(object sender, CommandEventArgs e)
    {
        Response.Redirect("~/FrontEnd/ProductDetail.aspx?id=" + e.CommandArgument);
    } 
    protected void Category_Command(object sender, CommandEventArgs e)
    {
        Response.Redirect("~/FrontEnd/ProductsByType.aspx?type=" + e.CommandArgument);
    }
    protected void AddToCartById(object sender, CommandEventArgs e)
    {
        if (Session["ID_KH"] == null)
        {
            Response.Redirect("~/Login/Login.aspx");
            return;
        }

        int userID = Convert.ToInt32(Session["ID_KH"]);
        int idSP = Convert.ToInt32(e.CommandArgument);

        using (SqlConnection conn = new SqlConnection(vpp))
        {
            conn.Open();

            SqlCommand check = new SqlCommand(
                "SELECT SoLuong FROM Cart WHERE idKH=@uid AND idSP=@idSP", conn);
            check.Parameters.AddWithValue("@uid", userID);
            check.Parameters.AddWithValue("@idSP", idSP);

            object result = check.ExecuteScalar();

            if (result == null)
            {
                SqlCommand insert = new SqlCommand(
                    "INSERT INTO Cart(idKH,idSP,SoLuong) VALUES(@uid,@idSP,1)", conn);
                insert.Parameters.AddWithValue("@uid", userID);
                insert.Parameters.AddWithValue("@idSP", idSP);
                insert.ExecuteNonQuery();
            }
            else
            {
                SqlCommand update = new SqlCommand(
                    "UPDATE Cart SET SoLuong = SoLuong + 1 WHERE idKH=@uid AND idSP=@idSP", conn);
                update.Parameters.AddWithValue("@uid", userID);
                update.Parameters.AddWithValue("@idSP", idSP);
                update.ExecuteNonQuery();
            }
        }
    }

}