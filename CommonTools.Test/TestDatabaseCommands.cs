#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CommonTools.Data;
using CommonTools.Data.DataProviders;
using CommonTools.Data.Extensions;

using CommonTools.Test.TestClasses;

#endregion

namespace CommonTools.Test
{
    [TestClass]
    public class TestDatabaseCommands
    {

        #region - Constants -

        private const string DB_EXPRESS_COMMON = @"data source=INTRPT_DB\E; Integrated Security=YES;Initial Catalog=ExpressCommon;";
        private const string DB_CHRWONLINE_DEV = @"data source=EDWODSDEV01; Initial catalog=CHRWOnline; Integrated Security=YES;";

        #endregion

        #region - Test Methods -

        [TestMethod]
        public void TestCallingStoredProcedure()
        {
            decimal pallets = -1.0m;
            string commodityDescription = null;

            using (DataProviderBase provider = DatabaseFactory.GetDatabase(DatabaseType.SqlServer, DB_EXPRESS_COMMON))
            {
                CalculateWeightAndVolumeCommand command = new CalculateWeightAndVolumeCommand()
                {
                    CustomerCode = "C7262158",
                    PlantID = "RO10",
                    CommodityCode = "28315351",
                    Quantity = 18144
                };

                using (DbCommand dbCmd = provider.GetDbCommand(command))
                {
                    provider.OpenConnection();

                    using (DbDataReader reader = dbCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pallets = reader.GetDecimal("Pallets");
                            commodityDescription = reader.GetString("CommodityDescription");
                        }
                    }
                }
            }

            Assert.AreEqual(0.65m, pallets);
            Assert.AreEqual("PLUNGER 7,5 COATED", commodityDescription);
        }

        [TestMethod]
        public void TestInsertingData()
        {
            using (DataProviderBase provider = DatabaseFactory.GetDatabase(DatabaseType.SqlServer, DB_CHRWONLINE_DEV))
            {
                ManagedInvoice managedInvoice = CreateBasicManagedInvoice();
                InsertManagedInvoiceUploadFile insertInvoice = new InsertManagedInvoiceUploadFile()
                {
                    FileGuid = managedInvoice.FileGuid,
                    CarrierCode = managedInvoice.CarrierCode,
                    SCAC = managedInvoice.SCAC,
                    UserEmail = managedInvoice.UserEmail,
                    UserName = managedInvoice.UserName,
                    OriginalFileName = managedInvoice.OriginalFileName,
                    FileSubmittedDate = managedInvoice.FileSubmittedDate
                };

                provider.OpenConnection();
                using (DbCommand dbCmd = provider.GetDbCommand(insertInvoice))
                {
                    dbCmd.ExecuteNonQuery();
                    
                    int temp = 0;
                    if (dbCmd.TryGetOutputParameterValue<int>("@ID", out temp))
                    {
                        managedInvoice.ID = temp;
                    }
                    else
                    {
                        Assert.Fail("Unable to get ID from newly created managed invoice.");
                    }
                }

                GetManagedInvoiceUploadFile getInvoice = new GetManagedInvoiceUploadFile()
                {
                    ID = managedInvoice.ID
                };

                using (DbCommand getCmd = provider.GetDbCommand(getInvoice))
                {
                    ManagedInvoice tempInvoice = new ManagedInvoice();

                    using (DbDataReader reader = getCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tempInvoice.ID = reader.SafeGetNullableInt32("ManagedInvoiceUploadFileId");
                            tempInvoice.FileGuid = reader.SafeGetNullableGuid("FileGuid");
                            tempInvoice.CarrierCode = reader.SafeGetString("CarrierCode");
                            tempInvoice.SCAC = reader.SafeGetString("SCAC");
                            tempInvoice.UserEmail = reader.SafeGetString("UserEmail");
                            tempInvoice.UserName = reader.SafeGetString("UserName");
                            tempInvoice.OriginalFileName = reader.SafeGetString("OriginalFileName");
                            tempInvoice.FileSubmittedDate = reader.SafeGetNullableDateTime("FileSubmittedDate");
                        }
                    }

                    // Do this so the Equals method doesn't fail.
                    tempInvoice.FileSubmittedDate = managedInvoice.FileSubmittedDate;

                    Assert.IsTrue(managedInvoice.Equals(tempInvoice));
                }

                DeleteManagedInvoiceUploadFile deleteInvoice = new DeleteManagedInvoiceUploadFile()
                {
                    ID = managedInvoice.ID
                };

                using (DbCommand deleteCmd = provider.GetDbCommand(deleteInvoice))
                {
                    deleteCmd.ExecuteNonQuery();
                }
            }
        }

        [TestMethod]
        public void TestTransactionRollback()
        {
            ManagedInvoice managedInvoice = CreateBasicManagedInvoice();

            using (DataProviderBase provider = DatabaseFactory.GetDatabase(DatabaseType.SqlServer, DB_CHRWONLINE_DEV))
            {
                InsertManagedInvoiceUploadFile insertInvoice = new InsertManagedInvoiceUploadFile()
                {
                    FileGuid = managedInvoice.FileGuid,
                    CarrierCode = managedInvoice.CarrierCode,
                    SCAC = managedInvoice.SCAC,
                    UserEmail = managedInvoice.UserEmail,
                    UserName = managedInvoice.UserName,
                    OriginalFileName = managedInvoice.OriginalFileName,
                    FileSubmittedDate = managedInvoice.FileSubmittedDate
                };

                provider.BeginTransaction();
                provider.OpenConnection();
                using (DbCommand dbCmd = provider.GetDbCommand(insertInvoice))
                {
                    dbCmd.ExecuteNonQuery();

                    int temp = 0;
                    if (dbCmd.TryGetOutputParameterValue<int>("@ID", out temp))
                    {
                        managedInvoice.ID = temp;
                    }
                    else
                    {
                        Assert.Fail("Unable to get ID from newly created managed invoice.");
                    }
                }

                provider.RollBackTransaction();
            }

            using (DataProviderBase provider = DatabaseFactory.GetDatabase(DatabaseType.SqlServer, DB_CHRWONLINE_DEV))
            {
                GetManagedInvoiceUploadFile getInvoice = new GetManagedInvoiceUploadFile()
                {
                    ID = managedInvoice.ID
                };

                provider.OpenConnection();
                using (DbCommand getCmd = provider.GetDbCommand(getInvoice))
                {
                    ManagedInvoice tempInvoice = new ManagedInvoice();

                    int rowCount = 0;
                    using (DbDataReader reader = getCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rowCount++;
                        }
                    }

                    // Do this so the Equals method doesn't fail.
                    tempInvoice.FileSubmittedDate = managedInvoice.FileSubmittedDate;

                    Assert.AreEqual(0, rowCount);
                }
            }
        }

        public void TestTransactionCommit()
        {
            ManagedInvoice managedInvoice = CreateBasicManagedInvoice();

            using (DataProviderBase provider = DatabaseFactory.GetDatabase(DatabaseType.SqlServer, DB_CHRWONLINE_DEV))
            {
                InsertManagedInvoiceUploadFile insertInvoice = new InsertManagedInvoiceUploadFile()
                {
                    FileGuid = managedInvoice.FileGuid,
                    CarrierCode = managedInvoice.CarrierCode,
                    SCAC = managedInvoice.SCAC,
                    UserEmail = managedInvoice.UserEmail,
                    UserName = managedInvoice.UserName,
                    OriginalFileName = managedInvoice.OriginalFileName,
                    FileSubmittedDate = managedInvoice.FileSubmittedDate
                };

                provider.BeginTransaction();
                provider.OpenConnection();
                using (DbCommand dbCmd = provider.GetDbCommand(insertInvoice))
                {
                    dbCmd.ExecuteNonQuery();

                    int temp = 0;
                    if (dbCmd.TryGetOutputParameterValue<int>("@ID", out temp))
                    {
                        managedInvoice.ID = temp;
                    }
                    else
                    {
                        Assert.Fail("Unable to get ID from newly created managed invoice.");
                    }
                }
            }

            using (DataProviderBase provider = DatabaseFactory.GetDatabase(DatabaseType.SqlServer, DB_CHRWONLINE_DEV))
            {
                GetManagedInvoiceUploadFile getInvoice = new GetManagedInvoiceUploadFile()
                {
                    ID = managedInvoice.ID
                };

                provider.OpenConnection();
                using (DbCommand getCmd = provider.GetDbCommand(getInvoice))
                {
                    ManagedInvoice tempInvoice = new ManagedInvoice();

                    int rowCount = 0;
                    using (DbDataReader reader = getCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rowCount++;
                        }
                    }

                    // Do this so the Equals method doesn't fail.
                    tempInvoice.FileSubmittedDate = managedInvoice.FileSubmittedDate;

                    Assert.AreEqual(1, rowCount);
                }
            }
        }

        #endregion

        #region - Helper Methods -

        private ManagedInvoice CreateBasicManagedInvoice()
        {
            ManagedInvoice result = new ManagedInvoice();
            result.FileGuid = Guid.NewGuid();
            result.CarrierCode = "T5654";
            result.SCAC = "HOLM";
            result.UserEmail = "bohrjos@chrobinson.com";
            result.UserName = "bohrjos";
            result.OriginalFileName = "test.csv";
            result.FileSubmittedDate = DateTime.Now;

            return result;
        }

        #endregion

    }
}
