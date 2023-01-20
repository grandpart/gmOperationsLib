using System.Text.Json.Serialization;
using Zephry;

namespace Grandmark
{
    public class OrganizationKey : Zephob
    {
        #region Fields
        private int _entKey;
        private int _orgKey;
        #endregion

        #region Properties
        [JsonPropertyName("entkey")]
        public int EntKey { get => _entKey; set { _entKey = value; } }
        [JsonPropertyName("orgkey")]
        public int OrgKey { get => _orgKey; set { _orgKey = value; }}
        #endregion

        #region Constructors
        public OrganizationKey() { }
        public OrganizationKey( int aEntKey,  int aOrgKey)
        {
            _entKey = aEntKey;
            _orgKey = aOrgKey;
        }
        #endregion

        #region Comparer

        /// <summary>
        ///   The Comparer class for OrganizationKey.
        /// </summary>
        public class EqualityComparer : IEqualityComparer<OrganizationKey>
        {
            public bool Equals(OrganizationKey aOrganizationKey1, OrganizationKey aOrganizationKey2)
            {
                return aOrganizationKey1._entKey == aOrganizationKey2._entKey &&
                       aOrganizationKey1._orgKey == aOrganizationKey2._orgKey;
            }

            public int GetHashCode(OrganizationKey aOrganizationKey)
            {
                return Convert.ToInt32(aOrganizationKey._entKey) ^
                       Convert.ToInt32(aOrganizationKey._orgKey);
            }
        }

        #endregion

        #region AssignFromSource
        public override void AssignFromSource(object aSource)
        {
            if (!(aSource is OrganizationKey))
            {
                throw new ArgumentException("Invalid assignment source", "OrganizationKey");
            }
            _entKey = ((OrganizationKey)aSource)._entKey;
            _orgKey = ((OrganizationKey)aSource)._orgKey;
        }
        #endregion
    }
 }
