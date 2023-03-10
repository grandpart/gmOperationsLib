using System.Data;
using System.Data.SqlClient;
using System.Text;
using Zephry;

namespace Grandmark
{
    public class WarehouseData
    {
        #region BuildSQL
        private static StringBuilder BuildSql()
        {
            var vStrignBuilder = new StringBuilder();
            vStrignBuilder.AppendLine("SELECT w.EntKey, w.WhsKey, w.WhsName, w.WhsCode, w.WhsIsTradingWarehouse ");
            vStrignBuilder.AppendLine("FROM Warehouse w");
            return vStrignBuilder;
        }
        #endregion

        #region DataToObject
        /// <summary>
        /// Populate object with values read from database
        /// </summary>
        /// <param name="aWarehouse"></param>
        /// <param name="aSqlDataReader"></param>
        private static void DataToObject(Warehouse aWarehouse, SqlDataReader aSqlDataReader)
        {
            aWarehouse.EntKey = Convert.ToInt32(aSqlDataReader["EntKey"]);
            aWarehouse.WhsKey = Convert.ToInt32(aSqlDataReader["Whskey"]);
            aWarehouse.WhsName = Convert.ToString(aSqlDataReader["WhsName"]);
            aWarehouse.WhsCode = Convert.ToString(aSqlDataReader["WhsCode"]);
            aWarehouse.WhsIsTradingWarehouse = Convert.ToBoolean(aSqlDataReader["WhsIsTradingWarehouse"]);
        }
        #endregion

        #region ObjectToData
        /// <summary>
        /// Object to data maps object fields to your sql parameters
        /// </summary>
        /// <param name="aSqlCommand"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aWarehouse"></param>
        private static void ObjectToData(SqlCommand aSqlCommand, UserKey aUserKey, Warehouse aWarehouse)
        {
            aSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
            aSqlCommand.Parameters.AddWithValue("@WhsName", aWarehouse.WhsName);
            aSqlCommand.Parameters.AddWithValue("@WhsCode", aWarehouse.WhsCode);
            aSqlCommand.Parameters.AddWithValue("@WhsIsTradingWarehouse", aWarehouse.WhsIsTradingWarehouse);
        }
        #endregion

        #region Load
        /// <summary>
        /// Load a single record from database
        /// </summary>
        /// <param name="aConnection"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aWarehouse"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Load(Connection aConnection, UserKey aUserKey, Warehouse aWarehouse)
        {
            if (aWarehouse == null)
            {
                throw new ArgumentNullException("aWarehouse");
            }

            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStrignBuilder = BuildSql();
                vStrignBuilder.AppendLine("WHERE w.EntKey = @EntKey");
                vStrignBuilder.AppendLine("AND   w.WhsKey = @WhsKey");
                vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                vSqlCommand.Parameters.AddWithValue("@WhsKey", aWarehouse.WhsKey);
                vSqlCommand.CommandText = vStrignBuilder.ToString();
                vSqlCommand.Connection.Open();
                using (var vSqlDataReader = vSqlCommand.ExecuteReader())
                {
                    if (vSqlDataReader.HasRows)
                    {
                        vSqlDataReader.Read();
                        DataToObject(aWarehouse, vSqlDataReader);
                    }
                    else
                    {
                        //Need to make sure on what to pass back when no record is returned
                        aWarehouse = null;
                    }
                    vSqlDataReader.Close();
                }
                vSqlCommand.Connection.Close();
            }
        }

        #endregion

        #region Insert with a Connection
        public static void Insert(Connection aConnection, UserKey aUserKey, Warehouse aWarehouse)
        {
            if (aWarehouse == null)
            {
                throw new ArgumentNullException(nameof(aWarehouse));
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                vSqlCommand.Connection.Open();
                InsertCommon(vSqlCommand, aUserKey, aWarehouse);
                vSqlCommand.Connection.Close();
            }
        }
        #endregion

        #region Insert with an SqlCommand
        public static void Insert(SqlCommand aSqlCommand, UserKey aUserKey, Warehouse aWarehouse)
        {
            if (aWarehouse == null)
            {
                throw new ArgumentNullException(nameof(aWarehouse));
            }
            InsertCommon(aSqlCommand, aUserKey, aWarehouse);
        }
        #endregion

        #region Insert Common
        private static void InsertCommon(SqlCommand aSqlCommand, UserKey aUserKey, Warehouse aWarehouse)
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("INSERT INTO Warehouse");
            vStringBuilder.AppendLine("       (EntKey, WhsName, WhsCode, WhsIsTradingWarehouse)");
            vStringBuilder.AppendLine("output inserted.Whs_Key");
            vStringBuilder.AppendLine("values");
            vStringBuilder.AppendLine("       (@EntKey, @WhsName, @WhsCode, @WhsIsTradingWarehouse)");
            ObjectToData(aSqlCommand, aUserKey, aWarehouse);
            aSqlCommand.CommandText = vStringBuilder.ToString();
            aWarehouse.WhsKey = Convert.ToInt32(aSqlCommand.ExecuteScalar());
        }
        #endregion

        #region Update
        public static void Update(Connection aConnection, UserKey aUserKey, Warehouse aWarehouse)
        {
            if (aWarehouse == null)
            {
                throw new ArgumentNullException("aWarehouse");
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStringBuilder = new StringBuilder();
                vStringBuilder.AppendLine("UPDATE Warehouse");
                vStringBuilder.AppendLine("set    WhsName = @WhsName,");
                vStringBuilder.AppendLine("       WhsCode = @WhsCode,");
                vStringBuilder.AppendLine("       WhsIsTradingWarehouse = @WhsIsTradingWarehouse");
                vStringBuilder.AppendLine("where  EntKey = @EntKey");
                vStringBuilder.AppendLine("and    WhsKey = @WhsKey");
                ObjectToData(vSqlCommand, aUserKey, aWarehouse);
                vSqlCommand.Parameters.AddWithValue("@WhsKey", aWarehouse.WhsKey);
                vSqlCommand.CommandText = vStringBuilder.ToString();
                vSqlCommand.Connection.Open();
                vSqlCommand.ExecuteNonQuery();
                vSqlCommand.Connection.Close();
            }
        }
        #endregion

        #region Delete
        public static void Delete(Connection aConnection, UserKey aUserKey, WarehouseKey aWarehouseKey)
        {
            if (aWarehouseKey == null)
            {
                throw new ArgumentNullException(nameof(aWarehouseKey));
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
                    vStringBuilder.AppendLine("delete Warehouse");
                    vStringBuilder.AppendLine("where  EntKey = @EntKey");
                    vStringBuilder.AppendLine("and    WhsKey = @WhsKey");
                    vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                    vSqlCommand.Parameters.AddWithValue("@WhsKey", aWarehouseKey.WhsKey);
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
