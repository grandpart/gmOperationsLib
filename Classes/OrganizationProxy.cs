using Zephry;

namespace Grandmark
{
    /// <summary>
    /// The hierarchical class that represents Organizations as parent/children 
    /// </summary>
    public class OrganizationProxy : Zephob
    {
        #region Fields
        private int _entKey;
        private int _orgKey;
        private int? _entKeyParent;
        private int? _orgKeyParent;
        private string _orgName = string.Empty;
        private List<OrganizationProxy> _organizationProxyList = new();
        #endregion

        #region Properties
        public int EntKey { get => _entKey; set { _entKey = value; } }
        public int OrgKey { get => _orgKey; set { _orgKey = value; } }
        public int? EntKeyParent { get => _entKeyParent; set => _entKeyParent = value; }
        public int? OrgKeyParent { get => _orgKeyParent; set => _orgKeyParent = value; }
        public string OrgName { get => _orgName; set => _orgName = value; }
        public List<OrganizationProxy> List { get => _organizationProxyList; set => _organizationProxyList = value; }
        #endregion

        #region AssignFromSource

        /// <summary>
        ///   Assigns all <c>aSource</c> object's values to this instance of <see cref="OrganizationProxy"/>.
        /// </summary>
        /// <param name="aSource">A source objcct.</param>
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not OrganizationProxy)
            {
                throw new ArgumentException("aOrganizationProxy");
            }

            _entKey = ((OrganizationProxy)aSource)._entKey;
            _orgKey = ((OrganizationProxy)aSource)._orgKey;
            _entKeyParent = ((OrganizationProxy)aSource)._entKeyParent;
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
