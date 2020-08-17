using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Gallery.DAL.Contexts;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Models;

namespace Gallery.DAL.Repository
{
    public class MediaRepository: IMediaRepository
    {
        protected readonly SqlContext Context;

        public MediaRepository(SqlContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> IsMediaExistAsync(string path)
        {
            return await Context.Media.AnyAsync(m => m.Path == path);
        }

        public async Task MediaDeletedStatusUpdateAsync(string path, bool status)
        {
            var media = await Context.Media.FirstOrDefaultAsync(m => m.Path == path);
            if (media != null)
            {
                media.IsDeleted = status;
            }
            await Context.SaveChangesAsync();
        }

        public async Task AddMediaToDatabaseAsync(string name, string path, User user, MediaType mediaType)
        {
            Context.Media.Add(new Media
            {
                Name = name,
                Path = path,
                PersonId = user.Id,
                MediaTypeId = mediaType.Id
            });
            await Context.SaveChangesAsync();
        }

        public async Task<Media> GetMediaByPathAsync(string path)
        {
            return await Context.Media.FirstOrDefaultAsync(m => m.Path == path);
        }

        public async Task<bool> IsMediaTypeExistAsync(string extension)
        {
            return await Context.MediaTypes.AnyAsync(m => m.Type == extension);
        }

        public async Task AddMediaTypeToDatabaseAsync(string type)
        {
            Context.MediaTypes.Add(new MediaType
            {
                Type = type
            });
            await Context.SaveChangesAsync();
        }

        public async Task<MediaType> GetMediaTypeAsync(string extension)
        {
            return await Context.MediaTypes.FirstOrDefaultAsync(m => m.Type == extension);
        }
    }
}