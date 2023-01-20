using System;
using Zephry;

namespace Grandmark
{
    public class OrganizationProxyBusiness
    {
        #region Load Collection
        public static void Load(Connection aConnection, UserKey aUserKey, OrganizationProxyCollection aOrganizationProxyCollection)
        {
            if (aOrganizationProxyCollection == null)
            {
                throw new ArgumentNullException(nameof(aOrganizationProxyCollection));
            }

            // No access control for lookups
            OrganizationProxyData.Load(aConnection, aUserKey, aOrganizationProxyCollection);
        }
        #endregion

    }
}
