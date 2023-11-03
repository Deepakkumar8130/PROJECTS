using Microsoft.Data.SqlClient;
namespace PROJECT4.Data
{
    public class ConnectDb
    {

        public SqlConnection connect;

        public ConnectDb()
        {
            connect = new SqlConnection(Connection.ConnectionStr);
        }

    }

    public static class Connection
    {
        internal static string ConnectionStr { get; set; }
    }
}
