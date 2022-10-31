// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Microsoft.Partner.Billing.V2.Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The HTTP request details.
    /// </summary>
    public class HttpRequestDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestDetails"/> class.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="headers">The headers to be added to the request.</param>
        public HttpRequestDetails(
            HttpMethod method,
            Uri requestUri,
            NameValueCollection headers = null)
        {
            this.Method = method ?? throw new ArgumentNullException(nameof(method));
            this.RequestUri = requestUri ?? throw new ArgumentNullException(nameof(requestUri));
            this.Headers = headers ?? new NameValueCollection();
        }

        /// <summary>
        /// Gets the request method.
        /// </summary>
        public HttpMethod Method { get; }

        /// <summary>
        /// Gets the request URI.
        /// </summary>
        public Uri RequestUri { get; }

        /// <summary>
        /// Gets the request headers.
        /// </summary>
        public NameValueCollection Headers { get; }
    }
}
