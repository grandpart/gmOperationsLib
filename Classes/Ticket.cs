using Newtonsoft.Json;
using System;
using Zephry;

namespace Grandmark
{
    public enum TicketStatus
    {
        Init = 1,
        Open = 5,
        Closed = 9
    }

    public class Ticket : Zephob
    {
        #region Fields
        private int _entKey;
        private int _tckKey;
        private int _usrKeySrc;
        private string _usrNameSrc = string.Empty;
        private int _usrKeyTgt;
        private string _usrNameTgt = string.Empty;
        private int _orgKey;
        private string _orgName = string.Empty;
        private int _ttpKey;
        private string _ttpName = string.Empty;
        private int _tprKey;
        private string _tprName = string.Empty;
        private int _tckStatus;
        private string _tckStatusString = string.Empty;
        private string _tckDescription = string.Empty;
        private DateTime _tckDateCapture;
        private DateTime? _tckDateAction;
        private DateTime? _tckDateClose;
        private int _tckHoursRequired;
        #endregion

        #region Properties
        public int EntKey { get => _entKey; set { _entKey = value; } }
        public int TckKey { get => _tckKey; set { _tckKey = value; } }
        public int UsrKeySrc { get => _usrKeySrc; set => _usrKeySrc = value; }
        public string UsrNameSrc { get => _usrNameSrc; set => _usrNameSrc = value; }
        public int UsrKeyTgt { get => _usrKeyTgt; set => _usrKeyTgt = value; }
        public string UsrNameTgt { get => _usrNameTgt; set => _usrNameTgt = value; }
        public int OrgKey { get => _orgKey; set => _orgKey = value; }
        public string OrgName { get => _orgName; set => _orgName = value; }
        public int TtpKey { get => _ttpKey; set => _ttpKey = value; }
        public string TtpName { get => _ttpName; set => _ttpName = value; }
        public int TprKey { get => _tprKey; set => _tprKey = value; }
        public string TprName { get => _tprName; set => _tprName = value; }
        public int TckHoursRequired { get => _tckHoursRequired; set => _tckHoursRequired = value; }
        public int TckStatus { get => _tckStatus; set => _tckStatus = value; }
        public string TckStatusString { get => _tckStatusString; set => _tckStatusString = value; }
        public string TckDescription { get => _tckDescription; set => _tckDescription = value; }
        public DateTime TckDateCapture { get => _tckDateCapture; set => _tckDateCapture = value; }
        public DateTime? TckDateAction { get => _tckDateAction; set => _tckDateAction = value; }
        public DateTime? TckDateClose { get => _tckDateClose; set => _tckDateClose = value; }
        #endregion

        #region AssignFromSource
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not Ticket)
            {
                throw new ArgumentException("Invalid Source Argument to Ticket Assign");
            }
            _entKey = ((Ticket)aSource)._entKey;
            _tckKey = ((Ticket)aSource)._tckKey;
            _usrKeySrc = ((Ticket)aSource)._usrKeySrc;
            _usrNameSrc = ((Ticket)aSource)._usrNameSrc;
            _usrKeyTgt = ((Ticket)aSource)._usrKeyTgt;
            _usrNameTgt = ((Ticket)aSource)._usrNameTgt;
            _orgKey = ((Ticket)aSource)._orgKey;
            _orgName = ((Ticket)aSource)._orgName;
            _ttpKey = ((Ticket)aSource)._ttpKey;
            _ttpName = ((Ticket)aSource)._ttpName;
            _tprKey = ((Ticket)aSource)._tprKey;
            _tprName = ((Ticket)aSource)._tprName;
            _tckStatus = ((Ticket)aSource)._tckStatus;
            _tckStatusString = ((Ticket)aSource)._tckStatusString;
            _tckDescription = ((Ticket)aSource)._tckDescription;
            _tckDateCapture = ((Ticket)aSource)._tckDateCapture;
            _tckDateAction = ((Ticket)aSource)._tckDateAction;
            _tckDateClose = ((Ticket)aSource)._tckDateClose;
            _tckHoursRequired = ((Ticket)aSource)._tckHoursRequired;
        }
        #endregion
    }
}
