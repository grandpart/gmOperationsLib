using Newtonsoft.Json;

namespace Grandmark
{
    /// <summary>
    /// The hierarchical class that represents Users as parent/children 
    /// </summary>
    public class UserProxy : UserKey
    {
        #region Fields
        private int? _entKeyParent;
        private int? _usrKeyParent;
        private string _usrName = string.Empty;
        private string _usrSurname = string.Empty;
        private string _usrEmail = string.Empty;
        private List<UserProxy> _list = new();
        #endregion

        #region Properties
        public int? EntKeyParent { get => _entKeyParent; set => _entKeyParent = value; }
        public int? UsrKeyParent { get => _usrKeyParent; set => _usrKeyParent = value; }
        public string UsrName { get => _usrName; set => _usrName = value; }
        public string UsrSurname { get => _usrSurname; set => _usrSurname = value; }
        public string UsrFullname { get => $"{_usrName} {_usrSurname}"; }
        public string UsrEmail { get => _usrEmail; set => _usrEmail = value; }
        public List<UserProxy> List { get => _list; set => _list = value; }
        #endregion

        #region AssignFromSource

        /// <summary>
        ///   Assigns all <c>aSource</c> object's values to this instance of <see cref="UserProxy"/>.
        /// </summary>
        /// <param name="aSource">A source objcct.</param>
        public override void AssignFromSource(object aSource)
        {
            if (!(aSource is UserProxy))
            {
                throw new ArgumentException("aUserProxy");
            }

            base.AssignFromSource(aSource);
            _entKeyParent = ((UserProxy)aSource)._entKeyParent;
            _usrKeyParent = ((UserProxy)aSource)._usrKeyParent;
            _usrName = ((UserProxy)aSource)._usrName;
            _usrSurname = ((UserProxy)aSource)._usrSurname;
            _usrEmail = ((UserProxy)aSource)._usrEmail;
            ((UserProxy)aSource)._list.Clear();
            ((UserProxy)aSource)._list.ForEach(vSourceUserProxy =>
            {
                var vTargetUserProxy = new UserProxy();
                vTargetUserProxy.AssignFromSource(vSourceUserProxy);
                _list.Add(vTargetUserProxy);
            });
        }

        #endregion
    }
}
