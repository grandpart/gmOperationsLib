using System.Text;
using System.Data;
using System.Data.SqlClient;
using Zephry;

namespace Grandmark
{
    public class SysRoleAccessData
    {
        #region HasRoleAccess with Connection
        public static bool HasRoleAccess(Connection aConnection, UserKey aUserKey, string aFunction)
        {
            if (aUserKey.UsrAdmin)
            {
                return true;
            }

            var vAccess = false;
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                vSqlCommand.Connection.Open();
                vAccess = HasRoleAccessCommon(vSqlCommand, aUserKey, aFunction);
                vSqlCommand.Connection.Close();
            }
            return vAccess;
        }
        #endregion

        #region HasRoleAccess with SqlCommand
        public static bool HasRoleAccess(SqlCommand aSqlCommand, UserKey aUserKey, string aFunction)
        {
            return aUserKey.UsrAdmin || HasRoleAccessCommon(aSqlCommand, aUserKey, aFunction);
        }

        #endregion

        #region HasRoleAccessCommon
        private static bool HasRoleAccessCommon(SqlCommand aSqlCommand, UserKey aUserKey, string aFunction)
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("select count(distinct url.SRLKey) as FNCAccessCount");
            vStringBuilder.AppendLine("from URLUserRole url,");
            vStringBuilder.AppendLine("     SRLSysRole srl");
            vStringBuilder.AppendLine("where url.SRLKey = srl.SRLKey");
            vStringBuilder.AppendLine("and url.USRKey = @USRKey");
            vStringBuilder.AppendLine("and srl.SRLCode = @FNCCode");
            aSqlCommand.Parameters.Clear();
            aSqlCommand.Parameters.AddWithValue("@USRKey", aUserKey.UsrKey);
            aSqlCommand.Parameters.AddWithValue("@FNCCode", aFunction);
            aSqlCommand.CommandText = vStringBuilder.ToString();
            return (int) aSqlCommand.ExecuteScalar() > 0;
        }

        #endregion
    }
}

