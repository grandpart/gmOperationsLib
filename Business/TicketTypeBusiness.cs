using Zephry;

namespace Grandmark
{
    public class TicketTypeBusiness
    {
        #region Load
        public static void Load(Connection aConnection, UserKey aUserKey, TicketType aTicketType)
        {
            if (aTicketType == null)
            {
                throw new ArgumentNullException("aTicketType");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "TicketType"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "TicketType");
            }

            TicketTypeData.Load(aConnection, aUserKey, aTicketType);
        }
        #endregion

        #region Load Collection
        public static void Load(Connection aConnection, UserKey aUserKey, TicketTypeCollection aTicketTypeCollection)
        {
            if (aTicketTypeCollection == null)
            {
                throw new ArgumentNullException("aTicketTypeCollection");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "TicketTypeCollection"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "TicketTypeCollection");
            }

            TicketTypeData.Load(aConnection, aUserKey, aTicketTypeCollection);
        }
        #endregion

        #region Insert
        public static void Insert(Connection aConnection, UserKey aUserKey, TicketType aTicketType)
        {
            if (aTicketType == null)
            {
                throw new ArgumentNullException("aTicketType");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "TicketType"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "TicketType");
            }

            TicketTypeData.Insert(aConnection, aUserKey, aTicketType);
        }

        #endregion

        #region Update
        public static void Update(Connection aConnection, UserKey aUserKey, TicketType aTicketType)
        {
            if (aTicketType == null)
            {
                throw new ArgumentNullException("aTicketType");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "TicketType"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "TicketType");
            }

            TicketTypeData.Update(aConnection, aUserKey, aTicketType);
        }

        #endregion

        #region Delete

        public static void Delete(Connection aConnection, UserKey aUserKey, TicketType aTicketType)
        {
            if (aTicketType == null)
            {
                throw new ArgumentNullException("aTicketType");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "TicketType"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "TicketType");
            }

            TicketTypeData.Delete(aConnection, aUserKey, aTicketType);
        }

        #endregion
    }
}
