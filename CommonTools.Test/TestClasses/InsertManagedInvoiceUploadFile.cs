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
    public sealed class InsertManagedInvoiceUploadFile : DataCommand
    {

        #region - Constructor -

        public InsertManagedInvoiceUploadFile()
            : base("USP_InsertManagedInvoiceUploadFile", System.Data.CommandType.StoredProcedure)
        {
            FileGuid = null;
            CarrierCode = null;
            SCAC = null;
            UserEmail = null;
            UserName = null;
            OriginalFileName = null;
            FileSubmittedDate = null;
        }

        #endregion

        #region - Properties -

        public Guid? FileGuid { get; set; }
        public string CarrierCode { get; set; }
        public string SCAC { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string OriginalFileName { get; set; }
        public DateTime? FileSubmittedDate { get; set; }

        #endregion

        #region - DataCommand Members -

        public override IEnumerable<DatabaseParameter> GetParameters()
        {
            if (!FileGuid.HasValue)
            {
                throw new InvalidOperationException("File Guid is required.");
            }

            if (string.IsNullOrWhiteSpace(CarrierCode))
            {
                throw new InvalidOperationException("Carrier Code is required.");
            }

            if (string.IsNullOrWhiteSpace(SCAC))
            {
                throw new InvalidOperationException("SCAC is required.");
            }

            if (string.IsNullOrWhiteSpace(UserEmail))
            {
                throw new InvalidOperationException("User Email is required.");
            }

            if (string.IsNullOrWhiteSpace(UserName))
            {
                throw new InvalidOperationException("User Name is required.");
            }

            if (string.IsNullOrWhiteSpace(OriginalFileName))
            {
                throw new InvalidOperationException("Original File Name is required.");
            }

            if (!FileSubmittedDate.HasValue)
            {
                throw new InvalidOperationException("File Submitted Date is required.");
            }

            yield return new DatabaseParameter("@ID", null, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
            yield return new DatabaseParameter("@FileGuid", FileGuid.ToString());
            yield return new DatabaseParameter("@CarrierCode", CarrierCode.Trim());
            yield return new DatabaseParameter("@SCAC", SCAC.Trim());
            yield return new DatabaseParameter("@UserEmail", UserEmail.Trim());
            yield return new DatabaseParameter("@UserName", UserName.Trim());
            yield return new DatabaseParameter("@OriginalFileName", OriginalFileName.Trim());
            yield return new DatabaseParameter("@FileSubmittedDate", FileSubmittedDate);
        }

        #endregion

    }
}
