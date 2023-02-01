using Newtonsoft.Json;
using Zephry;

namespace Grandmark
{
    /// <summary>
    /// Collection for Deparment
    /// </summary>
    public class DepartmentCollection : Zephob
    {
        /// <summary>
        /// Private fields for DepartmentCollection
        /// </summary>
        #region Fields
        private List<Department> _departmentList = new();
        #endregion

        /// <summary>
        /// Public properties for DepartmentCollection
        /// </summary>
        #region  Properties
        [JsonProperty("list")]
        public List<Department> DepartmentList { get => _departmentList; set => _departmentList = value; }
        #endregion

        #region Methods
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not DepartmentCollection)
            {
                throw new ArgumentException("aDepartmentCollection");
            }

            _departmentList.Clear();
            foreach (var vDepartmentSource in ((DepartmentCollection)aSource)._departmentList)
            {
                var vDepartmentTarget = new Department();
                vDepartmentTarget.AssignFromSource(vDepartmentSource);
                _departmentList.Add(vDepartmentTarget);
            }
        }
        #endregion
    }
}
