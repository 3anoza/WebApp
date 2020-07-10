using System;
using System.IO;

namespace Gallery.FileStorage.Services
{
    public class MediaStorageService : FileStorage.Interfaces.IMediaStorage 
    {

        public bool Upload(byte[] bytes, string path)
        {
            File.WriteAllBytes(path, bytes);
            return File.Exists(path);
        }

        public bool Delete(string path)
        {
            File.Delete(path);
            return File.Exists(path);
        }

        public byte[] Read(string path)
        {
           return File.ReadAllBytes(path);
        }
    }
}