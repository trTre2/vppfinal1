using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.IO;

public partial class additem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string jsText = File.ReadAllText(@"C:\Users\nguyn\source\repos\vppfinal1\products.js");

        // 1. Loại bỏ var products =
        jsText = jsText.Replace("var products =", "").Trim();

        // 2. Loại bỏ dấu ; ở cuối
        if (jsText.EndsWith(";"))
            jsText = jsText.Substring(0, jsText.Length - 1);

        // 3. Deserialize JSON
        JavaScriptSerializer jss = new JavaScriptSerializer();
        dynamic data = jss.Deserialize<dynamic>(jsText);

        // 4. Duyệt dữ liệu và insert vào SQL
        string conn = ConfigurationManager.ConnectionStrings["vpp"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conn))
        {
            

            con.Open();
            //SqlCommand deleteCmd = new SqlCommand("DELETE FROM San_Pham", con);
            //deleteCmd.ExecuteNonQuery();
            foreach (var p in data)
            {
                string loai = ConvertTypeToMaLoai(p["type"]);

                SqlCommand cmd = new SqlCommand(@"
            INSERT INTO San_Pham (MaLoai, TenSP, Gia, MoTa, TinhTrang, AnhSP)
            VALUES (@MaLoai, @TenSP, @Gia, @MoTa, @TinhTrang, @AnhSP)
        ", con);

                cmd.Parameters.AddWithValue("@MaLoai", loai);
                cmd.Parameters.AddWithValue("@TenSP", p["name"]);
                cmd.Parameters.AddWithValue("@Gia", p["price"]);
                cmd.Parameters.AddWithValue("@MoTa", p["description"]);
                cmd.Parameters.AddWithValue("@TinhTrang", "Còn hàng");
                cmd.Parameters.AddWithValue("@AnhSP", p["image"]);

                cmd.ExecuteNonQuery();
            }
        }


        Response.Write("✔ Đã thêm dữ liệu vào bảng San_Pham!");
    }

    private string ConvertTypeToMaLoai(string type)
    {
        switch (type)
        {
            case "but": return "B";
            case "giay": return "G";
            case "khac": return "K";
            default: return "K";
        }
    }
}
