using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrontEnd_ProductsByType : System.Web.UI.Page
{
    char temp;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string type = Request.QueryString["type"];

            if (string.IsNullOrEmpty(type))
            {
                Response.Redirect("../Index.aspx");
                return;
            }
            //if (type == "G")
            //    temp = 'G';
            //else if (type == "B")
            //    temp = 'B';
            //else
            //    temp = 'K';
            LoadProducts(type);
        }
    }

    private void LoadProducts(string maLoai)
    {
        rpProducts.DataSource = GetProducts.GetProductsByType(maLoai);
        rpProducts.DataBind();
    }
}