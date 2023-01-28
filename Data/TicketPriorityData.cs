using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Zephry;

namespace Grandmark
{
    public class TicketPriorityData
    {

        #region BuildSQL
        private static StringBuilder BuildSql()
        {
            var vStrignBuilder = new StringBuilder();
            vStrignBuilder.AppendLine("SELECT tp.EntKey, tp.TprKey, tp.TprName, tp.TprPriority, tp.TprClass");
            vStrignBuilder.AppendLine("FROM TicketPriority tp");
            return vStrignBuilder;
        }
        #endregion

        #region DataToObject
        /// <summary>
        /// Populate object with values read from database
        /// </summary>
        /// <param name="aTicketPriority"></param>
        /// <param name="aSqlDataReader"></param>
        private static void DataToObject(TicketPriority aTicketPriority, SqlDataReader aSqlDataReader)
        {
            aTicketPriority.EntKey = Convert.ToInt32(aSqlDataReader["EntKey"]);
            aTicketPriority.TprKey = Convert.ToInt32(aSqlDataReader["TprKey"]);
            aTicketPriority.TprName = Convert.ToString(aSqlDataReader["TprName"]);
            aTicketPriority.TprPriority = Convert.ToInt32(aSqlDataReader["TprPriority"]);
            aTicketPriority.TprClass = Convert.ToString(aSqlDataReader["TprClass"]);
        }
        #endregion

        #region ObjectToData
        /// <summary>
        /// Object to data maps object fields to your sql parameters
        /// </summary>
        /// <param name="aSqlCommand"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aTicketPriority"></param>
        private static void ObjectToData(SqlCommand aSqlCommand, UserKey aUserKey, TicketPriority aTicketPriority) 
        {
            aSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
            aSqlCommand.Parameters.AddWithValue("@TprName", aTicketPriority.TprName);
            aSqlCommand.Parameters.AddWithValue("@TprPriority", aTicketPriority.TprPriority);
            aSqlCommand.Parameters.AddWithValue("@TprClass", aTicketPriority.TprClass);
        }
        #endregion

        #region Load
        /// <summary>
        /// Load a single record from database
        /// </summary>
        /// <param name="aConnection"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aTicketPriority"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Load(Connection aConnection, UserKey aUserKey, TicketPriority aTicketPriority)
        {
            if (aTicketPriority == null)
            {
                throw new ArgumentNullException("aTicketPriority");
            }

            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStrignBuilder = BuildSql();
                vStrignBuilder.AppendLine("WHERE tp.EntKey = @EntKey");
                vStrignBuilder.AppendLine("AND   tp.TprKey = @TprKey");
                vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                vSqlCommand.Parameters.AddWithValue("@TprKey", aTicketPriority.TprKey);
                vSqlCommand.CommandText = vStrignBuilder.ToString();
                vSqlCommand.Connection.Open();
                using(var vSqlDataReader = vSqlCommand.ExecuteReader())
                {
                    if (vSqlDataReader.HasRows)
                    {
                        vSqlDataReader.Read();
                        DataToObject(aTicketPriority, vSqlDataReader);
                    }
                    else
                    {
                        //Need to make sure on what to pass back when no record is returned
                        aTicketPriority = null;
                    }
                    vSqlDataReader.Close();
                }
                vSqlCommand.Connection.Close();
            }
        }

        #endregion

        #region Insert with a Connection
        public static void Insert(Connection aConnection, UserKey aUserKey, TicketPriority aTicketPriority)
        {
            if (aTicketPriority == null)
            {
                throw new ArgumentNullException(nameof(aTicketPriority));
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                vSqlCommand.Connection.Open();
                InsertCommon(vSqlCommand, aUserKey, aTicketPriority);
                vSqlCommand.Connection.Close();
            }
        }
        #endregion

        #region Insert with an SqlCommand
        public static void Insert(SqlCommand aSqlCommand, UserKey aUserKey, TicketPriority aTicketPriority)
        {
            if (aTicketPriority == null)
            {
                throw new ArgumentNullException(nameof(aTicketPriority));
            }
            InsertCommon(aSqlCommand, aUserKey, aTicketPriority);
        }
        #endregion

        #region Insert Common
        private static void InsertCommon(SqlCommand aSqlCommand, UserKey aUserKey, TicketPriority aTicketPriority)
        {
            var vStringBuilder = new StringBuilder();
            vStringBuilder.AppendLine("INSERT INTO TicketPriority");
            vStringBuilder.AppendLine("       (EntKey, TprName, TprPriority, TprClass)");
            vStringBuilder.AppendLine("output inserted.TprKey");
            vStringBuilder.AppendLine("values");
            vStringBuilder.AppendLine("       (@EntKey, @TprName, @TprPriority, @TprClass)");
            ObjectToData(aSqlCommand, aUserKey, aTicketPriority);
            aSqlCommand.CommandText = vStringBuilder.ToString();
            aTicketPriority.TprKey = Convert.ToInt32(aSqlCommand.ExecuteScalar());
        }
        #endregion

        #region Update
        public static void Update(Connection aConnection, UserKey aUserKey, TicketPriority aTicketPriority)
        {
            if (aTicketPriority == null)
            {
                throw new ArgumentNullException("aTicketPriority");
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStringBuilder = new StringBuilder();
                vStringBuilder.AppendLine("UPDATE TicketPriority");
                vStringBuilder.AppendLine("set    TprName = @TprName,");
                vStringBuilder.AppendLine("       TprPriority = @TprPriority,");
                vStringBuilder.AppendLine("       TprClass = @TprClass");
                vStringBuilder.AppendLine("where  EntKey = @EntKey");
                vStringBuilder.AppendLine("and    TprKey = @TprKey");
                ObjectToData(vSqlCommand, aUserKey, aTicketPriority);
                vSqlCommand.Parameters.AddWithValue("@TprKey", aTicketPriority.TprKey);
                vSqlCommand.CommandText = vStringBuilder.ToString();
                vSqlCommand.Connection.Open();
                vSqlCommand.ExecuteNonQuery();
                vSqlCommand.Connection.Close();
            }
        }
        #endregion

        #region Delete
        public static void Delete(Connection aConnection, UserKey aUserKey, TicketPriority aTicketPriority)
        {
            if (aTicketPriority == null)
            {
                throw new ArgumentNullException(nameof(aTicketPriority));
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
                    vStringBuilder.AppendLine("delete TicketPriority");
                    vStringBuilder.AppendLine("where  EntKey = @EntKey");
                    vStringBuilder.AppendLine("and    TprKey = @TprKey");
                    vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                    vSqlCommand.Parameters.AddWithValue("@TprKey", aTicketPriority.TprKey);
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
