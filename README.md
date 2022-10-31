# Project

> This repo contains a samples showing usage of the new async model APIs to get billing and recon data.

> For API details, please refer to the Partner Center [docs](https://learn.microsoft.com/en-us/partner-center/develop/get-invoice-billed-unbilled-consumption).

## To run samples
**Prerequisites**

- You need to have Visual studio 2022 with .NET 6.0 installed.
- Bearer token to access API. The new APIs accept the same authentication bearer token as existing APIs as mentioned in the [docs](https://learn.microsoft.com/en-us/partner-center/develop/partner-center-authentication). There is no change in the logic/values for generating token for new APIs.


After downloading the file, open the solution samples\Microsoft.Partner.Billing.V2.Demo.sln and update below values in program.cs based on your scenario.
- accessToken: bearer token for authentication
- invoiceid: invoiceid for which to get billed data. Invoices from September ownwards are only available. Example G012040490
- downloadPath: local path where billing blobs should be downloaded. Example c:\downloads\
- extractUsageFilesPath: local path where billing blobs after uncompression will be generated. Example c:\downloads\
 

## Contributing

Presently this project is not open for contributions.  For any suggestions kindly reach out through regular support 
options for Partner Center.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

## Trademarks

This project may contain trademarks or logos for projects, products, or services. Authorized use of Microsoft 
trademarks or logos is subject to and must follow 
[Microsoft's Trademark & Brand Guidelines](https://www.microsoft.com/en-us/legal/intellectualproperty/trademarks/usage/general).
Use of Microsoft trademarks or logos in modified versions of this project must not cause confusion or imply Microsoft sponsorship.
Any use of third-party trademarks or logos are subject to those third-party's policies.
