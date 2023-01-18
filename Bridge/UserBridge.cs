using System;
using System.Configuration;
using System.Text;
using Zephry;

namespace Grandmark
{
    public class UserBridge
    {
        #region Invoke

        /// <summary>
        /// Invokes the UserDelegate method for an active subscriber with a connection token and business object
        /// </summary>
        /// <param name="aLogonToken"></param>
        /// <param name="aDelegate">A delegate.</param>
        /// <param name="aZephob"></param>
        public static void Invoke<T>(UserDelegate<T> aDelegate, T aZephob, LogonToken aLogonToken, Connection aConnection)
        {
            try
            {
                aDelegate(aConnection, InitializeSession(aConnection, aLogonToken), aZephob);
            }
            catch (TransactionStatusException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw TransactionException(ex);
            }
        }

        #endregion

        #region InitializeSession

        /// <summary>
        /// Initializes the session with the Session token file located 
        /// </summary>
        /// <returns></returns>
        private static UserKey InitializeSession(Connection aConnection, LogonToken aLogonToken)
        {
            var vUserKey = new UserKey();
            LogonBusiness.Authenticate(aConnection, aLogonToken, vUserKey);
            return vUserKey;
        }

        #endregion

        #region TransactionException
        // Build TransactionStatusException with an Exception
        private static TransactionStatusException TransactionException(Exception aException)
        {
            var vMessageStack = new StringBuilder();
            vMessageStack.AppendFormat("{0}", aException.Message).AppendLine();
            var vException = aException.InnerException;
            while (vException != null)
            {
                vMessageStack.AppendFormat("{0}", vException.Message).AppendLine();
                vException = vException.InnerException;
            }

            return new TransactionStatusException(TransactionResult.General, vMessageStack.ToString());
        }
        #endregion

    }
}
