using System;
using System.Collections.Generic;
using System.Linq;
using Zephry;

namespace Grandmark
{
    /// <summary>
    ///   StockCollection class.
    /// </summary>
    /// <remarks>
    ///   namespace Grandmark
    /// </remarks>
    public class StockCollection : Zephob
    {
        #region Fields

        private List<Stock> _StockList = new List<Stock>();

        #endregion

        #region  Properties

        /// <summary>
        /// Gets or sets the <see cref="Stock"/> list.
        /// </summary>
        /// <value>
        /// The Stock list.
        /// </value>
        public List<Stock> StockList
        {
            get { return _StockList; }
            set { _StockList = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        ///    Assigns all <c>aSource</c> object's values to this instance of <see cref="StockCollection"/>.
        /// </summary>
        /// <param name="aSource">A source objcct.</param>
        public override void AssignFromSource(object aSource)
        {
            if (!(aSource is StockCollection))
            {
                throw new ArgumentException("Invalid assignment source", "StockCollection");
            }

            _StockList.Clear();
            foreach (Stock vStockSource in ((StockCollection)aSource)._StockList)
            {
                Stock vStockTarget = new Stock();
                vStockTarget.AssignFromSource(vStockSource);
                _StockList.Add(vStockTarget);
            }
        }

        #endregion
    }
}
