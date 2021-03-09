using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using RephaseV2.Models;
using RephaseV2.Services.Interfaces;

namespace RephaseV2.Services
{
    public class AzureStorageHelper : IAzureStorageHelper
    {
        private IConfiguration _configuration;

        public AzureStorageHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Download images from Azure Blob Storage.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public async Task DownloadBlobAsync(string filepath, string image)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_configuration["TableStorageConnString"]);
                //  create a blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                //  create a container 
                CloudBlobContainer container = blobClient.GetContainerReference("images");
                //  create a block blob.
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(image);
                // download blob              
                await blockBlob.DownloadToFileAsync(filepath, FileMode.Create);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Uploads file from local storage to Azure Blob Storage.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task UploadBlobFromLocalAsync(string filepath, string fileName)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_configuration["TableStorageConnString"]);
                //  create a blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                //  create a container 
                CloudBlobContainer container = blobClient.GetContainerReference("images");
                //  create a block blob.
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
                // download blob               
                await blockBlob.UploadFromFileAsync(Path.Combine(filepath, fileName));

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Uploads file from local storage to a temporary storage location in Azure Blob Storage.
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="fileName"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public async Task UploadBlobFromWebAsync(Stream fileStream, string fileName, Guid sessionId)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_configuration["TableStorageConnString"]);
                // Create a blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                // Create a container 
                CloudBlobContainer container = blobClient.GetContainerReference("temp-images");
                await container.CreateIfNotExistsAsync();
                // Create a block blob.
                CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{sessionId}/{fileName}");
                blockBlob.Properties.ContentType = "image/jpg";
                // download blob               
                await blockBlob.UploadFromStreamAsync(fileStream);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Upload content json from azure storage.
        /// </summary>
        /// <returns></returns>
        public async void UploadContentJsonAsync(string json)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_configuration["TableStorageConnString"]);
                // Create the table client.
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                // Get a reference to a table named "json"
                CloudTable jsonTable = tableClient.GetTableReference("json");

                // Create a retrieve operation that takes a customer entity.
                TableOperation retrieveOperation = TableOperation.Retrieve<AzureTableEntity>("Rephase", "ContentJson_DEV");

                // Execute the operation.
                TableResult retrievedResult = await jsonTable.ExecuteAsync(retrieveOperation);

                Task.WaitAll();

                // Assign the result to a AzureTableEntity object.
                AzureTableEntity updateEntity = (AzureTableEntity)retrievedResult.Result;

                if (updateEntity != null)
                {
                    //Update json.
                    updateEntity.Value = json;

                    // Create the Replace TableOperation.
                    TableOperation updateOperation = TableOperation.Replace(updateEntity);

                    // Execute the operation.
                    await jsonTable.ExecuteAsync(updateOperation);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        ///// <summary>
        ///// Download content json from azure storage.
        ///// </summary>
        ///// <returns></returns>
        public async Task<string> DownloadContentJsonAsync()
        {
            try
            {

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_configuration["TableStorageConnString"]);
                // Create the table client.
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                // Get a reference to a table named "json"
                CloudTable jsonTable = tableClient.GetTableReference("json");

                // Create a retrieve operation that takes a customer entity.
                TableOperation retrieveOperation = TableOperation.Retrieve<AzureTableEntity>("Rephase", "ContentJson_DEV");

                // Execute the retrieve operation.
                TableResult retrievedResult = await jsonTable.ExecuteAsync(retrieveOperation);

                string json = null;
                // Print the phone number of the result.
                if (retrievedResult.Result != null)
                {
                    //retrievedResult.Result.Properties[0].Value.StringValue
                    json = ((AzureTableEntity)retrievedResult.Result).Value;
                }
                else
                {
                    Console.WriteLine("Json could not be retrieved.");
                }

                return json;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
