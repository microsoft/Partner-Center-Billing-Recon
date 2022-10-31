// -----------------------------------------------------------------------
// <copyright file="Manifest.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Partner.Billing.V2.Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a manifest to get full billing data
    /// </summary>
    public class Manifest
    {
        public string Version { get; set; }

        public string DataFormat { get; set; }

        public DateTime utcCreatedDateTime { get; set; }

        public string ETag { get; set; }

        public string PartnerTenantId { get; set; }

        public string RootFolder { get; set; }

        public string RootFolderSAS { get; set; }

        public DataPartitionType PartitionType { get; set; }

        public int BlobCount { get; set; }

        public long SizeInBytes { get; set; }

        public long ItemCount { get; set; }

        public IReadOnlyList<BillingBlob> Blobs { get; set; }
    }
}
