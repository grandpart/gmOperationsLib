using Grandmark;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Zephry;

namespace Grandmark
{
    public class ExchangeRateData
    {
        #region BuildSQL
        private static StringBuilder BuildSql()
        {
            var vStrignBuilder = new StringBuilder();
            vStrignBuilder.AppendLine("SELECT e.EntKey, e.ExrKey, e.CurKey, e.ExrFinYear, e.ExrFinMonth, e.ExrRate");
            vStrignBuilder.AppendLine("FROM ExchangeRate e");
            return vStrignBuilder;
        }
        #endregion

        #region DataToObject
        /// <summary>
        /// Populate object with values read from database
        /// </summary>
        /// <param name="aExchangeRate"></param>
        /// <param name="aSqlDataReader"></param>
        private static void DataToObject(ExchangeRate aExchangeRate, SqlDataReader aSqlDataReader)
        {
            aExchangeRate.EntKey = Convert.ToInt32(aSqlDataReader["EntKey"]);
            aExchangeRate.ExrKey = Convert.ToInt32(aSqlDataReader["ExrKey"]);
            aExchangeRate.CurKey = Convert.ToInt32(aSqlDataReader["CurKey"]);
            aExchangeRate.ExrFinYear = Convert.ToInt32(aSqlDataReader["ExrFinYear"]);
            aExchangeRate.ExrFinMonth = Convert.ToInt32(aSqlDataReader["ExrFinMonth"]);
            aExchangeRate.ExrRate = Convert.ToDecimal(aSqlDataReader["ExrRate"]);
        }
        #endregion

        #region ObjectToData
        /// <summary>
        /// Object to data maps object fields to your sql parameters
        /// </summary>
        /// <param name="aSqlCommand"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aExchangeRate"></param>
        private static void ObjectToData(SqlCommand aSqlCommand, UserKey aUserKey, ExchangeRate aExchangeRate)
        {
            aSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
            //aSqlCommand.Parameters.AddWithValue("@ExrKey", aExchangeRate.ExrKey);
            aSqlCommand.Parameters.AddWithValue("@CurKey", aExchangeRate.CurKey);
            aSqlCommand.Parameters.AddWithValue("@ExrFinYear", aExchangeRate.ExrFinYear);
            aSqlCommand.Parameters.AddWithValue("@ExrFinMonth", aExchangeRate.ExrFinMonth);
            aSqlCommand.Parameters.AddWithValue("@ExrRate", aExchangeRate.ExrRate);
        }
        #endregion

        #region Load
        /// <summary>
        /// Load a single record from database
        /// </summary>
        public static void Load(Connection aConnection, UserKey aUserKey, ExchangeRate aExchangeRate)
        {
            if (aExchangeRate == null)
            {
                throw new ArgumentNullException("aExchangeRate");
            }

            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStrignBuilder = BuildSql();
                vStrignBuilder.AppendLine("WHERE e.EntKey = @EntKey");
                vStrignBuilder.AppendLine("AND   e.ExrKey = @ExrKey");
                vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                vSqlCommand.Parameters.AddWithValue("@ExrKey", aExchangeRate.ExrKey);
                vSqlCommand.CommandText = vStrignBuilder.ToString();
                vSqlCommand.Connection.Open();
                using (var vSqlDataReader = vSqlCommand.ExecuteReader())
                {
                    if (vSqlDataReader.HasRows)
                    {
                        vSqlDataReader.Read();
                        DataToObject(aExchangeRate, vSqlDataReader);
                    }
                    else
                    {
                        //Need to make sure on what to pass back when no record is returned
                        aExchangeRate = null;
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
        /// <param name="aExchangeRate"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void LoadList(Connection aConnection, UserKey aUserKey, List<ExchangeRate> aExchangeRateCollection)
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
                vStrignBuilder.AppendLine("WHERE e.EntKey = @EntKey");
                vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);

                vSqlCommand.CommandText = vStrignBuilder.ToString();
                vSqlCommand.Connection.Open();

                using (SqlDataReader vSqlDataReader = vSqlCommand.ExecuteReader())
                {
                    while (vSqlDataReader.Read())
                    {
                        var vExchangeRate = new ExchangeRate();
                        DataToObject(vExchangeRate, vSqlDataReader);
                        aExchangeRateCollection.Add(vExchangeRate);

                    }

                    vSqlDataReader.Close();
                }
                vSqlCommand.Connection.Close();
            }
        }

        #endregion

        #region Insert with a Connection
        public static void Insert(Connection aConnection, UserKey aUserKey, ExchangeRate aExchangeRate)
        {
            if (aExchangeRate == null)
            {
                throw new ArgumentNullException(nameof(aExchangeRate));
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                vSqlCommand.Connection.Open();
                InsertCommon(vSqlCommand, aUserKey, aExchangeRate);
                vSqlCommand.Connection.Close();
            }
        }
        #endregion

        #region Insert with an SqlCommand
        public static void Insert(SqlCommand aSqlCommand, UserKey aUserKey, ExchangeRate aExchangeRate)
        {
            if (aExchangeRate == null)
            {
                throw new ArgumentNullException(nameof(aExchangeRate));
            }
            InsertCommon(aSqlCommand, aUserKey, aExchangeRate);
        }
        #endregion

        #region Insert Common
        private static void InsertCommon(SqlCommand aSqlCommand, UserKey aUserKey, ExchangeRate aExchangeRate)
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("INSERT INTO ExchangeRate");
            vStringBuilder.AppendLine("       (EntKey, CurKey, ExrFinYear, ExrFinMonth, ExrRate)");
            vStringBuilder.AppendLine("output inserted.ExrKey");
            vStringBuilder.AppendLine("values");
            vStringBuilder.AppendLine("       (@EntKey, @CurKey, @ExrFinYear, @ExrFinMonth, @ExrRate)");
            ObjectToData(aSqlCommand, aUserKey, aExchangeRate);
            aSqlCommand.CommandText = vStringBuilder.ToString();
            aExchangeRate.ExrKey = Convert.ToInt32(aSqlCommand.ExecuteScalar());
        }
        #endregion

        #region Update
        public static void Update(Connection aConnection, UserKey aUserKey, ExchangeRate aExchangeRate)
        {
            if (aExchangeRate == null)
            {
                throw new ArgumentNullException("aExchangeRate");
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStringBuilder = new StringBuilder();
                vStringBuilder.AppendLine("UPDATE ExchangeRate");
                vStringBuilder.AppendLine("set    CurKey = @CurKey,");
                vStringBuilder.AppendLine("       ExrFinYear = @ExrFinYear,");
                vStringBuilder.AppendLine("       ExrFinMonth = @ExrFinMonth,");
                vStringBuilder.AppendLine("       ExrRate = @ExrRate");
                vStringBuilder.AppendLine("where  EntKey = @EntKey");
                vStringBuilder.AppendLine("and    ExrKey = @ExrKey");
                ObjectToData(vSqlCommand, aUserKey, aExchangeRate);
                vSqlCommand.Parameters.AddWithValue("@ExrKey", aExchangeRate.ExrKey);
                vSqlCommand.CommandText = vStringBuilder.ToString();
                vSqlCommand.Connection.Open();
                vSqlCommand.ExecuteNonQuery();
                vSqlCommand.Connection.Close();
            }
        }
        #endregion

        #region Delete
        public static void Delete(Connection aConnection, UserKey aUserKey, ExchangeRate aExchangeRate)
        {
            if (aExchangeRate == null)
            {
                throw new ArgumentNullException(nameof(aExchangeRate));
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
                    vStringBuilder.AppendLine("delete ExchangeRate");
                    vStringBuilder.AppendLine("where  EntKey = @EntKey");
                    vStringBuilder.AppendLine("and    ExrKey = @ExrKey");
                    vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                    vSqlCommand.Parameters.AddWithValue("@ExrKey", aExchangeRate.ExrKey);
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
