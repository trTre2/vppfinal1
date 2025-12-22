using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;



/// <summary>
/// Summary description for BasePages
/// </summary>
public class BasePages : Page
{
    public string vpp = ConfigurationManager.ConnectionStrings["vpp"].ConnectionString;

    protected SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["vpp"].ConnectionString);
    public BasePages()
    {

        //
        // TODO: Add constructor logic here
        //
    }

}
//
// TODO: Add constructor logic here
//
