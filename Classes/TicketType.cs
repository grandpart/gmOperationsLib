using Grandmark;
using Newtonsoft.Json;
using Zephry;

namespace Grandmark
{
    /// <summary>
    /// TicketType describes a Ticket
    /// </summary>
    public class TicketType : Zephob
    {
        #region Fields
        private int _entkey;
        private int _ttpkey;
        private string _ttpname = string.Empty;
        private string? _ttpclass;
        #endregion

        #region Properties
        [JsonProperty("entkey")]
        public int EntKey { get => _entkey; set { _entkey = value; } }
        [JsonProperty("ttpkey")]
        public int TtpKey { get => _ttpkey; set { _ttpkey = value; } }
        [JsonProperty("ttpname")]
        public string TtpName { get { return _ttpname; } set { _ttpname = value; } }
        [JsonProperty("ttpclass")]
        public string? TtpClass { get { return _ttpclass; } set { _ttpclass = value; } }
        #endregion

        #region AssignFromSource
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not TicketType)
            {
                throw new ArgumentException("Invalid Source Argument to TicketType Assign");
            }
            _entkey = ((TicketType)aSource)._entkey;
            _ttpkey = ((TicketType)aSource)._ttpkey;
            _ttpname = ((TicketType)aSource)._ttpname;
            _ttpclass = ((TicketType)aSource)._ttpclass;
        }
        #endregion
    }
}
