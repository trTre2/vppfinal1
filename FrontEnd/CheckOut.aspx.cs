using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static GetOrder;

public partial class FrontEnd_CheckOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
            LoadCart();
    }

    private void LoadCart()
    {
        int userId = Convert.ToInt32(Session["ID_KH"]);
        if (userId==0)
        {
            Response.Redirect("~/Index.aspx");
            return;
        }
        DataTable dt = GetProducts.GetCartById(userId);

        if (dt.Rows.Count == 0)
        {
            gvCart.Visible = false;
            lblTotal.Text = "Giỏ hàng của bạn trống.";
            return;
        }

        gvCart.DataSource = dt;
        gvCart.DataBind();

        decimal total = 0;
        foreach (DataRow row in dt.Rows)
        {
            total += Convert.ToInt32(row["SoLuong"]) *
                     Convert.ToDecimal(row["Gia"]);
        }

        lblTotal.Text = "Tổng cộng: "+total.ToString("N0") + " VND";
    }
    protected void btnCheckout_Click(object sender, EventArgs e)
    {
        int idKH = Convert.ToInt32(Session["ID_KH"]);

        try
        {
            GetOrder.CreateOrderFromCart(idKH);
        
            ClientScript.RegisterStartupScript(
                this.GetType(),
                "ok",
                "alert('Đặt hàng thành công!'); window.location='ChiTietDonHang.aspx';",
                true
            );
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(
                this.GetType(),
                "err",
                $"alert('Lỗi đặt hàng: {ex.Message}');",
                true
            );
        }
    }
}