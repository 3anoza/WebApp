using System;
using System.Configuration;
using System.Web;
using System.IO;
using System.Windows.Media.Imaging;
using Gallery.DAL;

namespace Gallery.BLL
{
    public class SimilaritiesImages
    {
        public ConstantProvider Constants { get; } = new ConstantProvider();
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
                if (MimeMapping.GetMimeMapping(imageName) == ConfigurationManager.AppSettings.Get("FileType"))
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

        public string[] GetExifDataStrings(string pathToImage)
        {
            
            string[] exif = new string[Constants.InfoSize];
            FileInfo info = new FileInfo(pathToImage);
            using (FileStream fileStream = new FileStream(pathToImage, FileMode.Open))
            {
                BitmapSource img = BitmapFrame.Create(fileStream);
                BitmapMetadata metadata = (BitmapMetadata)img.Metadata;
                if (pathToImage.Contains("png"))
                {
                    for (byte i = 0; i < 4; i++)
                    {
                        exif[i] = "Empty";
                    }
                }
                else
                {
                    if (metadata.Title == null)
                    {
                        exif[0] = "Empty";
                    }
                    else
                    {
                        exif[0] = metadata.Title.ToString();
                    }
                    // check camera creator from metadata
                    if (metadata.CameraManufacturer == null)
                    {
                        exif[1] = "Empty";
                    }
                    else
                    {
                        exif[1] = metadata.CameraManufacturer.ToString();
                    }
                    // check camera model from metadata
                    if (metadata.CameraModel == null)
                    {
                        exif[2] = "Empty";
                    }
                    else
                    {
                        exif[2] = metadata.CameraModel.ToString();
                    }
                    // check file data maken from metadata
                    if (metadata.DateTaken == null)
                    {
                        exif[3] = "Empty";
                    }
                    else
                    {
                        exif[3] = metadata.DateTaken.ToString();
                    }
                }
                exif[4] = img.PixelWidth.ToString();
                exif[5] = img.PixelHeight.ToString();
                // convert file size from byte to Kbyte
                double KB = (double)info.Length / 1024;
                exif[6] = Math.Round(KB, 2).ToString();
                // save filename
                exif[7] = info.Name.ToString();
            }

            return exif;
        }
    }
}