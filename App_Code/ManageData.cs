using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ManageData
/// </summary>
public class ManageData : DbConection
{
    public ManageData()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    //Loại hàng

    public static DataTable GetLoaiSP()
    {
        SqlDataAdapter da = new SqlDataAdapter(
            "SELECT MaLoai, TenLoai FROM LoaiSP",
            DbConection.GetConnection()
        );

        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public static void InsertLoaiSP(string maLoai, string tenLoai)
    {
        SqlCommand cmd = new SqlCommand(
            "INSERT INTO LoaiSP(MaLoai, TenLoai) VALUES (@ma, @ten)",
            DbConection.GetConnection()
        );

        cmd.Parameters.AddWithValue("@ma", maLoai);
        cmd.Parameters.AddWithValue("@ten", tenLoai);

        cmd.Connection.Open();
        cmd.ExecuteNonQuery();
        cmd.Connection.Close();
    }

    public static void UpdateLoaiSP(string maLoai, string tenLoai)
    {
        SqlCommand cmd = new SqlCommand(
            "UPDATE LoaiSP SET TenLoai=@ten WHERE MaLoai=@ma",
            DbConection.GetConnection()
        );

        cmd.Parameters.AddWithValue("@ten", tenLoai);
        cmd.Parameters.AddWithValue("@ma", maLoai);
        cmd.Connection.Open();
        cmd.ExecuteNonQuery();
        cmd.Connection.Close();
    }

    public static void DeleteLoaiSP(string maLoai)
    {
        SqlCommand cmd = new SqlCommand(
            "DELETE FROM LoaiSP WHERE MaLoai=@ma",
            DbConection.GetConnection()
        );

        cmd.Parameters.AddWithValue("@ma", maLoai);
        cmd.Connection.Open();
        cmd.ExecuteNonQuery();
        cmd.Connection.Close();
    }
    public static bool CheckMaLoaiTonTai(string ma)
    {
        SqlCommand cmd = new SqlCommand(
            "SELECT COUNT(*) FROM LoaiSP WHERE MaLoai=@ma",
            DbConection.GetConnection()
        );
        cmd.Parameters.AddWithValue("@ma", ma);

        cmd.Connection.Open();
        int count = (int)cmd.ExecuteScalar();
        cmd.Connection.Close();

        return count > 0;
    }
    // ===== LOAD SẢN PHẨM =====
    public static DataTable GetSanPham()
    {
        string sql = @"SELECT id, MaSP, TenSP, Gia, MaLoai, MoTa, TinhTrang, AnhSP 
                       FROM San_Pham";
        SqlDataAdapter da = new SqlDataAdapter(sql, DbConection.GetConnection());
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    public static void InsertSanPham(string maloai, string tensp, decimal gia, string mota, string tinhtrang, string anh)
    {
        string sql = @"INSERT INTO San_Pham
                   (MaLoai, TenSP, Gia, MoTa, TinhTrang, AnhSP)
                   VALUES (@ml, @ten, @gia, @mt, @tt, @anh)";

        using (SqlConnection con = DbConection.GetConnection())
        using (SqlCommand cmd = new SqlCommand(sql, con))
        {
            cmd.Parameters.AddWithValue("@ml", maloai);
            cmd.Parameters.AddWithValue("@ten", tensp);
            cmd.Parameters.AddWithValue("@gia", gia);
            cmd.Parameters.AddWithValue("@mt", mota);
            cmd.Parameters.AddWithValue("@tt", tinhtrang);
            cmd.Parameters.AddWithValue("@anh", anh);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
    public static void UpdateSanPham(int id, string tensp, decimal gia, string maloai, string mota, string tinhtrang, string anh)
    {
        string sql = @"UPDATE San_Pham SET
                       TenSP=@ten,
                       Gia=@gia,
                       MaLoai=@ml,
                       MoTa=@mt,
                       TinhTrang=@tt,
                       AnhSP=@anh
                       WHERE id=@id";
        using (SqlConnection con = DbConection.GetConnection())
        {
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@ten", tensp);
                cmd.Parameters.AddWithValue("@gia", gia);
                cmd.Parameters.AddWithValue("@ml", maloai);
                cmd.Parameters.AddWithValue("@mt", mota);
                cmd.Parameters.AddWithValue("@tt", tinhtrang);
                cmd.Parameters.AddWithValue("@anh", anh);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    public static void DeleteSanPham(int id)
    {
        using (SqlConnection con = DbConection.GetConnection())
        {
            string sql = "DELETE FROM San_Pham WHERE id=@id";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {

                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    public static DataTable GetDonHang()
    {
        DataTable dt = new DataTable();

        string sql = @"
            SELECT 
                d.MaDH,
                k.TenKH,
                k.PhoneNumber,
                d.NgayDat,
                d.TrangThai,
                SUM(ct.SoLuong * sp.Gia) AS TongTien
            FROM DonHang d
            JOIN KhachHang k ON d.idKH = k.id
            JOIN ChiTietDonHang ct ON d.MaDH = ct.MaDH
            JOIN San_Pham sp ON ct.idSP = sp.id
            GROUP BY d.MaDH, k.TenKH, k.PhoneNumber, d.NgayDat, d.TrangThai
            ORDER BY d.MaDH DESC";

        using (SqlConnection con = DbConection.GetConnection())
        using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
        {
            da.Fill(dt);
        }

        return dt;
    }

    public static DataTable GetKhachHang()
    {
        DataTable dt = new DataTable();

        using (SqlConnection con = DbConection.GetConnection())
        using (SqlDataAdapter da =
            new SqlDataAdapter("SELECT id, TenKH FROM KhachHang", con))
        {
            da.Fill(dt);
        }

        return dt;
    }

    public static DataTable GetSanPhamByName(string keyword)
    {
        string sql = "SELECT id, TenSP, Gia FROM San_Pham WHERE TenSP LIKE @kw";
        DataTable dt = new DataTable();
        using (SqlConnection con = DbConection.GetConnection())
        using (SqlCommand cmd = new SqlCommand(sql, con))
        {
            cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }
        }
        return dt;
    }

    public static int InsertDonHang(int idKH, string trangThai)
    {
        string sql = @"INSERT INTO DonHang(idKH, TrangThai)
                   OUTPUT INSERTED.MaDH
                   VALUES(@kh,@tt)";

        using (SqlConnection con = DbConection.GetConnection())
        using (SqlCommand cmd = new SqlCommand(sql, con))
        {
            cmd.Parameters.AddWithValue("@kh", idKH);
            cmd.Parameters.AddWithValue("@tt", trangThai);
            con.Open();
            return (int)cmd.ExecuteScalar();
        }
    }
    public static void UpdateTongTienDonHang(int maDH)
    {
        string sql = @"UPDATE DonHang
                   SET TongTien = (SELECT SUM(SoLuong * DonGia) FROM ChiTietDonHang WHERE MaDH=@maDH)
                   WHERE MaDH=@maDH";

        using (SqlConnection con = DbConection.GetConnection())
        using (SqlCommand cmd = new SqlCommand(sql, con))
        {
            cmd.Parameters.AddWithValue("@maDH", maDH);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
    public static void InsertCTDH(int maDH, int idSP, int soLuong)
    {
        // Lấy giá SP
        decimal donGia = 0;
        string sqlGia = "SELECT Gia FROM San_Pham WHERE id=@idSP";
        using (SqlConnection con = DbConection.GetConnection())
        using (SqlCommand cmd = new SqlCommand(sqlGia, con))
        {
            cmd.Parameters.AddWithValue("@idSP", idSP);
            con.Open();
            donGia = (decimal)cmd.ExecuteScalar();
        }

        // Insert vào ChiTietDonHang
        string sql = @"INSERT INTO ChiTietDonHang(MaDH, idSP, SoLuong, DonGia)
                   VALUES(@dh,@sp,@sl,@dg)";
        using (SqlConnection con = DbConection.GetConnection())
        using (SqlCommand cmd = new SqlCommand(sql, con))
        {
            cmd.Parameters.AddWithValue("@dh", maDH);
            cmd.Parameters.AddWithValue("@sp", idSP);
            cmd.Parameters.AddWithValue("@sl", soLuong);
            cmd.Parameters.AddWithValue("@dg", donGia);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public static void UpdateDonHangTrangThai(int maDH, string trangThai)
    {
        string sql = "UPDATE DonHang SET TrangThai=@tt WHERE MaDH=@id";

        using (SqlConnection con = DbConection.GetConnection())
        using (SqlCommand cmd = new SqlCommand(sql, con))
        {
            cmd.Parameters.AddWithValue("@tt", trangThai);
            cmd.Parameters.AddWithValue("@id", maDH);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public static void UpdateCTDH(int maDH, int oldSP, int newSP, int soLuong)
    {
        string sql = @"
        UPDATE ChiTietDonHang
        SET idSP=@newSP, SoLuong=@sl
        WHERE MaDH=@dh AND idSP=@oldSP";

        using (SqlConnection con = DbConection.GetConnection())
        using (SqlCommand cmd = new SqlCommand(sql, con))
        {
            cmd.Parameters.AddWithValue("@dh", maDH);
            cmd.Parameters.AddWithValue("@oldSP", oldSP);
            cmd.Parameters.AddWithValue("@newSP", newSP);
            cmd.Parameters.AddWithValue("@sl", soLuong);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}