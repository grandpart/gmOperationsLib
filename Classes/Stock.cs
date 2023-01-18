using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zephry;

namespace Grandmark
{
    public class Stock : StockKey
    {
        #region Fields

        private string _stkName = string.Empty;

        #endregion

        #region Properties

        public string StkName
        {
            get { return _stkName; }
            set { _stkName = value; }
        }

        #endregion

        #region AssignFromSource

        /// <summary>
        ///   Assigns all <c>aSource</c> object's values to this instance of <see cref="Stock"/>.
        /// </summary>
        /// <param name="aSource">A source objcct.</param>
        public override void AssignFromSource(object aSource)
        {
            if (!(aSource is Stock))
            {
                throw new ArgumentException("Invalid Source Argument to Stock Assign");
            }
            base.AssignFromSource(aSource);
        }

        #endregion
    }
}
