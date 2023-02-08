using Newtonsoft.Json;
using Zephry;

namespace Grandmark
{
    /// <summary>
    /// Keeps departments within an entity.
    /// Finance, Warehouse, Stock etc..
    /// </summary>
    public class Department : Zephob
    {
        /// <summary>
        /// Private fields for Department
        /// </summary>
        #region Fields
        private int _entkey;
        private int _depkey;
        private string _depname;
        #endregion

        #region Properties
        /// <summary>
        /// Public properties for Department
        /// </summary>

        [JsonProperty("entkey")]
        public int EntKey { get => _entkey; set { _entkey = value; } }

        [JsonProperty("depkey")]
        public int DepKey { get => _depkey; set { _depkey = value; } }

        [JsonProperty("depname")]
        public string DepName { get { return _depname; } set { _depname = value; } }

        #endregion

        #region Constructor
        public Department()
        {
        }
        public Department(int aEntKey, int aDepKey)
        {
            _entkey = aEntKey;
            _depkey = aDepKey;
        }
        #endregion

        #region Comparer
        public class EqualityComparer : IEqualityComparer<Department>
        {
            public bool Equals(Department aDepartment1, Department aDepartment2)
            {
                return aDepartment1._entkey == aDepartment2._entkey &&
                    aDepartment1._depkey == aDepartment2._depkey;
            }

            public int GetHashCode(Department aDepartment)
            {
                return Convert.ToInt32(aDepartment._entkey) ^ Convert.ToInt32(aDepartment._depkey);
            }
        }
        #endregion

        #region AssignFromSource
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not Department)
            {
                throw new ArgumentException("Invalid Source Argument to Department Assign");
            }
            _entkey = ((Department)aSource)._entkey;
            _depkey = ((Department)aSource)._depkey;
            _depname = ((Department)aSource)._depname;
        }
        #endregion
    }
}
