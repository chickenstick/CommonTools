#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace CommonTools.Data
{
    public static class CommandTimeoutLengths
    {

        #region - Properties -

        public static int DefaultTimeout
        {
            get
            {
                return 30;
            }
        }

        public static int ShortTimeout
        {
            get
            {
                return 15;
            }
        }

        public static int LongTimeout
        {
            get
            {
                return 60;
            }
        }

        public static int IndefiniteTimeout
        {
            get
            {
                return 0;
            }
        }

        #endregion

    }
}
