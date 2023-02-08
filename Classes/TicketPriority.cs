using Newtonsoft.Json;
using Zephry;

namespace Grandmark
{
    /// <summary>
    /// TicketPriority class will inherit from TicketPriorityKey class.
    /// TicketPriority class will only have the fields not in the TicketPriorityKey class, as these will pull through from the inheritance.
    /// </summary>
    public class TicketPriority : Zephob
    {
        #region Fields
        private int _entkey;
        private int _tprkey;
        private string _tprname = string.Empty;
        private int _tprpriority;
        private string? _tprclass;
        #endregion

        #region Properties
        [JsonProperty("entkey")]
        public int EntKey { get => _entkey; set { _entkey = value; } }
        [JsonProperty("tprkey")]
        public int TprKey { get => _tprkey; set { _tprkey = value; } }
        [JsonProperty("tprname")]
        public string TprName { get { return _tprname; } set { _tprname = value; } }
        [JsonProperty("tprpriority")]
        public int TprPriority { get { return _tprpriority; } set { _tprpriority = value; } }
        [JsonProperty("tprclass")]
        public string? TprClass { get { return _tprclass; } set { _tprclass = value; } }
        #endregion

        #region AssignFromSource
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not TicketPriority)
            {
                throw new ArgumentException("Invalid Source Argument to TicketPriority Assign");
            }
            _entkey = ((TicketPriority)aSource)._entkey;
            _tprkey = ((TicketPriority)aSource)._tprkey;
            _tprname = ((TicketPriority)aSource)._tprname;
            _tprpriority = ((TicketPriority)aSource)._tprpriority;
            _tprclass = ((TicketPriority)aSource)._tprclass;
        }
        #endregion
    }
}
