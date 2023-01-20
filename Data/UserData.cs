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
            vStringBuilder.AppendLine("select usr.Ent_Key, usr.Usr_Key, usr.Usr_ParentEnt, usr.Usr_ParentKey,");
            vStringBuilder.AppendLine("       usr.QLF_Key, qlf.QLF_Name,  usr.Usr_Admin, usr.Usr_AutoAuthorize, usr.Usr_CanAuthorize,");
            vStringBuilder.AppendLine("       usr.Usr_UserID, usr.Usr_Password, usr.Usr_Name, usr.Usr_Surname,");
            vStringBuilder.AppendLine("       own.Usr_Name + ' ' + own.Usr_Surname as Usr_ParentFullName,");
            vStringBuilder.AppendLine("       usr.Usr_Identifier, usr.Usr_Email, usr.Usr_Rate, usr.Usr_Cost, usr.Usr_Mobile, usr.Usr_Phone,");
            vStringBuilder.AppendLine("       usr.Usr_Extension, usr.Usr_Fax, usr.Usr_Status, usr.Usr_StatusDate, usr.Usr_Token, usr.Usr_Avatar");
            vStringBuilder.AppendLine("from   Usr_User usr left outer join Usr_User own");
            vStringBuilder.AppendLine("                        on  usr.Usr_ParentEnt = own.Ent_Key");
            vStringBuilder.AppendLine("                        and usr.Usr_ParentKey = own.Usr_Key");
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
            aUser.UsrKey = Convert.ToInt32(aSqlDataReader["Usr_Key"]);
            aUser.ParentUsrKey = aSqlDataReader["Usr_ParentKey"] as int?;
            aUser.UsrAdmin = Convert.ToString(aSqlDataReader["Usr_Admin"]) == "Y";
            aUser.UsrAutoAuthorize = Convert.ToString(aSqlDataReader["Usr_AutoAuthorize"]) == "Y";
            aUser.UsrCanAuthorize = Convert.ToString(aSqlDataReader["Usr_CanAuthorize"]) == "Y";
            aUser.UsrUserId = Convert.ToString(aSqlDataReader["Usr_UserID"]) ?? string.Empty;
            aUser.UsrPassword = Convert.ToString(aSqlDataReader["Usr_Password"]) ?? string.Empty;
            aUser.UsrName = Convert.ToString(aSqlDataReader["Usr_Name"]) ?? string.Empty;
            aUser.UsrSurname = Convert.ToString(aSqlDataReader["Usr_Surname"]) ?? string.Empty;
            aUser.UsrParentFullName = Convert.ToString(aSqlDataReader["Usr_ParentFullName"]) ?? string.Empty;
            aUser.UsrEmail = Convert.ToString(aSqlDataReader["Usr_Email"]) ?? string.Empty;
            //
            aUser.UsrIdentifier = Convert.ToString(aSqlDataReader["Usr_Identifier"]) ?? string.Empty;
            aUser.UsrMobile = Convert.ToString(aSqlDataReader["Usr_Mobile"]) ?? string.Empty;
            aUser.UsrPhone = Convert.ToString(aSqlDataReader["Usr_Phone"]) ?? string.Empty;
            aUser.UsrExtension = Convert.ToString(aSqlDataReader["Usr_Extension"]) ?? string.Empty;
            aUser.UsrFax = Convert.ToString(aSqlDataReader["Usr_Fax"]) ?? string.Empty;
            //aUser.UsrAvatar = DrawingUtils.ByteToBase64String(CommonUtils.DbValueTo<byte[]>(aSqlDataReader["Usr_Avatar"], null));
            aUser.UsrStatus = (UserStatus)Convert.ToInt32(aSqlDataReader["Usr_Status"]);
            aUser.UsrStatusDate = Convert.ToDateTime(aSqlDataReader["Usr_StatusDate"]);
            aUser.UsrToken = Convert.ToString(aSqlDataReader["Usr_Token"]) ?? string.Empty;
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
                vStringBuilder.AppendLine("where usr.Usr_UserID = @USRUserID");
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
                vStringBuilder.AppendLine("where usr.Usr_Key = @USRKey");
                vSqlCommand.Parameters.AddWithValue("@USRKey", aUser.UsrKey);
                vSqlCommand.CommandText = vStringBuilder.ToString();
                vSqlCommand.Connection.Open();
                using (SqlDataReader vSqlDataReader = vSqlCommand.ExecuteReader())
                {
                    if (!vSqlDataReader.HasRows)
                    {
                        throw new Exception(String.Format("Expected User not found: Usr_Key = {0}", aUser.UsrKey));
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
            vStringBuilder.AppendLine("select Usr_Name + ' ' +  Usr_Surname");
            vStringBuilder.AppendLine("from Usr_User usr");
            vStringBuilder.AppendLine("where Usr_Key = @USRKey");
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
            vStringBuilder.AppendLine("insert into Usr_User");
            vStringBuilder.AppendLine("       (Usr_ParentKey, Org_Key, QLF_Key, Usr_UserID, Usr_Password,");
            vStringBuilder.AppendLine("        Usr_Admin, Usr_AutoAuthorize, Usr_CanAuthorize, Usr_Name, Usr_Surname, Usr_Email, Usr_Rate, Usr_Cost,");
            vStringBuilder.AppendLine("        Usr_Identifier, Usr_Mobile, Usr_Phone, Usr_Extension, Usr_Fax, Usr_Status, Usr_StatusDate)");
            vStringBuilder.AppendLine("output inserted.Usr_Key");
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
                vStringBuilder.AppendLine("update Usr_User");
                vStringBuilder.AppendLine("set    Usr_Name = @USRName,");
                vStringBuilder.AppendLine("       Usr_Surname = @USRSurname,");
                vStringBuilder.AppendLine("       Usr_AutoAuthorize = @USRAutoAuthorize,");
                vStringBuilder.AppendLine("       Usr_CanAuthorize = @USRCanAuthorize,");
                vStringBuilder.AppendLine("       Usr_Email = @USREmail,");
                vStringBuilder.AppendLine("       Usr_Rate = @USRRate,");
                vStringBuilder.AppendLine("       Usr_Cost = @USRCost,");
                vStringBuilder.AppendLine("       Usr_Identifier = @USRIdentifier,");
                vStringBuilder.AppendLine("       Usr_Mobile = @USRMobile,");
                vStringBuilder.AppendLine("       Usr_Phone = @USRPhone,");
                vStringBuilder.AppendLine("       Usr_Extension = @USRExtension,");
                vStringBuilder.AppendLine("       Usr_Fax = @USRFax,");
                vStringBuilder.AppendLine("       QLF_Key = @QLFKey,");
                vStringBuilder.AppendLine("       Org_Key = @OrgKey");
                vStringBuilder.AppendLine("where  Usr_Key = @USRKey");

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
                vStringBuilder.AppendLine("delete Usr_User");
                vStringBuilder.AppendLine("and    Usr_Key = @USRKey");
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
