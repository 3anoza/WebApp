using System;

namespace Gallery.FileStorage.Interfaces
{
    public interface IMediaStorage
    {
        /// <summary>
        /// Write to file current bytes array and save in current filesystem
        /// </summary>
        /// <returns>Status - added file or not</returns>
        /// <param name="bytes">Media file</param>
        /// <param name="path">path to media directory</param>
        bool Upload(byte[] bytes, string path);
        /// <summary>
        /// Delete current file from filesystem
        /// </summary>
        /// <param name="path"></param>
        bool Delete(string path);
        /// <summary>
        /// Read all bytes from set file
        /// </summary>
        /// <param name="path">path to media file</param>
        /// <returns>return bytes array</returns>
        byte[] Read(string path);

        /*
         * Block for async method's: 
         *
         * Task ReadAsync(byte[] bytes);
         * Task UpdateAsync(byte[] bytes, string path);
         * Task DeleteAsync(string path);
         */
    }
}