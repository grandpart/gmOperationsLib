using Grandmark;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Zephry;

namespace Grandmark
{
    public class BranchData
    {
        #region BuildSQL
        private static StringBuilder BuildSql()
        {
            var vStrignBuilder = new StringBuilder();
            vStrignBuilder.AppendLine("SELECT b.Ent_Key, b.Brh_Key, b.Brh_Name, b.Brh_Code ");
            vStrignBuilder.AppendLine("FROM Branch b");
            return vStrignBuilder;
        }
        #endregion

        #region DataToObject
        /// <summary>
        /// Populate object with values read from database
        /// </summary>
        /// <param name="aBranch"></param>
        /// <param name="aSqlDataReader"></param>
        private static void DataToObject(Branch aBranch, SqlDataReader aSqlDataReader)
        {
            aBranch.EntKey = Convert.ToInt32(aSqlDataReader["Ent_Key"]);
            aBranch.BrhKey = Convert.ToInt32(aSqlDataReader["Brh_Key"]);
            aBranch.BrhName = Convert.ToString(aSqlDataReader["Brh_Name"]);
            aBranch.BrhCode = Convert.ToString(aSqlDataReader["Brh_Code"]);
        }
        #endregion

        #region ObjectToData
        /// <summary>
        /// Object to data maps object fields to your sql parameters
        /// </summary>
        /// <param name="aSqlCommand"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aBranch"></param>
        private static void ObjectToData(SqlCommand aSqlCommand, UserKey aUserKey, Branch aBranch)
        {
            aSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
            aSqlCommand.Parameters.AddWithValue("@BrhName", aBranch.BrhName);
            aSqlCommand.Parameters.AddWithValue("@BrhCode", aBranch.BrhCode);
        }
        #endregion

        #region Load
        /// <summary>
        /// Load a single record from database
        /// </summary>
        /// <param name="aConnection"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aBranch"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Load(Connection aConnection, UserKey aUserKey, Branch aBranch)
        {
            if (aBranch == null)
            {
                throw new ArgumentNullException("aBranch");
            }

            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStrignBuilder = BuildSql();
                vStrignBuilder.AppendLine("WHERE b.Ent_Key = @EntKey");
                vStrignBuilder.AppendLine("AND   b.Brh_Key = @BrhKey");
                vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                vSqlCommand.Parameters.AddWithValue("@BrhKey", aBranch.BrhKey);
                vSqlCommand.CommandText = vStrignBuilder.ToString();
                vSqlCommand.Connection.Open();
                using (var vSqlDataReader = vSqlCommand.ExecuteReader())
                {
                    if (vSqlDataReader.HasRows)
                    {
                        vSqlDataReader.Read();
                        DataToObject(aBranch, vSqlDataReader);
                    }
                    else
                    {
                        //Need to make sure on what to pass back when no record is returned
                        aBranch = null;
                    }
                    vSqlDataReader.Close();
                }
                vSqlCommand.Connection.Close();
            }
        }

        #endregion

        #region Insert with a Connection
        public static void Insert(Connection aConnection, UserKey aUserKey, Branch aBranch)
        {
            if (aBranch == null)
            {
                throw new ArgumentNullException(nameof(aBranch));
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                vSqlCommand.Connection.Open();
                InsertCommon(vSqlCommand, aUserKey, aBranch);
                vSqlCommand.Connection.Close();
            }
        }
        #endregion

        #region Insert with an SqlCommand
        public static void Insert(SqlCommand aSqlCommand, UserKey aUserKey, Branch aBranch)
        {
            if (aBranch == null)
            {
                throw new ArgumentNullException(nameof(aBranch));
            }
            InsertCommon(aSqlCommand, aUserKey, aBranch);
        }
        #endregion

        #region Insert Common
        private static void InsertCommon(SqlCommand aSqlCommand, UserKey aUserKey, Branch aBranch)
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("INSERT INTO Branch");
            vStringBuilder.AppendLine("       (Ent_Key, Brh_Name, Brh_Code)");
            vStringBuilder.AppendLine("output inserted.Brh_Key");
            vStringBuilder.AppendLine("values");
            vStringBuilder.AppendLine("       (@EntKey, @BrhName, @BrhCode)");
            ObjectToData(aSqlCommand, aUserKey, aBranch);
            aSqlCommand.CommandText = vStringBuilder.ToString();
            aBranch.BrhKey = Convert.ToInt32(aSqlCommand.ExecuteScalar());
        }
        #endregion

        #region Update
        public static void Update(Connection aConnection, UserKey aUserKey, Branch aBranch)
        {
            if (aBranch == null)
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
                vStringBuilder.AppendLine("UPDATE Branch");
                vStringBuilder.AppendLine("set    Brh_Name = @BrhName,");
                vStringBuilder.AppendLine("       Brh_Code = @BrhCode");
                vStringBuilder.AppendLine("where  Ent_Key = @EntKey");
                vStringBuilder.AppendLine("and    Brh_Key = @BrhKey");
                ObjectToData(vSqlCommand, aUserKey, aBranch);
                vSqlCommand.Parameters.AddWithValue("@BrhKey", aBranch.BrhKey);
                vSqlCommand.CommandText = vStringBuilder.ToString();
                vSqlCommand.Connection.Open();
                vSqlCommand.ExecuteNonQuery();
                vSqlCommand.Connection.Close();
            }
        }
        #endregion

        #region Delete
        public static void Delete(Connection aConnection, UserKey aUserKey, BranchKey aBranchKey)
        {
            if (aBranchKey == null)
            {
                throw new ArgumentNullException(nameof(aBranchKey));
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
                    vStringBuilder.AppendLine("delete Branch");
                    vStringBuilder.AppendLine("where  Ent_Key = @EntKey");
                    vStringBuilder.AppendLine("and    Brh_Key = @BrhKey");
                    vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                    vSqlCommand.Parameters.AddWithValue("@BrhKey", aBranchKey.BrhKey);
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
