using System;
using Zephry;

namespace Grandmark
{
    /// <summary>
    ///   UserBusiness class.
    /// </summary>
    public class UserBusiness
    {
        #region Load by ID
        /// <summary>
        /// Load by ID User
        /// </summary>
        /// <param name="aConnection"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aUser"></param>
        public static void LoadById (Connection aConnection, User aUser)
        {
            if (aUser == null)
            {
                throw new ArgumentNullException("aUser");
            }

            if (String.IsNullOrWhiteSpace(aUser.UsrUserId))
            {
                throw new ArgumentNullException("Empty ID in LoadByID User Business");
            }

            UserData.LoadById(aConnection, aUser);
        }

        #endregion

        #region Load
        /// <summary>
        /// Load User
        /// </summary>
        /// <param name="aConnection"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aUser"></param>
        public static void Load(Connection aConnection, UserKey aUserKey, User aUser)
        {
            if (aUser == null)
            {
                throw new ArgumentNullException("aUser");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "User"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "User");
            }

            UserData.Load(aConnection, aUser);
        }

        #endregion

        #region Insert
        /// <summary>
        /// Insert by User User
        /// </summary>
        /// <param name="aConnection"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aUser"></param>
        public static void Insert(Connection aConnection, UserKey aUserKey, User aUser)
        {
            if (aUser == null)
            {
                throw new ArgumentNullException("aUser");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "User"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "User");
            }

            UserData.Insert(aConnection, aUser);
        }

        #endregion

        #region Update
        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="aConnection"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aUser"></param>
        public static void Update(Connection aConnection, UserKey aUserKey, User aUser)
        {
            if (aUser == null)
            {
                throw new ArgumentNullException("aUser");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "User"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "User");
            }

            UserData.Update(aConnection, aUserKey, aUser);
        }

        #endregion

        #region Delete
        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="aConnection"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aUser"></param>
        public static void Delete(Connection aConnection, UserKey aUserKey, User aUser)
        {
            if (aUser == null)
            {
                throw new ArgumentNullException("aUser");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "User"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "User");
            }

            UserData.Delete(aConnection, aUserKey, aUser);
        }

        #endregion
    }
}