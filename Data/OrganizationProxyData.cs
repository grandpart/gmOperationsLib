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
    ///   OrganizationProxyData class.
    /// </summary>
    public class OrganizationProxyData
    {
        #region BuildSQL

        /// <summary>
        ///   A standard SQL Statement that will return all <see cref="OrganizationProxy"/>, unfiltered and unsorted.
        /// </summary>
        /// <returns>A <see cref="StringBuilder"/></returns>
        private static StringBuilder BuildSql()
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("select Ent_Key, Org_Key, Ent_KeyParent, Org_KeyParent, Org_Name");
            vStringBuilder.AppendLine("from   Organization");
            return vStringBuilder;
        }

        #endregion

        #region DataToObject

        /// <summary>
        ///   Load a <see cref="SqlDataReader"/> into a <see cref="OrganizationProxy"/> object.
        /// </summary>
        /// <param name="aOrganizationProxy">A <see cref="OrganizationProxy"/> argument.</param>
        /// <param name="aSqlDataReader">A <see cref="SqlDataReader"/> argument.</param>
        public static void DataToObject(OrganizationProxy aOrganizationProxy, SqlDataReader aSqlDataReader)
        {
            aOrganizationProxy.EntKey = Convert.ToInt32(aSqlDataReader["Ent_Key"]);
            aOrganizationProxy.OrgKey = Convert.ToInt32(aSqlDataReader["Org_Key"]);
            aOrganizationProxy.SubKeyParent = aSqlDataReader["Ent_KeyParent"] as int?;
            aOrganizationProxy.OrgKeyParent = aSqlDataReader["Org_KeyParent"] as int?;
            aOrganizationProxy.OrgName = Convert.ToString(aSqlDataReader["Org_Name"]) ?? string.Empty;
        }
        #endregion

        #region Load ItemCollection with Connection
        public static void Load(Connection aConnection, UserKey aUserKey, OrganizationProxyCollection aOrganizationProxyCollection)
        {
            if (aOrganizationProxyCollection == null)
            {
                throw new ArgumentNullException("aOrganizationProxyCollection");
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                vSqlCommand.Connection.Open();
                LoadCommon(vSqlCommand, aUserKey, aOrganizationProxyCollection);
                vSqlCommand.Connection.Close();
            }
        }

        #endregion

        #region Load ItemCollection with an SqlCommand
        public static void Load(SqlCommand aSqlCommand, UserKey aUserKey, OrganizationProxyCollection aOrganizationProxyCollection)
        {
            if (aOrganizationProxyCollection == null)
            {
                throw new ArgumentNullException("aOrganizationProxyCollection");
            }
            LoadCommon(aSqlCommand, aUserKey, aOrganizationProxyCollection);
        }
        #endregion

        #region Load ItemCollection Common
        public static void LoadCommon(SqlCommand aSqlCommand, UserKey aUserKey, OrganizationProxyCollection aOrganizationProxyCollection)
        {
            // Create a lookup dictionary
            var vKeyMap = new Dictionary<int?, OrganizationProxy>();

            // Get a flat list of OrganizationProxy for the collection and the dictionary
            var vStringBuilder = BuildSql();
            vStringBuilder.AppendLine("where Ent_Key = @EntKey");
            vStringBuilder.AppendLine("order by Org_Name");
            aSqlCommand.Parameters.Clear();
            aSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
            aSqlCommand.CommandText = vStringBuilder.ToString();
            using (SqlDataReader vSqlDataReader = aSqlCommand.ExecuteReader())
            {
                while (vSqlDataReader.Read())
                {
                    var vOrganizationProxy = new OrganizationProxy();
                    DataToObject(vOrganizationProxy, vSqlDataReader);
                    aOrganizationProxyCollection.OrganizationProxyList.Add(vOrganizationProxy);
                    vKeyMap.Add(vOrganizationProxy.OrgKey, vOrganizationProxy);
                }
                vSqlDataReader.Close();
            }

            // Convert to tree, unless Flat was requested
            if (aOrganizationProxyCollection.Flat) return;
            foreach (var vOrganizationProxy in vKeyMap.Values)
            {
                if (vOrganizationProxy.OrgKeyParent != null)
                {
                    vKeyMap[vOrganizationProxy.OrgKeyParent].OrganizationProxyList.Add(vOrganizationProxy);
                }
            }
            aOrganizationProxyCollection.OrganizationProxyList.RemoveAll(item => item.OrgKeyParent != null);
        }
        #endregion
    }
}
