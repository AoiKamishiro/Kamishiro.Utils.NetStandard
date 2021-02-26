using Kamishiro.Utils.DotNetCore.Enums;
using System.IO;

namespace Kamishiro.Utils.DotNetCore.Azure
{
    public interface IAZStorage
    {
        string ConnectionString { get; set; }
        StatusCode Download(string containerName, string blobName, Stream stream);
        StatusCode Upload(string containerName, string blobName, string mime, Stream stream);
    }
}
