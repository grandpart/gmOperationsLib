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
            vStrignBuilder.AppendLine("SELECT EntKey, DptKey, DptName");
            vStrignBuilder.AppendLine("FROM Department");
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
            aDepartment.DptKey = Convert.ToInt32(aSqlDataReader["DptKey"]);
            aDepartment.DptName = Convert.ToString(aSqlDataReader["DptName"]);
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
            aSqlCommand.Parameters.AddWithValue("@DptName", aDepartment.DptName);
        }
        #endregion

        #region Load Single
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
                vStrignBuilder.AppendLine("where EntKey = @EntKey");
                vStrignBuilder.AppendLine("and   DptKey = @DptKey");
                vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                vSqlCommand.Parameters.AddWithValue("@DptKey", aDepartment.DptKey);
                vSqlCommand.CommandText = vStrignBuilder.ToString();
                vSqlCommand.Connection.Open();
                using (var vSqlDataReader = vSqlCommand.ExecuteReader())
                {
                    if (vSqlDataReader.HasRows)
                    {
                        vSqlDataReader.Read();
                        DataToObject(aDepartment, vSqlDataReader);
                    }
                    vSqlDataReader.Close();
                }
                vSqlCommand.Connection.Close();
            }
        }
        #endregion

        #region Load Collection
        public static void Load(Connection aConnection, UserKey aUserKey, DepartmentCollection aDepartmenCollection)
        {
            if (aDepartmenCollection == null)
            {
                throw new ArgumentNullException("aDepartmentCollection");
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                vSqlCommand.Connection.Open();

                var vStringBuilder = BuildSql();
                vStringBuilder.AppendLine("where EntKey = @EntKey");
                vStringBuilder.AppendLine("order by DptKey");
                vSqlCommand.Parameters.Clear();
                vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                vSqlCommand.CommandText = vStringBuilder.ToString();
                using (SqlDataReader vSqlDataReader = vSqlCommand.ExecuteReader())
                {
                    while (vSqlDataReader.Read())
                    {
                        var vDepartment = new Department();
                        DataToObject(vDepartment, vSqlDataReader);
                        aDepartmenCollection.List.Add(vDepartment);
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
            vStringBuilder.AppendLine("insert into Department");
            vStringBuilder.AppendLine("       (EntKey, DptName)");
            vStringBuilder.AppendLine("output inserted.DptKey");
            vStringBuilder.AppendLine("values");
            vStringBuilder.AppendLine("       (@EntKey, @DptName)");
            ObjectToData(aSqlCommand, aUserKey, aDepartment);
            aSqlCommand.CommandText = vStringBuilder.ToString();
            aDepartment.DptKey = Convert.ToInt32(aSqlCommand.ExecuteScalar());
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
                vStringBuilder.AppendLine("update Department");
                vStringBuilder.AppendLine("set    DptName = @DptName");
                vStringBuilder.AppendLine("where  EntKey = @EntKey");
                vStringBuilder.AppendLine("and    DptKey = @DptKey");
                ObjectToData(vSqlCommand, aUserKey, aDepartment);
                vSqlCommand.Parameters.AddWithValue("@DptKey", aDepartment.DptKey);
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
                    vStringBuilder.AppendLine("and    DptKey = @DptKey");
                    vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                    vSqlCommand.Parameters.AddWithValue("@DptKey", aDepartment.DptKey);
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
