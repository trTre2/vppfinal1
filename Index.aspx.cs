using System;
using System.Configuration;
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

        string query = "SELECT TOP 10 MaSP, TenSP, Gia, AnhSP FROM San_Pham";
        SqlDataAdapter da = new SqlDataAdapter(query, conn);
        DataTable dt = new DataTable();
        da.Fill(dt);

        featured_products.DataSource = dt;
        featured_products.DataBind();
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
}