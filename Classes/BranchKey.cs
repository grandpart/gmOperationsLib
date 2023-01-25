using Newtonsoft.Json;
using Zephry;

namespace Grandmark
{
    public class BranchKey : Zephob
    {
        #region Fields
        private int _entkey;
        private int _brhkey;
        #endregion

        #region Properties
        /// <summary>
        /// The [JsonProperty fields will also be the fields listed on your body when submitting the request]
        /// </summary>
        /// 
        [JsonProperty("entkey")]
        public int EntKey { get => _entkey; set { _entkey = value; } }

        [JsonProperty("brhkey")]
        public int BrhKey { get => _brhkey; set { _brhkey = value; } }
        #endregion

        #region Constructors
        public BranchKey() { }

        public BranchKey(int aEntKey, int aBrhKey)
        {
            _entkey = aEntKey;
            _brhkey = aBrhKey;
        }
        #endregion

        #region Comparer
        public class EqualityComparer : IEqualityComparer<BranchKey>
        {
            public bool Equals(BranchKey aBranchKey1, BranchKey aBranchKey2)
            {
                return aBranchKey1._entkey == aBranchKey2._entkey &&
                    aBranchKey1._brhkey == aBranchKey2._brhkey;
            }

            public int GetHashCode(BranchKey aBranchKey)
            {
                return Convert.ToInt32(aBranchKey._entkey) ^ Convert.ToInt32(aBranchKey._brhkey);
            }
        }
        #endregion

        #region AssignmentFromSource
        public override void AssignFromSource(object aSource)
        {
            if (!(aSource is BranchKey))
            {
                throw new ArgumentException("Invalid assignment source", "BranchKey");
            }
            _entkey = ((BranchKey)aSource)._entkey;
            _brhkey = ((BranchKey)aSource)._brhkey;
        }
        #endregion
    }
}
