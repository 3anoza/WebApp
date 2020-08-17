using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallery.DAL.Models;

namespace Gallery.DAL.Interfaces
{
    public interface IMediaRepository
    {
        Task<bool> IsMediaExistAsync(string path);
        Task MediaDeletedStatusUpdateAsync(string path,bool status);
        Task AddMediaToDatabaseAsync(string name, string path, User user, MediaType mediaType);
        Task<Media> GetMediaByPathAsync(string path);
        Task<bool> IsMediaTypeExistAsync(string extension);
        Task AddMediaTypeToDatabaseAsync(string type);
        Task<MediaType> GetMediaTypeAsync(string extension);
    }
}
