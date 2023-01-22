using Grandmark;

namespace Grandmark
{
    public class TicketPriorityProxy : TicketPriorityKey
    {

        private string _tprName;
        private int _tprPriority;
        private string _tprClass;
        private List<TicketPriorityProxy> _ticketPriorityProxyList = new();

        public string TprName
        {
            get { return _tprName; }
            set { _tprName = value; }
        }

        public int TprPriority
        {
            get { return _tprPriority; }
            set { _tprPriority = value; }
        }

        public string TprClass
        {
            get { return _tprClass; }
            set { _tprClass = value; }
        }

        public List<TicketPriorityProxy> TicketPriorityProxyList
        {
            get { return _ticketPriorityProxyList; }
            set { _ticketPriorityProxyList = value; }
        }


        public override void AssignFromSource(object aSource)
        {
            if (!(aSource is TicketPriorityProxy))
            {
                throw new ArgumentException("aTicketPriorityProxy");
            }

            base.AssignFromSource(aSource);
            _tprName = ((TicketPriorityProxy)aSource)._tprName;
            _tprPriority = ((TicketPriorityProxy)aSource)._tprPriority;
            _tprClass = ((TicketPriorityProxy)aSource)._tprClass;
            ((TicketPriorityProxy)aSource)._ticketPriorityProxyList.Clear();
            ((TicketPriorityProxy)aSource)._ticketPriorityProxyList.ForEach(vSourceTicketPriorityProxy =>
            {
                var vTargetOrganizationProxy = new TicketPriorityProxy();
                vTargetOrganizationProxy.AssignFromSource(vSourceTicketPriorityProxy);
                _ticketPriorityProxyList.Add(vTargetOrganizationProxy);
            });
        }
    }
}
