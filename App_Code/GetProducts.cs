using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for GetProducts
/// </summary>
public class GetProducts : DbConection
{
    public GetProducts()
    {

        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable GetCartById(int id)
    {
        using (SqlConnection con = GetConnection())
        {
            string sql = @"
            SELECT 
                c.SoLuong, 
                s.TenSP, 
                s.Gia, 
                s.AnhSP,
                s.id AS idSP
            FROM Cart c 
            JOIN San_Pham s ON c.idSP = s.id
            WHERE c.idKH = @uid";

            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.SelectCommand.Parameters.AddWithValue("@uid", id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
    public static DataTable GetProductsByType(string maLoai)
    {
        using (SqlConnection con = GetConnection())
        {
            SqlCommand cmd = new SqlCommand("sp_GetSanPham_ByLoai", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaLoai", maLoai);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
    public static DataTable GetTopByType(char maLoai,int top,int id)
    {
        using (SqlConnection con = GetConnection())
        {
            string sql = "Select top (@top) id,TenSP,AnhSP,Gia from San_Pham where MaLoai = @MaLoai and id != @id ORDER BY NEWID();";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.SelectCommand.Parameters.AddWithValue("@top",top);
            da.SelectCommand.Parameters.AddWithValue("@MaLoai",maLoai);
            da.SelectCommand.Parameters.AddWithValue("@id",id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
    public static DataTable GetTopProduct(int top)
    {
        using (SqlConnection con = GetConnection())
        {
            string sql = "Select top (@top) id,TenSP,AnhSP,Gia from San_Pham ORDER BY NEWID();";
            SqlDataAdapter da = new SqlDataAdapter(sql,con);
            da.SelectCommand.Parameters.AddWithValue("@top", top);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
    public static DataRow GetDetail(int id)
    {
        using (SqlConnection con = GetConnection())
        {
            SqlCommand cmd = new SqlCommand("sp_GetSanPham_Detail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
                return null;

            return dt.Rows[0];
        }
    }
    public static DataRow GetAds()
    {
        using (SqlConnection con = GetConnection())
        {
            string sql = "Select top (1) * from Ads ORDER BY NEWID();";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
                return null;
            return dt.Rows[0];
        }
    }
}