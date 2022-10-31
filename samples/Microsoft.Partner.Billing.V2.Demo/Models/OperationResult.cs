// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Microsoft.Partner.Billing.V2.Demo.Models
{
    using Microsoft.Partner.Billing.V2.Demo.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    public class OperationResult
    {
        public OperationStatus Status { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime LastActionDateTime { get; set; }

        public Uri ResourceLocation { get; set; }

        public TimeSpan? RetryAfter { get; set; }
    }
}
