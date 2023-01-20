using System;
using System.Collections.Generic;
using System.Linq;
using Zephry;

namespace Grandmark
{
    /// <summary>
    /// The hierarchical class that represents Organizations as parent/children 
    /// </summary>
    public class OrganizationProxy : OrganizationKey
    {
        #region Fields
        private int? _subKeyParent;
        private int? _orgKeyParent;
        private string _orgName = string.Empty;
        private List<OrganizationProxy> _organizationProxyList = new();
        #endregion

        #region Properties
        public int? SubKeyParent
        {
            get { return _subKeyParent; }
            set { _subKeyParent = value; }
        }

        public int? OrgKeyParent
        {
            get { return _orgKeyParent; }
            set { _orgKeyParent = value; }
        }

        public string OrgName
        {
            get { return _orgName; }
            set { _orgName = value; }
        }

        public List<OrganizationProxy> OrganizationProxyList
        {
            get { return _organizationProxyList; }
            set { _organizationProxyList = value; }
        }
        #endregion

        #region AssignFromSource

        /// <summary>
        ///   Assigns all <c>aSource</c> object's values to this instance of <see cref="OrganizationProxy"/>.
        /// </summary>
        /// <param name="aSource">A source objcct.</param>
        public override void AssignFromSource(object aSource)
        {
            if (!(aSource is OrganizationProxy))
            {
                throw new ArgumentException("aOrganizationProxy");
            }

            base.AssignFromSource(aSource);
            _subKeyParent = ((OrganizationProxy)aSource)._subKeyParent;
            _orgKeyParent = ((OrganizationProxy)aSource)._orgKeyParent;
            _orgName = ((OrganizationProxy)aSource)._orgName;
            ((OrganizationProxy)aSource)._organizationProxyList.Clear();
            ((OrganizationProxy)aSource)._organizationProxyList.ForEach(vSourceOrganizationProxy =>
            {
                var vTargetOrganizationProxy = new OrganizationProxy();
                vTargetOrganizationProxy.AssignFromSource(vSourceOrganizationProxy);
                _organizationProxyList.Add(vTargetOrganizationProxy);
            });
        }

        #endregion
    }
}
