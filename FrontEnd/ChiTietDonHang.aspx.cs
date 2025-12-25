using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrontEnd_ChiTietDonHang : System.Web.UI.Page
{
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            
                LoadOrderDetail();
            }
        }

        private void LoadOrderDetail()
        {
            if (!int.TryParse(Request.QueryString["MaDH"], out int maDH))
            {
            if (Session["Role"] == null) { Response.Redirect("~/Login/Login.aspx"); }
            if (Session["Role"].ToString() == "customer")
                return;
            //Response.Redirect("~/Index.aspx");
            else
                Response.Redirect("~/BackEnd/DonHang.aspx");
                DataTable dt = GetOrder.GetOrderDetail(maDH);
                gvOrderDetail.DataSource = dt;
            }
        }
       
        
    }