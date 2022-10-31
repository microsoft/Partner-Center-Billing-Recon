// -----------------------------------------------------------------------
// <copyright file="HttpRequestHandler.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Partner.Billing.V2.Demo.HttpRequest
{
    using Microsoft.Partner.Billing.V2.Demo.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    public class HttpRequestHandler : IHttpRequestHandler
    {
        public async Task<HttpResponseMessage> SendRequestAsync(
            HttpRequestDetails requestDetails)
        {
            if (requestDetails == null)
            {
                throw new ArgumentNullException(nameof(requestDetails));
            }

            HttpResponseMessage response = null;

            try
            {
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(requestDetails.Method, requestDetails.RequestUri))
                    {
                        foreach (var headerName in requestDetails.Headers.AllKeys)
                        {
                            request.Headers.Add(headerName, requestDetails.Headers[headerName]);
                        }

                        try
                        {
                            response = await client.SendAsync(request, CancellationToken.None);
                        }
                        catch (OperationCanceledException ex)
                        {
                            if (ex.CancellationToken.IsCancellationRequested)
                            {
                                throw;
                            }

                            response = new HttpResponseMessage(HttpStatusCode.GatewayTimeout);
                        }
                        catch (WebException ex)
                        {
                            if (ex.Status == WebExceptionStatus.Timeout || ex.Status == WebExceptionStatus.RequestCanceled)
                            {
                                // It may be a request timeout.
                                response = new HttpResponseMessage(HttpStatusCode.GatewayTimeout);
                            }
                            else
                            {
                                throw ex;
                            }
                        }

                        if (response.IsSuccessStatusCode)
                        {
                            return response;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response?.Dispose();
                throw;
            }

            return response;
        }
    }
}
