using System.Text;
using System.Data;
using System.Data.SqlClient;
using Zephry;

namespace Grandmark
{
    /// <summary>
    ///   ConnectionData class.
    /// </summary>
    public class ConnectionData
    {
        #region Load
        public static void Load(Connection aConnection, string aCncCode)
        {
            if (aCncCode == null)
            {
                throw new ArgumentNullException(nameof(aCncCode));
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                Connection vSourceConnection = new();
                var vStringBuilder = new StringBuilder();
                vStringBuilder.AppendLine("select Cnc_Server, Cnc_Name, Cnc_User, Cnc_Password");
                vStringBuilder.AppendLine("from Connection");
                vStringBuilder.AppendLine("where Cnc_Code = @CncCode");
                vSqlCommand.Parameters.AddWithValue("@CncCode", aCncCode);
                vSqlCommand.CommandText = vStringBuilder.ToString();
                vSqlCommand.Connection.Open();
                using (SqlDataReader vSqlDataReader = vSqlCommand.ExecuteReader())
                {
                    if (!vSqlDataReader.HasRows)
                    {
                        throw new Exception(String.Format("Expected Connection not found: Cnc_Code = {0}", aCncCode));
                    }
                    vSqlDataReader.Read();
                    aConnection.DbServer = Convert.ToString(vSqlDataReader["Cnc_Server"]);
                    aConnection.DbName = Convert.ToString(vSqlDataReader["Cnc_Name"]);
                    aConnection.DbUser = Convert.ToString(vSqlDataReader["Cnc_User"]);
                    aConnection.DbPassword = Convert.ToString(vSqlDataReader["Cnc_Password"]);
                    vSqlDataReader.Close();
                }
                vSqlCommand.Connection.Close();
            }
        }
        #endregion

    }
}
