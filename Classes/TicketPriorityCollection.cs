using Newtonsoft.Json;
using Zephry;

namespace Grandmark
{
    public class TicketPriorityCollection : Zephob
    {
        #region Fields
        private List<TicketPriority> _ticketPriorityList = new();
        #endregion

        #region  Properties
        [JsonProperty("list")]
        public List<TicketPriority> TicketPriorityList { get => _ticketPriorityList; set => _ticketPriorityList = value; }
        #endregion

        #region Methods
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not TicketPriorityCollection)
            {
                throw new ArgumentException("aTicketPriorityCollection");
            }

            _ticketPriorityList.Clear();
            foreach (var vCurrencySource in ((TicketPriorityCollection)aSource)._ticketPriorityList)
            {
                var vTicketPriorityTarget = new TicketPriority();
                vTicketPriorityTarget.AssignFromSource(vCurrencySource);
                _ticketPriorityList.Add(vTicketPriorityTarget);
            }
        }
        #endregion
    }
}
