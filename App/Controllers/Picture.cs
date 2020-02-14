using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Windows.Media.Imaging;
using App.Constants;

namespace App.Controllers
{
    public class Picture
    {
        /// <summary>
        /// Получает или задает путь к файлам сервера
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Получает или задает хеш-сумму для файла
        /// </summary>
        public string Hash { get; set; }
        /// <summary>
        /// Переопределяет индекс файла который используется в Index.cshtml или _Layout.cshtml 
        /// </summary>
        public int Index { get; set; }
        public string[] Stats = new string[ConstantProvider.exifParamSize];
        private void Imgs(object sender, EventArgs e)
        {
            //...
        }
        public void Download()
        {
            //...
        }
        public void Upload()
        {
            //...
        }
        public bool MD5_(string testedImage, string ImagesPath)
        {
            bool res;
            
            Bitmap bmp1 = (Bitmap)Bitmap.FromFile(testedImage);
            string[] files = Directory.GetFiles(ImagesPath);
            Bitmap bmp2;
            for (int i = 0; i < files.Length;)
            {
                bmp2 = (Bitmap)Bitmap.FromFile(files[i]);
                res = CompareBitmapsFast(bmp1, bmp2);
                bmp2.Dispose();
                if (!res)
                    return true;
                //Console.WriteLine(string.Format("CompareBitmapsFast Time: {0} ms", sw.ElapsedMilliseconds));

            }
            bmp1.Dispose();
            return false;
            //...
        }

        public static bool CompareBitmapsFast(Bitmap bmp1, Bitmap bmp2)
        {
            if (bmp1 == null || bmp2 == null)
                return false;
            if (object.Equals(bmp1, bmp2))
                return true;
            if (!bmp1.Size.Equals(bmp2.Size) || !bmp1.PixelFormat.Equals(bmp2.PixelFormat))
                return false;

            int bytes = bmp1.Width * bmp1.Height * (Image.GetPixelFormatSize(bmp1.PixelFormat) / 8);

            bool result = true;
            byte[] b1bytes = new byte[bytes];
            byte[] b2bytes = new byte[bytes];

            BitmapData bitmapData1 = bmp1.LockBits(new Rectangle(0, 0, bmp1.Width, bmp1.Height), ImageLockMode.ReadOnly, bmp1.PixelFormat);
            BitmapData bitmapData2 = bmp2.LockBits(new Rectangle(0, 0, bmp2.Width, bmp2.Height), ImageLockMode.ReadOnly, bmp2.PixelFormat);

            Marshal.Copy(bitmapData1.Scan0, b1bytes, 0, bytes);
            Marshal.Copy(bitmapData2.Scan0, b2bytes, 0, bytes);

            for (int n = 0; n <= bytes - 1; n++)
            {
                if (b1bytes[n] != b2bytes[n])
                {
                    result = false;
                    break;
                }
            }

            bmp1.UnlockBits(bitmapData1);
            bmp2.UnlockBits(bitmapData2);

            return result;
        }

        /// <summary>
        /// writes EXIF ​​date of image from specified path (<paramref name="pathToImage"/>) 
        /// </summary>
        public void Preview(string pathToImage)
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
        /// <summary>
        /// Метод удаления JPEG файла из директории сервера
        /// </summary>   
        public void Delete()
        {
            Path = "~/Content/Images/" + Path;
            System.IO.File.Delete(Path);
            //...
        }

    }
}