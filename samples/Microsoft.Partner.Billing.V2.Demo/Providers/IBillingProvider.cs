﻿// -----------------------------------------------------------------------
// <copyright file="IBillingProvider.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Partner.Billing.V2.Demo.Providers
{
    using Microsoft.Partner.Billing.V2.Demo.Enums;
    using Microsoft.Partner.Billing.V2.Demo.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBillingProvider
    {
        Task<string> SubmitRequest(string token, string invoiceId, Fragment fragment);

        Task<OperationResult> GetOperationStatus(string token, string uri);

        Task<Manifest> GetManifest(string token, string uri);

        void IngestBilledUsageData(IEnumerable<BilledUsage> billedUsages);
    }
}
