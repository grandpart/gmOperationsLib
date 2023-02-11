using Zephry;

namespace Grandmark
{
    public class UserProxyCollection : Zephob
    {
        #region Fields
        private bool _flat = false;
        private RippleSelect _rippleSelect = RippleSelect.All;
        private List<UserProxy> _list = new();
        #endregion

        #region  Properties
        public bool Flat { get => _flat; set => _flat = value; }
        public RippleSelect RippleSelect { get => _rippleSelect; set => _rippleSelect = value; }
        public List<UserProxy> List { get => _list; set => _list = value; }
        #endregion

        #region Methods
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not UserProxyCollection)
            {
                throw new ArgumentException("aUserProxyCollection");
            }

            _flat = ((UserProxyCollection)aSource)._flat;
            _rippleSelect = ((UserProxyCollection)aSource)._rippleSelect;
            _list.Clear();
            foreach (var vUserProxySource in ((UserProxyCollection)aSource)._list)
            {
                var vUserProxyTarget = new UserProxy();
                vUserProxyTarget.AssignFromSource(vUserProxySource);
                _list.Add(vUserProxyTarget);
            }
        }
        #endregion
    }
}