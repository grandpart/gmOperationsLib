using Newtonsoft.Json;
using Zephry;

namespace Grandmark
{
    public class OrganizationProxyCollection : Zephob
    {
        #region Fields
        private bool _flat = false;
        private List<OrganizationProxy> _list = new();
        #endregion

        #region  Properties
        public bool Flat { get => _flat; set => _flat = value; }
        public List<OrganizationProxy> List { get => _list; set => _list = value; }
        #endregion

        #region Methods
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not OrganizationProxyCollection)
            {
                throw new ArgumentException("aOrganizationProxyCollection");
            }

            _flat = ((OrganizationProxyCollection) aSource)._flat;
            _list.Clear();
            foreach (var vOrganizationProxySource in ((OrganizationProxyCollection) aSource)._list)
            {
                var vOrganizationProxyTarget = new OrganizationProxy();
                vOrganizationProxyTarget.AssignFromSource(vOrganizationProxySource);
                _list.Add(vOrganizationProxyTarget);
            }
        }
        #endregion
    }
}