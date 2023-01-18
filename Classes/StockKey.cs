using System;
using System.Collections.Generic;
using System.Linq;
using Zephry;

namespace Grandmark
{
    /// <summary>
    ///   StockKey class.
    /// </summary>
    /// <remarks>
    ///   namespace Grandmark
    /// </remarks>
    public class StockKey : Zephob
    {
        #region Fields

        private int _stkKey;

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
            get { return _stkKey; }
            set { _stkKey = value; }
        }
        #endregion

        #region Constructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="StockKey"/> class.
        /// </summary>
        public StockKey() { }

        /// <summary>
        ///   Initializes a new instance of the <see cref="StockKey"/> class. Set private fields to constructor arguments.
        /// </summary>
        /// <param name="aUsrKey">A Stock key.</param>
        /// <param name="aUsrAdmin"></param>
        public StockKey(int aUsrKey)
        {
            _stkKey = aUsrKey;
        }

        #endregion

        #region Comparer

        /// <summary>
        ///   The Comparer class for StockKey.
        /// </summary>
        public class EqualityComparer : IEqualityComparer<StockKey>
        {
            public bool Equals(StockKey x, StockKey y) => x._stkKey == y._stkKey;

            public int GetHashCode(StockKey aStockKey)
            {
                return Convert.ToInt32(aStockKey._stkKey);
            }
        }

        #endregion

        #region AssignFromSource
        public override void AssignFromSource(object aSource)
        {
            if (aSource is not StockKey)
            {
                throw new ArgumentException("Invalid assignment source", "StockKey");
            }
            _stkKey = ((StockKey)aSource)._stkKey;
        }
        #endregion
    }
}