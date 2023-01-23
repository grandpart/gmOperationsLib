﻿using Zephry;

namespace Grandmark
{
    public class TicketPriorityBusiness
    {
        #region Load
        public static void Load(Connection aConnection, UserKey aUserKey, TicketPriority aTicketPriority)
        {
            if (aTicketPriority == null)
            {
                throw new ArgumentNullException("aTicketPriority");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "TicketPriority"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "TicketPriority");
            }

            TicketPriorityData.Load(aConnection, aUserKey, aTicketPriority);
        }
        #endregion

        #region Load List
        public static void LoadList(Connection aConnection, UserKey aUserKey, List<TicketPriority> aTicketPriorityList)
        {
            if (aTicketPriorityList == null)
            {
                throw new ArgumentNullException("aTicketPriority");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "TicketPriority"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "TicketPriority");
            }

            TicketPriorityData.LoadList(aConnection, aUserKey, aTicketPriorityList);
        }
        #endregion

        #region Insert
        public static void Insert(Connection aConnection, UserKey aUserKey, TicketPriority aTicketPriority)
        {
            if (aTicketPriority == null)
            {
                throw new ArgumentNullException("aTicketPriority");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "TicketPriority"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "TicketPriority");
            }

            TicketPriorityData.Insert(aConnection, aUserKey, aTicketPriority);
        }

        #endregion

        #region Update
        public static void Update(Connection aConnection, UserKey aUserKey, TicketPriority aTicketPriority)
        {
            if (aTicketPriority == null)
            {
                throw new ArgumentNullException("aTicketPriority");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "TicketPriority"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "TicketPriority");
            }

            TicketPriorityData.Update(aConnection, aUserKey, aTicketPriority);
        }

        #endregion

        #region Delete

        public static void Delete(Connection aConnection, UserKey aUserKey, TicketPriorityKey aTicketPriorityKey)
        {
            if (aTicketPriorityKey == null)
            {
                throw new ArgumentNullException("aTicketPriority");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "TicketPriority"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "TicketPriority");
            }

            TicketPriorityData.Delete(aConnection, aUserKey, aTicketPriorityKey);
        }

        #endregion
    }
}
