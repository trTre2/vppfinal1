using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Index : BasePages
{
    int userID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            string uid = Session["UserID"].ToString();
            userID = Convert.ToInt32(uid);
        }
        if (!IsPostBack)
        {
            LoadProducts();
        }
    }
    private void LoadProducts()
    {
        DataTable dt = new DataTable();
        dt = GetProducts.GetTopProduct(8);
        featured_products.DataSource = dt;
        featured_products.DataBind();
        DataTable dt1 = new DataTable();
        dt1 = GetProducts.GetTopByType('G',5);
        paper.DataSource = dt1;
        paper.DataBind();
        DataTable dt2 = new DataTable();
        dt2 = GetProducts.GetTopByType('B',5);
        pen.DataSource = dt2;
        pen.DataBind();
        DataTable dt3 = new DataTable();
        dt3 = GetProducts.GetTopByType('K',5);
        other.DataSource = dt3;
        other.DataBind();
    }
    protected void rpSanPham_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "AddToCartById")
        {
            int idSP = Convert.ToInt32(e.CommandArgument);
            conn.Open();
            // Kiểm tra có tồn tại chưa
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

            conn.Close();

        }
    }
    protected void Category_Command(object sender, CommandEventArgs e)
    {
        Response.Redirect("FrontEnd/ProductsByType.aspx?type=" + e.CommandArgument);
    }

}