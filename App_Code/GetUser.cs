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
    public static int Signup(string email, string tenKH, string phone, string diaChi,
                         string role, string username, string password)
    {
        using (SqlConnection con = DbConection.GetConnection())
        {
            SqlCommand cmd = new SqlCommand("sp_Signup", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@TenKH", tenKH);
            cmd.Parameters.AddWithValue("@Phone", phone);
            cmd.Parameters.AddWithValue("@DiaChi", diaChi);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@Role", role);

            con.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
    public static DataTable GetProfileByAccountID(int accId)
    {
        using (SqlConnection con = DbConection.GetConnection())
        {
            string sql = @"
            SELECT a.Username, a.Role, a.ID_KH,
                   k.MaKH, k.TenKH, k.PhoneNumber,
                   k.DiaChi, k.Email, k.NgayTao
            FROM Users a
            JOIN KhachHang k ON a.ID_KH = k.id
            WHERE a.ID_KH = @id";

            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.SelectCommand.Parameters.AddWithValue("@id", accId);

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
    public static DataTable GetUsers()
    {
        using (SqlConnection con = DbConection.GetConnection())
        {
            string sql = @"
        SELECT u.ID, u.Username,u.Password, u.Role, k.id AS ID_KH,
               k.TenKH, k.Email, k.PhoneNumber, k.DiaChi
        FROM Users u
        LEFT JOIN KhachHang k ON u.ID_KH = k.id";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
    public static void UpdateUser(int id, string role, string password)
    {
        using (SqlConnection con = DbConection.GetConnection())
        {
            string sql = @"UPDATE Users 
                       SET Role=@role,
                           Password = CASE WHEN @pass='' THEN Password ELSE @pass END
                       WHERE ID=@id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@role", role);
            cmd.Parameters.AddWithValue("@pass", password);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
    public static void CreateKhachHangForUser(int userId, string ten, string phone, string diachi, string email)
    {
        using (SqlConnection con = DbConection.GetConnection())
        {
            string sql = @"
        DECLARE @newID INT;

        INSERT INTO KhachHang(TenKH,PhoneNumber,DiaChi,Email)
        VALUES(@ten,@phone,@dc,@mail);

        SET @newID = SCOPE_IDENTITY();

        UPDATE Users SET ID_KH = @newID WHERE ID=@uid;
        ";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ten", ten);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@dc", diachi);
            cmd.Parameters.AddWithValue("@mail", email);
            cmd.Parameters.AddWithValue("@uid", userId);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public static void RemoveKhachHangLink(int id)
    {
        using (SqlConnection con = DbConection.GetConnection())
        {
            string sql = "UPDATE Users SET ID_KH=NULL WHERE ID=@id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public static void UpdateUserRole(int id, string role)
    {
        using (SqlConnection con = DbConection.GetConnection())
        {
            string sql = "UPDATE Users SET Role=@role WHERE ID=@id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@role", role);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public static void DeleteUser(int id)
    {
        using (SqlConnection con = DbConection.GetConnection())
        {
            string sql = "DELETE FROM Users WHERE ID=@id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
    public static DataTable GetUsersByRole(string role)
    {
        using (SqlConnection con = DbConection.GetConnection())
        {
            string sql = "SELECT u.ID, u.Username,u.Password, u.Role, k.TenKH, k.Email, k.PhoneNumber, k.DiaChi " +
                         "FROM Users u LEFT JOIN KhachHang k ON u.ID_KH = k.id " +
                         "WHERE u.Role=@role ORDER BY u.Username";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@role", role);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
    public class UserInfo
    {
        public int ID { get; set; }
        public int? ID_KH { get; set; }
    }
    public static UserInfo GetUserByID(int id)
    {
        using (SqlConnection con = DbConection.GetConnection())
        {
            string sql = "SELECT ID, ID_KH FROM Users WHERE ID=@id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0) return null;

            return new UserInfo
            {
                ID = (int)dt.Rows[0]["ID"],
                ID_KH = dt.Rows[0]["ID_KH"] == DBNull.Value ? null : (int?)dt.Rows[0]["ID_KH"]
            };
        }
    }
    public static void UpdateKhachHang(int idKH, string ten, string phone, string diachi, string email)
    {
        using (SqlConnection con = DbConection.GetConnection())
        {
            string sql = @"
            UPDATE KhachHang
            SET TenKH=@ten,
                PhoneNumber=@phone,
                DiaChi=@dc,
                Email=@mail
            WHERE ID=@id";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ten", ten);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@dc", diachi);
            cmd.Parameters.AddWithValue("@mail", email);
            cmd.Parameters.AddWithValue("@id", idKH);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public static DataTable GetOrdersByUser(int userId)
    {
        using (SqlConnection con=GetConnection())
        using (SqlCommand cmd = new SqlCommand(
            @"SELECT 
            DH.MaDH,
            DH.NgayDat,
            DH.TongTien,
            DH.TrangThai
          FROM DonHang DH
          WHERE DH.idKH = @uid
          ORDER BY DH.NgayDat DESC", con))
        {
            cmd.Parameters.AddWithValue("@uid", userId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
