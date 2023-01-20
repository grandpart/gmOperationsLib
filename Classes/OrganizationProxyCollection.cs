using System;
using System.Collections.Generic;
using System.Linq;
using Zephry;

namespace Grandmark
{
    /// <summary>
    ///   OrganizationProxyCollection class.
    /// </summary>
    /// <remarks>
    ///   namespace Grandmark
    /// </remarks>
    public class OrganizationProxyCollection : Zephob
    {
        #region Fields

        private bool _flat = false;
        private List<OrganizationProxy> _organizationList = new List<OrganizationProxy>();

        #endregion

        #region  Properties

        public bool Flat
        {
            get { return _flat; }
            set { _flat = value; }
        }

        public List<OrganizationProxy> OrganizationProxyList
        {
            get { return _organizationList; }
            set { _organizationList = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        ///    Assigns all <c>aSource</c> object's values to this instance of <see cref="OrganizationProxyCollection"/>.
        /// </summary>
        /// <param name="aSource">A source object.</param>
        public override void AssignFromSource(object aSource)
        {
            if (!(aSource is OrganizationProxyCollection))
            {
                throw new ArgumentException("aOrganizationProxyCollection");
            }

            _flat = ((OrganizationProxyCollection) aSource)._flat;
            _organizationList.Clear();
            foreach (var vOrganizationProxySource in ((OrganizationProxyCollection) aSource)._organizationList)
            {
                var vOrganizationProxyTarget = new OrganizationProxy();
                vOrganizationProxyTarget.AssignFromSource(vOrganizationProxySource);
                _organizationList.Add(vOrganizationProxyTarget);
            }
        }

        #endregion
    }
}