using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class BackEnd_QLSanPham : System.Web.UI.Page
{
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadLoai();
            LoadGrid();
        }
    }

    void LoadLoai()
    {
        ddlLoai.DataSource = ManageData.GetLoaiSP();
        ddlLoai.DataTextField = "TenLoai";
        ddlLoai.DataValueField = "MaLoai";
        ddlLoai.DataBind();
    }

    void LoadGrid()
    {
        gvSanPham.DataSource = ManageData.GetSanPham();
        gvSanPham.DataBind();
    }

    protected void btnThem_Click(object sender, EventArgs e)
    {
        decimal gia;
        if (txtTen.Text == "" || !decimal.TryParse(txtGia.Text, out gia))
        {
            lblMsg.Text = "Dữ liệu không hợp lệ";
            return;
        }

        string imgPath = "";
        if (fuAnh.HasFile)
        {
            string fileName = Path.GetFileName(fuAnh.FileName);
            imgPath = "~/images/products/" + fileName;
            fuAnh.SaveAs(Server.MapPath(imgPath));
        }

        ManageData.InsertSanPham(
            ddlLoai.SelectedValue,
            txtTen.Text,
            gia,
            ftbMoTa.Text,
            txtTinhTrang.Text,
            imgPath
        );

        LoadGrid();
        lblMsg.Text = "Đã thêm sản phẩm";
        txtTen.Text = "";
        txtGia.Text = "";
        ftbMoTa.Text = "";
        txtTinhTrang.Text = "";
        ddlLoai.SelectedIndex = 0;
        fuAnh.Dispose(); 
    }

    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        left.Visible = false;
        gvSanPham.EditIndex = e.NewEditIndex;
        LoadGrid();

        GridViewRow row = gvSanPham.Rows[e.NewEditIndex];
        string script = $"document.getElementById('{row.ClientID}').scrollIntoView({{behavior:'smooth', block:'center'}});";
        ScriptManager.RegisterStartupScript(this, GetType(), "scrollToRow", script, true);

    }

    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvSanPham.EditIndex = -1;
        LoadGrid();
        left.Visible = true;
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int id = (int)gvSanPham.DataKeys[e.RowIndex].Value;
        GridViewRow r = gvSanPham.Rows[e.RowIndex];

        string ten = ((TextBox)r.Cells[1].Controls[0]).Text;
        decimal gia = decimal.Parse(((TextBox)r.Cells[2].Controls[0]).Text);

        DropDownList ddlLoai =
            (DropDownList)r.FindControl("ddlLoaiEdit");
        string maloai = ddlLoai.SelectedValue;

        string tinhtrang = ((TextBox)r.Cells[4].Controls[0]).Text;

        FileUpload fu = (FileUpload)r.FindControl("fuEditAnh");
        HiddenField hd = (HiddenField)r.FindControl("hdAnhCu");

        string anh = hd.Value;
        if (fu.HasFile)
        {
            string path = "~/images/products/" + fu.FileName;
            fu.SaveAs(Server.MapPath(path));
            anh = path;
        }

        ManageData.UpdateSanPham(
            id, ten, gia, maloai,
            "",        // mô tả sửa ở form riêng
            tinhtrang,
            anh
        );
        left.Visible = true;
        gvSanPham.EditIndex = -1;
        LoadGrid();
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = (int)gvSanPham.DataKeys[e.RowIndex].Value;
        ManageData.DeleteSanPham(id);
        LoadGrid();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow &&
            e.Row.RowState.HasFlag(DataControlRowState.Edit))
        {
            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlLoaiEdit");
            ddl.DataSource = ManageData.GetLoaiSP();
            ddl.DataTextField = "TenLoai";
            ddl.DataValueField = "MaLoai";
            ddl.DataBind();

            string maloai = DataBinder.Eval(e.Row.DataItem, "MaLoai").ToString();
            ddl.SelectedValue = maloai;
        }
    }
}
