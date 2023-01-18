using System;
using Zephry;

namespace Grandmark
{
    /// <summary>
    /// User class.
    /// </summary>
    /// <remarks>
    ///   namespace Grandmark
    /// </remarks>
    public class User : UserKey
    {
        #region Fields

        private int? _usrParentEnt;
        private int? _usrParentKey;
        private string _usrUserId = null!;
        private string _usrPassword = null!;
        private bool _usrAutoAuthorize;
        private bool _usrCanAuthorize;
        private int _usrWorkday;
        private string _usrName = null!;
        private string _usrSurname = null!;
        private string _usrParentFullName = null!;
        private string _usrEmail = null!;
        private string _usrIdentifier = string.Empty;
        private string _usrMobile = string.Empty;
        private string _usrPhone = string.Empty;
        private string _usrExtension = string.Empty;
        private string _usrFax = string.Empty;
        private string? _usrAvatar = string.Empty;
        //
        private UserStatus _usrStatus;
        private DateTime _usrStatusDate;
        private string _usrToken = string.Empty;

        #endregion
        
        #region Properties

        public int? ParentUsrKey
        {
            get { return _usrParentKey; }
            set { _usrParentKey = value; }
        }

        public string UsrUserId
        {
            get { return _usrUserId; }
            set { _usrUserId = value; }
        }

        public string UsrPassword
        {
            get { return _usrPassword; }
            set { _usrPassword = value; }
        }

        public bool UsrAutoAuthorize
        {
            get { return _usrAutoAuthorize; }
            set { _usrAutoAuthorize = value; }
        }

        public bool UsrCanAuthorize
        {
            get { return _usrCanAuthorize; }
            set { _usrCanAuthorize = value; }
        }

        public int UsrWorkday
        {
            get { return _usrWorkday; }
            set { _usrWorkday = value; }
        }

        public string UsrName
        {
            get { return _usrName; }
            set { _usrName = value; }
        }

        public string UsrSurname
        {
            get { return _usrSurname; }
            set { _usrSurname = value; }
        }

        public string UsrFullName => $"{_usrName} {_usrSurname}";

        public string UsrParentFullName
        {
            get { return _usrParentFullName; }
            set { _usrParentFullName = value; }
        }

        public string UsrEmail
        {
            get { return _usrEmail; }
            set { _usrEmail = value; }
        }

        public string UsrIdentifier
        {
            get { return _usrIdentifier; }
            set { _usrIdentifier = value; }
        }

        public string UsrMobile
        {
            get { return _usrMobile; }
            set { _usrMobile = value; }
        }

        public string UsrPhone
        {
            get { return _usrPhone; }
            set { _usrPhone = value; }
        }

        public string UsrExtension
        {
            get { return _usrExtension; }
            set { _usrExtension = value; }
        }

        public string UsrFax
        {
            get { return _usrFax; }
            set { _usrFax = value; }
        }

        public string? UsrAvatar
        {
            get { return _usrAvatar; }
            set { _usrAvatar = value; }
        }

        public UserStatus UsrStatus
        {
            get { return _usrStatus; }
            set { _usrStatus = value; }
        }

        public DateTime UsrStatusDate
        {
            get { return _usrStatusDate; }
            set { _usrStatusDate = value; }
        }

        public string UsrToken
        {
            get { return _usrToken; }
            set { _usrToken = value; }
        }

        #endregion

        #region AssignFromSource

        /// <summary>
        ///   Assigns all <c>aSource</c> object's values to this instance of <see cref="User"/>.
        /// </summary>
        /// <param name="aSource">A source objcct.</param>
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not User)
            {
                throw new ArgumentException("Invalid Source Argument to User Assign");
            }
            base.AssignFromSource(aSource);
            _usrParentEnt = ((User)aSource)._usrParentEnt;
            _usrParentKey = ((User)aSource)._usrParentKey;
            _usrUserId = ((User)aSource)._usrUserId;
            _usrPassword = ((User)aSource)._usrPassword;
            _usrAutoAuthorize = ((User)aSource)._usrAutoAuthorize;
            _usrCanAuthorize = ((User)aSource)._usrCanAuthorize;
            _usrWorkday = ((User)aSource)._usrWorkday;
            _usrName = ((User)aSource)._usrName;
            _usrSurname = ((User)aSource)._usrSurname;
            _usrEmail = ((User)aSource)._usrEmail;
            _usrParentFullName = ((User)aSource)._usrParentFullName;
            _usrIdentifier = ((User)aSource)._usrIdentifier;
            _usrMobile = ((User)aSource)._usrMobile;
            _usrPhone = ((User)aSource)._usrPhone;
            _usrExtension = ((User)aSource)._usrExtension;
            _usrFax = ((User)aSource)._usrFax;
            _usrAvatar = ((User)aSource)._usrAvatar;
            _usrStatus = ((User)aSource)._usrStatus;
            _usrStatusDate = ((User)aSource)._usrStatusDate;
            _usrToken = ((User)aSource)._usrToken;
        }

        #endregion
    }
 }
