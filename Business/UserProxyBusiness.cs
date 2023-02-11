using System;
using Zephry;

namespace Grandmark
{
    public class UserProxyBusiness
    {
        #region Load Collection
        public static void Load(Connection aConnection, UserKey aUserKey, UserProxyCollection aUserProxyCollection)
        {
            if (aUserProxyCollection == null)
            {
                throw new ArgumentNullException(nameof(aUserProxyCollection));
            }

            // No access control for lookups
            UserProxyData.Load(aConnection, aUserKey, aUserProxyCollection);
        }
        #endregion

    }
}
