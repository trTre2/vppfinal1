using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

public partial class BackEnd_QLBannerQC : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["vpp"].ConnectionString);
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fuAds.HasFile)
        {
            try
            {
                using (var img = System.Drawing.Image.FromStream(fuAds.PostedFile.InputStream))
                {
                    if (img.Width <= img.Height)
                    {
                        lblMsg.Text = "Ảnh phải ngang (Width > Height)";
                        return;
                    }
                }

                // Lưu file lên server
                string fileName = Path.GetFileName(fuAds.FileName);
                string savePath = Server.MapPath("~/uploads/ads/" + fileName);
                fuAds.SaveAs(savePath);

                // Nếu là thêm mới
                if (ViewState["EditID"] == null)
                {
                    string sql = "INSERT INTO Ads(Link_Ads, TenDoiTac, NgayHetHan) VALUES(@link,@dt,@ngay)";
                    using (con)
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@link", "/uploads/ads/" + fileName);
                        cmd.Parameters.AddWithValue("@dt", txtDoiTac.Text);
                        cmd.Parameters.AddWithValue("@ngay", txtNgayHetHan.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    // Update record
                    int id = (int)ViewState["EditID"];
                    string sql = "UPDATE Ads SET Link_Ads=@link, TenDoiTac=@dt, NgayHetHan=@ngay WHERE id=@id";
                    using (con)
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@link", "/uploads/ads/" + fileName);
                        cmd.Parameters.AddWithValue("@dt", txtDoiTac.Text);
                        cmd.Parameters.AddWithValue("@ngay", txtNgayHetHan.Text);
                        cmd.Parameters.AddWithValue("@id", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    ViewState["EditID"] = null;
                }

                lblMsg.Text = "Upload thành công!";
                LoadAds(); // reload GridView
            }
            catch
            {
                lblMsg.Text = "Ảnh không hợp lệ.";
            }
        }
        else
        {
            lblMsg.Text = "Chưa chọn ảnh!";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadAds();
        }
    }

    private void LoadAds()
    {
        gvAds.DataSource = ManageData.GetAllAds();
        gvAds.DataBind();
    }

    // Thêm quảng cáo
    protected void btnThem_Click(object sender, EventArgs e)
    {
        if (fuAds.HasFile)
        {
            string fileName = "../images/ads/" + fuAds.FileName;
            fuAds.SaveAs(Server.MapPath(fileName));

            string doiTac = txtDoiTac.Text.Trim();
            DateTime? ngayHetHan = null;
            if (DateTime.TryParse(txtNgayHetHan.Text, out DateTime dt))
            {
                ngayHetHan = dt;
            }
            bool isActive = chkIsActive.Checked;

            ManageData.InsertAds(fileName, doiTac, ngayHetHan, isActive);
            LoadAds();
            lblMsg.Text = "Thêm quảng cáo thành công!";
        }
        else
        {
            lblMsg.Text = "Chưa chọn ảnh!";
        }
    }

    protected void ddlTrangThaiFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadAdsFilter();
    }

    private void LoadAdsFilter()
    {
        string filter = ddlTrangThaiFilter.SelectedValue; // "" | "1" | "0"
        DataTable dt = ManageData.GetAllAds();

        if (!string.IsNullOrEmpty(filter))
        {
            bool isActive = filter == "1";
            DataView dv = dt.DefaultView;
            dv.RowFilter = "IsActive=" + (isActive ? "true" : "false");
            gvAds.DataSource = dv;
        }
        else
        {
            gvAds.DataSource = dt;
        }
        gvAds.DataBind();
    }

    protected void gvAds_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvAds.EditIndex = e.NewEditIndex;
        LoadAds();
    }

    // Hủy edit
    protected void gvAds_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvAds.EditIndex = -1;
        LoadAds();
    }

    // Cập nhật quảng cáo
    protected void gvAds_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gvAds.Rows[e.RowIndex];
        int id = Convert.ToInt32(gvAds.DataKeys[e.RowIndex].Value);

        TextBox txtDoiTacEdit = (TextBox)row.FindControl("txtDoiTacEdit");
        TextBox txtNgayEdit = (TextBox)row.FindControl("txtNgayHetHanEdit");
        CheckBox chkActiveEdit = (CheckBox)row.FindControl("chkActiveEdit");
        FileUpload fuEdit = (FileUpload)row.FindControl("fuAnhEdit");
        HiddenField hdAnhCu = (HiddenField)row.FindControl("hdAnhCu");

        string doiTac = txtDoiTacEdit.Text.Trim();
        DateTime? ngayHetHan = null;
        if (DateTime.TryParse(txtNgayHetHan.Text, out DateTime dt))
        {
            ngayHetHan = dt;
        }
        bool isActive = chkActiveEdit.Checked;

        string linkAnh = fuEdit.HasFile ? "../images/ads/" + fuEdit.FileName : hdAnhCu.Value;
        if (fuEdit.HasFile)
            fuEdit.SaveAs(Server.MapPath(linkAnh));

        ManageData.UpdateAds(id, linkAnh, doiTac, ngayHetHan, isActive);

        gvAds.EditIndex = -1;
        LoadAds();
        lblMsg.Text = "Cập nhật quảng cáo thành công!";
    }

    // Xóa quảng cáo
    protected void gvAds_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gvAds.DataKeys[e.RowIndex].Value);
        ManageData.DeleteAds(id);
        LoadAds();
        lblMsg.Text = "Xóa quảng cáo thành công!";
    }

}