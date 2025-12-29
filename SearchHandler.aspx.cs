using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Script.Serialization;

public partial class SearchHandler : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string key = Request["key"];
        if (string.IsNullOrEmpty(key)) return;

        var list = new List<object>();

        using (SqlConnection conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["vpp"].ConnectionString))
        {
            string sql = @"
                SELECT TOP 10 id, TenSP, Gia
                FROM San_Pham
                WHERE TenSP LIKE @k";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@k", "%" + key + "%");

            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                list.Add(new
                {
                    Id = rd["id"],
                    TenSP = rd["TenSP"].ToString(),
                    Gia = string.Format("{0:N0} đ", rd["Gia"])
                });
            }
        }

        Response.ContentType = "application/json";
        Response.Write(new JavaScriptSerializer().Serialize(list));
        Response.End();
    }
}
