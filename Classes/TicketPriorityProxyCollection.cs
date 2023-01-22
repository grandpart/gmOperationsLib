using System.Runtime.InteropServices;
using Zephry;

namespace Grandmark
{
    public class TicketPriorityProxyCollection : Zephob
    {

        private List<TicketPriorityProxy> _ticketPriorityList = new List<TicketPriorityProxy>();


        public List<TicketPriorityProxy> TicketPriorityList
        {
            get { return _ticketPriorityList; }
            set { _ticketPriorityList = value; }
        }

        public override void AssignFromSource(object aSource)
        {
            if (!(aSource is TicketPriorityProxyCollection))
            {
                throw new ArgumentException("aTicketPriorityProxyCollection");
            }

            _ticketPriorityList.Clear();
            foreach (var vTicketPriorityProxySource in ((TicketPriorityProxyCollection)aSource)._ticketPriorityList)
            {
                var vOrganizationProxyTarget = new OrganizationProxy();
                vOrganizationProxyTarget.AssignFromSource(vTicketPriorityProxySource);
                _ticketPriorityList.Add(vTicketPriorityProxySource);
            }
        }
    }
}
