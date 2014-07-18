#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTools.Data;

#endregion

namespace CommonTools.Test.TestClasses
{
    public sealed class DeleteManagedInvoiceUploadFile : DataCommand
    {

        #region - Constructor -

        public DeleteManagedInvoiceUploadFile()
            : base("USP_DeleteManagedInvoiceUploadFile", System.Data.CommandType.StoredProcedure)
        {
            ID = null;
        }

        #endregion

        #region - Properties -

        public int? ID { get; set; }

        #endregion

        #region - DataCommand Members -

        public override IEnumerable<DatabaseParameter> GetParameters()
        {
            if (!ID.HasValue)
            {
                throw new InvalidOperationException("ID is required.");
            }

            yield return new DatabaseParameter("@ID", ID.Value);
        }

        #endregion

    }
}
