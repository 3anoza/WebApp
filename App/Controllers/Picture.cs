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
        public string[] Stats = new string[8];
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
        public bool MD5_(string filePath)
        {
            bool res = false;
            Bitmap bmp1 = (Bitmap)Bitmap.FromFile(filePath);
            string[] files = Directory.GetFiles(@"C:\Users\tdp4t\source\repos\App\App\Content\Images");
            Bitmap bmp2;
            for (int i = 0; i < files.Length; i++)
            {
                bmp2 = (Bitmap)Bitmap.FromFile(files[i]);
                res = CompareBitmapsFast(bmp1, bmp2);
                bmp2.Dispose();
                return res;
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
        /// Метод обрабатывает метадату изображения
        /// <para>
        /// для работы требует заранее заданую переменную Path
        /// </para>
        /// </summary>
        public void Preview()
        {
            FileInfo info = new FileInfo(Path);
            FileStream fsl = new FileStream(Path, FileMode.Open);

            BitmapSource img = BitmapFrame.Create(fsl);
            BitmapMetadata md = (BitmapMetadata)img.Metadata;
            if (md.Title == null)
            {
                Stats[0] = "Информация отсутсвует";
            }
            else
            {
               Stats[0] = md.Title.ToString();
            }
            if (md.CameraManufacturer == null)
            {
                Stats[1] = "Информация отсутсвует";
            }
            else
            {
               Stats[1] = md.CameraManufacturer.ToString();
            }
            if (md.CameraModel == null)
            {
                Stats[2] = "Информация отсутсвует";
            }
            else
            {
                Stats[2] = md.CameraModel.ToString();
            }
            if (md.DateTaken == null)
            {
                Stats[3] = "Информация отсутсвует";
            }
            else
            {
                Stats[3] = md.DateTaken.ToString();
            }
            Stats[4] = img.PixelWidth.ToString();
            Stats[5] = img.PixelHeight.ToString();
            double KB = (double)info.Length / 1024;
            Stats[6] = Math.Round(KB,2).ToString();
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