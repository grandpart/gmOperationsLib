using System.Data;
using System.Data.SqlClient;
using Zephry;

namespace Grandmark
{
    public class TicketEditorData
    {

        #region Load
        // Load with a connection
        public static void Load(Connection aConnection, UserKey aUserKey, TicketEditor aTicketEditor)
        {
            if (aTicketEditor == null || 
                aTicketEditor.Ticket == null ||
                aTicketEditor.TprList == null ||
                aTicketEditor.TtpList == null)
            {
                throw new ArgumentNullException(nameof(aTicketEditor));
            }

            using (var vSqlCommand = new SqlCommand()
            {
                CommandType = CommandType.Text,
                Connection = new SqlConnection(aConnection.SqlConnectionString)
            })
            {
                vSqlCommand.Connection.Open();
                TicketTypeData.Load(vSqlCommand, aUserKey, aTicketEditor.TtpList);
                TicketPriorityData.Load(vSqlCommand, aUserKey, aTicketEditor.TprList);
                if (aTicketEditor.Ticket.TckKey != 0)
                {
                    TicketData.Load(vSqlCommand, aUserKey, aTicketEditor.Ticket);
                }
                vSqlCommand.Connection.Close();
            }
        }
        #endregion
    }
}