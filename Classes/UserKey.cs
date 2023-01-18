using System;
using System.Collections.Generic;
using System.Linq;
using Zephry;

namespace Grandmark
{
    /// <summary>
    ///   UserKey class.
    /// </summary>
    /// <remarks>
    ///   namespace Grandmark
    /// </remarks>
    public class UserKey : Zephob
    {
        #region Fields

        private int _usrKey;
        private bool _usrAdmin;

        #endregion

        #region Properties

        /// <summary>
        ///   Gets or sets the <see cref="Subscriber"/> key.
        /// </summary>
        /// <value>
        ///   The <see cref="Subscriber"/> key.
        /// </value
        public int UsrKey
        {
            get { return _usrKey; }
            set { _usrKey = value; }
        }
        public bool UsrAdmin
        {
            get { return _usrAdmin; }
            set { _usrAdmin = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="UserKey"/> class.
        /// </summary>
        public UserKey() { }

        /// <summary>
        ///   Initializes a new instance of the <see cref="UserKey"/> class. Set private fields to constructor arguments.
        /// </summary>
        /// <param name="aUsrKey">A User key.</param>
        /// <param name="aUsrAdmin"></param>
        public UserKey( int aClnKey, int aUsrKey, bool aUsrAdmin)
        {
            _usrKey = aUsrKey;
            _usrAdmin = aUsrAdmin;
        }

        #endregion

        #region Comparer

        /// <summary>
        ///   The Comparer class for UserKey.
        /// </summary>
        public class EqualityComparer : IEqualityComparer<UserKey>
        {
            /// <summary>
            ///   Tests equality of Key1 and Key2.
            /// </summary>
            /// <param name="aUserKey1">Key 1.</param>
            /// <param name="aUserKey2">Key 2.</param>
            /// <returns>True if the composite keys are equal, else false.</returns>
            public bool Equals(UserKey aUserKey1, UserKey aUserKey2)
            {
                return aUserKey1._usrKey == aUserKey2._usrKey;
            }

            /// <summary>
            ///   Returns a hash code for this instance.
            /// </summary>
            /// <param name="aUserKey">A UserKey.</param>
            /// <returns>
            ///   A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
            /// </returns>
            public int GetHashCode(UserKey aUserKey)
            {
                return Convert.ToInt32(aUserKey._usrKey);
            }
        }

        #endregion

        #region AssignFromSource

        /// <summary>
        ///   Assigns all <c>aSource</c> object's values to this instance of <see cref="User"/>.
        /// </summary>
        /// <param name="aSource">A source objcct.</param>
        public override void AssignFromSource(object aSource)
        {
            if (!(aSource is UserKey))
            {
                throw new ArgumentException("Invalid assignment source", "UserKey");
            }
            _usrKey = ((UserKey)aSource)._usrKey;
            _usrAdmin = ((UserKey)aSource)._usrAdmin;
        }

        #endregion
    }
 }
