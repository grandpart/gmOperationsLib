using System;
using System.Text;
using System.Configuration;
using Zephry;

namespace Grandmark
{
    public class AnonymousBridge
    {
        #region Invoke

        /// <summary>
        /// Invokes the UserDelegate method with a connection token and business object
        /// </summary>
        /// <param name="aDelegate">A delegate.</param>
        /// <param name="aZephob"></param>
        public static void Invoke<T>(AnonymousDelegate<T> aDelegate, T aZephob, Connection aConnection)
        {
            try
            {
                // Invoke the method identified by aDelegate
                aDelegate(aConnection, aZephob);
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

        #region TransactionException
        // Build Exception Status exception
        private static TransactionStatusException TransactionException(Exception aException)
        {
            var vMessageStack = new StringBuilder();
            vMessageStack.AppendFormat("AnonymousBridge.Invoke {0}", aException.Message).AppendLine();
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
