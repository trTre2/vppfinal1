using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BackEnd_QLLoaiHang : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadLoaiHang();
    }

    void LoadLoaiHang()
    {
        gvLoaiHang.DataSource = ManageData.GetLoaiSP();
        gvLoaiHang.DataBind();
    }

    protected void btnThem_Click(object sender, EventArgs e)
    {
        string ma = txtMaLoai.Text.Trim().ToUpper();
        string ten = txtTenLoai.Text.Trim();

        if (ma == "" || ten == "")
        {
            lblThongBao.Text = "Không được để trống dữ liệu";
            return;
        }

        if (ma.Length != 1 || !char.IsLetter(ma[0]))
        {
            lblThongBao.Text = "Mã loại phải là 1 chữ cái";
            return;
        }

        if (ManageData.CheckMaLoaiTonTai(ma))
        {
            lblThongBao.Text = "Mã loại đã tồn tại";
            return;
        }

        ManageData.InsertLoaiSP(ma, ten);
        txtMaLoai.Text = "";
        txtTenLoai.Text = "";
        lblThongBao.Text = "";

        LoadLoaiHang();
    }

    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {

        gvLoaiHang.EditIndex = e.NewEditIndex;
        LoadLoaiHang();
    }

    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvLoaiHang.EditIndex = -1;
        LoadLoaiHang();
    }

    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string maLoai = gvLoaiHang.DataKeys[e.RowIndex].Value.ToString();
        GridViewRow row = gvLoaiHang.Rows[e.RowIndex];

        TextBox txtTen = (TextBox)row.Cells[1].Controls[0];

        ManageData.UpdateLoaiSP(maLoai, txtTen.Text.Trim());

        gvLoaiHang.EditIndex = -1;
        LoadLoaiHang();
    }

    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string maLoai = gvLoaiHang.DataKeys[e.RowIndex].Value.ToString();
        ManageData.DeleteLoaiSP(maLoai);
        LoadLoaiHang();
    }
}
