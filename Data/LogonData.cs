using System.Text;
using System.Data;
using System.Data.SqlClient;
using Zephry;

namespace Grandmark
{
    /// <summary>
    ///   UserData class.
    /// </summary>
    public class LogonData
    {
        #region Logon

        public static void Logon(Connection aConnection, LogonToken aLogonToken)
        {
            if (aLogonToken == null)
            {
                throw new ArgumentNullException(nameof(aLogonToken));
            }

            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStringBuilder = new StringBuilder();
                vStringBuilder.AppendLine("update SysUser");
                vStringBuilder.AppendLine("set UsrToken = NEWID()");
                vStringBuilder.AppendLine("output inserted.UsrToken, inserted.UsrAdmin");
                vStringBuilder.AppendLine("from SysUser usr ");
                vStringBuilder.AppendLine("where UsrUserID = @UsrUserID");
                vStringBuilder.AppendLine("and   UsrPassword = @UsrToken");
                vSqlCommand.Parameters.AddWithValue("@UsrUserID", aLogonToken.UserId);
                vSqlCommand.Parameters.AddWithValue("@UsrToken", aLogonToken.Token);
                vSqlCommand.CommandText = vStringBuilder.ToString();
                vSqlCommand.Connection.Open();
                using (var vSqlDataReader = vSqlCommand.ExecuteReader())
                {
                    if (!vSqlDataReader.HasRows)
                    {
                        throw new TransactionStatusException(TransactionResult.Access, "Logon failed.");
                    }
                    vSqlDataReader.Read();
                    aLogonToken.Token = Convert.ToString(vSqlDataReader["UsrToken"]);
                    aLogonToken.Admin = Convert.ToString(vSqlDataReader["UsrAdmin"]) == "Y";

                    // close reader
                    vSqlDataReader.Close();

                }
                vSqlCommand.Connection.Close();
            }
        }
        
        #endregion

        #region Authenticate

        public static void Authenticate(Connection aConnection, LogonToken aLogonToken, UserKey aUserKey)
        {
            if (aLogonToken == null)
            {
                throw new ArgumentNullException(nameof(aLogonToken));
            }
            if (aUserKey == null)
            {
                throw new ArgumentNullException(nameof(aUserKey));
            }
            using (var vSqlCommand = new SqlCommand
            {
                CommandType = CommandType.Text, Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStringBuilder = new StringBuilder();
                vStringBuilder.AppendLine("select usr.EntKey, usr.UsrKey, usr.UsrAdmin");
                vStringBuilder.AppendLine("from SysUser usr");
                vStringBuilder.AppendLine("where usr.UsrUserID = @UsrUserID");
                vStringBuilder.AppendLine("and   usr.UsrToken = @UsrToken");
                vSqlCommand.Parameters.AddWithValue("@UsrUserID", aLogonToken.UserId);
                vSqlCommand.Parameters.AddWithValue("@UsrToken", aLogonToken.Token);
                vSqlCommand.CommandText = vStringBuilder.ToString();
                vSqlCommand.Connection.Open();
                using (var vSqlDataReader = vSqlCommand.ExecuteReader())
                {
                    if (!vSqlDataReader.HasRows)
                    {
                        throw new TransactionStatusException(TransactionResult.Hijack, "Your UserID has either expired, or it has been hijacked. Please reconnect.");
                    }
                    vSqlDataReader.Read();
                    aUserKey.EntKey = Convert.ToInt32(vSqlDataReader["EntKey"]);
                    aUserKey.UsrKey = Convert.ToInt32(vSqlDataReader["UsrKey"]);
                    aUserKey.UsrAdmin = Convert.ToString(vSqlDataReader["UsrAdmin"]) == "Y";
                    vSqlDataReader.Close();

                }
                vSqlCommand.Connection.Close();
            }
        }

        #endregion

    }
}
