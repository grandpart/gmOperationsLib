using Zephry;

namespace Grandmark
{
    /// <summary>
    /// TicketType describes a Ticket
    /// </summary>
    public class TicketEditor : Zephob
    {
        #region Fields
        private List<KeyValue>? _ttpList = new();
        private List<KeyValue>? _tprList = new();
        private Ticket? _ticket = new();
        #endregion

        #region Properties
        public List<KeyValue>? TtpList { get => _ttpList; set => _ttpList = value; }
        public List<KeyValue>? TprList { get => _tprList; set => _tprList = value; }
        public Ticket? Ticket { get => _ticket; set => _ticket = value; }
        #endregion

        #region AssignFromSource
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not TicketType)
            {
                throw new ArgumentException("Invalid Source Argument to TicketType Assign");
            }
            _ttpList = ((TicketEditor)aSource)._ttpList;
            _tprList = ((TicketEditor)aSource)._tprList;
            _ticket = ((TicketEditor)aSource)._ticket;
        }
        #endregion
    }
}
