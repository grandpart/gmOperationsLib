using Zephry;

namespace Grandmark
{
    public class TicketEditorBusiness
    {
        #region Load
        public static void Load(Connection aConnection, UserKey aUserKey, TicketEditor aTicketEditor)
        {
            if (aTicketEditor == null)
            {
                throw new ArgumentNullException(nameof(aTicketEditor));
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "TicketEditor"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "TicketEditor");
            }

            TicketEditorData.Load(aConnection, aUserKey, aTicketEditor);
        }
        #endregion

    }
}
