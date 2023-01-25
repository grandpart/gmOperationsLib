using System.Data.SqlClient;
using System.Data;
using System.Text;
using Zephry;

namespace Grandmark
{
    public class BranchCollectionData
    {
        #region BuildSQL
        private static StringBuilder BuildSql()
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("select b.Ent_Key, b.Brh_Key, b.Brh_Name,  b.Brh_Code");
            vStringBuilder.AppendLine("from   Branch b");
            return vStringBuilder;
        }

        #endregion

        #region DataToObject
        public static void DataToObject(Branch aBranch, SqlDataReader aSqlDataReader)
        {
            aBranch.EntKey = Convert.ToInt32(aSqlDataReader["Ent_Key"]);
            aBranch.BrhKey = Convert.ToInt32(aSqlDataReader["Brh_Key"]);
            aBranch.BrhName = Convert.ToString(aSqlDataReader["Brh_Name"]);
            aBranch.BrhCode = Convert.ToString(aSqlDataReader["Brh_Code"]);
        }
        #endregion

        #region Load ItemCollection with Connection
        public static void Load(Connection aConnection, UserKey aUserKey, BranchCollection aBranchCollection)
        {
            if (aBranchCollection == null)
            {
                throw new ArgumentNullException("aBranchCollection");
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                vSqlCommand.Connection.Open();
                LoadCommon(vSqlCommand, aUserKey, aBranchCollection);
                vSqlCommand.Connection.Close();
            }
        }

        #endregion

        #region Load ItemCollection with an SqlCommand
        public static void Load(SqlCommand aSqlCommand, UserKey aUserKey, BranchCollection aBranchCollection)
        {
            if (aBranchCollection == null)
            {
                throw new ArgumentNullException("aBranchCollection");
            }
            LoadCommon(aSqlCommand, aUserKey, aBranchCollection);
        }
        #endregion

        #region Load ItemCollection Common
        public static void LoadCommon(SqlCommand aSqlCommand, UserKey aUserKey, BranchCollection aBranchCollection)
        {
            // Get a flat list of OrganizationProxy for the collection and the dictionary
            var vStringBuilder = BuildSql();
            vStringBuilder.AppendLine("where b.Ent_Key = @EntKey");
            vStringBuilder.AppendLine("order by b.Brh_Key");
            aSqlCommand.Parameters.Clear();
            aSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
            aSqlCommand.CommandText = vStringBuilder.ToString();
            using (SqlDataReader vSqlDataReader = aSqlCommand.ExecuteReader())
            {
                while (vSqlDataReader.Read())
                {
                    var vBranch = new Branch();
                    DataToObject(vBranch, vSqlDataReader);
                    aBranchCollection.BranchList.Add(vBranch);
                }
                vSqlDataReader.Close();
            }
        }
        #endregion
    }
}
