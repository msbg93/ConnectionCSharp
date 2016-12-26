using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data.Common;
/// <summary>
/// Summary description for Connection
/// </summary>
public class Connection
{
    private static readonly string connectionString = ConfigurationManager.ConnectionStrings["PFMS"].ConnectionString;
    private static readonly DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
	static SqlConnection conn;
	public Connection()
	{
        // TODO: Add constructor logic here
	}

    public static SqlConnection GetConnection()
    {

        string dbcon = ConfigurationManager.ConnectionStrings["PFMS"].ConnectionString;
        try
            {
                
                    conn = new SqlConnection(dbcon);                   
                    conn.Open();
                    return con;
               
            }
        catch (Exception)
        {
             throw;
        }
    }

       
    public static DataTable GetDataTable(string sql, SqlParameter[] parameters)
        {
            try
            {
                using (DbConnection connection = factory.CreateConnection())
                {
                    connection.ConnectionString = connectionString;

                    using (DbCommand command = factory.CreateCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = sql;

                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                if (parameter != null)
                                    command.Parameters.Add(parameter);
                            }
                        }
                        using (DbDataAdapter adapter = factory.CreateDataAdapter())
                        {
                            adapter.SelectCommand = command;
                            
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            return dt;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

}
