using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DbConection
/// </summary>
public class DbConection
{
    public DbConection()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    protected static SqlConnection GetConnection()
    {
        return new SqlConnection(
            ConfigurationManager.ConnectionStrings["vpp"].ConnectionString
        );
    }
}