// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Microsoft.Partner.Billing.V2.Demo
{
    using Azure;
    using Azure.Storage.Files.DataLake;
    using Azure.Storage.Files.DataLake.Models;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Partner.Billing.V2.Demo.Enums;
    using Microsoft.Partner.Billing.V2.Demo.HttpRequest;
    using Microsoft.Partner.Billing.V2.Demo.Models;
    using Microsoft.Partner.Billing.V2.Demo.Providers;
    using Microsoft.Partner.Billing.V2.Demo.Services;
    using Newtonsoft.Json;
    using System;
    using System.IO.Compression;
    using JsonSerializer = Newtonsoft.Json.JsonSerializer;

    public class Program
    {
        private static string accessToken = ""; //update partner center authentication token value
        private static string invoiceid = ""; //update invoiceid

        private static string downloadPath = "";//update local path for download files
        private static string extractUsageFilesPath = "";//update local path for  extract GZ files.        
        
        private static Fragment fragment = Fragment.Full; // update fragment value either Full or Basic
        private static IBillingUsageService? billingUsageService = null;

        static async Task Main(string[] args)
        {
            try
            {
                //setup
                var services = new ServiceCollection();
                InitializeDependency(services);

                // initiate sequence of Partner Center API calls to get Manifest details from PC  
                await GetBilledUsage(accessToken, invoiceid, fragment);
               
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Get billed rated usage line items for the closed billing period
        /// </summary>
        /// <param name="accessToken">bearer token for authentication</param>
        /// <param name="invoiceid">InvoiceId</param>
        /// <param name="fragment"></param>
        /// <returns></returns>
        public static async Task GetBilledUsage(string accessToken, string invoiceid, Fragment fragment)
        {
            try
            {
                // Step : 1# Get manifest using Partner Center APIs,
                var manifest = await billingUsageService.GetManifestForBilledUsage(accessToken, invoiceid, fragment);
                
                if(manifest == null)
                {
                    throw new Exception("Manifest file is not ready yet. Please try after sometime");
                }

                // Step : 2# Get actual usage data/files from Azure blob storage
                var rootFolderSAS = manifest.RootFolderSAS;
                var blobs = manifest.Blobs;
                await DownloadBlob(rootFolderSAS, blobs);

                Console.WriteLine("Download is completed");

                // Step : 3# Process usage files after files are downloaded.
                ExtractGZFiles();

                // Step : 4# Consume usage data as per consumer needs
                // this sample shows to insert usage data in SQL server
                IngestBilledUsageDataIntoDB();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static void IngestBilledUsageDataIntoDB()
        {
            var billedUsages = new List<BilledUsage>();
            var d = new DirectoryInfo(extractUsageFilesPath);

            FileInfo[] Files = d.GetFiles("*.json");

            foreach (FileInfo file in Files)
            {
                var reader = new StreamReader(file.FullName);
                string jsonString = reader.ReadToEnd();

                var jsonReader = new JsonTextReader(new StringReader(jsonString))
                {
                    SupportMultipleContent = true 
                };

                var jsonSerializer = new JsonSerializer();
                while (jsonReader.Read())
                {
                    var billedUsage = jsonSerializer.Deserialize<BilledUsage>(jsonReader);
                    billedUsages.Add(billedUsage);
                }
            }

            if (billedUsages.Any())
            {
                billingUsageService.IngestBilledUsageData(billedUsages);
                Console.WriteLine("Number of billing usage recon records are inerted into sql server db is {0}", billedUsages.Count);
            }
        }        

        private static void InitializeDependency(IServiceCollection services)
        {
            services.AddScoped<IBillingUsageService, BillingUsageService>();
            services.AddScoped<IBillingProvider, BillingProvider>();
            services.AddScoped<IHttpRequestHandler, HttpRequestHandler>();
            services.AddScoped<SqlServerProvider>();
            var serviceProvider = services.BuildServiceProvider();
            billingUsageService = serviceProvider.GetService<IBillingUsageService>();
        }

       /// <summary>
       /// Download files from storage account using Microsoft Azure Storage SDK
       /// </summary>
       /// <param name="rootFolderSAS"></param>
       /// <param name="blobs"></param>
        private static async Task DownloadBlob(string rootFolderSAS, IReadOnlyList<BillingBlob> blobs)
        {
            // Get client for folder containing all the blobs/files
            DataLakeDirectoryClient directory = new DataLakeDirectoryClient(new Uri(rootFolderSAS));           

            foreach(var blob in blobs)
            {
                // get file client for each file/blob
                var fileClient = directory.GetFileClient(blob.Name);

                // read blob content and download to local path
                Response<FileDownloadInfo> downloadResponse = await fileClient.ReadAsync();
                BinaryReader reader = new BinaryReader(downloadResponse.Value.Content);
                FileStream fileStream = File.OpenWrite(downloadPath + blob.Name);

                int bufferSize = 4096;
                byte[] buffer = new byte[bufferSize];
                int count;

                while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
                {
                    fileStream.Write(buffer, 0, count);
                }

                await fileStream.FlushAsync();

                fileStream.Close();
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        private static void ExtractGZFiles()
        {
            var d = new DirectoryInfo(downloadPath);

            FileInfo[] Files = d.GetFiles("*.json.gz"); 

            foreach (FileInfo file in Files)
            {
                using FileStream compressedFileStream = File.Open(file.FullName, FileMode.Open);
                var extacrFileName = file.Name.Replace(file.Extension, "");
                using FileStream outputFileStream = File.Create(extractUsageFilesPath + extacrFileName);
                using var decompressor = new GZipStream(compressedFileStream, CompressionMode.Decompress);
                decompressor.CopyTo(outputFileStream);
            }
        }
    }
   
}

