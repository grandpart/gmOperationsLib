using System.Data.SqlClient;
using System.Data;
using System.Text;
using Zephry;

namespace Grandmark
{
    public class TicketPriorityCollectionData
    {
        #region BuildSQL
        private static StringBuilder BuildSql()
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("select Ent_Key, Tpr_Key, Tpr_Name,  Tpr_Priority, Tpr_Class");
            vStringBuilder.AppendLine("from   TicketPriority");
            return vStringBuilder;
        }

        #endregion

        #region DataToObject
        public static void DataToObject(TicketPriority aTicketPriority, SqlDataReader aSqlDataReader)
        {
            aTicketPriority.EntKey = Convert.ToInt32(aSqlDataReader["Ent_Key"]);
            aTicketPriority.TprKey = Convert.ToInt32(aSqlDataReader["Tpr_Key"]);
            aTicketPriority.TprName = Convert.ToString(aSqlDataReader["Tpr_Name"]);
            aTicketPriority.TprPriority = Convert.ToInt32(aSqlDataReader["Tpr_Priority"]);
            aTicketPriority.TprClass = Convert.ToString(aSqlDataReader["Tpr_Class"]) ?? string.Empty;
        }
        #endregion

        #region Load ItemCollection with Connection
        public static void Load(Connection aConnection, UserKey aUserKey, TicketPriorityCollection aTicketPriorityCollection)
        {
            if (aTicketPriorityCollection == null)
            {
                throw new ArgumentNullException("aTicketPriorityCollection");
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                vSqlCommand.Connection.Open();
                LoadCommon(vSqlCommand, aUserKey, aTicketPriorityCollection);
                vSqlCommand.Connection.Close();
            }
        }

        #endregion

        #region Load ItemCollection with an SqlCommand
        public static void Load(SqlCommand aSqlCommand, UserKey aUserKey, TicketPriorityCollection aTicketPriorityCollection)
        {
            if (aTicketPriorityCollection == null)
            {
                throw new ArgumentNullException("aTicketPriorityCollection");
            }
            LoadCommon(aSqlCommand, aUserKey, aTicketPriorityCollection);
        }
        #endregion

        #region Load ItemCollection Common
        public static void LoadCommon(SqlCommand aSqlCommand, UserKey aUserKey, TicketPriorityCollection aTicketPriorityCollection)
        {
            // Get a flat list of OrganizationProxy for the collection and the dictionary
            var vStringBuilder = BuildSql();
            vStringBuilder.AppendLine("where Ent_Key = @EntKey");
            vStringBuilder.AppendLine("order by Tpr_Key");
            aSqlCommand.Parameters.Clear();
            aSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
            aSqlCommand.CommandText = vStringBuilder.ToString();
            using (SqlDataReader vSqlDataReader = aSqlCommand.ExecuteReader())
            {
                while (vSqlDataReader.Read())
                {
                    var VTicketPriority = new TicketPriority();
                    DataToObject(VTicketPriority, vSqlDataReader);
                    aTicketPriorityCollection.TicketPriorityList.Add(VTicketPriority);
                }
                vSqlDataReader.Close();
            }
        }
        #endregion
    }
}
