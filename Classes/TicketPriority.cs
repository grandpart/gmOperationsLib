﻿using Grandmark;
using Newtonsoft.Json;

namespace Grandmark
{
    /// <summary>
    /// TicketPriority class will inherit from TicketPriorityKey class.
    /// TicketPriority class will only have the fields not in the TicketPriorityKey class, as these will pull through from the inheritance.
    /// </summary>
    public class TicketPriority : TicketPriorityKey
    {
        #region Fields
        private string _tprname;
        private int _tprpriority;
        private string? _tprclass;
        #endregion

        #region Properties
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
            base.AssignFromSource(aSource);
            _tprname = ((TicketPriority)aSource)._tprname;
            _tprpriority = ((TicketPriority)aSource)._tprpriority;
            _tprclass = ((TicketPriority)aSource)._tprclass;
        }
        #endregion
    }
}