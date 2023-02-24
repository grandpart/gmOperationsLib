using Zephry;

namespace Grandmark
{
    public class CurrencyBusiness
    {
        #region Load
        public static void Load(Connection aConnection, UserKey aUserKey, Currency aCurrency)
        {
            if (aCurrency == null)
            {
                throw new ArgumentNullException("aCurrency");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Creditor"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "Creditor");
            }

            CurrencyData.Load(aConnection, aUserKey, aCurrency);
        }
        #endregion

        #region Load List
        public static void Load(Connection aConnection, UserKey aUserKey, CurrencyCollection aCurrencyCollection)
        {
            if (aCurrencyCollection == null)
            {
                throw new ArgumentNullException("aCurrencyList");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Creditor"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "Creditor");
            }

            CurrencyData.Load(aConnection, aUserKey, aCurrencyCollection);
        }
        #endregion

        #region Insert
        public static void Insert(Connection aConnection, UserKey aUserKey, Currency aCurrency)
        {
            if (aCurrency == null)
            {
                throw new ArgumentNullException("aCurrency");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Creditor"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "Creditor");
            }

            CurrencyData.Insert(aConnection, aUserKey, aCurrency);
        }

        #endregion

        #region Update
        public static void Update(Connection aConnection, UserKey aUserKey, Currency aCurrency)
        {
            if (aCurrency == null)
            {
                throw new ArgumentNullException("aCurrency");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Creditor"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "Creditor");
            }

            CurrencyData.Update(aConnection, aUserKey, aCurrency);
        }

        #endregion

        #region Delete

        public static void Delete(Connection aConnection, UserKey aUserKey, Currency aCurrency)
        {
            if (aCurrency == null)
            {
                throw new ArgumentNullException("aCurrency");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Creditor"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "Creditor");
            }

            CurrencyData.Delete(aConnection, aUserKey, aCurrency);
        }

        #endregion
    }
}
