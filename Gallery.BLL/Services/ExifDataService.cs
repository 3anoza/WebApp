using System;
using System.IO;
using System.Windows.Media.Imaging;
using Gallery.BLL.Interfaces;
using Gallery.DAL.ProjectProperties;

namespace Gallery.BLL.Services
{
    public class ExifDataService : IExifDataService
    {
        public string[] GetExifDataStrings(string pathToImage)
        {
            Properties prop = new Properties();
            string[] exif = new string[prop.ExifArraySize];
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