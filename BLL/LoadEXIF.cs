using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Services
{
    public class LoadEXIF
    {
         public void EXIF(string pathToImage)
        {
            // Create value, he containted information about current file
            FileInfo info = new FileInfo(pathToImage);
            // Create file stream, he will read data from current file
            FileStream fsl = new FileStream(pathToImage, FileMode.Open);
            // Create value, he containted bitmap frame from file stream 
            BitmapSource img = BitmapFrame.Create(fsl);
            // Create value. he containted all metadata from current bitmap frame. This working only for JPEG files
            BitmapMetadata md = (BitmapMetadata)img.Metadata;
            // check what's the file type we have in this method 
            if (pathToImage.Contains("png"))
            {
                // set all metadata information in unknown status
                Stats[0] = "Информация отсутсвует";
                Stats[1] = "Информация отсутсвует";
                Stats[2] = "Информация отсутсвует";
                Stats[3] = "Информация отсутсвует";
            }
            else
            {
                // check file title from metadata
                if (md.Title == null)
                {
                    Stats[0] = "Информация отсутсвует";
                }
                else
                {
                    Stats[0] = md.Title.ToString();
                }
                // check camera creator from metadata
                if (md.CameraManufacturer == null)
                {
                    Stats[1] = "Информация отсутсвует";
                }
                else
                {
                    Stats[1] = md.CameraManufacturer.ToString();
                }
                // check camera model from metadata
                if (md.CameraModel == null)
                {
                    Stats[2] = "Информация отсутсвует";
                }
                else
                {
                    Stats[2] = md.CameraModel.ToString();
                }
                // check file data maken from metadata
                if (md.DateTaken == null)
                {
                    Stats[3] = "Информация отсутсвует";
                }
                else
                {
                    Stats[3] = md.DateTaken.ToString();
                }
            }
            // save file extension 
            Stats[4] = img.PixelWidth.ToString();
            Stats[5] = img.PixelHeight.ToString();
            // convert file size from byte to Kbyte
            double KB = (double)info.Length / 1024;
            Stats[6] = Math.Round(KB,2).ToString();
            // save filename
            Stats[7] = info.Name.ToString();
            fsl.Close();
        }
    }
}