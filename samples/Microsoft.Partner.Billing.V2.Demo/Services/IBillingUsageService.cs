// -----------------------------------------------------------------------
// <copyright file="IBillingUsageService.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Partner.Billing.V2.Demo.Services
{
    using Microsoft.Partner.Billing.V2.Demo.Enums;
    using Microsoft.Partner.Billing.V2.Demo.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBillingUsageService
    {
        Task<Manifest> GetManifestForBilledUsage(string token, string invoiceId, Fragment fragment);

        void IngestBilledUsageData(IEnumerable<BilledUsage> billedUsages);
    }
}
