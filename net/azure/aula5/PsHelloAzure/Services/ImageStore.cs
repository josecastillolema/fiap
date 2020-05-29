using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PsHelloAzure.Services
{
    public class ImageStore
    {
        CloudBlobClient blobClient;
        string baseUri = "https://pshellostorage.blob.core.windows.net/";

        public ImageStore()
        {
            var credentials = new StorageCredentials("pshellostorage", "hTJoaNE9VRgHrkySec8R8n8ZjfKO3Cn6kusBK/w59tQURdiw1O79Ski0E+lbuF89fzSRTHqhkHRc8XE+F5t7mg==");
            blobClient = new CloudBlobClient(new Uri(baseUri), credentials);
        }

        public async Task<string> SaveImage(Stream imageStream)
        {
            var imageId = Guid.NewGuid().ToString();
            var container = blobClient.GetContainerReference("images");
            var blob = container.GetBlockBlobReference(imageId);
            await blob.UploadFromStreamAsync(imageStream);
            return imageId;
        }

        public string UriFor(string imageId)
        {
            var sasPolicy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-15),
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(15)
            };

            var container = blobClient.GetContainerReference("images");
            var blob = container.GetBlockBlobReference(imageId);
            var sas = blob.GetSharedAccessSignature(sasPolicy);
            return $"{baseUri}images/{imageId}{sas}";
        }
    }
}
