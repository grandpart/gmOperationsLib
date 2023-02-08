using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Zephry;

namespace Grandmark
{
    public class TicketData
    {

        #region BuildSQL
        private static StringBuilder BuildSql()
        {
            var vStrignBuilder = new StringBuilder();
            vStrignBuilder.AppendLine("select tck.EntKey, tck.TckKey, ");
            vStrignBuilder.AppendLine("       tck.UsrKeySrc, usrs.UsrName + ' ' + usrs.UsrSurname as UsrNameSrc, ");
            vStrignBuilder.AppendLine("       tck.UsrKeyTgt, usrt.UsrName + ' ' + usrt.UsrSurname as UsrNameTgt, ");
            vStrignBuilder.AppendLine("       tck.OrgKey, org.OrgName, tck.TtpKey, ttp.TtpName, tck.TprKey, tpr.TprName, ");
            vStrignBuilder.AppendLine("       TckStatus, TckDescription, TckDateCapture, TckDateAction, TckDateClose, TckHoursRequired ");
            vStrignBuilder.AppendLine("from Ticket tck, SysUser usrs, SysUser usrt, Organization org, TicketType ttp, TicketPriority tpr ");
            vStrignBuilder.AppendLine("where tck.EntKey = usrs.EntKey ");
            vStrignBuilder.AppendLine("and   tck.UsrKeySrc = usrs.UsrKey ");
            vStrignBuilder.AppendLine("and   tck.EntKey = usrt.EntKey ");
            vStrignBuilder.AppendLine("and   tck.UsrKeyTgt = usrt.UsrKey ");
            vStrignBuilder.AppendLine("and   tck.EntKey = org.EntKey ");
            vStrignBuilder.AppendLine("and   tck.OrgKey = org.OrgKey ");
            vStrignBuilder.AppendLine("and   tck.EntKey = ttp.EntKey ");
            vStrignBuilder.AppendLine("and   tck.TtpKey = ttp.TtpKey ");
            vStrignBuilder.AppendLine("and   tck.EntKey = tpr.EntKey ");
            vStrignBuilder.AppendLine("and   tck.TprKey = tpr.TprKey ");
            return vStrignBuilder;
        }
        #endregion

        #region DataToObject
        /// <summary>
        /// Populate object with values read from database
        /// </summary>
        /// <param name="aTicket"></param>
        /// <param name="aSqlDataReader"></param>
        private static void DataToObject(Ticket aTicket, SqlDataReader aSqlDataReader)
        {
            aTicket.TckKey = Convert.ToInt32(aSqlDataReader["TckKey"]);
            aTicket.TckStatus = Convert.ToInt32(aSqlDataReader["TckStatus"]);
            aTicket.TckStatusString = ((TicketStatus)aTicket.TckStatus).ToString();
            aTicket.TckDescription = Convert.ToString(aSqlDataReader["TckDescription"]);
            aTicket.UsrKeySrc = Convert.ToInt32(aSqlDataReader["UsrKeySrc"]);
            aTicket.UsrNameSrc = Convert.ToString(aSqlDataReader["UsrNameSrc"]);
            aTicket.UsrKeyTgt = Convert.ToInt32(aSqlDataReader["UsrKeyTgt"]);
            aTicket.UsrNameTgt = Convert.ToString(aSqlDataReader["UsrNameTgt"]);
            aTicket.OrgKey = Convert.ToInt32(aSqlDataReader["OrgKey"]);
            aTicket.OrgName = Convert.ToString(aSqlDataReader["OrgName"]);
            aTicket.TtpKey = Convert.ToInt32(aSqlDataReader["TtpKey"]);
            aTicket.TtpName = Convert.ToString(aSqlDataReader["TtpName"]);
            aTicket.TprKey = Convert.ToInt32(aSqlDataReader["TprKey"]);
            aTicket.TprName = Convert.ToString(aSqlDataReader["TprName"]);
            aTicket.TckDateCapture = Convert.ToDateTime(aSqlDataReader["TckDateCapture"]);
            aTicket.TckDateAction = aSqlDataReader["TckDateAction"] as DateTime?;
            aTicket.TckDateClose = aSqlDataReader["TckDateClose"] as DateTime?;
            aTicket.TckHoursRequired = Convert.ToInt32(aSqlDataReader["TckHoursRequired"]);
        }
        #endregion

        #region Load
        // Load with a connection
        public static void Load(Connection aConnection, UserKey aUserKey, Ticket aTicket)
        {
            if (aTicket == null)
            {
                throw new ArgumentNullException(nameof(aTicket));
            }

            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                vSqlCommand.Connection.Open();
                LoadCommon(vSqlCommand, aUserKey, aTicket);
                vSqlCommand.Connection.Close();
            }
        }

        // Load with a Command
        public static void Load(SqlCommand aSqlCommand, UserKey aUserKey, Ticket aTicket)
        {
            if (aTicket == null)
            {
                throw new ArgumentNullException(nameof(aTicket));
            }
            LoadCommon(aSqlCommand, aUserKey, aTicket);
        }

        // Load common
        public static void LoadCommon(SqlCommand aSqlCommand, UserKey aUserKey, Ticket aTicket)
        {
            var vStrignBuilder = BuildSql();
            vStrignBuilder.AppendLine("and   tck.EntKey = @EntKey");
            vStrignBuilder.AppendLine("and   tck.TckKey = @TckKey");
            aSqlCommand.Parameters.Clear();
            aSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
            aSqlCommand.Parameters.AddWithValue("@TckKey", aTicket.TckKey); 
            aSqlCommand.CommandText = vStrignBuilder.ToString();
                using (var vSqlDataReader = aSqlCommand.ExecuteReader())
                {
                    if (vSqlDataReader.HasRows)
                    {
                        vSqlDataReader.Read();
                        DataToObject(aTicket, vSqlDataReader);
                    }
                    vSqlDataReader.Close();
                }
        }
        #endregion

        #region Load Collection
        public static void Load(Connection aConnection, UserKey aUserKey, TicketCollection aTicketCollection)
        {
            if (aTicketCollection == null)
            {
                throw new ArgumentNullException(nameof(aTicketCollection));
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStringBuilder = BuildSql();
                vStringBuilder.AppendLine("and   tck.EntKey = @EntKey");
                vStringBuilder.AppendLine("order by TckDateCapture");
                vSqlCommand.Parameters.Clear();
                vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                vSqlCommand.CommandText = vStringBuilder.ToString();
                vSqlCommand.Connection.Open();
                using (SqlDataReader vSqlDataReader = vSqlCommand.ExecuteReader())
                {
                    while (vSqlDataReader.Read())
                    {
                        var VTicket = new Ticket();
                        DataToObject(VTicket, vSqlDataReader);
                        aTicketCollection.List.Add(VTicket);
                    }
                    vSqlDataReader.Close();
                }
                vSqlCommand.Connection.Close();
            }
        }
        #endregion

        #region Insert
        public static void Insert(Connection aConnection, UserKey aUserKey, Ticket aTicket)
        {
            if (aTicket == null)
            {
                throw new ArgumentNullException(nameof(aTicket));
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStringBuilder = new StringBuilder();
                vStringBuilder.AppendLine("insert into Ticket ");
                vStringBuilder.AppendLine("    (EntKey, UsrKeySrc, UsrKeyTgt, OrgKey, TtpKey, TprKey, TckStatus, ");
                vStringBuilder.AppendLine("     TckDescription, TckDateCapture, TckHoursRequired) ");
                vStringBuilder.AppendLine("output inserted.TckKey");
                vStringBuilder.AppendLine("values");
                vStringBuilder.AppendLine("    (@EntKey, @UsrKeySrc, @UsrKeyTgt, @OrgKey, @TtpKey, @TprKey, @TckStatus, ");
                vStringBuilder.AppendLine("     @TckDescription, GetDate(), @TckHoursRequired) ");
                vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                vSqlCommand.Parameters.AddWithValue("@TckStatus", (int)TicketStatus.Init);
                vSqlCommand.Parameters.AddWithValue("@TckDescription", aTicket.TckDescription);
                vSqlCommand.Parameters.AddWithValue("@UsrKeySrc", aUserKey.UsrKey);
                vSqlCommand.Parameters.AddWithValue("@UsrKeyTgt", aUserKey.UsrKey);
                vSqlCommand.Parameters.AddWithValue("@OrgKey", aTicket.OrgKey); // get from user
                vSqlCommand.Parameters.AddWithValue("@TtpKey", aTicket.TtpKey);
                vSqlCommand.Parameters.AddWithValue("@TprKey", aTicket.TprKey);
                vSqlCommand.Parameters.AddWithValue("@TckHoursRequired", 8);
                vSqlCommand.Connection.Open();
                vSqlCommand.CommandText = vStringBuilder.ToString();
                aTicket.TckKey = Convert.ToInt32(vSqlCommand.ExecuteScalar());
                vSqlCommand.Connection.Close();
            }
        }
        #endregion

        #region Update
        public static void Update(Connection aConnection, UserKey aUserKey, Ticket aTicket)
        {
            if (aTicket == null)
            {
                throw new ArgumentNullException("aTicket");
            }
            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                var vStringBuilder = new StringBuilder();
                vStringBuilder.AppendLine("update Ticket");
                vStringBuilder.AppendLine("set UsrKeyTgt = @UsrKeyTgt,");
                vStringBuilder.AppendLine("    OrgKey = @OrgKey,");
                vStringBuilder.AppendLine("    TtpKey = @TtpKey,");
                vStringBuilder.AppendLine("    TprKey = @TprKey,");
                vStringBuilder.AppendLine("    TckStatus = @TckStatus,");
                vStringBuilder.AppendLine("    TckDescription = @TckDescription,");
                //vStringBuilder.AppendLine("    TckDateCapture = @TckDateCapture,");
                //vStringBuilder.AppendLine("    TckDateAction = @TckDateAction,");
                //vStringBuilder.AppendLine("    TckDateClose = @TckDateClose,");
                vStringBuilder.AppendLine("    TckHoursRequired = @TckHoursRequired");
                vStringBuilder.AppendLine("where  EntKey = @EntKey");
                vStringBuilder.AppendLine("and    TckKey = @TckKey");
                vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                vSqlCommand.Parameters.AddWithValue("@TckKey", aTicket.TckKey);
                vSqlCommand.Parameters.AddWithValue("@TckStatus", (TicketStatus)aTicket.TckStatus);
                vSqlCommand.Parameters.AddWithValue("@TckDescription", aTicket.TckDescription);
                vSqlCommand.Parameters.AddWithValue("@UsrKeyTgt", aTicket.UsrKeyTgt);
                vSqlCommand.Parameters.AddWithValue("@OrgKey", aTicket.OrgKey);
                vSqlCommand.Parameters.AddWithValue("@TtpKey", aTicket.TtpKey);
                vSqlCommand.Parameters.AddWithValue("@TprKey", aTicket.TprKey);
                vSqlCommand.Parameters.AddWithValue("@TckHoursRequired", aTicket.TckHoursRequired);
                vSqlCommand.CommandText = vStringBuilder.ToString();
                vSqlCommand.Connection.Open();
                vSqlCommand.ExecuteNonQuery();
                vSqlCommand.Connection.Close();
            }
        }
        #endregion

        #region Delete
        public static void Delete(Connection aConnection, UserKey aUserKey, Ticket aTicket)
        {
            if (aTicket == null)
            {
                throw new ArgumentNullException(nameof(aTicket));
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
                    vStringBuilder.AppendLine("delete Ticket");
                    vStringBuilder.AppendLine("where  EntKey = @EntKey");
                    vStringBuilder.AppendLine("and    TckKey = @TckKey");
                    vSqlCommand.Parameters.AddWithValue("@EntKey", aUserKey.EntKey);
                    vSqlCommand.Parameters.AddWithValue("@TckKey", aTicket.TckKey);
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
