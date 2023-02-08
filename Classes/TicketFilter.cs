using Grandmark;
using Newtonsoft.Json;
using Zephry;

namespace Grandmark
{
    /// <summary>
    /// TicketFilter filters a Ticket
    /// </summary>
    public class TicketFilter : Zephob
    {
        #region Fields
        private RippleSelect _usrRipple;
        private RippleSelect _orgRipple;
        private TicketStatus _status = TicketStatus.Open;
        private string _find = string.Empty;

        #endregion
        public RippleSelect UsrRipple { get => _usrRipple; set => _usrRipple = value; }
        public RippleSelect OrgRipple { get => _orgRipple; set => _orgRipple = value; }
        public TicketStatus Status { get => _status; set => _status = value; }
        public string Find { get => _find; set => _find = value; }
        #region Properties

        #endregion

        #region AssignFromSource
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not TicketFilter)
            {
                throw new ArgumentException("Invalid Source Argument to TicketFilter Assign");
            }
            _usrRipple = ((TicketFilter)aSource)._usrRipple;
            _orgRipple = ((TicketFilter)aSource)._orgRipple;
            _status = ((TicketFilter)aSource)._status;
            _find = ((TicketFilter)aSource)._find;
        }
        #endregion
    }
}
