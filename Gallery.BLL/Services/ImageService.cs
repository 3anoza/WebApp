using System;
using System.Text.RegularExpressions;
using Gallery.BLL.Interfaces;
using Gallery.FileStorage.Interfaces;

namespace Gallery.BLL.Services
{
    public class ImageService : IImageService
    {
        protected  readonly IMediaStorage _mediaStorage;
        public ImageService(IMediaStorage mediaStorage)
        {
            _mediaStorage = mediaStorage ?? throw new ArgumentNullException();
        }

        public bool Upload(byte[] bytes, string path)
        {
            return _mediaStorage.Upload(bytes, path);
        }

        public byte[] Read(string path)
        {
            return _mediaStorage.Read(path);
        }

        public string FilenameGenerator(string filename)
        {
            try
            {
                return Regex.Replace
                (filename,
                    @"[^\w\.@-]",
                    "",
                    RegexOptions.None,
                    TimeSpan.FromSeconds(1.5));
            }
            catch (RegexMatchTimeoutException e)
            {
                return "[ERR]: " + e.ToString();
            }
        }

        public bool Delete(string path)
        {
            return _mediaStorage.Delete(path);
        }
    }
}