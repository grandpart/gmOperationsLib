using Zephry;

namespace Grandmark
{
    public class ExchangeRateBusiness
    {
        #region Load
        public static void Load(Connection aConnection, UserKey aUserKey, ExchangeRate aExchangeRate)
        {
            if (aExchangeRate == null)
            {
                throw new ArgumentNullException("aExchangeRate");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "ExchangeRate"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "ExchangeRate");
            }

            ExchangeRateData.Load(aConnection, aUserKey, aExchangeRate);
        }
        #endregion

        #region Load List
        public static void LoadList(Connection aConnection, UserKey aUserKey, ExchangeRateCollection aExchangeRateCollection)
        {
            if (aExchangeRateCollection == null)
            {
                throw new ArgumentNullException("aExchangeRateCollection");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "ExchangeRateCollection"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "ExchangeRateCollection");
            }

            ExchangeRateCollectionData.Load(aConnection, aUserKey, aExchangeRateCollection);
        }
        #endregion

        #region Insert
        public static void Insert(Connection aConnection, UserKey aUserKey, ExchangeRate aExchangeRate)
        {
            if (aExchangeRate == null)
            {
                throw new ArgumentNullException("aExchangeRate");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "ExchangeRate"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "ExchangeRate");
            }

            ExchangeRateData.Insert(aConnection, aUserKey, aExchangeRate);
        }

        #endregion

        #region Update
        public static void Update(Connection aConnection, UserKey aUserKey, ExchangeRate aExchangeRate)
        {
            if (aExchangeRate == null)
            {
                throw new ArgumentNullException("aExchangeRate");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "ExchangeRate"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "ExchangeRate");
            }

            ExchangeRateData.Update(aConnection, aUserKey, aExchangeRate);
        }

        #endregion

        #region Delete

        public static void Delete(Connection aConnection, UserKey aUserKey, ExchangeRate aExchangeRate)
        {
            if (aExchangeRate == null)
            {
                throw new ArgumentNullException("aExchangeRate");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "ExchangeRate"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "ExchangeRate");
            }

            ExchangeRateData.Delete(aConnection, aUserKey, aExchangeRate);
        }

        #endregion
    }
}
