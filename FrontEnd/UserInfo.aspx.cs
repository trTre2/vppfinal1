using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserInfo : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ID_KH"] == null)
        {
            Response.Redirect("~/Login/Login.aspx");
            return;
        }

        if (!IsPostBack)
            LoadUserInfo();
        LoadOrders();
    }

    void LoadUserInfo()
    {
        int userId = Convert.ToInt32(Session["ID_KH"]);
        DataTable dt = GetUser.GetProfileByAccountID(userId);
        if (dt.Rows.Count == 0) return;

        DataRow r = dt.Rows[0];

        lblTenKH.Text = r["TenKH"].ToString();
        lblPhone.Text = r["PhoneNumber"].ToString();
        lblDiaChi.Text = r["DiaChi"].ToString();
        lblEmail.Text = r["Email"].ToString();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ToggleEdit(true);

        txtTenKH.Text = lblTenKH.Text;
        txtPhone.Text = lblPhone.Text;
        txtDiaChi.Text = lblDiaChi.Text;
        txtEmail.Text = lblEmail.Text;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        GetUser.UpdateKhachHang(
            Convert.ToInt32(Session["ID_KH"]),
            txtTenKH.Text,
            txtPhone.Text,
            txtDiaChi.Text,
            txtEmail.Text
        );

        ToggleEdit(false);
        LoadUserInfo();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ToggleEdit(false);
    }
    void ToggleEdit(bool edit)
    {
        lblTenKH.Visible = !edit;
        lblPhone.Visible = !edit;
        lblDiaChi.Visible = !edit;
        lblEmail.Visible = !edit;

        txtTenKH.Visible = edit;
        txtPhone.Visible = edit;
        txtDiaChi.Visible = edit;
        txtEmail.Visible = edit;

        btnEdit.Visible = !edit;
        btnSave.Visible = edit;
        btnCancel.Visible = edit;
    }
    void LoadOrders()
    {
        int userId = Convert.ToInt32(Session["ID_KH"]);

        DataTable dt = GetUser.GetOrdersByUser(userId);

        if (dt.Rows.Count == 0)
        {
            lblNoOrder.Visible = true;
            rpOrders.Visible = false;
            return;
        }

        rpOrders.DataSource = dt;
        rpOrders.DataBind();
    }

}