using Zephry;

namespace Grandmark
{
    public class TicketPriorityProxyBusiness
    {
        #region Load Collection
        public static void Load(Connection aConnection, UserKey aUserKey, TicketPriorityProxyCollection aTicketPriorityProxyCollection)
        {
            if (aTicketPriorityProxyCollection == null)
            {
                throw new ArgumentNullException(nameof(aTicketPriorityProxyCollection));
            }

            // No access control for lookups
            TicketPriorityProxyData.Load(aConnection, aUserKey, aTicketPriorityProxyCollection);
        }
        #endregion
    }
}
