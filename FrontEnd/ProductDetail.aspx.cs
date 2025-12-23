using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class FrontEnd_DetailProduct : BasePages
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null)
        {
            Response.Redirect("~/Index.aspx");
            return;
        }

        if (!int.TryParse(Request.QueryString["id"], out int id))
        {
            Response.Redirect("~/Index.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LoadProduct(id);
        }
    }

    private void LoadProduct(int id)
    {
        DataRow row = GetProducts.GetDetail(id);

        if (row == null)
        {
            Response.Redirect("~/Index.aspx");
            return;
        }
        DataRow dr = GetProducts.GetAds();
        ads.ImageUrl = $"~/images/ads/{dr["Link_ads"].ToString()}";
        imgProduct.ImageUrl = row["AnhSP"].ToString();
        lblProductTitle.Text = row["TenSP"].ToString();
        lblProductCode.Text = "Mã sản phẩm: " + row["MaSP"];
        lblProductPrice.Text = string.Format("{0:N0} đ", row["Gia"]);
        lblDescription.Text = row["MoTa"].ToString();
        char maLoai = Convert.ToChar(row["MaLoai"]);
        DataTable dt = GetProducts.GetTopByType(maLoai, 5, id);
        dlSameType.DataSource = dt;
        dlSameType.DataBind();
    }
    protected void AddMainProductToCart(object sender, CommandEventArgs e)
    {
        if (Session["ID_KH"] == null)
        {
            Response.Redirect("~/Login/Login.aspx");
            return;
        }

        if (!int.TryParse(Request.QueryString["id"], out int idSP))
            return;
        int.TryParse(txtQuantity.Text, out int sl);
        int userID = Convert.ToInt32(Session["ID_KH"]);
        AddToCart(userID, idSP, sl);
    }

    protected void AddToCartById(object sender, CommandEventArgs e)
    {
        if (Session["ID_KH"] == null)
        {
            Response.Redirect("~/Login/Login.aspx");
            return;
        }

        if (!int.TryParse(e.CommandArgument?.ToString(), out int idSP))
            return;

        int userID = Convert.ToInt32(Session["ID_KH"]);
        AddToCart(userID, idSP, 1);
    }
    private void AddToCart(int userID, int idSP, int quantity)
    {
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
                    "INSERT INTO Cart(idKH,idSP,SoLuong) VALUES(@uid,@idSP,@sl)", conn);
                insert.Parameters.AddWithValue("@uid", userID);
                insert.Parameters.AddWithValue("@idSP", idSP);
                insert.Parameters.AddWithValue("@sl", quantity);
                insert.ExecuteNonQuery();
            }
            else
            {
                SqlCommand update = new SqlCommand(
                    "UPDATE Cart SET SoLuong = SoLuong + @sl, NgayThem = GETDATE() WHERE idKH=@uid AND idSP=@idSP", conn);
                update.Parameters.AddWithValue("@uid", userID);
                update.Parameters.AddWithValue("@idSP", idSP);
                update.Parameters.AddWithValue("@sl", quantity);
                update.ExecuteNonQuery();
            }
        }
    }


}