using System;
using System.Collections.Generic;
using System.Linq;
using Zephry;

namespace Grandmark
{
    /// <summary>
    ///   UserCollection class.
    /// </summary>
    /// <remarks>
    ///   namespace Grandmark
    /// </remarks>
    public class UserCollection : Zephob
    {
        #region Fields

        private int _subKey;
        private string _subName = string.Empty;
        private int _qualificationKeyFilter;
        private List<User> _userList = new List<User>();

        #endregion

        #region  Properties

        /// <summary>
        ///   Gets or sets a value indicating whether this <see cref="UserCollection"/> is filtered.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this <see cref="UserCollection"/> is filtered; otherwise, <c>false</c>.
        /// </value>
        public int SubKey
        {
            get { return _subKey; }
            set { _subKey = value; }
        }
        /// <summary>
        ///   Gets or sets the <c>ClnName</c>.
        /// </summary>
        /// <value>
        ///   The <c>ClnName</c>.
        /// </value>
        public string SubName
        {
            get { return _subName; }
            set { _subName = value; }
        }
        /// <summary>
        ///   Gets or sets the <c>QualificationKeyFilter</c>.
        /// </summary>
        /// <value>
        ///   The <c>QualificationKeyFilter</c>.
        /// </value>
        public int QualificationKeyFilter
        {
            get { return _qualificationKeyFilter; }
            set { _qualificationKeyFilter = value; }
        }
        /// <summary>
        /// Gets or sets the <see cref="User"/> list.
        /// </summary>
        /// <value>
        /// The User list.
        /// </value>
        public List<User> UserList
        {
            get { return _userList; }
            set { _userList = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        ///    Assigns all <c>aSource</c> object's values to this instance of <see cref="UserCollection"/>.
        /// </summary>
        /// <param name="aSource">A source objcct.</param>
        public override void AssignFromSource(object aSource)
        {
            if (!(aSource is UserCollection))
            {
                throw new ArgumentException("Invalid assignment source", "UserCollection");
            }

            _subKey = ((UserCollection)aSource)._subKey;
            _subName = ((UserCollection)aSource)._subName;
            _qualificationKeyFilter = ((UserCollection)aSource)._qualificationKeyFilter;
            _userList.Clear();
            foreach (User vUserSource in ((UserCollection)aSource)._userList)
            {
                User vUserTarget = new();
                vUserTarget.AssignFromSource(vUserSource);
                _userList.Add(vUserTarget);
            }
        }

        #endregion
    }
}
