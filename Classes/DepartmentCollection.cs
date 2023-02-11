using Zephry;

namespace Grandmark
{
    public class DepartmentCollection : Zephob
    {
        #region Fields
        private List<Department> _list = new();
        #endregion

        #region  Properties
        public List<Department> List { get => _list; set => _list = value; }
        #endregion

        #region Methods
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not DepartmentCollection)
            {
                throw new ArgumentException("aDepartmentCollection");
            }

            _list.Clear();
            foreach (var vDepartmentSource in ((DepartmentCollection)aSource)._list)
            {
                var vDepartmentTarget = new Department();
                vDepartmentTarget.AssignFromSource(vDepartmentSource);
                _list.Add(vDepartmentTarget);
            }
        }
        #endregion
    }
}
