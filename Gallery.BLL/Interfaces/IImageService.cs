namespace Gallery.BLL.Interfaces
{
    public interface IImageService
    {
        /// <summary>
        /// The method writes an array of bytes to a file.
        /// </summary>
        /// <param name="bytes">file in byte array</param>
        /// <param name="path">path to the saved directory</param>
        /// <returns>Execution result</returns>
        bool Upload(byte[] bytes, string path);
        /// <summary>
        /// The method deletes the file at the given path 
        /// </summary>
        /// <param name="path">path to file</param>
        /// <returns>Execution result</returns>
        bool Delete(string path);
        /// <summary>
        /// The method read all bytes from the given path
        /// </summary>
        /// <param name="path">path to file</param>
        /// <returns>file in byte array</returns>
        byte[] Read(string path);
        /// <summary>
        /// The method for generating a new name for the transferred file
        /// </summary>
        /// <param name="filename">current file name</param>
        /// <returns>New file name</returns>
        string FilenameGenerator(string filename);
    }
}