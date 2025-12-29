using FreeTextBoxControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BackEnd_QLTaiKhoan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            LoadGrid();
        }
    }

    void LoadGrid()
    {
        string roleFilter = ddlRoleFilter.SelectedValue;
        gvUsers.DataSource = string.IsNullOrEmpty(roleFilter) ?
            GetUser.GetUsers() :
            GetUser.GetUsersByRole(roleFilter);
        gvUsers.DataBind();
    }

    // Sự kiện lọc khi chọn Role
    protected void ddlRoleFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvUsers.EditIndex = -1; // reset Edit nếu đang edit
        LoadGrid();
    }

    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        left.Visible = false;
        gvUsers.EditIndex = e.NewEditIndex;
        LoadGrid();
    }

    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        left.Visible = true;
        gvUsers.EditIndex = -1;
        LoadGrid();
    }

    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        left.Visible = true;
        int id = (int)gvUsers.DataKeys[e.RowIndex].Value;
        GridViewRow row = gvUsers.Rows[e.RowIndex];

        string role = ((DropDownList)row.FindControl("ddlRoleEdit")).SelectedValue;

        string ten = ((TextBox)row.FindControl("txtTenKHEdit"))?.Text.Trim();
        string email = ((TextBox)row.FindControl("txtEmailEdit"))?.Text.Trim();
        string phone = ((TextBox)row.FindControl("txtPhoneEdit"))?.Text.Trim();
        string address = ((TextBox)row.FindControl("txtAddressEdit"))?.Text.Trim();

        // Cập nhật Role
        GetUser.UpdateUserRole(id, role);

        // Nếu là customer thì update thông tin Khách hàng
        if (role == "customer")
        {
            int? idKH = GetUser.GetUserByID(id).ID_KH;
            if (idKH.HasValue)
            {
                GetUser.UpdateKhachHang(idKH.Value, ten, phone, address, email);
            }
        }

        gvUsers.EditIndex = -1;
        LoadGrid();
    }

    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = (int)gvUsers.DataKeys[e.RowIndex].Value;
        GetUser.DeleteUser(id);
        LoadGrid();
    }
    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlCustomer.Visible = ddlRole.SelectedValue == "customer";
    }

    protected void btnThem_Click(object sender, EventArgs e)
    {
        string role = ddlRole.SelectedValue;
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            lblMsg.Text = "Tên đăng nhập và mật khẩu không được rỗng";
            return;
        }

        if (role == "customer")
        {
            // Lấy thông tin khách hàng từ form
            string ten = txtTenKH.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();

            if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(email))
            {
                lblMsg.Text = "Họ tên và email không được rỗng";
                return;
            }
            int idKH = GetUser.Signup(email, ten, phone, address, username, password);
            GetUser.InsertUser(username, password, role, idKH);
        }
        else
        {
            GetUser.InsertUser(username, password, role, null);
        }

        lblMsg.Text = "Đã thêm tài khoản";
        LoadGrid();
        ClearForm();
    }
    void ClearForm()
    {
        txtUsername.Text = "";
        txtPassword.Text = "";
        txtTenKH.Text = "";
        txtEmail.Text = "";
        txtPhone.Text = "";
        txtAddress.Text = "";
    }
}