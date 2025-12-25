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
    public static void CreateOrderFromCart(int id)
    {
        using (SqlConnection con = GetConnection())
        {
            SqlCommand cmd = new SqlCommand("sp_CreateOrderFromCart", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

        }
    }
    public static DataTable GetOrderDetail(int maDH)
    {
        using (SqlConnection con = GetConnection())
        {
            string query = "SELECT * FROM ChiTietDonHang RIGHT JOIN DonHang WHERE MaDH = @maDH";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@maDH", SqlDbType.Int).Value = maDH;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
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
