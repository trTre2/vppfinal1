using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BackEnd_QLDonHang : System.Web.UI.Page
{
        // ViewState lưu DataTable tạm
private DataTable DonHangTemp
    {
        get
        {
            if (ViewState["DonHangTemp"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("idSP", typeof(int));
                dt.Columns.Add("TenSP");
                dt.Columns.Add("Gia", typeof(decimal));
                dt.Columns.Add("SoLuong", typeof(int));
                ViewState["DonHangTemp"] = dt;
            }
            return (DataTable)ViewState["DonHangTemp"];
        }
        set { ViewState["DonHangTemp"] = value; }
    }

    // Load Khách hàng
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDonHang();
            LoadKhachHangFilter();
            ddlKhachHang.DataSource = ManageData.GetKhachHang();
            ddlKhachHang.DataTextField = "TenKH";
            ddlKhachHang.DataValueField = "id";
            ddlKhachHang.DataBind();
            gvDonHangTemp.DataSource = DonHangTemp;
            gvDonHangTemp.DataBind();
        }
    }
    protected void btnTimSP_Click(object sender, EventArgs e)
    {
        gvTimSP.DataSource = ManageData.GetSanPhamByName(txtTimSP.Text.Trim());
        gvTimSP.DataBind();
    }

    protected void btnThemSP_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int idSP = Convert.ToInt32(btn.CommandArgument);
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        TextBox txtSL = (TextBox)row.FindControl("txtSL");
        int sl = int.Parse(txtSL.Text);

        DataTable dt = DonHangTemp;

        DataRow[] found = dt.Select("idSP=" + idSP);
        if (found.Length > 0)
        {
            found[0]["SoLuong"] = (int)found[0]["SoLuong"] + sl; // cộng số lượng
        }
        else
        {
            dt.Rows.Add(idSP, HttpUtility.HtmlDecode(row.Cells[0].Text), decimal.Parse(row.Cells[1].Text), sl);
        }
        if (sl <= 0)
        {
            lblThongBao.Text = "Số lượng phải > 0";
            return;
        }
        DonHangTemp = dt;
        gvDonHangTemp.DataSource = dt;
        gvDonHangTemp.DataBind();
    }

    void LoadDonHang()
    {
        string idKH = ddlKhachHangFilter.SelectedValue;
        string trangThai = ddlTrangThaiFilter.SelectedValue;

        gvDonHang.DataSource = ManageData.GetDonHangFilter(idKH, trangThai);
        gvDonHang.DataBind();
    }

    protected void Filter_Changed(object sender, EventArgs e)
    {
        LoadDonHang();
    }
    void LoadKhachHangFilter()
    {
        DataTable dt = ManageData.GetKhachHang();
        ddlKhachHangFilter.DataSource = dt;
        ddlKhachHangFilter.DataTextField = "TenKH";
        ddlKhachHangFilter.DataValueField = "id";
        ddlKhachHangFilter.DataBind();
        ddlKhachHangFilter.Items.Insert(0, new ListItem("Tất cả khách hàng", ""));
    }

    protected void btnXoaSP_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int idSP = Convert.ToInt32(btn.CommandArgument);
        DataTable dt = DonHangTemp;
        DataRow r = dt.Select("idSP=" + idSP)[0];
        dt.Rows.Remove(r);
        DonHangTemp = dt;
        gvDonHangTemp.DataSource = dt;
        gvDonHangTemp.DataBind();
    }
    
    // Lưu đơn hàng
    protected void btnLuuDon_Click(object sender, EventArgs e)
    {
        if (DonHangTemp.Rows.Count == 0)
        {
            lblThongBao.Text = "Chưa chọn sản phẩm!";
            return;
        }

        int idKH = int.Parse(ddlKhachHang.SelectedValue);
        string tt = ddlTrangThai.SelectedValue;
        int maDH = ManageData.InsertDonHang(idKH, tt);

        foreach (DataRow r in DonHangTemp.Rows)
        {
            ManageData.InsertCTDH(maDH, (int)r["idSP"], (int)r["SoLuong"]);
        }

        // Sau khi thêm tất cả SP, update tổng tiền
        ManageData.UpdateTongTienDonHang(maDH);
        DonHangTemp.Clear();
        gvDonHangTemp.DataSource = DonHangTemp;
        gvDonHangTemp.DataBind();

        lblThongBao.Text = "Thêm đơn hàng thành công!";
        LoadDonHang();
    }
    protected void gvDonHang_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && gvDonHang.EditIndex != e.Row.RowIndex)
        {
            string maDH = gvDonHang.DataKeys[e.Row.RowIndex].Value.ToString();

            string url = ResolveUrl("~/BackEnd/ChiTietDonHang.aspx?MaDH=" + maDH);

            for (int i = 0; i < e.Row.Cells.Count - 1; i++)
            {
                e.Row.Cells[i].Attributes["onclick"] =
                    $"window.location='{url}'";
                e.Row.Cells[i].Style["cursor"] = "pointer";
            }
        }
    }


    protected void gvDonHang_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvDonHang.EditIndex = e.NewEditIndex;
        LoadDonHang();
        GridViewRow row = gvDonHang.Rows[e.NewEditIndex];
        string script = $"document.getElementById('{row.ClientID}').scrollIntoView({{behavior:'smooth', block:'center'}});";
        ScriptManager.RegisterStartupScript(this, GetType(), "scrollToRow", script, true);
    }

    protected void gvDonHang_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvDonHang.EditIndex = -1;
        LoadDonHang();
    }

    protected void gvDonHang_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int maDH = Convert.ToInt32(gvDonHang.DataKeys[e.RowIndex].Value);
        DropDownList ddlTrangThai = (DropDownList)gvDonHang.Rows[e.RowIndex].FindControl("ddlTrangThaiEdit");

        if (ddlTrangThai != null)
            ManageData.UpdateDonHangTrangThai(maDH, ddlTrangThai.SelectedValue);

        gvDonHang.EditIndex = -1;
        LoadDonHang();
    }
    protected void gvDonHang_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int maDH = Convert.ToInt32(gvDonHang.DataKeys[e.RowIndex].Value);

        ManageData.DeleteDonHang(maDH);
        LoadDonHang();
    }

}