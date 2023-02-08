using Newtonsoft.Json;
using Zephry;

namespace Grandmark
{
    public class TicketTypeCollection : Zephob
    {
        #region Fields
        private List<TicketType> _ticketTypeList = new();
        #endregion

        #region  Properties
        [JsonProperty("list")]
        public List<TicketType> TicketTypeList { get => _ticketTypeList; set => _ticketTypeList = value; }
        #endregion

        #region Methods
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not TicketTypeCollection)
            {
                throw new ArgumentException("aTicketTypeCollection");
            }

            _ticketTypeList.Clear();
            foreach (var vTypeSource in ((TicketTypeCollection)aSource)._ticketTypeList)
            {
                var vTicketTypeTarget = new TicketType();
                vTicketTypeTarget.AssignFromSource(vTypeSource);
                _ticketTypeList.Add(vTicketTypeTarget);
            }
        }
        #endregion
    }
}
