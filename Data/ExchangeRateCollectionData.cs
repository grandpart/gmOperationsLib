using System.Data.SqlClient;
using System.Data;
using System.Text;
using Zephry;

namespace Grandmark
{
    public class ExchangeRateCollectionData
    {
        #region BuildSQL

        /// <summary>
        ///   A standard SQL Statement that will return all <see cref="OrganizationProxy"/>, unfiltered and unsorted.
        /// </summary>
        /// <returns>A <see cref="StringBuilder"/></returns>
        private static StringBuilder BuildSql()
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("select EntKey, ExrKey, CurKey, ExrFinYear, ExrFinMonth, ExrRate");
            vStringBuilder.AppendLine("from   ExchangeRate");
            return vStringBuilder;
        }

        #endregion

        #region DataToObject

        /// <summary>
        ///   Load a <see cref="SqlDataReader"/> into a <see cref="OrganizationProxy"/> object.
        /// </summary>
        /// <param name="aExchangeRate">A <see cref="ExchangeRate"/> argument.</param>
        /// <param name="aSqlDataReader">A <see cref="SqlDataReader"/> argument.</param>
        public static void DataToObject(ExchangeRate aExchangeRate, SqlDataReader aSqlDataReader)
        {
            aExchangeRate.EntKey = Convert.ToInt32(aSqlDataReader["EntKey"]);
            aExchangeRate.ExrKey = Convert.ToInt32(aSqlDataReader["ExrKey"]);
            aExchangeRate.CurKey = Convert.ToInt32(aSqlDataReader["CurKey"]);
            aExchangeRate.ExrFinYear = Convert.ToInt32(aSqlDataReader["ExrFinyear"]);
            aExchangeRate.ExrFinMonth = Convert.ToInt32(aSqlDataReader["ExrFinMonth"]);
            aExchangeRate.ExrRate = Convert.ToDecimal(aSqlDataReader["ExrRate"]);
        }
        #endregion

        #region Load ItemCollection with Connection
        public static void Load(Connection aConnection, UserKey aUserKey, ExchangeRateCollection aExchangeRateCollection)
        {
            if (aExchangeRateCollection == null)
            {
                throw new ArgumentNullException("aExchangeRateCollection");
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                vSqlCommand.Connection.Open();
                LoadCommon(vSqlCommand, aUserKey, aExchangeRateCollection);
                vSqlCommand.Connection.Close();
            }
        }

        #endregion

        #region Load ItemCollection with an SqlCommand
        public static void Load(SqlCommand aSqlCommand, UserKey aUserKey, ExchangeRateCollection aExchangeRateCollection)
        {
            if (aExchangeRateCollection == null)
            {
                throw new ArgumentNullException("aExchangeRateCollection");
            }
            LoadCommon(aSqlCommand, aUserKey, aExchangeRateCollection);
        }
        #endregion

        #region Load ItemCollection Common
        public static void LoadCommon(SqlCommand aSqlCommand, UserKey aUserKey, ExchangeRateCollection aExchangeRateCollection)
        {
            // Get a flat list of OrganizationProxy for the collection and the dictionary
            var vStringBuilder = BuildSql();
            vStringBuilder.AppendLine("where EntKey = @EntKey");
            vStringBuilder.AppendLine("order by ExrKey");
            aSqlCommand.Parameters.Clear();
            aSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
            aSqlCommand.CommandText = vStringBuilder.ToString();
            using (SqlDataReader vSqlDataReader = aSqlCommand.ExecuteReader())
            {
                while (vSqlDataReader.Read())
                {
                    var vExchangeRate = new ExchangeRate();
                    DataToObject(vExchangeRate, vSqlDataReader);
                    aExchangeRateCollection.ExchangeRateList.Add(vExchangeRate);
                }
                vSqlDataReader.Close();
            }
        }
        #endregion
    }
}
