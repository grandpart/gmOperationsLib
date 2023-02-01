using System.Data.SqlClient;
using System.Data;
using System.Text;
using Zephry;

namespace Grandmark
{
    public class DepartmentData
    {
        #region BuildSQL
        private static StringBuilder BuildSql()
        {
            var vStrignBuilder = new StringBuilder();
            vStrignBuilder.AppendLine("SELECT d.EntKey, d.DepKey, d.DepName");
            vStrignBuilder.AppendLine("FROM Department d");
            return vStrignBuilder;
        }
        #endregion

        #region DataToObject
        /// <summary>
        /// Populate object with values read from database
        /// </summary>
        /// <param name="aDepartment"></param>
        /// <param name="aSqlDataReader"></param>
        private static void DataToObject(Department aDepartment, SqlDataReader aSqlDataReader)
        {
            aDepartment.EntKey = Convert.ToInt32(aSqlDataReader["EntKey"]);
            aDepartment.DepKey = Convert.ToInt32(aSqlDataReader["DepKey"]);
            aDepartment.DepName = Convert.ToString(aSqlDataReader["DepName"]);
        }
        #endregion

        #region ObjectToData
        /// <summary>
        /// Object to data maps object fields to your sql parameters
        /// </summary>
        /// <param name="aSqlCommand"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aDepartment"></param>
        private static void ObjectToData(SqlCommand aSqlCommand, UserKey aUserKey, Department aDepartment)
        {
            aSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
            aSqlCommand.Parameters.AddWithValue("@DepName", aDepartment.DepName);
        }
        #endregion

        #region Load
        /// <summary>
        /// Load a single record from database
        /// </summary>
        /// <param name="aConnection"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aDepartment"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Load(Connection aConnection, UserKey aUserKey, Department aDepartment)
        {
            if (aDepartment == null)
            {
                throw new ArgumentNullException("aDepartment");
            }

            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStrignBuilder = BuildSql();
                vStrignBuilder.AppendLine("WHERE d.EntKey = @EntKey");
                vStrignBuilder.AppendLine("AND   d.DepKey = @DepKey");
                vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                vSqlCommand.Parameters.AddWithValue("@DepKey", aDepartment.DepKey);
                vSqlCommand.CommandText = vStrignBuilder.ToString();
                vSqlCommand.Connection.Open();
                using (var vSqlDataReader = vSqlCommand.ExecuteReader())
                {
                    if (vSqlDataReader.HasRows)
                    {
                        vSqlDataReader.Read();
                        DataToObject(aDepartment, vSqlDataReader);
                    }
                    else
                    {
                        //Need to make sure on what to pass back when no record is returned
                        aDepartment = null;
                    }
                    vSqlDataReader.Close();
                }
                vSqlCommand.Connection.Close();
            }
        }

        #endregion

        #region Insert with a Connection
        public static void Insert(Connection aConnection, UserKey aUserKey, Department aDepartment)
        {
            if (aDepartment == null)
            {
                throw new ArgumentNullException(nameof(aDepartment));
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                vSqlCommand.Connection.Open();
                InsertCommon(vSqlCommand, aUserKey, aDepartment);
                vSqlCommand.Connection.Close();
            }
        }
        #endregion

        #region Insert with an SqlCommand
        public static void Insert(SqlCommand aSqlCommand, UserKey aUserKey, Department aDepartment)
        {
            if (aDepartment == null)
            {
                throw new ArgumentNullException(nameof(aDepartment));
            }
            InsertCommon(aSqlCommand, aUserKey, aDepartment);
        }
        #endregion

        #region Insert Common
        private static void InsertCommon(SqlCommand aSqlCommand, UserKey aUserKey, Department aDepartment)
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("INSERT INTO Department");
            vStringBuilder.AppendLine("       (EntKey, DepName)");
            vStringBuilder.AppendLine("output inserted.DepKey");
            vStringBuilder.AppendLine("values");
            vStringBuilder.AppendLine("       (@EntKey, @DepName)");
            ObjectToData(aSqlCommand, aUserKey, aDepartment);
            aSqlCommand.CommandText = vStringBuilder.ToString();
            aDepartment.DepKey = Convert.ToInt32(aSqlCommand.ExecuteScalar());
        }
        #endregion

        #region Update
        public static void Update(Connection aConnection, UserKey aUserKey, Department aDepartment)
        {
            if (aDepartment == null)
            {
                throw new ArgumentNullException("aDepartment");
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStringBuilder = new StringBuilder();
                vStringBuilder.AppendLine("UPDATE Department");
                vStringBuilder.AppendLine("set    DepName = @DepName");
                vStringBuilder.AppendLine("where  EntKey = @EntKey");
                vStringBuilder.AppendLine("and    DepKey = @DepKey");
                ObjectToData(vSqlCommand, aUserKey, aDepartment);
                vSqlCommand.Parameters.AddWithValue("@DepKey", aDepartment.DepKey);
                vSqlCommand.CommandText = vStringBuilder.ToString();
                vSqlCommand.Connection.Open();
                vSqlCommand.ExecuteNonQuery();
                vSqlCommand.Connection.Close();
            }
        }
        #endregion

        #region Delete
        public static void Delete(Connection aConnection, UserKey aUserKey, Department aDepartment)
        {
            if (aDepartment == null)
            {
                throw new ArgumentNullException(nameof(aDepartment));
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
                    vStringBuilder.AppendLine("delete Department");
                    vStringBuilder.AppendLine("where  EntKey = @EntKey");
                    vStringBuilder.AppendLine("and    DepKey = @DepKey");
                    vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                    vSqlCommand.Parameters.AddWithValue("@DepKey", aDepartment.DepKey);
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
