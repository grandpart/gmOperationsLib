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
                vStringBuilder.AppendLine("set Usr_Token = NEWID()");
                vStringBuilder.AppendLine("output inserted.Usr_Token, inserted.Usr_Name, inserted.Usr_Surname, inserted.Usr_Admin");
                vStringBuilder.AppendLine("from SysUser usr ");
                vStringBuilder.AppendLine("where Usr_UserID = @UsrUserID");
                vStringBuilder.AppendLine("and   Usr_Password = @UsrToken");
                vSqlCommand.Parameters.AddWithValue("@UsrUserID", aLogonToken.UserId);
                vSqlCommand.Parameters.AddWithValue("@UsrToken", aLogonToken.Token);
                vSqlCommand.CommandText = vStringBuilder.ToString();
                vSqlCommand.Connection.Open();
                using (var vSqlDataReader = vSqlCommand.ExecuteReader())
                {
                    if (!vSqlDataReader.HasRows)
                    {
                        throw new Exception("User Logon Failed");
                    }
                    vSqlDataReader.Read();
                    aLogonToken.Token = Convert.ToString(vSqlDataReader["Usr_Token"]);
                    aLogonToken.Name = Convert.ToString(vSqlDataReader["Usr_Name"]);
                    aLogonToken.Surname = Convert.ToString(vSqlDataReader["Usr_Surname"]);
                    aLogonToken.Admin = Convert.ToString(vSqlDataReader["Usr_Admin"]) == "Y";

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
                vStringBuilder.AppendLine("select usr.Ent_Key, usr.Usr_Key, usr.Usr_Admin");
                vStringBuilder.AppendLine("from SysUser usr");
                vStringBuilder.AppendLine("where usr.Usr_UserID = @UsrUserID");
                vStringBuilder.AppendLine("and   usr.Usr_Token = @UsrToken");
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
                    aUserKey.EntKey = Convert.ToInt32(vSqlDataReader["Ent_Key"]);
                    aUserKey.UsrKey = Convert.ToInt32(vSqlDataReader["Usr_Key"]);
                    aUserKey.UsrAdmin = Convert.ToString(vSqlDataReader["Usr_Admin"]) == "Y";
                    vSqlDataReader.Close();

                }
                vSqlCommand.Connection.Close();
            }
        }

        #endregion

    }
}
