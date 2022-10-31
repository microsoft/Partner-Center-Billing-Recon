using Microsoft.Partner.Billing.V2.Demo.Models;
using Microsoft.Partner.Billing.V2.Demo.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Partner.Billing.V2.Demo.Providers
{
    public class SqlServerProvider
    {
        /// <summary>
        /// Ingest billed usage into db.
        /// </summary>
        /// <param name="billedUsages"></param>
        public void IngestBilledUsageData(IEnumerable<BilledUsage> billedUsages)
        {
            var columnNames = new List<string> { "PartnerId","PartnerName", "CustomerId", "CustomerName","CustomerDomainName", "CustomerCountry",
                                                "MpnId","InvoiceNumber","ProductId","SkuId","AvailabilityId","SkuName", "ProductName",
                                                "PublisherName","PublisherId", "SubscriptionDescription","SubscriptionId",
                                                "ChargeStartDate", "ChargeEndDate","UsageDate","MeterType","MeterCategory",
                                                "MeterId","MeterSubCategory","MeterName","MeterRegion","Unit",
                                                "ResourceLocation","ConsumedService","ResourceGroup","ResourceURI",
                                                "ChargeType","UnitPrice","Quantity","UnitType","BillingPreTaxTotal",
                                                "BillingCurrency","PricingPreTaxTotal","PricingCurrency","ServiceInfo1",
                                                "ServiceInfo2","AdditionalInfo","Tags","EffectiveUnitPrice","PCToBCExchangeRate",
                                                "PCToBCExchangeRateDate","EntitlementId","EntitlementDescription","CreditType",
                                                "BenefitId","BenefitOrderId","BenefitType" };
            var bulkData = new DataTable();
            bulkData.Columns.Add(columnNames[0], typeof(string)); //PartnerId
            bulkData.Columns.Add(columnNames[1], typeof(string)); //PartnerName
            bulkData.Columns.Add(columnNames[2], typeof(string)); //CustomerId
            bulkData.Columns.Add(columnNames[3], typeof(string)); //CustomerName
            bulkData.Columns.Add(columnNames[4], typeof(string)); //CustomerDomainName
            bulkData.Columns.Add(columnNames[5], typeof(string)); //CustomerCountry
            bulkData.Columns.Add(columnNames[6], typeof(string)); //MpnId
            bulkData.Columns.Add(columnNames[7], typeof(string));  // InvoiceNumber
            bulkData.Columns.Add(columnNames[8], typeof(string));  // ProductId
            bulkData.Columns.Add(columnNames[9], typeof(string));  // SkuId
            bulkData.Columns.Add(columnNames[10], typeof(string)); // AvailabilityId
            bulkData.Columns.Add(columnNames[11], typeof(string)); // SkuName
            bulkData.Columns.Add(columnNames[12], typeof(string)); // ProductName
            bulkData.Columns.Add(columnNames[13], typeof(string)); // PublisherName
            bulkData.Columns.Add(columnNames[14], typeof(string));  // PublisherId
            bulkData.Columns.Add(columnNames[15], typeof(string));  // SubscriptionDescription
            bulkData.Columns.Add(columnNames[16], typeof(string));  // SubscriptionId
            bulkData.Columns.Add(columnNames[17], typeof(DateTime));  // ChargeStartDate
            bulkData.Columns.Add(columnNames[18], typeof(DateTime));  // ChargeEndDate
            bulkData.Columns.Add(columnNames[19], typeof(DateTime));  // UsageDate
            bulkData.Columns.Add(columnNames[20], typeof(string));  // MeterType
            bulkData.Columns.Add(columnNames[21], typeof(string));  // MeterCategory
            bulkData.Columns.Add(columnNames[22], typeof(string));  // MeterId
            bulkData.Columns.Add(columnNames[23], typeof(string));  // MeterSubCategory
            bulkData.Columns.Add(columnNames[24], typeof(string));  // MeterName
            bulkData.Columns.Add(columnNames[25], typeof(string));  // MeterRegion
            bulkData.Columns.Add(columnNames[26], typeof(string));  // Unit
            bulkData.Columns.Add(columnNames[27], typeof(string));  // ResourceLocation
            bulkData.Columns.Add(columnNames[28], typeof(string));  // ConsumedService
            bulkData.Columns.Add(columnNames[29], typeof(string));  // ResourceGroup
            bulkData.Columns.Add(columnNames[30], typeof(string));  // ResourceURI
            bulkData.Columns.Add(columnNames[31], typeof(string));  // ChargeType
            bulkData.Columns.Add(columnNames[32], typeof(decimal));  // UnitPrice
            bulkData.Columns.Add(columnNames[33], typeof(decimal));  // Quantity
            bulkData.Columns.Add(columnNames[34], typeof(string));  // UnitType
            bulkData.Columns.Add(columnNames[35], typeof(decimal));  // BillingPreTaxTotal
            bulkData.Columns.Add(columnNames[36], typeof(string));  // BillingCurrency
            bulkData.Columns.Add(columnNames[37], typeof(decimal));  // PricingPreTaxTotal
            bulkData.Columns.Add(columnNames[38], typeof(string));  // PricingCurrency
            bulkData.Columns.Add(columnNames[39], typeof(string));  // ServiceInfo1
            bulkData.Columns.Add(columnNames[40], typeof(string));  // ServiceInfo2
            bulkData.Columns.Add(columnNames[41], typeof(string));  // AdditionalInfo
            bulkData.Columns.Add(columnNames[42], typeof(string));  // Tags
            bulkData.Columns.Add(columnNames[43], typeof(decimal));  // EffectiveUnitPrice
            bulkData.Columns.Add(columnNames[44], typeof(decimal));  // PCToBCExchangeRate
            bulkData.Columns.Add(columnNames[45], typeof(string));  // PCToBCExchangeRateDate
            bulkData.Columns.Add(columnNames[46], typeof(string));  // EntitlementId
            bulkData.Columns.Add(columnNames[47], typeof(string));  // EntitlementDescription
            bulkData.Columns.Add(columnNames[48], typeof(string));  // CreditType
            bulkData.Columns.Add(columnNames[49], typeof(string));  // BenefitId
            bulkData.Columns.Add(columnNames[50], typeof(string));  // BenefitOrderId
            bulkData.Columns.Add(columnNames[51], typeof(string));  // BenefitType

            foreach (var billedUsage in billedUsages)
            {
                var row = bulkData.NewRow();
                row[0] = billedUsage.PartnerId;
                row[1] = billedUsage.PartnerName;
                row[2] = billedUsage.CustomerId;
                row[3] = billedUsage.CustomerName;
                row[4] = billedUsage.CustomerDomainName;
                row[5] = billedUsage.CustomerCountry;
                row[6] = billedUsage.MpnId;
                row[7] = billedUsage.InvoiceNumber;
                row[8] = billedUsage.ProductId;
                row[9] = billedUsage.SkuId;
                row[10] = billedUsage.AvailabilityId;
                row[11] = billedUsage.SkuName;
                row[12] = billedUsage.ProductName;
                row[13] = billedUsage.PublisherName;
                row[14] = billedUsage.PublisherId;
                row[15] = billedUsage.SubscriptionDescription;
                row[16] = billedUsage.SubscriptionId;
                row[17] = billedUsage.ChargeStartDate;
                row[18] = billedUsage.ChargeEndDate;
                row[19] = billedUsage.UsageDate;
                row[20] = billedUsage.MeterType;
                row[21] = billedUsage.MeterCategory;
                row[22] = billedUsage.MeterId;
                row[23] = billedUsage.MeterSubCategory;
                row[24] = billedUsage.MeterName;
                row[25] = billedUsage.MeterRegion;
                row[26] = billedUsage.Unit;
                row[27] = billedUsage.ResourceLocation;
                row[28] = billedUsage.ConsumedService;
                row[29] = billedUsage.ResourceGroup;
                row[30] = billedUsage.ResourceURI;
                row[31] = billedUsage.ChargeType;
                row[32] = billedUsage.UnitPrice;
                row[33] = billedUsage.Quantity;
                row[34] = billedUsage.UnitType;
                row[35] = billedUsage.BillingPreTaxTotal;
                row[36] = billedUsage.BillingCurrency;
                row[37] = billedUsage.PricingPreTaxTotal;
                row[38] = billedUsage.PricingCurrency;
                row[39] = billedUsage.ServiceInfo1;
                row[40] = billedUsage.ServiceInfo2;
                row[41] = billedUsage.AdditionalInfo;
                row[42] = billedUsage.Tags;
                row[43] = billedUsage.EffectiveUnitPrice;
                row[44] = billedUsage.PCToBCExchangeRate;
                row[45] = billedUsage.PCToBCExchangeRateDate;
                row[46] = billedUsage.EntitlementId;
                row[47] = billedUsage.EntitlementDescription;
                row[48] = billedUsage.CreditType;
                row[49] = billedUsage.BenefitId;
                row[50] = billedUsage.BenefitOrderId;
                row[51] = billedUsage.BenefitType;
                bulkData.Rows.Add(row);
            }

            string connetionString = AppConfiguration.GetDBConnectionString();

            try
            {
                using (var sqlConnection = new SqlConnection(connetionString))
                {
                    sqlConnection.Open();

                    using (var sqlBulkCopy = new SqlBulkCopy(sqlConnection) { DestinationTableName = AppConfiguration.GetBilledUsageTableName() })
                    {
                        sqlBulkCopy.WriteToServer(bulkData);
                    }
                    sqlConnection.Close();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
