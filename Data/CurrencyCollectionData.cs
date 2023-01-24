using Grandmark;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Zephry;

namespace Grandmark
{
    public class CurrencyCollectionData
    {
        #region BuildSQL

        /// <summary>
        ///   A standard SQL Statement that will return all <see cref="OrganizationProxy"/>, unfiltered and unsorted.
        /// </summary>
        /// <returns>A <see cref="StringBuilder"/></returns>
        private static StringBuilder BuildSql()
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("select Ent_Key, Cur_Key,  Cur_Code, Cur_Prefix, Cur_Name");
            vStringBuilder.AppendLine("from   Currency");
            return vStringBuilder;
        }

        #endregion

        #region DataToObject

        /// <summary>
        ///   Load a <see cref="SqlDataReader"/> into a <see cref="OrganizationProxy"/> object.
        /// </summary>
        /// <param name="aOrganizationProxy">A <see cref="OrganizationProxy"/> argument.</param>
        /// <param name="aSqlDataReader">A <see cref="SqlDataReader"/> argument.</param>
        public static void DataToObject(Currency aCurrency, SqlDataReader aSqlDataReader)
        {
            aCurrency.EntKey = Convert.ToInt32(aSqlDataReader["Ent_Key"]);
            aCurrency.CurKey = Convert.ToInt32(aSqlDataReader["Cur_Key"]);
            aCurrency.CurCode = Convert.ToString(aSqlDataReader["Cur_Code"]);
            aCurrency.CurPrefix = Convert.ToString(aSqlDataReader["Cur_Prefix"]);
            aCurrency.CurName = Convert.ToString(aSqlDataReader["Cur_Name"]) ?? string.Empty;
        }
        #endregion

        #region Load ItemCollection with Connection
        public static void Load(Connection aConnection, UserKey aUserKey, CurrencyCollection aCurrencyCollection)
        {
            if (aCurrencyCollection == null)
            {
                throw new ArgumentNullException("aCurrencyCollection");
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                vSqlCommand.Connection.Open();
                LoadCommon(vSqlCommand, aUserKey, aCurrencyCollection);
                vSqlCommand.Connection.Close();
            }
        }

        #endregion

        #region Load ItemCollection with an SqlCommand
        public static void Load(SqlCommand aSqlCommand, UserKey aUserKey, CurrencyCollection aCurrencyCollection)
        {
            if (aCurrencyCollection == null)
            {
                throw new ArgumentNullException("aCurrencyCollection");
            }
            LoadCommon(aSqlCommand, aUserKey, aCurrencyCollection);
        }
        #endregion

        #region Load ItemCollection Common
        public static void LoadCommon(SqlCommand aSqlCommand, UserKey aUserKey, CurrencyCollection aCurrencyCollection)
        {
            // Get a flat list of OrganizationProxy for the collection and the dictionary
            var vStringBuilder = BuildSql();
            vStringBuilder.AppendLine("where Ent_Key = @EntKey");
            vStringBuilder.AppendLine("order by Cur_Code");
            aSqlCommand.Parameters.Clear();
            aSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
            aSqlCommand.CommandText = vStringBuilder.ToString();
            using (SqlDataReader vSqlDataReader = aSqlCommand.ExecuteReader())
            {
                while (vSqlDataReader.Read())
                {
                    var vCurrency = new Currency();
                    DataToObject(vCurrency, vSqlDataReader);
                    aCurrencyCollection.CurrencyList.Add(vCurrency);
                }
                vSqlDataReader.Close();
            }
        }
        #endregion
    }
}
