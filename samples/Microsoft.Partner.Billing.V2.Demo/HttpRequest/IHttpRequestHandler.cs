// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Microsoft.Partner.Billing.V2.Demo.HttpRequest
{
    using Microsoft.Partner.Billing.V2.Demo.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IHttpRequestHandler
    {
        Task<HttpResponseMessage> SendRequestAsync(HttpRequestDetails requestDetails);
    }
}
