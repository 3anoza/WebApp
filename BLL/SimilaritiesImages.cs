using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Drawing;
using System.Configuration;

namespace App.Services
{
    public class SimilaritiesImages
    {
        public string[] Stats = new string[ConstantProvider.exifParamSize];
        public bool CompareImages(string testedImage, string ImagesPath)
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
        public string DownloadImage(string pathToImages)
        {
            // set response header
            Response.AddHeader(
            "Content-Disposition", "attachment; filename=\"" + Path.GetFileName(pathToImages) + "\"");
            try
            {
                // start download
                Response.WriteFile(pathToImages);
                // end download
                Response.End();
            }
            catch (Exception error)
            {
                // logging exception
                Debug.WriteLine(ConstantProvider.FILE_DOWNLOADING_FAILED + error.Message);
            }
            return null;
        }
        public void DeleteImage(string pathToImages)
        {
            if ((System.IO.File.Exists(pathToImages))) //+ ViewBag.WriteLine)))
            {
                try
                {
                    // delete file
                    System.IO.File.Delete(pathToImages);// + ViewBag.WriteLine);
                }
                catch (Exception error)
                {
                    // logging exception
                    Debug.WriteLine(ConstantProvider.FILE_DELETION_FAILED + error.Message);
                }
            }
        }
        public void Upload(string pathToImages)
        {
            if (pathToImages != null)
            {
                FileInfo info;
                info = new FileInfo(pathToImages.InputStream.ToString());
                //***Debug messages*** 
                //*MessageBox.Show(ConfigurationManager.AppSettings.Get("FileType") + files.ContentType + "\n" + ConfigurationManager.AppSettings.Get("FileType").Contains(files.ContentType));
                if (ConfigurationManager.AppSettings.Get("FileType").Contains(pathToImages.ContentType))
                {
                    //*MessageBox.Show("Type Correct");
                    // Verify that the user selected a file
                    if (pathToImages != null && pathToImages.ContentLength > 0)
                    {
                        //*MessageBox.Show("File isn't empty");
                        // extract only the filename
                        var fileName = Path.GetFileName(pathToImages.FileName);
                        // store the file inside ~/App_Data/uploads folder
                        var path1 = Path.Combine(Server.MapPath("~/Content/Temp"), fileName);
                        //*MessageBox.Show("File add in temp directory");
                        try
                        {
                            pathToImages.SaveAs(path1);
                        }
                        catch (Exception error)
                        {
                            ViewBag.Error = error.Message;
                        }
                        var path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings.Get("ImageDirectory")), fileName);
                        //*MessageBox.Show("File add in main directory");
                        DateTime time = DateTime.Now.AddYears(-1);
                        //*MessageBox.Show(time.ToString());
                        //*MessageBox.Show(info.Directory.LastAccessTime.ToString());
                        if (info.Directory.LastAccessTime >= time)
                        {
                            //*MessageBox.Show("Pass: problem in copy protect");
                            if (CompareImages(path1, Server.MapPath(ConfigurationManager.AppSettings.Get("ImageDirectory"))) == false)
                            {
                                //*MessageBox.Show("EXIF correct");

                                pathToImages.SaveAs(path);
                                try
                                {
                                    System.IO.File.Delete(path1);
                                }
                                catch (Exception error)
                                {
                                    ViewBag.Error = error.Message;
                                }
                            }
                            else { ViewBag.Error = "Ошибка: такой файл уже существует"; }
                        }
                        else
                        {
                            ViewBag.Error = "Ошибка: файл создан более чем год назад";
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Ошибка: файл пустой или не выбран";
                    }
                }
                else
                {
                    ViewBag.Error = "Ошибка: формат файла не поддерживаеться";
                }
                // redirect back to the index action to show the form once again
            }
            else
            {
                ViewBag.Error = "Ошибка: файл не выбран";
            }
        }
    }
}