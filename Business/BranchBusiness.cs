using Grandmark;
using Zephry;

namespace Grandmark
{
    public class BranchBusiness
    {
        #region Load
        public static void Load(Connection aConnection, UserKey aUserKey, Branch aBranch)
        {
            if (aBranch == null)
            {
                throw new ArgumentNullException("aBranch");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Branch"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "Branch");
            }

            BranchData.Load(aConnection, aUserKey, aBranch);
        }
        #endregion

        #region Load List
        public static void LoadList(Connection aConnection, UserKey aUserKey, BranchCollection aBranchCollection)
        {
            if (aBranchCollection == null)
            {
                throw new ArgumentNullException("aBranchCollection");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "BranchCollection"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "BranchCollection");
            }

            BranchCollectionData.Load(aConnection, aUserKey, aBranchCollection);
        }
        #endregion

        #region Insert
        public static void Insert(Connection aConnection, UserKey aUserKey, Branch aBranch)
        {
            if (aBranch == null)
            {
                throw new ArgumentNullException("aBranch");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Branch"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "Branch");
            }

            BranchData.Insert(aConnection, aUserKey, aBranch);
        }

        #endregion

        #region Update
        public static void Update(Connection aConnection, UserKey aUserKey, Branch aBranch)
        {
            if (aBranch == null)
            {
                throw new ArgumentNullException("aBranch");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Branch"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "Branch");
            }

            BranchData.Update(aConnection, aUserKey, aBranch);
        }

        #endregion

        #region Delete

        public static void Delete(Connection aConnection, UserKey aUserKey, BranchKey aBranchKey)
        {
            if (aBranchKey == null)
            {
                throw new ArgumentNullException("aBranchKey");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Branch"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "Branch");
            }

            BranchData.Delete(aConnection, aUserKey, aBranchKey);
        }

        #endregion
    }
}
