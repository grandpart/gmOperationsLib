using Grandmark;
using Newtonsoft.Json;

namespace Grandmark
{
    public class Currency : CurrencyKey
    {
        #region Fields
        private string _curcode;
        private string _curprefix;
        private string _curname;
        #endregion

        #region Properties
        [JsonProperty("curcode")]
        public string CurCode { get { return _curcode; } set { _curcode = value; } }

        [JsonProperty("curprefix")]
        public string CurPrefix { get { return _curprefix; } set { _curprefix = value; } }

        [JsonProperty("curname")]
        public string CurName { get { return _curname; } set { _curname = value; } }

        #endregion

        #region AssignFromSource
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not TicketPriority)
            {
                throw new ArgumentException("Invalid Source Argument to Currency Assign");
            }
            base.AssignFromSource(aSource);
            _curcode = ((Currency)aSource)._curcode;
            _curprefix = ((Currency)aSource)._curprefix;
            _curname = ((Currency)aSource)._curname;
        }
        #endregion
    }
}
