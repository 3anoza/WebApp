using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Gallery.BLL.Contracts;
using Gallery.BLL.Interfaces;
using Gallery.FileStorage.Interfaces;
using Gallery.DAL.Interfaces;

namespace Gallery.BLL.Services
{
    public class ImageService : IImageService
    {
        protected readonly IMediaStorage _mediaStorage;
        protected readonly IMediaRepository _mediaRepository;
        protected readonly IRepository _repository;
        public ImageService(IMediaStorage mediaStorage, IMediaRepository mediaRepository, IRepository repository)
        {
            _mediaStorage = mediaStorage ?? throw new ArgumentNullException(nameof(_mediaStorage));
            _mediaRepository = mediaRepository ?? throw new ArgumentNullException(nameof(_mediaRepository));
            _repository = repository ?? throw new ArgumentNullException(nameof(_repository));
        }

        public async Task<bool> UploadImageAsync(byte[] bytes, string path, UserDto userDto)
        {
            var IsMediaExistAsync = await _mediaRepository.IsMediaExistAsync(path);
            if (IsMediaExistAsync)
            {
                var media = await _mediaRepository.GetMediaByPathAsync(path);
                if (media.IsDeleted)
                {
                    await _mediaRepository.MediaDeletedStatusUpdateAsync(path, false);
                }
            }

            var extension = Path.GetExtension(path);
            var user = await _repository.FindUserAsync(userDto.UserEmail, userDto.Password);
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

        public async Task<bool> DeleteImageAsync(string path)
        {
            var IsMediaExistAsync = await _mediaRepository.IsMediaExistAsync(path);
            if (IsMediaExistAsync)
            {
                await _mediaRepository.MediaDeletedStatusUpdateAsync(path, true);
            }
            return _mediaStorage.Delete(path);
        }
    }
}