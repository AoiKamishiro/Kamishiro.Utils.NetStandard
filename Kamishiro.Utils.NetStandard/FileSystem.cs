using Kamishiro.Utils.DotNetCore.Enums;
using System;
using System.IO;

namespace Kamishiro.Utils.DotNetCore.FileSystem
{
    public class FileSystem : IFileSystem
    {
        public string FilePath { get; }
        private readonly string FullPath;

        public FileSystem(string filePath)
        {
            FilePath = filePath;
            FullPath = Path.GetFullPath(filePath);
        }
        public StatusCode Load(Stream stream, bool overwrite = true)
        {
            if (!File.Exists(FilePath))
            {
                Console.WriteLine("The file did not exist. \"{0}\"", FullPath);
                return StatusCode.Error;
            }

            if (stream.Length != 0)
            {
                if (!overwrite)
                {
                    Console.WriteLine("The data already exists in the Stream. \"{0}\"", FullPath);
                    return StatusCode.Error;
                }
                else
                {
                    stream = new MemoryStream();
                }
            }

            FileStream fileStream = File.Open(FilePath, FileMode.Open);
            try
            {
                fileStream.Seek(0, SeekOrigin.Begin);
                fileStream.CopyTo(stream);
                Console.WriteLine("The file was successfully loaded. \"{0}\"", FullPath);
                fileStream.Dispose();
                return StatusCode.OK;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while loading the file. \"{0}\" Message: {1}", FullPath, e.Message);
                fileStream.Dispose();
                return StatusCode.Error;
            }
        }
        public StatusCode Save(Stream stream, bool overwrite = true)
        {
            if (stream.Length == 0)
            {
                Console.WriteLine("The stream is empty.. \"{0}\"", FullPath);
                return StatusCode.Error;
            }

            if (File.Exists(FilePath))
            {
                if (!overwrite)
                {
                    Console.WriteLine("The file already exists. \"{0}\"", FullPath);
                    return StatusCode.Error;
                }
            }

            FileStream fileStream;
            if (File.Exists(FilePath))
            {
                fileStream = File.Open(FilePath, FileMode.Create);
            }
            else
            {
                fileStream = File.Create(FilePath);
            }
            try
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
                Console.WriteLine("The file was successfully saved. \"{0}\"", FullPath);
                fileStream.Dispose();
                return StatusCode.OK;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while saving the file. \"{0}\" Message: {1}", FullPath, e.Message);
                fileStream.Dispose();
                return StatusCode.Error;
            }
        }
        public StatusCode Delete()
        {
            if (!File.Exists(FilePath))
            {
                Console.WriteLine("The file did not exist. Could not delete the file. \"{0}\"", FullPath);
                return StatusCode.Error;
            }

            try
            {
                File.Delete(FilePath);
                Console.WriteLine("File deleted successfully. \"{0}\"", FullPath);
                return StatusCode.OK;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while deleting the file. \"{0}\" Message: {1}", FullPath, e.Message);
                return StatusCode.Error;
            }
        }
    }
}
