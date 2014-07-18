#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace CommonTools.Enumerations
{
    /// <summary>
    /// Attribute that ascribes a synonym to an enum value.
    /// </summary>
    public sealed class EnumDescriptionAttribute : Attribute
    {

        #region - Constructor -

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumDescriptionAttribute"/> class.
        /// </summary>
        /// <param name="synonym">The synonym.</param>
        public EnumDescriptionAttribute(string synonym)
            : base()
        {
            this.Synonym = synonym;
        }

        #endregion

        #region - Properties -

        /// <summary>
        /// Gets or sets the synonym.
        /// </summary>
        /// <value>
        /// The synonym.
        /// </value>
        public string Synonym { get; set; }

        #endregion

    }
}
