using System;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for GetUser
/// </summary>
public class GetUser : DbConection
{
    public GetUser()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataRow Login(string username, string password)
    {
        using (SqlConnection con = GetConnection())
        {
            SqlCommand cmd = new SqlCommand("sp_Login", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }
    }
    public static int Signup(string email,string tenKH,string phone,string diaChi,string username,string password)
    {
        using (SqlConnection conn = DbConection.GetConnection())
        {
            SqlCommand cmd = new SqlCommand("sp_Signup", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@TenKH", tenKH);
            cmd.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(phone) ? (object)DBNull.Value : phone);
            cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrEmpty(diaChi) ? (object)DBNull.Value : diaChi);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);

            conn.Open();
            return (int)cmd.ExecuteScalar();
        }
    }
}
