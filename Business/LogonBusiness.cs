using System;
using Zephry;
using static System.String;

namespace Grandmark
{
    public class LogonBusiness
    {

        #region Logon
        /// <summary>
        /// Logon LogonToken
        /// </summary>
        /// <param name="aConnection"></param>
        /// <param name="aLogonToken"></param>
        public static void Logon(Connection aConnection, LogonToken aLogonToken)
        {
            if (aLogonToken == null)
            {
                throw new ArgumentNullException(nameof(aLogonToken));
            }

            if (IsNullOrWhiteSpace(aLogonToken.UserId) || IsNullOrWhiteSpace(aLogonToken.Token))
            {
                throw new ArgumentNullException(nameof(aLogonToken), "UserID and Password must be supplied");
            }

            LogonData.Logon(aConnection, aLogonToken);
        }

        #endregion

        #region Authenticate

        public static void Authenticate(Connection aConnection, LogonToken aLogonToken, UserKey aUserKey)
        {
            if (aLogonToken == null)
            {
                throw new ArgumentNullException(nameof(aLogonToken));
            }

            if (IsNullOrWhiteSpace(aLogonToken.UserId) || IsNullOrWhiteSpace(aLogonToken.Token))
            {
                throw new ArgumentNullException(nameof(aLogonToken), "UserID and Authentication Token must be supplied");
            }

            if (aUserKey == null)
            {
                throw new ArgumentNullException(nameof(aUserKey), "Invalid attempt to logon or corrupt user credentials");
            }

            LogonData.Authenticate(aConnection, aLogonToken, aUserKey);
        }

        #endregion

    }
}
