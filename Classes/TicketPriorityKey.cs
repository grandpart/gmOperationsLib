using Grandmark;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json.Serialization;
using Zephry;

namespace gmOperationsLib.Classes
{
    public class TicketPriorityKey : Zephob
    {
        #region Fields
        private int _entkey;
        private int _tprkey;
        #endregion


        #region Properties

        [JsonProperty("entkey")]
        public int EntKey { get => _entkey; set { _entkey = value; } }

        [JsonProperty("tprkey")]
        public int TprKey { get => _tprkey; set { _tprkey = value; } }
        #endregion


        #region Constructors
        public TicketPriorityKey() { }

        public TicketPriorityKey(int aEntKey, int aTprKey)
        {
            _entkey = aEntKey;
            _tprkey = aTprKey;
        }
        #endregion


        #region Comparer
        public class EqualityComparer : IEqualityComparer<TicketPriorityKey>
        {
            public bool Equals(TicketPriorityKey aTicketPriorityKey1, TicketPriorityKey aTicketPriorityKey2)
            {
                return aTicketPriorityKey1._entkey == aTicketPriorityKey2._entkey &&
                    aTicketPriorityKey1._tprkey == aTicketPriorityKey2._tprkey;
            }

            public int GetHashCode(TicketPriorityKey aTicketPriorityKey)
            {
                return Convert.ToInt32(aTicketPriorityKey._entkey) ^ Convert.ToInt32(aTicketPriorityKey._tprkey);
            }
        }
        #endregion


        #region AssignmentFromSource
        public override void AssignFromSource(object aSource)
        {
            if (!(aSource is TicketPriorityKey))
            {
                throw new ArgumentException("Invalid assignment source", "TicketPriorityKey");
            }
            _entkey = ((TicketPriorityKey)aSource)._entkey;
            _tprkey = ((TicketPriorityKey)aSource)._tprkey;
        }
        #endregion
    }
}
