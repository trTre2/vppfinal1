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
    public static DataTable GetTopByType(char maLoai,int top)
    {
        using (SqlConnection con = GetConnection())
        {
            string sql = "Select top (@top) id,TenSP,AnhSP,Gia from San_Pham where MaLoai = @MaLoai";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.SelectCommand.Parameters.AddWithValue("@top",top);
            da.SelectCommand.Parameters.AddWithValue("@MaLoai",maLoai);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
    public static DataTable GetTopProduct(int top)
    {
        using (SqlConnection con = GetConnection())
        {
            string sql = "Select top (@top) id,TenSP,AnhSP,Gia from San_Pham";
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
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt.Rows[0];
        }
    }
}