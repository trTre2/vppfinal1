using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrontEnd_ProductsByType : BasePages
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadLoai(); 

            string type = Request.QueryString["type"];

            if (string.IsNullOrEmpty(type))
            {
                Response.Redirect("~/Index.aspx");
                return;
            }

            if (ddlLoai.Items.FindByValue(type) != null)
                ddlLoai.SelectedValue = type;

            LoadProducts(type);
        }
    }

    private void LoadProducts(string maLoai)
    {
        bytype.DataSource = GetProducts.GetProductsByType(maLoai);
        bytype.DataBind();
    }

    void LoadLoai()
    {
        DataTable dt = GetProducts.GetLoaiSP();

        ddlLoai.DataSource = dt;
        ddlLoai.DataTextField = "TenLoai";
        ddlLoai.DataValueField = "MaLoai";
        ddlLoai.DataBind();

        ddlLoai.Items.Insert(0, new ListItem("-- Chọn loại --", "0"));
    }

    protected void ddlLoai_SelectedIndexChanged(object sender, EventArgs e)
    {
        string loai = ddlLoai.SelectedValue;

        if (loai == "0")
            Response.Redirect("~/Index.aspx");
        else
            Response.Redirect("~/FrontEnd/ProductsByType.aspx?type=" + loai);
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
