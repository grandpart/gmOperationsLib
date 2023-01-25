using Newtonsoft.Json;
using Zephry;

namespace Grandmark
{
    public class WarehouseCollection : Zephob
    {
        #region Fields
        private List<Warehouse> _warehouseList = new();
        #endregion

        #region Properties
        [JsonProperty("list")]
        public List<Warehouse> WarehouseList { get { return _warehouseList; } set { _warehouseList = value; } }
        #endregion

        #region Methods
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not WarehouseCollection)
            {
                throw new ArgumentException("aWarehouseCollection");
            }

            _warehouseList.Clear();
            foreach (var vWarehouseSource in ((WarehouseCollection)aSource)._warehouseList)
            {
                var vWarehouseTarget = new Warehouse();
                vWarehouseTarget.AssignFromSource(vWarehouseSource);
                _warehouseList.Add(vWarehouseTarget);
            }
        }
        #endregion
    }
}
