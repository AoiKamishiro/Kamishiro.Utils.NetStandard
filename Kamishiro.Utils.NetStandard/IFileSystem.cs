using Kamishiro.Utils.NetStandard.Enums;
using System.IO;

namespace Kamishiro.Utils.NetStandard.FileSystem
{
    public interface IFileSystem
    {
        string FilePath { get; }
        StatusCode Load(Stream stream, bool overwrite);
        StatusCode Save(Stream stream, bool overwrite);
        StatusCode Delete();
    }
}
