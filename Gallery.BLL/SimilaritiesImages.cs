using System.Web;
using System.IO;


namespace Gallery.BLL
{
    public class SimilaritiesImages
    {
        /// <summary>
        /// Will return logic value (<typeparamref name="true"/> or <typeparamref name="false"/>) depending on whether there is such a hash in the database
        /// </summary>
        /// <param name="imgHash">image hash</param>
        /// <returns></returns>
        public bool CompareHashes(string imgHash)
        {
            //empty now
            return false;
        }

        /// <summary>
        /// Method of downloading an image from the server,
        /// returns true if the download was successful and false if an error occurs
        /// </summary>
        /// <param name="serverPathToImage">full or relative path to the image file in the server directory</param>
        /// <returns></returns>
        public bool DownloadImage(string serverPathToImage)
        {
            return false;
        }

        public bool DeleteImage(string imageHash)
        {
            return false;
        }

        public bool Upload(string pathToImages)
        {
            bool status = false;
            if (pathToImages != null)
            {
                string imageName = Path.GetFileName(pathToImages);
                FileInfo information = new FileInfo(imageName);
                if (MimeMapping.GetMimeMapping(imageName) == allowedFormat)
                {
                    if (information.Length > 0)
                    {
                        //need to be modified
                    }
                    else
                    {
                        status = false;
                    }
                }
                else
                {
                    status = false;
                }
            }
            else
            {
                status = false;
            }

            return status;
        }
    }
}