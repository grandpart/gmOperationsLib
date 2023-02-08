using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Zephry;

namespace Grandmark
{
    /// <summary>
    ///   OrganizationData class.
    /// </summary>
    public class OrganizationData
    {
        #region BuildSQL

        /// <summary>
        ///   A standard SQL Statement that will return all <see cref="Organization"/>, unfiltered and unsorted.
        /// </summary>
        /// <returns>A <see cref="StringBuilder"/></returns>
        private static StringBuilder BuildSql()
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("select org.EntKey, org.OrgKey, org.EntKeyParent, org.OrgKeyParent,");
            vStringBuilder.AppendLine("       org.OrgName, isnull(own.OrgName, '-- none --') as OrgNameParent");
            vStringBuilder.AppendLine("from Organization org left outer join Organization own");
            vStringBuilder.AppendLine("                          on  org.EntKeyParent = own.EntKey");
            vStringBuilder.AppendLine("                          and org.OrgKeyParent = own.OrgKey");
            return vStringBuilder;
        }

        #endregion

        #region DataToObject

        /// <summary>
        ///   Load a <see cref="SqlDataReader"/> into a <see cref="Organization"/> object.
        /// </summary>
        /// <param name="aOrganization">A <see cref="Organization"/> argument.</param>
        /// <param name="aSqlDataReader">A <see cref="SqlDataReader"/> argument.</param>
        public static void DataToObject(Organization aOrganization, SqlDataReader aSqlDataReader)
        {
            aOrganization.OrgKey = Convert.ToInt32(aSqlDataReader["OrgKey"]);
            aOrganization.EntKeyParent = aSqlDataReader["EntKeyParent"] as int?;
            aOrganization.OrgKeyParent = aSqlDataReader["OrgKeyParent"] as int?;
            aOrganization.OrgName = Convert.ToString(aSqlDataReader["OrgName"]);
            aOrganization.OrgNameParent = Convert.ToString(aSqlDataReader["OrgNameParent"]);
        }

        #endregion

        #region ObjectToData
        public static void ObjectToData(SqlCommand aSqlCommand, UserKey aUserKey, Organization aOrganization)
        {
            aSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
            if (aOrganization.OrgKeyParent == null || aOrganization.OrgKeyParent < 1)
            {
                aSqlCommand.Parameters.AddWithValue("@EntKeyParent", DBNull.Value);
                aSqlCommand.Parameters.AddWithValue("@OrgKeyParent", DBNull.Value);
            }
            else
            {
                aSqlCommand.Parameters.AddWithValue("@EntKeyParent", aUserKey.EntKey);
                aSqlCommand.Parameters.AddWithValue("@OrgKeyParent", aOrganization.OrgKeyParent);
            }
            aSqlCommand.Parameters.AddWithValue("@OrgName", aOrganization.OrgName);
        }
        #endregion

        #region Load
        public static void Load(Connection aConnection, UserKey aUserKey, Organization aOrganization)
        {
            if (aOrganization == null)
            {
                throw new ArgumentNullException("aOrganization");
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStringBuilder = BuildSql();
                vStringBuilder.AppendLine("where org.EntKey = @EntKey");
                vStringBuilder.AppendLine("and   org.OrgKey = @OrgKey");
                vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                vSqlCommand.Parameters.AddWithValue("@OrgKey", aOrganization.OrgKey);
                vSqlCommand.CommandText = vStringBuilder.ToString();
                vSqlCommand.Connection.Open();
                using (var vSqlDataReader = vSqlCommand.ExecuteReader())
                {
                    if (vSqlDataReader.HasRows)
                    {
                        vSqlDataReader.Read();
                        DataToObject(aOrganization, vSqlDataReader);
                    }
                    else
                    {
                        aOrganization.OrgNameParent = "-- none --";
                    }
                    vSqlDataReader.Close();
                }
                vSqlCommand.Connection.Close();
            }
        }
        #endregion

        #region Insert with a Connection
        public static void Insert(Connection aConnection, UserKey aUserKey, Organization aOrganization)
        {
            if (aOrganization == null)
            {
                throw new ArgumentNullException(nameof(aOrganization));
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                vSqlCommand.Connection.Open();
                InsertCommon(vSqlCommand, aUserKey, aOrganization);
                vSqlCommand.Connection.Close();
            }
        }
        #endregion

        #region Insert with an SqlCommand
        public static void Insert(SqlCommand aSqlCommand, UserKey aUserKey, Organization aOrganization)
        {
            if (aOrganization == null)
            {
                throw new ArgumentNullException(nameof(aOrganization));
            }
            InsertCommon(aSqlCommand, aUserKey, aOrganization);
        }
        #endregion

        #region Insert Common
        private static void InsertCommon(SqlCommand aSqlCommand, UserKey aUserKey, Organization aOrganization)
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("insert into Organization");
            vStringBuilder.AppendLine("       (EntKey, EntKeyParent, OrgKeyParent, OrgName)");
            vStringBuilder.AppendLine("output inserted.OrgKey");
            vStringBuilder.AppendLine("values");
            vStringBuilder.AppendLine("       (@EntKey, @EntKeyParent, @OrgKeyParent, @OrgName)");
            ObjectToData(aSqlCommand, aUserKey, aOrganization);
            aSqlCommand.CommandText = vStringBuilder.ToString();
            aOrganization.OrgKey = Convert.ToInt32(aSqlCommand.ExecuteScalar());
        }
        #endregion

        #region Update
        public static void Update(Connection aConnection, UserKey aUserKey, Organization aOrganization)
        {
            if (aOrganization == null)
            {
                throw new ArgumentNullException("aOrganization");
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStringBuilder = new StringBuilder();
                vStringBuilder.AppendLine("update Organization");
                vStringBuilder.AppendLine("set    OrgKeyParent = @OrgKeyParent,");
                vStringBuilder.AppendLine("       OrgName = @OrgName");
                vStringBuilder.AppendLine("where  EntKey = @EntKey");
                vStringBuilder.AppendLine("and    OrgKey = @OrgKey");
                ObjectToData(vSqlCommand, aUserKey, aOrganization);
                vSqlCommand.Parameters.AddWithValue("@OrgKey", aOrganization.OrgKey);
                vSqlCommand.CommandText = vStringBuilder.ToString();
                vSqlCommand.Connection.Open();
                vSqlCommand.ExecuteNonQuery();
                vSqlCommand.Connection.Close();
            }
        }
        #endregion

        #region Delete
        public static void Delete(Connection aConnection, UserKey aUserKey, OrganizationKey aOrganizationKey)
        {
            if (aOrganizationKey == null)
            {
                throw new ArgumentNullException(nameof(aOrganizationKey));
            }
            try
            {
                using (var vSqlCommand = new SqlCommand()
                {
                    CommandType = CommandType.Text,
                    Connection = new SqlConnection(aConnection.SqlConnectionString)
                })
                {
                    var vStringBuilder = new StringBuilder();
                    vStringBuilder.AppendLine("delete Organization");
                    vStringBuilder.AppendLine("where  EntKey = @EntKey");
                    vStringBuilder.AppendLine("and    OrgKey = @OrgKey");
                    vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                    vSqlCommand.Parameters.AddWithValue("@OrgKey", aOrganizationKey.OrgKey);
                    vSqlCommand.CommandText = vStringBuilder.ToString();
                    vSqlCommand.Connection.Open();
                    vSqlCommand.ExecuteNonQuery();
                    vSqlCommand.Connection.Close();
                }
            }
            catch (SqlException sx)
            {
                if (sx.Number == CommonConstants.DeleteReferenceNumber)
                {
                    throw new TransactionStatusException(TransactionResult.Delete,
                        string.Format(CommonConstants.DeleteReferenceMessage, sx.HResult));
                }
                throw;
            }
        }
        #endregion
     }
 }
