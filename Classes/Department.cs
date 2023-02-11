using Zephry;

namespace Grandmark
{
    public class Department : Zephob
    {
        #region Fields
        private int _entKey;
        private int _dptKey;
        private string _dptName = string.Empty;
        #endregion

        #region Properties
        public int EntKey { get => _entKey; set { _entKey = value; } }
        public int DptKey { get => _dptKey; set { _dptKey = value; } }
        public string DptName { get { return _dptName; } set { _dptName = value; } }
        #endregion

        #region AssignFromSource
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not Department)
            {
                throw new ArgumentException("Invalid Source Argument to Department Assign");
            }
            _entKey = ((Department)aSource)._entKey;
            _dptKey = ((Department)aSource)._dptKey;
            _dptName = ((Department)aSource)._dptName;
        }
        #endregion
    }
}
