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
    ///   UserProxyData class.
    /// </summary>
    public class UserProxyData
    {
        #region BuildSQL

        /// <summary>
        ///   A standard SQL Statement that will return all <see cref="UserProxy"/>, unfiltered and unsorted.
        /// </summary>
        /// <returns>A <see cref="StringBuilder"/></returns>
        private static StringBuilder BuildSql()
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("select EntKey, UsrKey, EntKeyParent, UsrKeyParent, UsrName, UsrSurname, UsrEmail");
            vStringBuilder.AppendLine("from   SysUser");
            return vStringBuilder;
        }

        #endregion

        #region DataToObject

        /// <summary>
        ///   Load a <see cref="SqlDataReader"/> into a <see cref="UserProxy"/> object.
        /// </summary>
        /// <param name="aUserProxy">A <see cref="UserProxy"/> argument.</param>
        /// <param name="aSqlDataReader">A <see cref="SqlDataReader"/> argument.</param>
        public static void DataToObject(UserProxy aUserProxy, SqlDataReader aSqlDataReader)
        {
            aUserProxy.EntKey = Convert.ToInt32(aSqlDataReader["EntKey"]);
            aUserProxy.UsrKey = Convert.ToInt32(aSqlDataReader["UsrKey"]);
            aUserProxy.EntKeyParent = aSqlDataReader["EntKeyParent"] as int?;
            aUserProxy.UsrKeyParent = aSqlDataReader["UsrKeyParent"] as int?;
            aUserProxy.UsrName = Convert.ToString(aSqlDataReader["UsrName"]) ?? string.Empty;
            aUserProxy.UsrSurname = Convert.ToString(aSqlDataReader["UsrSurname"]) ?? string.Empty;
            aUserProxy.UsrEmail = Convert.ToString(aSqlDataReader["UsrEmail"]) ?? string.Empty;
        }
        #endregion

        #region Load ItemCollection with Connection
        public static void Load(Connection aConnection, UserKey aUserKey, UserProxyCollection aUserProxyCollection)
        {
            if (aUserProxyCollection == null)
            {
                throw new ArgumentNullException(nameof(aUserProxyCollection));
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                vSqlCommand.Connection.Open();
                LoadCommon(vSqlCommand, aUserKey, aUserProxyCollection);
                vSqlCommand.Connection.Close();
            }
        }

        #endregion

        #region Load ItemCollection with an SqlCommand
        public static void Load(SqlCommand aSqlCommand, UserKey aUserKey, UserProxyCollection aUserProxyCollection)
        {
            if (aUserProxyCollection == null)
            {
                throw new ArgumentNullException("aUserProxyCollection");
            }
            LoadCommon(aSqlCommand, aUserKey, aUserProxyCollection);
        }
        #endregion

        #region Load ItemCollection Common
        public static void LoadCommon(SqlCommand aSqlCommand, UserKey aUserKey, UserProxyCollection aUserProxyCollection)
        {
            // Create a lookup dictionary
            var vKeyMap = new Dictionary<int?, UserProxy>();

            // Get a flat list of UserProxy for the collection and the dictionary
            var vStringBuilder = BuildSql();
            vStringBuilder.AppendLine("where EntKey = @EntKey");
            vStringBuilder.AppendLine("order by UsrName");
            aSqlCommand.Parameters.Clear();
            aSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
            aSqlCommand.CommandText = vStringBuilder.ToString();
            using (SqlDataReader vSqlDataReader = aSqlCommand.ExecuteReader())
            {
                while (vSqlDataReader.Read())
                {
                    var vUserProxy = new UserProxy();
                    DataToObject(vUserProxy, vSqlDataReader);
                    aUserProxyCollection.List.Add(vUserProxy);
                    vKeyMap.Add(vUserProxy.UsrKey, vUserProxy);
                }
                vSqlDataReader.Close();
            }

            // Convert to tree, unless Flat was requested
            if (aUserProxyCollection.Flat) return;
            foreach (var vUserProxy in vKeyMap.Values)
            {
                if (vUserProxy.UsrKeyParent != null)
                {
                    vKeyMap[vUserProxy.UsrKeyParent].List.Add(vUserProxy);
                }
            }
            aUserProxyCollection.List.RemoveAll(item => item.UsrKeyParent != null);
        }
        #endregion
    }
}
