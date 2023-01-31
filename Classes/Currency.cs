using Grandmark;
using Newtonsoft.Json;
using Zephry;

namespace Grandmark
{
    public class Currency : Zephob
    {
        #region Fields

        private int _entkey;
        private int _curkey;
        private string _curcode;
        private string _curprefix;
        private string _curname;
        #endregion

        #region Properties

        [JsonProperty("entkey")]
        public int EntKey { get { return _entkey; } set { _entkey = value; } }
        [JsonProperty("curkey")]
        public int CurKey { get { return _curkey; } set { _curkey = value; } }

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
            _entkey = ((Currency)aSource)._entkey;
            _curkey= ((Currency)aSource)._curkey;
            _curcode = ((Currency)aSource)._curcode;
            _curprefix = ((Currency)aSource)._curprefix;
            _curname = ((Currency)aSource)._curname;
        }
        #endregion
    }
}
