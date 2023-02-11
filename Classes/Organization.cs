using Zephry;

namespace Grandmark
{
    /// <summary>
    /// Organization class.
    /// </summary>
    /// <remarks>
    ///   namespace Grandmark
    /// </remarks>
    public class Organization : Zephob
    {
        #region Fields
        private int _entKey;
        private int _orgKey;
        private string _orgName = string.Empty;
        private int? _entKeyParent;
        private int? _orgKeyParent;
        private string _orgNameParent = string.Empty;
        #endregion

        #region Properties
        public int EntKey { get => _entKey; set { _entKey = value; } }
        public int OrgKey { get => _orgKey; set { _orgKey = value; } }
        public string OrgName { get => _orgName; set => _orgName = value; }
        public int? EntKeyParent { get => _entKeyParent; set => _entKeyParent = value; }
        public int? OrgKeyParent { get => _orgKeyParent; set { _orgKeyParent = value; } }
        public string OrgNameParent { get => _orgNameParent; set => _orgNameParent = value; }
        #endregion

        #region AssignFromSource

        /// <summary>
        ///   Assigns all <c>aSource</c> object's values to this instance of <see cref="Organization"/>.
        /// </summary>
        /// <param name="aSource">A source objcct.</param>
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not Organization)
            {
                throw new ArgumentException("Invalid Source Argument to Organization Assign");
            }

            _entKey = ((Organization)aSource)._entKey;
            _orgKey = ((Organization)aSource)._orgKey;
            _orgName = ((Organization)aSource)._orgName;
            _entKeyParent = ((Organization)aSource)._entKeyParent;
            _orgKeyParent = ((Organization)aSource)._orgKeyParent;
            _orgNameParent = ((Organization)aSource)._orgNameParent;
        }

        #endregion
    }
 }
