using System;

namespace Gallery.FileStorage.Interfaces
{
    public interface IMediaStorage
    {
        /// <summary>
        /// Return True if file exist in current filesystem
        /// </summary>
        /// <param name="bytes">Media file</param>
        /// <returns></returns>
        bool IsExist(byte[] bytes);
        /// <summary>
        /// Write to file current bytes array and save in current filesystem
        /// </summary>
        /// <param name="bytes">Media file</param>
        void Create(byte[] bytes);
        /// <summary>
        /// Rewrite file in filesystem
        /// </summary>
        /// <param name="bytes">Medial file</param>
        void Update(byte[] bytes);
        /// <summary>
        /// Delete current file from filesystem
        /// </summary>
        /// <param name="bytes"></param>
        void Delete(byte[] bytes);

        /*
         * Block for async method's: 
         *
         * Task CreateAsync(byte[] bytes);
         * Task UpdateAsync(byte[] bytes);
         * Task DeleteAsync(byte[] bytes);
         */
    }
}