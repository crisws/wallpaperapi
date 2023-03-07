using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using wallpaperapi.Data.Enums;

namespace wallpaperapi.Service
{
    public class AzureBlobSotrageService : IAzureStorageService
    {
        private readonly string _azureStorageConnectionString;
        private readonly string _baselink;


        public AzureBlobSotrageService(IConfiguration configuration)
        {
            _azureStorageConnectionString = configuration.GetValue<string>("AzureStorageConnectionString");
            _baselink = configuration.GetValue<string>("AzureLinkBase");

        }

        public async Task DeleteAsync(ContainerEnum container, string blobFilename)
        {
            var containerName = Enum.GetName(typeof(ContainerEnum), container).ToLower();
            var blobContainerClient = new BlobContainerClient(_azureStorageConnectionString, containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobFilename);

            try
            {
                await blobClient.DeleteAsync();
            }
            catch
            {
            }
        }

        public async Task<string> UploadAsync(IFormFile file, ContainerEnum container, string blobName = null)
        {
            if (file.Length == 0) return null;

            var containerName = Enum.GetName(typeof(ContainerEnum), container).ToLower();

            var blobContainerClient = new BlobContainerClient(_azureStorageConnectionString, containerName);

            var extension = Path.GetExtension(file.FileName);


            if (string.IsNullOrEmpty(blobName))
            {
                blobName = Guid.NewGuid().ToString() + extension;
            }



            var blobClient = blobContainerClient.GetBlobClient(blobName);

            var blobHttpHeader = new BlobHttpHeaders { ContentType = file.ContentType };



            // Open a stream for the file we want to upload
            await using (Stream stream = file.OpenReadStream())
            {
                // Upload the file async
                await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeader });
            }

            return _baselink +"/"+ containerName +"/"+ blobName;
        }
    }

    public interface IAzureStorageService
    {
        Task<string> UploadAsync(IFormFile file, ContainerEnum container, string blobName = null);

        Task DeleteAsync(ContainerEnum container, string blobFilename);
    }



}
