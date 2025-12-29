using FreeTextBoxControls.Support;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GetOrder
/// </summary>
public class GetOrder : DbConection
{
    public GetOrder()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static int CreateOrderFromCart(int id)
    {
        using (SqlConnection con = GetConnection())
        {
            SqlCommand cmd = new SqlCommand("sp_CreateOrderFromCart", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@idKH", SqlDbType.Int).Value = id;
            con.Open();

            return (int)cmd.ExecuteScalar();
            
    }
    }
    public static DataTable GetOrderDetail(int maDH)
    {
        using (SqlConnection con = GetConnection())
        {
            string query = "SELECT TenSP,SoLuong,DonGia,TongTien,TrangThai,a.SoLuong * DonGia AS ThanhTien FROM ChiTietDonHang a INNER JOIN DonHang b on a.MaDH = b.MaDH inner join San_Pham c on a.idSP = c.id where a.MaDH = @maDH";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            cmd.Parameters.AddWithValue("@maDH", maDH);
            da.SelectCommand.Parameters.AddWithValue("@maDH", maDH);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
    public int GetOrderID(int maDH) { 
        using (SqlConnection con = GetConnection())
        {
            string query = "SELECT MaDH FROM DonHang WHERE MaDH = @maDH";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@maDH", SqlDbType.Int).Value = maDH;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return maDH;
        }
    }
}
