using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Zephry;

namespace Grandmark
{
    /// <summary>
    ///   UserData class.
    /// </summary>
    public class UserData
    {
        #region BuildSQL

        /// <summary>
        ///   A standard SQL Statement that will return all <see cref="User"/>, unfiltered and unsorted.
        /// </summary>
        /// <returns>A <see cref="StringBuilder"/></returns>
        private static StringBuilder BuildSql()
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("select usr.EntKey, usr.UsrKey, usr.UsrParentEnt, usr.UsrParentKey,");
            vStringBuilder.AppendLine("       usr.QLFKey, qlf.QLFName,  usr.UsrAdmin, usr.UsrAutoAuthorize, usr.UsrCanAuthorize,");
            vStringBuilder.AppendLine("       usr.UsrUserID, usr.UsrPassword, usr.UsrName, usr.UsrSurname,");
            vStringBuilder.AppendLine("       own.UsrName + ' ' + own.UsrSurname as UsrParentFullName,");
            vStringBuilder.AppendLine("       usr.UsrIdentifier, usr.UsrEmail, usr.UsrRate, usr.UsrCost, usr.UsrMobile, usr.UsrPhone,");
            vStringBuilder.AppendLine("       usr.UsrExtension, usr.UsrFax, usr.UsrStatus, usr.UsrStatusDate, usr.UsrToken, usr.UsrAvatar");
            vStringBuilder.AppendLine("from   UsrUser usr left outer join UsrUser own");
            vStringBuilder.AppendLine("                        on  usr.UsrParentEnt = own.EntKey");
            vStringBuilder.AppendLine("                        and usr.UsrParentKey = own.UsrKey");
            return vStringBuilder;
        }

        #endregion

        #region DataToObject

        /// <summary>
        ///   Load a <see cref="SqlDataReader"/> into a <see cref="User"/> object.
        /// </summary>
        /// <param name="aUser">A <see cref="User"/> argument.</param>
        /// <param name="aSqlDataReader">A <see cref="SqlDataReader"/> argument.</param>
        public static void DataToObject(User aUser, SqlDataReader aSqlDataReader)
        {
            aUser.UsrKey = Convert.ToInt32(aSqlDataReader["UsrKey"]);
            aUser.ParentUsrKey = aSqlDataReader["UsrParentKey"] as int?;
            aUser.UsrAdmin = Convert.ToString(aSqlDataReader["UsrAdmin"]) == "Y";
            aUser.UsrAutoAuthorize = Convert.ToString(aSqlDataReader["UsrAutoAuthorize"]) == "Y";
            aUser.UsrCanAuthorize = Convert.ToString(aSqlDataReader["UsrCanAuthorize"]) == "Y";
            aUser.UsrUserId = Convert.ToString(aSqlDataReader["UsrUserID"]) ?? string.Empty;
            aUser.UsrPassword = Convert.ToString(aSqlDataReader["UsrPassword"]) ?? string.Empty;
            aUser.UsrName = Convert.ToString(aSqlDataReader["UsrName"]) ?? string.Empty;
            aUser.UsrSurname = Convert.ToString(aSqlDataReader["UsrSurname"]) ?? string.Empty;
            aUser.UsrParentFullName = Convert.ToString(aSqlDataReader["UsrParentFullName"]) ?? string.Empty;
            aUser.UsrEmail = Convert.ToString(aSqlDataReader["UsrEmail"]) ?? string.Empty;
            //
            aUser.UsrIdentifier = Convert.ToString(aSqlDataReader["UsrIdentifier"]) ?? string.Empty;
            aUser.UsrMobile = Convert.ToString(aSqlDataReader["UsrMobile"]) ?? string.Empty;
            aUser.UsrPhone = Convert.ToString(aSqlDataReader["UsrPhone"]) ?? string.Empty;
            aUser.UsrExtension = Convert.ToString(aSqlDataReader["UsrExtension"]) ?? string.Empty;
            aUser.UsrFax = Convert.ToString(aSqlDataReader["UsrFax"]) ?? string.Empty;
            //aUser.UsrAvatar = DrawingUtils.ByteToBase64String(CommonUtils.DbValueTo<byte[]>(aSqlDataReader["UsrAvatar"], null));
            aUser.UsrStatus = (UserStatus)Convert.ToInt32(aSqlDataReader["UsrStatus"]);
            aUser.UsrStatusDate = Convert.ToDateTime(aSqlDataReader["UsrStatusDate"]);
            aUser.UsrToken = Convert.ToString(aSqlDataReader["UsrToken"]) ?? string.Empty;
        }

        #endregion

        #region Load by ID
        /// <summary>
        /// The overloaded Load method that will return a <see cref="User"/> object specified by a UserID.
        /// </summary>
        /// <param name="aConnection"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aUser"></param>
        public static void LoadById(Connection aConnection, User aUser)
        {
            if (aUser == null)
            {
                throw new ArgumentNullException("aUser");
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStringBuilder = BuildSql();
                vStringBuilder.AppendLine("where usr.UsrUserID = @USRUserID");
                vSqlCommand.Parameters.AddWithValue("@USRUserID", aUser.UsrUserId);
                vSqlCommand.CommandText = vStringBuilder.ToString();
                vSqlCommand.Connection.Open();
                using (SqlDataReader vSqlDataReader = vSqlCommand.ExecuteReader())
                {
                    if (!vSqlDataReader.HasRows)
                    {
                        throw new Exception("Smart User Authentication Failed (0901)");
                    }
                    vSqlDataReader.Read();
                    DataToObject(aUser, vSqlDataReader);
                    vSqlDataReader.Close();
                }
                vSqlCommand.Connection.Close();
            }
        }

        #endregion

        #region Load
        /// <summary>
        /// The overloaded Load method that will return a specific <see cref="User"/>, with keys in the <c>aUser</c> argument.
        /// </summary>
        /// <param name="aConnection"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aUser"></param>
        public static void Load(Connection aConnection, User aUser)
        {
            if (aUser == null)
            {
                throw new ArgumentNullException("aUser");
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStringBuilder = BuildSql();
                vStringBuilder.AppendLine("where usr.UsrKey = @USRKey");
                vSqlCommand.Parameters.AddWithValue("@USRKey", aUser.UsrKey);
                vSqlCommand.CommandText = vStringBuilder.ToString();
                vSqlCommand.Connection.Open();
                using (SqlDataReader vSqlDataReader = vSqlCommand.ExecuteReader())
                {
                    if (!vSqlDataReader.HasRows)
                    {
                        throw new Exception(String.Format("Expected User not found: UsrKey = {0}", aUser.UsrKey));
                    }
                    vSqlDataReader.Read();
                    DataToObject(aUser, vSqlDataReader);
                    vSqlDataReader.Close();
                }
                vSqlCommand.Connection.Close();
            }
        }

        #endregion

        #region Name
        /// <summary>
        /// Get a user name only
        /// </summary>
        /// <param name="aSqlCommand"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aUsrKey"></param>
        public static string Name(SqlCommand aSqlCommand, int aUsrKey)
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("select UsrName + ' ' +  UsrSurname");
            vStringBuilder.AppendLine("from UsrUser usr");
            vStringBuilder.AppendLine("where UsrKey = @USRKey");
            aSqlCommand.Parameters.Clear();
            aSqlCommand.Parameters.AddWithValue("@USRKey", aUsrKey);
            aSqlCommand.CommandText = vStringBuilder.ToString();
            var vObject = aSqlCommand.ExecuteScalar();
            return (string)vObject;
        }

        #endregion

        #region Insert by Anonymous

        /// <summary>
        /// Insert called by anonymous public on registration
        /// </summary>
        /// <param name="aSqlCommand"></param>
        /// <param name="aUser"></param>
        public static void Insert(SqlCommand aSqlCommand, User aUser)
        {
            if (aUser == null)
            {
                throw new ArgumentNullException(nameof(aUser));
            }
            InsertCommon(aSqlCommand, aUser);
        }

        #endregion

        #region Insert by User
        /// <summary>
        /// Insert called by an admin User
        /// </summary>
        /// <param name="aConnection"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aUser"></param>
        public static void Insert(Connection aConnection, User aUser)
        {
            if (aUser == null)
            {
                throw new ArgumentNullException(nameof(aUser));
            }

            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                vSqlCommand.Connection.Open();
                InsertCommon(vSqlCommand, aUser);
                vSqlCommand.Connection.Close();
            }
        }

        #endregion

        #region Insert Common

        private static void InsertCommon(SqlCommand aSqlCommand, User aUser)
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("insert into UsrUser");
            vStringBuilder.AppendLine("       (UsrParentKey, OrgKey, QLFKey, UsrUserID, UsrPassword,");
            vStringBuilder.AppendLine("        UsrAdmin, UsrAutoAuthorize, UsrCanAuthorize, UsrName, UsrSurname, UsrEmail, UsrRate, UsrCost,");
            vStringBuilder.AppendLine("        UsrIdentifier, UsrMobile, UsrPhone, UsrExtension, UsrFax, UsrStatus, UsrStatusDate)");
            vStringBuilder.AppendLine("output inserted.UsrKey");
            vStringBuilder.AppendLine("values");
            vStringBuilder.AppendLine("       (@USRParentUSRKey, @OrgKey, @QLFKey, @USRUserID, @USRPassword,");
            vStringBuilder.AppendLine("        @USRAdmin, @USRAutoAuthorize, @USRCanAuthorize, @USRName, @USRSurname, @USREmail, @USRRate, @USRCost,");
            vStringBuilder.AppendLine("        @USRIdentifier, @USRMobile, @USRPhone, @USRExtension, @USRFax, @USRStatus, GetDate())");

            aSqlCommand.Parameters.Clear();
            if (aUser.ParentUsrKey == null)
            {
                aSqlCommand.Parameters.AddWithValue("@USRParentUSRKey", DBNull.Value);
            }
            else
            {
                aSqlCommand.Parameters.AddWithValue("@USRParentUSRKey", aUser.ParentUsrKey);
            }
            aSqlCommand.Parameters.AddWithValue("@USRUserID", aUser.UsrUserId);
            aSqlCommand.Parameters.AddWithValue("@USRPassword", aUser.UsrPassword);
            aSqlCommand.Parameters.AddWithValue("@USRAdmin", aUser.UsrAdmin ? "Y" : "N");
            aSqlCommand.Parameters.AddWithValue("@USRAutoAuthorize", aUser.UsrAutoAuthorize ? "Y" : "N");
            aSqlCommand.Parameters.AddWithValue("@USRCanAuthorize", aUser.UsrCanAuthorize ? "Y" : "N");
            aSqlCommand.Parameters.AddWithValue("@USRName", aUser.UsrName);
            aSqlCommand.Parameters.AddWithValue("@USRSurname", aUser.UsrSurname);
            aSqlCommand.Parameters.AddWithValue("@USREmail", aUser.UsrEmail);
            aSqlCommand.Parameters.AddWithValue("@USRIdentifier", string.IsNullOrWhiteSpace(aUser.UsrIdentifier) ? string.Empty : aUser.UsrIdentifier);
            aSqlCommand.Parameters.AddWithValue("@USRMobile", string.IsNullOrWhiteSpace(aUser.UsrMobile) ? string.Empty : aUser.UsrMobile);
            aSqlCommand.Parameters.AddWithValue("@USRPhone", string.IsNullOrWhiteSpace(aUser.UsrPhone) ? string.Empty : aUser.UsrPhone);
            aSqlCommand.Parameters.AddWithValue("@USRExtension", string.IsNullOrWhiteSpace(aUser.UsrExtension) ? string.Empty : aUser.UsrExtension);
            aSqlCommand.Parameters.AddWithValue("@USRFax", string.IsNullOrWhiteSpace(aUser.UsrFax) ? string.Empty : aUser.UsrFax);
            aSqlCommand.Parameters.AddWithValue("@USRStatus", UserStatus.Active);

            aSqlCommand.CommandText = vStringBuilder.ToString();
            aUser.UsrKey = Convert.ToInt32(aSqlCommand.ExecuteScalar());
        }

        #endregion

        #region Update
        /// <summary>
        /// Update a User
        /// </summary>
        /// <param name="aConnection"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aUser"></param>
        public static void Update(Connection aConnection, UserKey aUserKey, User aUser)
        {
            if (aUser == null)
            {
                throw new ArgumentNullException("aUser");
            }

            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStringBuilder = new StringBuilder();
                vStringBuilder.AppendLine("update UsrUser");
                vStringBuilder.AppendLine("set    UsrName = @USRName,");
                vStringBuilder.AppendLine("       UsrSurname = @USRSurname,");
                vStringBuilder.AppendLine("       UsrAutoAuthorize = @USRAutoAuthorize,");
                vStringBuilder.AppendLine("       UsrCanAuthorize = @USRCanAuthorize,");
                vStringBuilder.AppendLine("       UsrEmail = @USREmail,");
                vStringBuilder.AppendLine("       UsrRate = @USRRate,");
                vStringBuilder.AppendLine("       UsrCost = @USRCost,");
                vStringBuilder.AppendLine("       UsrIdentifier = @USRIdentifier,");
                vStringBuilder.AppendLine("       UsrMobile = @USRMobile,");
                vStringBuilder.AppendLine("       UsrPhone = @USRPhone,");
                vStringBuilder.AppendLine("       UsrExtension = @USRExtension,");
                vStringBuilder.AppendLine("       UsrFax = @USRFax,");
                vStringBuilder.AppendLine("       QLFKey = @QLFKey,");
                vStringBuilder.AppendLine("       OrgKey = @OrgKey");
                vStringBuilder.AppendLine("where  UsrKey = @USRKey");

                vSqlCommand.Parameters.AddWithValue("@USRKey", aUser.UsrKey);
                vSqlCommand.Parameters.AddWithValue("@USRName", aUser.UsrName);
                vSqlCommand.Parameters.AddWithValue("@USRSurname", aUser.UsrSurname);
                vSqlCommand.Parameters.AddWithValue("@USRAutoAuthorize", aUser.UsrAutoAuthorize ? "Y" : "N");
                vSqlCommand.Parameters.AddWithValue("@USRCanAuthorize", aUser.UsrCanAuthorize ? "Y" : "N");
                vSqlCommand.Parameters.AddWithValue("@USREmail", aUser.UsrEmail);
                vSqlCommand.Parameters.AddWithValue("@USRIdentifier", String.IsNullOrWhiteSpace(aUser.UsrIdentifier) ? String.Empty : aUser.UsrIdentifier);
                vSqlCommand.Parameters.AddWithValue("@USRMobile", String.IsNullOrWhiteSpace(aUser.UsrMobile) ? String.Empty : aUser.UsrMobile);
                vSqlCommand.Parameters.AddWithValue("@USRPhone", String.IsNullOrWhiteSpace(aUser.UsrPhone) ? String.Empty : aUser.UsrPhone);
                vSqlCommand.Parameters.AddWithValue("@USRExtension", String.IsNullOrWhiteSpace(aUser.UsrExtension) ? String.Empty : aUser.UsrExtension);
                vSqlCommand.Parameters.AddWithValue("@USRFax", String.IsNullOrWhiteSpace(aUser.UsrFax) ? String.Empty : aUser.UsrFax);

                vSqlCommand.CommandText = vStringBuilder.ToString();
                vSqlCommand.Connection.Open();
                vSqlCommand.ExecuteNonQuery();
                vSqlCommand.Connection.Close();
            }
        }

        #endregion

        #region Delete
        /// <summary>
        /// Delete a User
        /// </summary>
        /// <param name="aConnection"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aUser"></param>
        public static void Delete(Connection aConnection, UserKey aUserKey, User aUser)
        {
            if (aUser == null)
            {
                throw new ArgumentNullException("aUser");
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStringBuilder = new StringBuilder();
                vStringBuilder.AppendLine("delete UsrUser");
                vStringBuilder.AppendLine("and    UsrKey = @USRKey");
                vSqlCommand.Parameters.AddWithValue("@USRKey", aUser.UsrKey);
                vSqlCommand.CommandText = vStringBuilder.ToString();
                vSqlCommand.Connection.Open();
                vSqlCommand.ExecuteNonQuery();
                vSqlCommand.Connection.Close();
            }
        }

        #endregion
        
     }
 }
