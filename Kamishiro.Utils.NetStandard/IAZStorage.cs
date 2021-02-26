using Kamishiro.Utils.NetStandard.Enums;
using System.IO;

namespace Kamishiro.Utils.NetStandard.Azure
{
    public interface IAZStorage
    {
        string ConnectionString { get; set; }
        StatusCode Download(string containerName, string blobName, Stream stream);
        StatusCode Upload(string containerName, string blobName, string mime, Stream stream);
    }
}
