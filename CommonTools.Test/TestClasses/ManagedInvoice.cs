#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace CommonTools.Test.TestClasses
{
    [Serializable]
    public class ManagedInvoice : IEquatable<ManagedInvoice>
    {

        #region - Constructor -

        public ManagedInvoice()
        {
            ID = null;
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

        public int? ID { get; set; }
        public Guid? FileGuid { get; set; }
        public string CarrierCode { get; set; }
        public string SCAC { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string OriginalFileName { get; set; }
        public DateTime? FileSubmittedDate { get; set; }

        #endregion

        #region - IEquatable<ManagedInvoice> Members -

        public bool Equals(ManagedInvoice other)
        {
            if (other == null)
            {
                return false;
            }

            if (ID.HasValue && other.ID.HasValue)
            {
                if (!ID.Value.Equals(other.ID.Value))
                {
                    return false;
                }
            }
            else if(ID.HasValue && !other.ID.HasValue)
            {
                return false;
            }
            else if (!ID.HasValue && other.ID.HasValue)
            {
                return false;
            }

            if (FileGuid.HasValue && other.FileGuid.HasValue)
            {
                if (!FileGuid.Value.Equals(other.FileGuid.Value))
                {
                    return false;
                }
            }
            else if(FileGuid.HasValue && !other.FileGuid.HasValue)
            {
                return false;
            }
            else if (!FileGuid.HasValue && other.FileGuid.HasValue)
            {
                return false;
            }

            if (CarrierCode != null && other.CarrierCode != null)
            {
                if (!CarrierCode.Equals(other.CarrierCode))
                {
                    return false;
                }
            }
            else if (CarrierCode != null && other.CarrierCode == null)
            {
                return false;
            }
            else if (CarrierCode == null && other.CarrierCode != null)
            {
                return false;
            }

            if (SCAC != null && other.SCAC != null)
            {
                if (!SCAC.Equals(other.SCAC))
                {
                    return false;
                }
            }
            else if (SCAC != null && other.SCAC == null)
            {
                return false;
            }
            else if (SCAC == null && other.SCAC != null)
            {
                return false;
            }

            if (UserEmail != null && other.UserEmail != null)
            {
                if (!UserEmail.Equals(other.UserEmail))
                {
                    return false;
                }
            }
            else if (UserEmail != null && other.UserEmail == null)
            {
                return false;
            }
            else if (UserEmail == null && other.UserEmail != null)
            {
                return false;
            }

            if (UserName != null && other.UserName != null)
            {
                if (!UserName.Equals(other.UserName))
                {
                    return false;
                }
            }
            else if (UserName != null && other.UserName == null)
            {
                return false;
            }
            else if (UserName == null && other.UserName != null)
            {
                return false;
            }

            if (OriginalFileName != null && other.OriginalFileName != null)
            {
                if (!OriginalFileName.Equals(other.OriginalFileName))
                {
                    return false;
                }
            }
            else if (OriginalFileName != null && other.OriginalFileName == null)
            {
                return false;
            }
            else if (OriginalFileName == null && other.OriginalFileName != null)
            {
                return false;
            }

            if (FileSubmittedDate.HasValue && other.FileSubmittedDate.HasValue)
            {
                long first = FileSubmittedDate.Value.ToUniversalTime().Ticks / 10000;
                long second = other.FileSubmittedDate.Value.ToUniversalTime().Ticks / 10000;

                if (!first.Equals(second))
                {
                    return false;
                }
            }
            else if (FileSubmittedDate.HasValue && !other.FileSubmittedDate.HasValue)
            {
                return false;
            }
            else if (!FileSubmittedDate.HasValue && other.FileSubmittedDate.HasValue)
            {
                return false;
            }

            return true;
        }

        #endregion

    }
}
