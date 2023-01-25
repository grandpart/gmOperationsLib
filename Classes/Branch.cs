using Newtonsoft.Json;

namespace Grandmark
{
    public class Branch :BranchKey
    {
        #region Fields
        private string _brhname;
        private string _brhcode;
        #endregion

        #region Properties

        [JsonProperty("brhname")]
        public string BrhName { get { return _brhname; } set { _brhname = value; } }

        [JsonProperty("brhcode")]
        public string BrhCode { get { return _brhcode; } set { _brhcode = value; } }


        #endregion

        #region AssignFromSource
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not Branch)
            {
                throw new ArgumentException("Invalid Source Argument to Branch Assign");
            }
            base.AssignFromSource(aSource);
            _brhname = ((Branch)aSource)._brhname;
            _brhcode = ((Branch)aSource)._brhcode;
        }
        #endregion
    }
}
