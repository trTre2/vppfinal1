using FreeTextBoxControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static GetUser;

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
        GridViewRow row = gvUsers.Rows[e.NewEditIndex];

        DropDownList ddlRole = row.FindControl("ddlRoleEdit") as DropDownList;
        if (ddlRole == null) return;

        string role = ddlRole.SelectedValue;
    }


    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

        gvUsers.EditIndex = -1;
        LoadGrid();
        left.Visible = true;
    }

    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        int id = (int)gvUsers.DataKeys[e.RowIndex].Value;
        GridViewRow row = gvUsers.Rows[e.RowIndex];

        string role = ((DropDownList)row.FindControl("ddlRoleEdit")).SelectedValue;
        string pass = ((TextBox)row.FindControl("txtPasswordEdit")).Text.Trim();
        string ten = ((TextBox)row.FindControl("txtTenKHEdit")).Text.Trim();
        string email = ((TextBox)row.FindControl("txtEmailEdit")).Text.Trim();
        string phone = ((TextBox)row.FindControl("txtPhoneEdit")).Text.Trim();
        string address = ((TextBox)row.FindControl("txtAddressEdit")).Text.Trim();

        GetUser.UpdateUser(id, role, pass);

        UserInfo u = GetUser.GetUserByID(id);
        int? idKH = u?.ID_KH;

        if (role == "customer")
        {
            if (!idKH.HasValue)
                GetUser.CreateKhachHangForUser(id, ten, phone, address, email);
            else
                GetUser.UpdateKhachHang(idKH.Value, ten, phone, address, email);
        }
        else if (idKH.HasValue)
        {
            GetUser.RemoveKhachHangLink(id);
        }

        gvUsers.EditIndex = -1;
        LoadGrid();
        left.Visible = true;
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

            int result;

            if (role == "customer")
            {
                string ten = txtTenKH.Text.Trim();
                string email = txtEmail.Text.Trim();
                string phone = txtPhone.Text.Trim();
                string address = txtAddress.Text.Trim();
                result = GetUser.Signup(email, ten, phone, address, role, username, password);
            }
            else
            {
                result = GetUser.Signup(null, null, null, null, role, username, password);
            }


            if (result == -1)
            {
                lblMsg.Text = "Username đã tồn tại";
                return;
            }
            if (result == -2)
            {
                lblMsg.Text = "Email đã tồn tại";
                return;
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