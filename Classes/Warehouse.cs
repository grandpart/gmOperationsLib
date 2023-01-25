using Newtonsoft.Json;

namespace Grandmark
{
    public class Warehouse : WarehouseKey
    {
        #region Fields
        private string _whsname;
        private string _whscode;
        private bool _whsistradingwarehouse;
        #endregion

        #region Properties

        [JsonProperty("whsname")]
        public string WhsName { get { return _whsname; } set { _whsname = value; } }

        [JsonProperty("whscode")]
        public string WhsCode { get { return _whscode; } set { _whscode = value; } }

        [JsonProperty("istradingwarehouse")]
        public bool WhsIsTradingWarehouse { get { return _whsistradingwarehouse;} set { _whsistradingwarehouse = value;} }

        #endregion

        #region AssignFromSource
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not Warehouse)
            {
                throw new ArgumentException("Invalid Source Argument to Warehouse Assign");
            }
            base.AssignFromSource(aSource);
            _whsname = ((Warehouse)aSource)._whsname;
            _whscode = ((Warehouse)aSource)._whscode;
            _whsistradingwarehouse = ((Warehouse)aSource)._whsistradingwarehouse;
        }
        #endregion
    }
}
