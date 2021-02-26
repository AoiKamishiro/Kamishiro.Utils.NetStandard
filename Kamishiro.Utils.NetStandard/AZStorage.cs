using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Kamishiro.Utils.NetStandard.Enums;
using System;
using System.IO;

namespace Kamishiro.Utils.NetStandard.Azure
{
    public class AZStorage : IAZStorage
    {
        public string ConnectionString { get; set; }

        public AZStorage(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public StatusCode Download(string containerName, string blobName, Stream stream)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(ConnectionString);
            BlobContainerClient blobContainerClient;
            BlobClient blobClient;
            try
            {
                blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
                blobClient = blobContainerClient.GetBlobClient(blobName);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while retrieving the Blob. " + e.Message);
                return StatusCode.Error;
            }

            if (stream.Length != 0)
            {
                stream = new MemoryStream();
                Console.WriteLine("Stream was initialized because it was not empty.");
            }

            blobClient.DownloadToAsync(stream).Wait();
            _ = stream.Seek(0, SeekOrigin.Begin);

            Console.WriteLine("Finished downloading Blob from {0}/{1}.", containerName, blobName);
            return StatusCode.OK;
        }
        public StatusCode Upload(string containerName, string blobName, string mime, Stream stream)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(ConnectionString);
            BlobContainerClient blobContainerClient;
            BlobClient blobClient;
            try
            {
                blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
                blobClient = blobContainerClient.GetBlobClient(blobName);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while retrieving the Blob. " + e.Message);
                return StatusCode.Error;
            }
            _ = stream.Seek(0, SeekOrigin.Begin);
            blobClient.UploadAsync(stream, true).Wait();
            BlobProperties blobProperties = blobClient.GetProperties();
            BlobHttpHeaders blobHttpHeaders = new BlobHttpHeaders
            {
                ContentType = mime,
                ContentDisposition = blobProperties.ContentDisposition,
                CacheControl = blobProperties.CacheControl,
                ContentEncoding = blobProperties.ContentEncoding,
                ContentHash = blobProperties.ContentHash,
                ContentLanguage = blobProperties.ContentLanguage
            };
            blobClient.SetHttpHeadersAsync(blobHttpHeaders).Wait();
            Console.WriteLine("Finished uploading Blob to {0}/{1}.", containerName, blobName);
            return StatusCode.OK;
        }
    }

}
