using System.Data;
using System.Data.SqlClient;
using System.Text;
using Zephry;

namespace Grandmark
{
    public class WarehouseCollectionData
    {
        #region BuildSQL
        private static StringBuilder BuildSql()
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("select w.Ent_Key, w.Whs_Key, w.Whs_Name,  w.Whs_Code, w.Whs_IsTradingWarehouse");
            vStringBuilder.AppendLine("from   Warehouse w");
            return vStringBuilder;
        }

        #endregion

        #region DataToObject
        public static void DataToObject(Warehouse aWarehouse, SqlDataReader aSqlDataReader)
        {
            aWarehouse.EntKey = Convert.ToInt32(aSqlDataReader["Ent_Key"]);
            aWarehouse.WhsKey = Convert.ToInt32(aSqlDataReader["Whs_Key"]);
            aWarehouse.WhsName = Convert.ToString(aSqlDataReader["Whs_Name"]);
            aWarehouse.WhsCode = Convert.ToString(aSqlDataReader["Whs_Code"]);
            aWarehouse.WhsIsTradingWarehouse = Convert.ToBoolean(aSqlDataReader["Whs_IsTradingWarehouse"]);
        }
        #endregion

        #region Load ItemCollection with Connection
        public static void Load(Connection aConnection, UserKey aUserKey, WarehouseCollection aWarehouseCollection)
        {
            if (aWarehouseCollection == null)
            {
                throw new ArgumentNullException("aWarehouseCollection");
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                vSqlCommand.Connection.Open();
                LoadCommon(vSqlCommand, aUserKey, aWarehouseCollection);
                vSqlCommand.Connection.Close();
            }
        }

        #endregion

        #region Load ItemCollection with an SqlCommand
        public static void Load(SqlCommand aSqlCommand, UserKey aUserKey, WarehouseCollection aWarehouseCollection)
        {
            if (aWarehouseCollection == null)
            {
                throw new ArgumentNullException("aWarehouseCollection");
            }
            LoadCommon(aSqlCommand, aUserKey, aWarehouseCollection);
        }
        #endregion

        #region Load ItemCollection Common
        public static void LoadCommon(SqlCommand aSqlCommand, UserKey aUserKey, WarehouseCollection aWarehouseCollection)
        {
            // Get a flat list of OrganizationProxy for the collection and the dictionary
            var vStringBuilder = BuildSql();
            vStringBuilder.AppendLine("where w.Ent_Key = @EntKey");
            vStringBuilder.AppendLine("order by w.Whs_Key");
            aSqlCommand.Parameters.Clear();
            aSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
            aSqlCommand.CommandText = vStringBuilder.ToString();
            using (SqlDataReader vSqlDataReader = aSqlCommand.ExecuteReader())
            {
                while (vSqlDataReader.Read())
                {
                    var vWarehouse = new Warehouse();
                    DataToObject(vWarehouse, vSqlDataReader);
                    aWarehouseCollection.WarehouseList.Add(vWarehouse);
                }
                vSqlDataReader.Close();
            }
        }
        #endregion
    }
}
