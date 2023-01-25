using Newtonsoft.Json;
using Zephry;

namespace Grandmark
{
    public class BranchCollection :Zephob
    {
        #region Fields
        private List<Branch> _branchList = new();
        #endregion

        #region Properties
        [JsonProperty("list")]
        public List<Branch> BranchList { get { return _branchList; } set { _branchList = value; } }
        #endregion

        #region Methods
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not BranchCollection)
            {
                throw new ArgumentException("aBranchCollection");
            }

            _branchList.Clear();
            foreach (var vBranchSource in ((BranchCollection)aSource)._branchList)
            {
                var vBranchTarget = new Branch();
                vBranchTarget.AssignFromSource(vBranchSource);
                _branchList.Add(vBranchTarget);
            }
        }
        #endregion
    }
}
