using Zephry;

namespace Grandmark
{
    public class TicketCollection : Zephob
    {
        #region Fields
        private List<Ticket> _list = new();
        #endregion

        #region  Properties
        public List<Ticket> List { get => _list; set => _list = value; }
        #endregion

        #region Methods
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not TicketCollection)
            {
                throw new ArgumentException("aTicketCollection");
            }

            _list.Clear();
            foreach (var vSource in ((TicketCollection)aSource)._list)
            {
                var vTicketTarget = new Ticket();
                vTicketTarget.AssignFromSource(vSource);
                _list.Add(vTicketTarget);
            }
        }
        #endregion
    }
}
