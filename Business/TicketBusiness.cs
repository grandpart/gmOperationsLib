using Zephry;

namespace Grandmark
{
    public class TicketBusiness
    {
        #region Load
        public static void Load(Connection aConnection, UserKey aUserKey, Ticket aTicket)
        {
            if (aTicket == null)
            {
                throw new TransactionStatusException(TransactionResult.BadData, "Ticket object is null");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Ticket"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "Ticket");
            }

            TicketData.Load(aConnection, aUserKey, aTicket);
        }
        #endregion

        #region Load Collection
        public static void Load(Connection aConnection, UserKey aUserKey, TicketCollection aTicketCollection)
        {
            if (aTicketCollection == null)
            {
                throw new TransactionStatusException(TransactionResult.BadData, "TicketCollection object is null");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "TicketCollection"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "TicketCollection");
            }

            TicketData.Load(aConnection, aUserKey, aTicketCollection);
        }
        #endregion

        #region Insert
        public static void Insert(Connection aConnection, UserKey aUserKey, Ticket aTicket)
        {
            if (aTicket == null)
            {
                throw new TransactionStatusException(TransactionResult.BadData, "Ticket object is null");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Ticket"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "Ticket");
            }

            TicketData.Insert(aConnection, aUserKey, aTicket);
        }

        #endregion

        #region Update
        public static void Update(Connection aConnection, UserKey aUserKey, Ticket aTicket)
        {
            if (aTicket == null)
            {
                throw new TransactionStatusException(TransactionResult.BadData, "Ticket object is null");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Ticket"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "Ticket");
            }

            TicketData.Update(aConnection, aUserKey, aTicket);
        }

        #endregion

        #region Delete

        public static void Delete(Connection aConnection, UserKey aUserKey, Ticket aTicket)
        {
            if (aTicket == null)
            {
                throw new TransactionStatusException(TransactionResult.BadData, "Ticket object is null");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Ticket"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "Ticket");
            }

            TicketData.Delete(aConnection, aUserKey, aTicket);
        }

        #endregion
    }
}
