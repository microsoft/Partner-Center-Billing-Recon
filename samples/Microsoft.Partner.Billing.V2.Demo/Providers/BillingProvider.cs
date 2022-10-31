// -----------------------------------------------------------------------
// <copyright file="BillingProvider.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Partner.Billing.V2.Demo.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;
    using static System.Net.Mime.MediaTypeNames;
    using Microsoft.Partner.Billing.V2.Demo.Enums;
    using Microsoft.Partner.Billing.V2.Demo.HttpRequest;
    using Microsoft.Partner.Billing.V2.Demo.Models;
    using Microsoft.Partner.Billing.V2.Demo.Utilities;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class BillingProvider : IBillingProvider
    {
        private readonly IHttpRequestHandler httpRequestHandler = null;
        private readonly string rootURL = AppConfiguration.GetAPIRootURL();
        private readonly SqlServerProvider sqlServerProvider = null;

        public BillingProvider(IHttpRequestHandler httpRequestHandler, SqlServerProvider sqlServerProvider)
        {
            this.httpRequestHandler = httpRequestHandler;
            this.sqlServerProvider = sqlServerProvider;
        }

        /// <summary>
        /// submit request for get billed or unbilled consumption line items. 
        /// Request wull be accepted and  will return a 202 HTTP status(Accepted) and a location header with the URI.
        /// </summary>
        /// <param name="token">pc authentication token</param>
        /// <param name="invoiceId">InvoiceId</param>
        /// <param name="fragment">Fragement</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<string> SubmitRequest(string token, string invoiceId, Fragment fragment)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            if (string.IsNullOrWhiteSpace(invoiceId))
            {
                throw new ArgumentNullException(nameof(invoiceId));
            }

            var uri = rootURL + "/v1/billedusage/invoices/" + invoiceId + "?Fragment =" + fragment;
            var operationLocation = await SetupClientAndExecuteActionAsync(HttpMethod.Post, uri, token,
                             response =>
                             {
                                 var operationLocation = response.Headers.GetValues("Operation-Location").FirstOrDefault();

                                 if (string.IsNullOrEmpty(operationLocation))
                                 {
                                     throw new Exception("Operation-Location in request header should not be null or empty.");
                                 }

                                 return operationLocation;
                             });

            return operationLocation;
        }

        /// <summary>
        /// receive the success status, keep polling this API at a regular interval. 
        /// If the requested data is unavailable, the API response will include a Retry-After header indicating 
        /// how long you should wait before sending another request
        /// </summary>
        /// <param name="token"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<OperationResult> GetOperationStatus(string token, string uri)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException(nameof(uri));
            }

            var operationResult = await SetupClientAndExecuteActionAsync(HttpMethod.Get, uri, token,
                             response =>
                             {
                                 string responseString = response.Content == null ? string.Empty : response.Content.ReadAsStringAsync().Result;
                                 var res = JsonConvert.DeserializeObject<OperationResult>(responseString);
                                 res.RetryAfter = response.Headers?.RetryAfter?.Delta;
                                 return res;
                             });
            return operationResult;
        }

        /// <summary>
        /// This endpoint provides a storage folder from which actual billing data can be downloaded
        /// </summary>
        /// <param name="token"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<Manifest> GetManifest(string token, string uri)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException(nameof(uri));
            }

            var manifest = await SetupClientAndExecuteActionAsync(HttpMethod.Get, uri, token,
                            response =>
                            {
                                string responseString = response.Content == null ? string.Empty : response.Content.ReadAsStringAsync().Result;
                                var manifest = JsonConvert.DeserializeObject<Manifest>(responseString);
                                return manifest;
                            });
            return manifest;
        }

        public void IngestBilledUsageData(IEnumerable<BilledUsage> billedUsages)
        {
           sqlServerProvider.IngestBilledUsageData(billedUsages);
        }

        /// <summary>
        /// Set Up HttpClient and Call API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="uri"></param>
        /// <param name="token"></param>
        /// <param name="deserializationFunction"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<T> SetupClientAndExecuteActionAsync<T>(HttpMethod method, string uri, string token, Func<HttpResponseMessage, T> deserializationFunction = null) where T : class
        {
            var correlationid = Guid.NewGuid().ToString();
            var requestid = Guid.NewGuid().ToString();
            var requestDetails = new HttpRequestDetails(method, new Uri(uri));
            requestDetails.Headers["ms-correlationid"] = correlationid;
            requestDetails.Headers["ms-requestid"] = requestid;
            requestDetails.Headers["ms-cv"] = $"{Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 0x16)}.0";
            requestDetails.Headers["Authorization"] = $"{"Bearer "}{token}";

            using (var responseMessage = await this.httpRequestHandler.SendRequestAsync(requestDetails))
            {
                string responseString = responseMessage.Content == null ? string.Empty : await responseMessage.Content.ReadAsStringAsync();
                
                if (!responseMessage.IsSuccessStatusCode)
                {
                    string message = string.Format("StatusCode = {0}, ErrorMessage = {1}, correlationid={2}", responseMessage.StatusCode.ToString(), responseString, correlationid);
                    throw new Exception(message);
                }
               
                return deserializationFunction != null
                     ? deserializationFunction.Invoke(responseMessage)
                     : JsonConvert.DeserializeObject<T>(responseString);
            }
         }
    }
}
