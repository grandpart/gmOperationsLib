using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Zephry;

namespace Grandmark
{
    public class TicketTypeData
    {

        #region BuildSQL
        private static StringBuilder BuildSql()
        {
            var vStrignBuilder = new StringBuilder();
            vStrignBuilder.AppendLine("select ttp.EntKey, ttp.TtpKey, ttp.TtpName, ttp.TtpClass");
            vStrignBuilder.AppendLine("from TicketType ttp");
            return vStrignBuilder;
        }
        #endregion

        #region DataToObject
        /// <summary>
        /// Populate object with values read from database
        /// </summary>
        /// <param name="aTicketType"></param>
        /// <param name="aSqlDataReader"></param>
        private static void DataToObject(TicketType aTicketType, SqlDataReader aSqlDataReader)
        {
            aTicketType.EntKey = Convert.ToInt32(aSqlDataReader["EntKey"]);
            aTicketType.TtpKey = Convert.ToInt32(aSqlDataReader["TtpKey"]);
            aTicketType.TtpName = Convert.ToString(aSqlDataReader["TtpName"]);
            aTicketType.TtpClass = Convert.ToString(aSqlDataReader["TtpClass"]);
        }
        #endregion

        #region ObjectToData
        /// <summary>
        /// Object to data maps object fields to your sql parameters
        /// </summary>
        /// <param name="aSqlCommand"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aTicketType"></param>
        private static void ObjectToData(SqlCommand aSqlCommand, UserKey aUserKey, TicketType aTicketType)
        {
            aSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
            aSqlCommand.Parameters.AddWithValue("@TtpName", aTicketType.TtpName);
            aSqlCommand.Parameters.AddWithValue("@TtpClass", aTicketType.TtpClass);
        }
        #endregion

        #region Load
        public static void Load(Connection aConnection, UserKey aUserKey, TicketType aTicketType)
        {
            if (aTicketType == null)
            {
                throw new ArgumentNullException("aTicketType");
            }

            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStrignBuilder = BuildSql();
                vStrignBuilder.AppendLine("where ttp.EntKey = @EntKey");
                vStrignBuilder.AppendLine("and   ttp.TtpKey = @TtpKey");
                vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                vSqlCommand.Parameters.AddWithValue("@TtpKey", aTicketType.TtpKey);
                vSqlCommand.CommandText = vStrignBuilder.ToString();
                vSqlCommand.Connection.Open();
                using (var vSqlDataReader = vSqlCommand.ExecuteReader())
                {
                    if (vSqlDataReader.HasRows)
                    {
                        vSqlDataReader.Read();
                        DataToObject(aTicketType, vSqlDataReader);
                    }
                    vSqlDataReader.Close();
                }
                vSqlCommand.Connection.Close();
            }
        }

        #endregion

        #region Load Collection
        public static void Load(Connection aConnection, UserKey aUserKey, TicketTypeCollection aTicketTypeCollection)
        {
            if (aTicketTypeCollection == null)
            {
                throw new ArgumentNullException(nameof(aTicketTypeCollection));
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStringBuilder = BuildSql();
                vStringBuilder.AppendLine("where EntKey = @EntKey");
                vStringBuilder.AppendLine("order by TtpName");
                vSqlCommand.Parameters.Clear();
                vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                vSqlCommand.CommandText = vStringBuilder.ToString();
                vSqlCommand.Connection.Open();
                using (SqlDataReader vSqlDataReader = vSqlCommand.ExecuteReader())
                {
                    while (vSqlDataReader.Read())
                    {
                        var VTicketType = new TicketType();
                        DataToObject(VTicketType, vSqlDataReader);
                        aTicketTypeCollection.TicketTypeList.Add(VTicketType);
                    }
                    vSqlDataReader.Close();
                }
                vSqlCommand.Connection.Close();
            }
        }
        #endregion

        #region Load KeyValue
        public static void Load(SqlCommand aSqlCommand, UserKey aUserKey, List<KeyValue> aKeyValueCollection)
        {
            if (aKeyValueCollection == null)
            {
                throw new ArgumentNullException(nameof(aKeyValueCollection));
            }
            {
                var vStringBuilder = new StringBuilder();
                vStringBuilder.AppendLine("select TtpKey, TtpName");
                vStringBuilder.AppendLine("from TicketType");
                vStringBuilder.AppendLine("where EntKey = @EntKey");
                vStringBuilder.AppendLine("order by TtpName");
                aSqlCommand.Parameters.Clear();
                aSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                aSqlCommand.CommandText = vStringBuilder.ToString();
                using (SqlDataReader vSqlDataReader = aSqlCommand.ExecuteReader())
                {
                    while (vSqlDataReader.Read())
                    {
                        var vKeyValue = new KeyValue
                        {
                            Key = Convert.ToInt32(vSqlDataReader["TtpKey"]),
                            Value = Convert.ToString(vSqlDataReader["TtpName"])
                        };
                        aKeyValueCollection.Add(vKeyValue);
                    }
                    vSqlDataReader.Close();
                }
            }
        }
        #endregion

        #region Insert
        public static void Insert(Connection aConnection, UserKey aUserKey, TicketType aTicketType)
        {
            if (aTicketType == null)
            {
                throw new ArgumentNullException(nameof(aTicketType));
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStringBuilder = new StringBuilder();
                vStringBuilder.AppendLine("insert into TicketType");
                vStringBuilder.AppendLine("    (EntKey, TtpName, TtpClass)");
                vStringBuilder.AppendLine("output inserted.TtpKey");
                vStringBuilder.AppendLine("values");
                vStringBuilder.AppendLine("    (@EntKey, @TtpName, @TtpClass)");
                ObjectToData(vSqlCommand, aUserKey, aTicketType);
                vSqlCommand.Connection.Open();
                vSqlCommand.CommandText = vStringBuilder.ToString();
                aTicketType.TtpKey = Convert.ToInt32(vSqlCommand.ExecuteScalar());
                vSqlCommand.Connection.Close();
            }
        }
        #endregion

        #region Update
        public static void Update(Connection aConnection, UserKey aUserKey, TicketType aTicketType)
        {
            if (aTicketType == null)
            {
                throw new ArgumentNullException("aTicketType");
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStringBuilder = new StringBuilder();
                vStringBuilder.AppendLine("UPDATE TicketType");
                vStringBuilder.AppendLine("set    TtpName = @TtpName,");
                vStringBuilder.AppendLine("       TtpClass = @TtpClass");
                vStringBuilder.AppendLine("where  EntKey = @EntKey");
                vStringBuilder.AppendLine("and    TtpKey = @TtpKey");
                ObjectToData(vSqlCommand, aUserKey, aTicketType);
                vSqlCommand.Parameters.AddWithValue("@TtpKey", aTicketType.TtpKey);
                vSqlCommand.CommandText = vStringBuilder.ToString();
                vSqlCommand.Connection.Open();
                vSqlCommand.ExecuteNonQuery();
                vSqlCommand.Connection.Close();
            }
        }
        #endregion

        #region Delete
        public static void Delete(Connection aConnection, UserKey aUserKey, TicketType aTicketType)
        {
            if (aTicketType == null)
            {
                throw new ArgumentNullException(nameof(aTicketType));
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
                    vStringBuilder.AppendLine("delete TicketType");
                    vStringBuilder.AppendLine("where  EntKey = @EntKey");
                    vStringBuilder.AppendLine("and    TtpKey = @TtpKey");
                    vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                    vSqlCommand.Parameters.AddWithValue("@TtpKey", aTicketType.TtpKey);
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
