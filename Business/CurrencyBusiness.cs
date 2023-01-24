﻿using Zephry;

namespace Grandmark
{
    public class CurrencyBusiness
    {
        #region Load
        public static void Load(Connection aConnection, UserKey aUserKey, Currency aCurrency)
        {
            if (aCurrency == null)
            {
                throw new ArgumentNullException("aTicketPriority");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "TicketPriority"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "TicketPriority");
            }

            CurrencyData.Load(aConnection, aUserKey, aCurrency);
        }
        #endregion

        #region Load List
        public static void LoadList(Connection aConnection, UserKey aUserKey, List<Currency> aCurrencyList)
        {
            if (aCurrencyList == null)
            {
                throw new ArgumentNullException("aCurrencyList");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "CurrencyList"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "CurrencyList");
            }

            CurrencyData.LoadList(aConnection, aUserKey, aCurrencyList);
        }
        #endregion

        #region Insert
        public static void Insert(Connection aConnection, UserKey aUserKey, Currency aCurrency)
        {
            if (aCurrency == null)
            {
                throw new ArgumentNullException("aCurrency");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Currency"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "Currency");
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

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Currency"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "Currency");
            }

            CurrencyData.Update(aConnection, aUserKey, aCurrency);
        }

        #endregion

        #region Delete

        public static void Delete(Connection aConnection, UserKey aUserKey, CurrencyKey aCurrencyKey)
        {
            if (aCurrencyKey == null)
            {
                throw new ArgumentNullException("aCurrencyKey");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Currency"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "Currency");
            }

            CurrencyData.Delete(aConnection, aUserKey, aCurrencyKey);
        }

        #endregion
    }
}