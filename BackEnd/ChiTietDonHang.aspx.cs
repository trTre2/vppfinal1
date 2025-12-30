using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BackEnd_ChiTietDonHang : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Role"] == null)
                Response.Redirect("~/Index.aspx");
            if (Request.QueryString["MaDH"] != null && Session["Role"] != null)
                LoadOrderDetail();
            else
            {
                if (Session["Role"].ToString() == "admin")
                    Response.Redirect("~/BackEnd/DonHang.aspx");
                else if (Session["Role"] == null) { Response.Redirect("~/Login/Login.aspx"); }
                else if (Session["Role"].ToString() == "customer") Response.Redirect("~/FrontEnd/ChiTietDonHang.aspx");
            }
        }
    }

    private void LoadOrderDetail()
    {
        if (int.TryParse(Request.QueryString["MaDH"], out int MaDH))
        {
            DataTable dt = GetOrder.GetOrderDetail(MaDH);
            gvOrderDetail.DataSource = dt;
            gvOrderDetail.DataBind();
            DataRow r = dt.Rows[0];
            decimal tongTien = r["TongTien"] == DBNull.Value ? 0 : Convert.ToDecimal(r["TongTien"]);
            lblTotal.Text = string.Format("{0:N0} VNĐ", tongTien);
        }


    }
}