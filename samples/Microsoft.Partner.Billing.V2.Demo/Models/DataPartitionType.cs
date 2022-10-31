﻿// -----------------------------------------------------------------------
// <copyright file="DataPartitionType.cs" company="Microsoft">
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
    /// Enums providing type of partitions for billing data.
    /// </summary>
    public enum DataPartitionType
    {
        /// <summary>
        /// Data is partitioned based on max number of rows/records.
        /// </summary>
        Default
    }
}
