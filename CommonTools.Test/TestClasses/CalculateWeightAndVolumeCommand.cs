#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTools.Data;

#endregion

namespace CommonTools.Test.TestClasses
{
    public sealed class CalculateWeightAndVolumeCommand : DataCommand
    {

        #region - Constants -

        private const string PARAM_CUSTOMER_CODE = "@CustomerCode";
        private const string PARAM_PLANT_ID = "@PlantID";
        private const string PARAM_COMMODITY_CODE = "@CommodityCode";
        private const string PARAM_QUANTITY = "@Quantity";
        private const string PARAM_DEBUG = "@Debug";

        #endregion

        #region - Constructor -

        public CalculateWeightAndVolumeCommand()
            : base("CHRWOnline_CalculateWeightAndVolume_2")
        {
            CustomerCode = null;
            PlantID = null;
            CommodityCode = null;
            Quantity = null;
            Debug = null;
        }

        #endregion

        #region - Properties -

        public string CustomerCode { get; set; }

        public string PlantID { get; set; }

        public string CommodityCode { get; set; }

        public int? Quantity { get; set; }

        public bool? Debug { get; set; }

        #endregion

        #region - Public Methods -

        public override IEnumerable<DatabaseParameter> GetParameters()
        {
            if (!this.Quantity.HasValue)
            {
                throw new InvalidOperationException("The quantity must be set.");
            }

            yield return new DatabaseParameter(PARAM_CUSTOMER_CODE, this.CustomerCode);
            yield return new DatabaseParameter(PARAM_PLANT_ID, this.PlantID);
            yield return new DatabaseParameter(PARAM_COMMODITY_CODE, this.CommodityCode);
            yield return new DatabaseParameter(PARAM_QUANTITY, this.Quantity.Value);

            if (this.Debug.HasValue)
            {
                yield return new DatabaseParameter(PARAM_DEBUG, this.Debug.Value);
            }
        }

        #endregion

    }
}
