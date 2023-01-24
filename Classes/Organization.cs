using Newtonsoft.Json;

namespace Grandmark
{
    /// <summary>
    /// Organization class.
    /// </summary>
    /// <remarks>
    ///   namespace Grandmark
    /// </remarks>
    public class Organization : OrganizationKey
    {
        #region Fields

        private string _orgName = string.Empty;
        private int? _entKeyParent;
        private int? _orgKeyParent;
        private string _orgNameParent = string.Empty;

        #endregion
        #region Properties
        [JsonProperty("orgname")]
        public string OrgName { get => _orgName; set => _orgName = value; }
        [JsonProperty("entkeyparent")]
        public int? EntKeyParent { get => _entKeyParent; set => _entKeyParent = value; }
        [JsonProperty("orgkeyparent")]
        public int? OrgKeyParent { get => _orgKeyParent; set { _orgKeyParent = value; } }
        [JsonProperty("orgnameparent")]
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
            base.AssignFromSource(aSource);
            _orgName = ((Organization)aSource)._orgName;
            _entKeyParent = ((Organization)aSource)._entKeyParent;
            _orgKeyParent = ((Organization)aSource)._orgKeyParent;
            _orgNameParent = ((Organization)aSource)._orgNameParent;
        }

        #endregion
    }
 }
