using Zephry;

namespace Grandmark
{
    public class CustomerStreamBusiness
    {
        #region Load
        public static void Load(Connection aConnection, UserKey aUserKey, KeyValueCollection aKeyValueCollection)
        {
            if (aKeyValueCollection == null)
            {
                throw new ArgumentNullException("aKeyValueCollection");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "aKeyValueCollection"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "aKeyValueCollection");
            }

            CustomerStreamData.Load(aConnection, aUserKey, aKeyValueCollection);
        }
        #endregion

    }
}
