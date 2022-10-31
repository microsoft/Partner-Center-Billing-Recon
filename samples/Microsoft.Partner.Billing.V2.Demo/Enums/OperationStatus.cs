// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Microsoft.Partner.Billing.V2.Demo.Enums
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents status of Operation
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OperationStatus
    {
        /// <summary>
        /// Processing of operation has not yet started
        /// </summary>
        NotStarted,
        /// <summary>
        /// processing is running, data should be available soon.
        /// </summary>
        Running,
        /// <summary>
        /// Data is ready
        /// </summary>
        Succeeded,

        /// <summary>
        /// failed to generate data, use a new operation
        /// </summary>
        Failed
    }
}
