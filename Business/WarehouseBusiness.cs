using Zephry;

namespace Grandmark
{
    public class WarehouseBusiness
    {
        #region Load
        public static void Load(Connection aConnection, UserKey aUserKey, Warehouse aWarehouse)
        {
            if (aWarehouse == null)
            {
                throw new ArgumentNullException("aWarehouse");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Warehouse"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "Warehouse");
            }

            WarehouseData.Load(aConnection, aUserKey, aWarehouse);
        }
        #endregion

        #region Load List
        public static void LoadList(Connection aConnection, UserKey aUserKey, WarehouseCollection aWarehouseCollection)
        {
            if (aWarehouseCollection == null)
            {
                throw new ArgumentNullException("aWarehouseCollection");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "WarehouseCollection"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "WarehouseCollection");
            }

            WarehouseCollectionData.Load(aConnection, aUserKey, aWarehouseCollection);
        }
        #endregion

        #region Insert
        public static void Insert(Connection aConnection, UserKey aUserKey, Warehouse aWarehouse)
        {
            if (aWarehouse == null)
            {
                throw new ArgumentNullException("aWarehouse");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Warehouse"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "Warehouse");
            }

            WarehouseData.Insert(aConnection, aUserKey, aWarehouse);
        }

        #endregion

        #region Update
        public static void Update(Connection aConnection, UserKey aUserKey, Warehouse aWarehouse)
        {
            if (aWarehouse == null)
            {
                throw new ArgumentNullException("aWarehouse");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Warehouse"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "Warehouse");
            }

            WarehouseData.Update(aConnection, aUserKey, aWarehouse);
        }

        #endregion

        #region Delete

        public static void Delete(Connection aConnection, UserKey aUserKey, WarehouseKey aWarehouseKey)
        {
            if (aWarehouseKey == null)
            {
                throw new ArgumentNullException("aWarehouseKey");
            }

            if (!SysRoleAccessData.HasRoleAccess(aConnection, aUserKey, "Warehouse"))
            {
                throw new TransactionStatusException(TransactionResult.Access, "Warehouse");
            }

            WarehouseData.Delete(aConnection, aUserKey, aWarehouseKey);
        }

        #endregion
    }
}
