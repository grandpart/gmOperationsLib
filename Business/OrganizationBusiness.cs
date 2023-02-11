using System;
using Zephry;

namespace Grandmark
{
    /// <summary>
    ///   OrganizationBusiness class.
    /// </summary>
    public class OrganizationBusiness
    {
        #region Load
        public static void Load(Connection aConnection, UserKey aUserKey, Organization aOrganization)
        {
            if (aOrganization == null)
            {
                throw new ArgumentNullException("aOrganization");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Organization"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "Organization");
            }

            OrganizationData.Load(aConnection, aUserKey, aOrganization);
        }

        #endregion

        #region Insert
        public static void Insert(Connection aConnection, UserKey aUserKey, Organization aOrganization)
        {
            if (aOrganization == null)
            {
                throw new ArgumentNullException("aOrganization");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Organization"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "Organization");
            }

            OrganizationData.Insert(aConnection, aUserKey, aOrganization);
        }

        #endregion

        #region Update
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aConnection"></param>
        /// <param name="aUserKey"></param>
        /// <param name="aOrganization"></param>
        public static void Update(Connection aConnection, UserKey aUserKey, Organization aOrganization)
        {
            if (aOrganization == null)
            {
                throw new ArgumentNullException("aOrganization");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Organization"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "Organization");
            }

            OrganizationData.Update(aConnection, aUserKey, aOrganization);
        }

        #endregion

        #region Delete

        public static void Delete(Connection aConnection, UserKey aUserKey, Organization aOrganization)
        {
            if (aOrganization == null)
            {
                throw new ArgumentNullException("aOrganization");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Organization"))
            {
                throw new TransactionStatusException(TransactionResult.Role, "Organization");
            }

            OrganizationData.Delete(aConnection, aUserKey, aOrganization);
        }

        #endregion
    }
}