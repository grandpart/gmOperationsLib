using Zephry;

namespace Grandmark
{
    public class DepartmentBusiness
    {
        #region Load
        public static void Load(Connection aConnection, UserKey aUserKey, Department aDepartment)
        {
            if (aDepartment == null)
            {
                throw new ArgumentNullException("aDepartment");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Department"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "Department");
            }

            DepartmentData.Load(aConnection, aUserKey, aDepartment);
        }
        #endregion

        #region Load List
        public static void LoadList(Connection aConnection, UserKey aUserKey, DepartmentCollection aDepartmentCollection)
        {
            if (aDepartmentCollection == null)
            {
                throw new ArgumentNullException("aDepartmentCollection");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "DepartmentCollection"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "DepartmentCollection");
            }

            DepartmentData.Load(aConnection, aUserKey, aDepartmentCollection);
        }
        #endregion

        #region Insert
        public static void Insert(Connection aConnection, UserKey aUserKey, Department aDepartment)
        {
            if (aDepartment == null)
            {
                throw new ArgumentNullException("aDepartment");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Department"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "Department");
            }

            DepartmentData.Insert(aConnection, aUserKey, aDepartment);
        }

        #endregion

        #region Update
        public static void Update(Connection aConnection, UserKey aUserKey, Department aDepartment)
        {
            if (aDepartment == null)
            {
                throw new ArgumentNullException("aDepartment");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Department"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "Department");
            }

            DepartmentData.Update(aConnection, aUserKey, aDepartment);
        }

        #endregion

        #region Delete

        public static void Delete(Connection aConnection, UserKey aUserKey, Department aDepartment)
        {
            if (aDepartment == null)
            {
                throw new ArgumentNullException("aDepartment");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Department"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "Department");
            }

            DepartmentData.Delete(aConnection, aUserKey, aDepartment);
        }

        #endregion
    }
}
