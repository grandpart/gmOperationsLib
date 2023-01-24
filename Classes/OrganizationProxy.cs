using Newtonsoft.Json;

namespace Grandmark
{
    /// <summary>
    /// The hierarchical class that represents Organizations as parent/children 
    /// </summary>
    public class OrganizationProxy : OrganizationKey
    {
        #region Fields
        private int? _entKeyParent;
        private int? _orgKeyParent;
        private string _orgName = string.Empty;
        private List<OrganizationProxy> _organizationProxyList = new();
        #endregion

        #region Properties
        [JsonProperty("entparent")]
        public int? EntKeyParent { get => _entKeyParent; set => _entKeyParent = value; }
        [JsonProperty("orgparent")]
        public int? OrgKeyParent { get => _orgKeyParent; set => _orgKeyParent = value; }
        [JsonProperty("orgname")]
        public string OrgName { get => _orgName; set => _orgName = value; }
        [JsonProperty("list")]
        public List<OrganizationProxy> OrganizationProxyList { get => _organizationProxyList; set => _organizationProxyList = value; }
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
