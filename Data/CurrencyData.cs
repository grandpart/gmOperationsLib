using System.Data;
using System.Data.SqlClient;
using System.Text;
using Zephry;

namespace Grandmark
{
    public class CurrencyData
    {
        #region BuildSQL
        private static StringBuilder BuildSql()
        {
            var vStrignBuilder = new StringBuilder();
            vStrignBuilder.AppendLine("SELECT c.EntKey, c.CurKey, c.CurCode, c.CurPrefix, c.CurName");
            vStrignBuilder.AppendLine("FROM Currency c");
            return vStrignBuilder;
        }
        #endregion

        #region DataToObject
        /// <summary>
        /// Populate object with values read from database
        /// </summary>
        /// <param name="aTicketPriority"></param>
        /// <param name="aSqlDataReader"></param>
        private static void DataToObject(Currency aCurrency, SqlDataReader aSqlDataReader)
        {
            aCurrency.EntKey = Convert.ToInt32(aSqlDataReader["EntKey"]);
            aCurrency.CurKey = Convert.ToInt32(aSqlDataReader["CurKey"]);
            aCurrency.CurCode = Convert.ToString(aSqlDataReader["CurCode"]);
            aCurrency.CurPrefix = Convert.ToString(aSqlDataReader["CurPrefix"]);
            aCurrency.CurName = Convert.ToString(aSqlDataReader["CurName"]);
        }
        #endregion

        #region ObjectToData
        /// <summary>
        /// Object to data maps object fields to your sql parameters
        /// </summary>
        /// <param name="aSqlCommand"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aTicketPriority"></param>
        private static void ObjectToData(SqlCommand aSqlCommand, UserKey aUserKey, Currency aCurrency)
        {
            aSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
            aSqlCommand.Parameters.AddWithValue("@CurCode", aCurrency.CurCode);
            aSqlCommand.Parameters.AddWithValue("@CurPrefix", aCurrency.CurPrefix);
            aSqlCommand.Parameters.AddWithValue("@CurName", aCurrency.CurName);
        }
        #endregion

        #region Load
        /// <summary>
        /// Load a single record from database
        /// </summary>
        public static void Load(Connection aConnection, UserKey aUserKey, Currency aCurrency)
        {
            if (aCurrency == null)
            {
                throw new ArgumentNullException("aCurrency");
            }

            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStrignBuilder = BuildSql();
                vStrignBuilder.AppendLine("WHERE c.EntKey = @EntKey");
                vStrignBuilder.AppendLine("AND   c.CurKey = @CurKey");
                vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                vSqlCommand.Parameters.AddWithValue("@CurKey", aCurrency.CurKey);
                vSqlCommand.CommandText = vStrignBuilder.ToString();
                vSqlCommand.Connection.Open();
                using (var vSqlDataReader = vSqlCommand.ExecuteReader())
                {
                    if (vSqlDataReader.HasRows)
                    {
                        vSqlDataReader.Read();
                        DataToObject(aCurrency, vSqlDataReader);
                    }
                    else
                    {
                        //Need to make sure on what to pass back when no record is returned
                        aCurrency = null;
                    }
                    vSqlDataReader.Close();
                }
                vSqlCommand.Connection.Close();
            }
        }

        #endregion

        #region Load List
        /// <summary>
        /// Load a single record from database
        /// </summary>
        /// <param name="aConnection"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aTicketPriority"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Load(Connection aConnection, UserKey aUserKey, CurrencyCollection aCurrencyCollection)
        {
            //if (aTicketPriority == null)
            //{
            //    throw new ArgumentNullException("aTicketPriority");
            //}

            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStrignBuilder = BuildSql();

                //We want the list of priorities for the entity the user is linked to
                vStrignBuilder.AppendLine("WHERE c.EntKey = @EntKey");
                vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);

                vSqlCommand.CommandText = vStrignBuilder.ToString();
                vSqlCommand.Connection.Open();

                using (SqlDataReader vSqlDataReader = vSqlCommand.ExecuteReader())
                {
                    while (vSqlDataReader.Read())
                    {
                        var vCurrency = new Currency();
                        DataToObject(vCurrency, vSqlDataReader);
                        aCurrencyCollection.List.Add(vCurrency);

                    }

                    vSqlDataReader.Close();
                }
                vSqlCommand.Connection.Close();
            }
        }

        #endregion

        #region Insert with a Connection
        public static void Insert(Connection aConnection, UserKey aUserKey, Currency aCurrency)
        {
            if (aCurrency == null)
            {
                throw new ArgumentNullException(nameof(aCurrency));
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                vSqlCommand.Connection.Open();
                InsertCommon(vSqlCommand, aUserKey, aCurrency);
                vSqlCommand.Connection.Close();
            }
        }
        #endregion

        #region Insert with an SqlCommand
        public static void Insert(SqlCommand aSqlCommand, UserKey aUserKey, Currency aCurrency)
        {
            if (aCurrency == null)
            {
                throw new ArgumentNullException(nameof(aCurrency));
            }
            InsertCommon(aSqlCommand, aUserKey, aCurrency);
        }
        #endregion

        #region Insert Common
        private static void InsertCommon(SqlCommand aSqlCommand, UserKey aUserKey, Currency aCurrency)
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("INSERT INTO Currency");
            vStringBuilder.AppendLine("       (EntKey, CurCode, CurPrefix, CurName)");
            vStringBuilder.AppendLine("output inserted.CurKey");
            vStringBuilder.AppendLine("values");
            vStringBuilder.AppendLine("       (@EntKey, @CurCode, @CurPrefix, @CurName)");
            ObjectToData(aSqlCommand, aUserKey, aCurrency);
            aSqlCommand.CommandText = vStringBuilder.ToString();
            aCurrency.CurKey = Convert.ToInt32(aSqlCommand.ExecuteScalar());
        }
        #endregion

        #region Update
        public static void Update(Connection aConnection, UserKey aUserKey, Currency aCurrency)
        {
            if (aCurrency == null)
            {
                throw new ArgumentNullException("aCurrency");
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStringBuilder = new StringBuilder();
                vStringBuilder.AppendLine("UPDATE Currency");
                vStringBuilder.AppendLine("set    CurCode = @CurCode,");
                vStringBuilder.AppendLine("       CurPrefix = @CurPrefix,");
                vStringBuilder.AppendLine("       CurName = @CurName");
                vStringBuilder.AppendLine("where  EntKey = @EntKey");
                vStringBuilder.AppendLine("and    CurKey = @CurKey");
                ObjectToData(vSqlCommand, aUserKey, aCurrency);
                vSqlCommand.Parameters.AddWithValue("@CurKey", aCurrency.CurKey);
                vSqlCommand.CommandText = vStringBuilder.ToString();
                vSqlCommand.Connection.Open();
                vSqlCommand.ExecuteNonQuery();
                vSqlCommand.Connection.Close();
            }
        }
        #endregion

        #region Delete
        public static void Delete(Connection aConnection, UserKey aUserKey, Currency aCurrency)
        {
            if (aCurrency == null)
            {
                throw new ArgumentNullException(nameof(aCurrency));
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
                    vStringBuilder.AppendLine("delete Currency");
                    vStringBuilder.AppendLine("where  EntKey = @EntKey");
                    vStringBuilder.AppendLine("and    CurKey = @CurKey");
                    vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                    vSqlCommand.Parameters.AddWithValue("@CurKey", aCurrency.CurKey);
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
