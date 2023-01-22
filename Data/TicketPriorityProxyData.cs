using System.Data;
using System.Data.SqlClient;
using System.Text;
using Zephry;

namespace Grandmark
{
    public class TicketPriorityProxyData
    {
        #region BuildSQL

        /// <summary>
        ///   A standard SQL Statement that will return all <see cref="TicketPriorityProxy"/>, unfiltered and unsorted.
        /// </summary>
        /// <returns>A <see cref="StringBuilder"/></returns>
        private static StringBuilder BuildSql()
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("select Ent_Key, Tpr_Key, Tpr_Name, Tpr_Priority, Tpr_Class");
            vStringBuilder.AppendLine("from   TicketPriority");
            return vStringBuilder;
        }

        #endregion

        #region DataToObject

        /// <summary>
        ///   Load a <see cref="SqlDataReader"/> into a <see cref="TicketPriorityProxy"/> object.
        /// </summary>
        /// <param name="aTicketPriorityProxy">A <see cref="TicketPriorityProxy"/> argument.</param>
        /// <param name="aSqlDataReader">A <see cref="SqlDataReader"/> argument.</param>
        public static void DataToObject(TicketPriorityProxy aTicketPriorityProxy, SqlDataReader aSqlDataReader)
        {
            aTicketPriorityProxy.EntKey = Convert.ToInt32(aSqlDataReader["Ent_Key"]);
            aTicketPriorityProxy.TprKey = Convert.ToInt32(aSqlDataReader["Tpr_Key"]);
            aTicketPriorityProxy.TprName = Convert.ToString(aSqlDataReader["Tpr_Name"]);
            aTicketPriorityProxy.TprPriority = Convert.ToInt32(aSqlDataReader["Tpr_Priority"]);
            aTicketPriorityProxy.TprClass = Convert.ToString(aSqlDataReader["Tpr_Class"]);
        }
        #endregion

        #region Load ItemCollection with Connection
        public static void Load(Connection aConnection, UserKey aUserKey, TicketPriorityProxyCollection aTicketPriorityProxyCollection)
        {
            if (aTicketPriorityProxyCollection == null)
            {
                throw new ArgumentNullException("aTicketPriorityProxyCollection");
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                vSqlCommand.Connection.Open();
                LoadCommon(vSqlCommand, aUserKey, aTicketPriorityProxyCollection);
                vSqlCommand.Connection.Close();
            }
        }

        #endregion

        #region Load ItemCollection with an SqlCommand
        public static void Load(SqlCommand aSqlCommand, UserKey aUserKey, TicketPriorityProxyCollection aTicketPriorityProxyCollection)
        {
            if (aTicketPriorityProxyCollection == null)
            {
                throw new ArgumentNullException("aTicketPriorityProxyCollection");
            }
            LoadCommon(aSqlCommand, aUserKey, aTicketPriorityProxyCollection);
        }
        #endregion

        #region Load ItemCollection Common
        public static void LoadCommon(SqlCommand aSqlCommand, UserKey aUserKey, TicketPriorityProxyCollection aTicketPriorityProxyCollection)
        {
            // Create a lookup dictionary
            //var vKeyMap = new Dictionary<int?, TicketPriorityProxy>();

            // Get a flat list of OrganizationProxy for the collection and the dictionary
            var vStringBuilder = BuildSql();
            vStringBuilder.AppendLine("where Ent_Key = @EntKey");
            vStringBuilder.AppendLine("order by Tpr_Priority");
            aSqlCommand.Parameters.Clear();
            aSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
            aSqlCommand.CommandText = vStringBuilder.ToString();
            using (SqlDataReader vSqlDataReader = aSqlCommand.ExecuteReader())
            {
                while (vSqlDataReader.Read())
                {
                    var vTicketPriorityProxy = new TicketPriorityProxy();
                    DataToObject(vTicketPriorityProxy, vSqlDataReader);
                    aTicketPriorityProxyCollection.TicketPriorityList.Add(vTicketPriorityProxy);
                    //vKeyMap.Add(vTicketPriorityProxy.OrgKey, vTicketPriorityProxy);
                }
                vSqlDataReader.Close();
            }
            return;
        }
        #endregion
    }
}
