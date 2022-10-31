// -----------------------------------------------------------------------
// <copyright file="BilledUsage.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Partner.Billing.V2.Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Metadata.Ecma335;
    using System.Text;
    using System.Threading.Tasks;

    public class BilledUsage
    {
        public string PartnerId  { get; set; }
        public string PartnerName { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDomainName { get; set; }
        
        public string CustomerCountry  { get; set; }

        public string MpnId  { get; set; }

        public string InvoiceNumber  { get; set; }

        public string ProductId  { get; set; }

        public string SkuId  { get; set; }

        public string AvailabilityId { get; set; }

        public string SkuName  { get; set; }

        public string ProductName { get; set; }

        public string PublisherName { get; set; }
      
        public string PublisherId { get; set; }
        
        public string SubscriptionDescription { get; set; }
    
        public string SubscriptionId { get; set; }

        public DateTime ChargeStartDate { get; set; }

        public DateTime ChargeEndDate { get; set; }
       
        public DateTime UsageDate { get; set; }

        public string MeterType { get; set; }

        public string MeterCategory { get; set; }
     
        public string MeterId { get; set; }

        public string MeterSubCategory { get; set; }
 
        public string MeterName { get; set; }
    
        public string MeterRegion { get; set; }

        public string Unit { get; set; }

        public string ResourceLocation { get; set; }

        public string ConsumedService { get; set; }

        public string ResourceGroup { get; set; }
       
        public string ResourceURI { get; set; }
 
        public string ChargeType   { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal Quantity { get; set; }

        public string UnitType { get; set; }

        public decimal BillingPreTaxTotal { get; set; }

        public string BillingCurrency { get; set; }
      
        public decimal PricingPreTaxTotal { get; set; }
      
        public string PricingCurrency { get; set; }
        
        public string ServiceInfo1 { get; set; }

        public string ServiceInfo2 { get; set; }

        public string AdditionalInfo { get; set; }

        public string Tags { get; set; }

        public decimal EffectiveUnitPrice { get; set; }
       
        public decimal PCToBCExchangeRate { get; set; }

        public DateTime PCToBCExchangeRateDate { get; set; }
      
        public string EntitlementId { get; set; }
        
        public string EntitlementDescription { get; set; }

        public string CreditType { get; set; }
        public string BenefitId { get; set; }
        public string BenefitOrderId { get; set; }

        public string BenefitType { get; set; }
    }
}
