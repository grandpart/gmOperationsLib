using Newtonsoft.Json;
using Zephry;

namespace Grandmark
{
    public class OrganizationProxyCollection : Zephob
    {
        #region Fields
        private bool _flat = false;
        private List<OrganizationProxy> _organizationList = new();
        #endregion

        #region  Properties
        [JsonProperty("flat")]
        public bool Flat { get => _flat; set => _flat = value; }
        [JsonProperty("list")]
        public List<OrganizationProxy> OrganizationProxyList { get => _organizationList; set => _organizationList = value; }
        #endregion

        #region Methods
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