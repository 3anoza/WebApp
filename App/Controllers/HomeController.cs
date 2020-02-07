using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using System.Windows.Media.Imaging;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        //Picture picture = new Picture();
        [HttpGet]
        public ActionResult Index()
        {

            //string[] files = Directory.GetFiles("/Content/Images/");
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string LOL = form.GetKey(0);
            ViewBag.WriteLine = LOL;
            if (LOL[LOL.Length - 1] == '1')
            {
                ViewBag.WriteLine = LOL.Remove(LOL.Length - 1);
                LOL = ViewBag.WriteLine;
                Response.AddHeader(
                "Content-Disposition", "attachment; filename=\"" + LOL.Remove(0,51) + "\"");
                Response.WriteFile(LOL);
                Response.End();
                return null;
            }
            else
            {
                if ((System.IO.File.Exists(ViewBag.WriteLine))) //+ ViewBag.WriteLine)))
                {
                    try
                    {
                        System.IO.File.Delete(ViewBag.WriteLine);// + ViewBag.WriteLine);
                        ViewBag.f = "1";
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Deletion of file failed: " + ex.Message);
                        ViewBag.f = "2";
                    }
                }
            }
            
            //string[] files = Directory.GetFiles("/Content/Images/");
            return View();
        }

        public ActionResult About()
        {
            
            return View();
        }

        /*
         ViewBag.fSize
         ViewBag.sizex
         ViewBag.sizey
         ViewBag.name
         ViewBag.ExifVer
         ViewBag.camBrand
         ViewBag.camModel
         ViewBag.createTime
         ViewBag.timeNow
         */
        [HttpPost]
        public ActionResult About(HttpPostedFileBase files)
        {
            //ConfigurationManager.AppSettings["FileType"].Contains(file.format);
            if (files != null)
            {
                FileInfo info;
                info = new FileInfo(files.InputStream.ToString());

              MessageBox.Show(ConfigurationManager.AppSettings.Get("FileType") + files.ContentType + "\n" + ConfigurationManager.AppSettings.Get("FileType").Contains(files.ContentType));
                    if (ConfigurationManager.AppSettings.Get("FileType").Contains(files.ContentType))
                    {
                    MessageBox.Show("Type Correct");
                    // Verify that the user selected a file
                    if (files != null && files.ContentLength > 0)
                        {
                        MessageBox.Show("File isn't empty");
                        // extract only the filename
                        var fileName = Path.GetFileName(files.FileName);
                        // store the file inside ~/App_Data/uploads folder
                            var path1 = Path.Combine(Server.MapPath("~/Content/Temp"), fileName);
                    MessageBox.Show("File add in temp directory");
                        try
                        {
                                files.SaveAs(path1);
                            }
                            catch
                            {
                            ViewBag.Error = "Запрещённое действие";
                            }
                            
                            var path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings.Get("ImageDirectory")), fileName);
                    MessageBox.Show("File add in main directory");
                        // ViewBag.Error = path.ToString();
                        DateTime time = DateTime.Now.AddYears(-1);
                            if (info.Directory.LastAccessTime >= time)
                            {

                            Picture picture = new Picture();
                            if (picture.MD5_(path1,Server.MapPath(ConfigurationManager.AppSettings.Get("ImageDirectory"))) == false)
                            {
                                MessageBox.Show("EXIF correct");

                                files.SaveAs(path);
                                try
                                {
                                    System.IO.File.Delete(path1);
                                }
                                catch
                                {
                                    ViewBag.Error = "Запрещённое действие";
                                }
                                
                                
                            }
                            else { ViewBag.Error = "Ошибка: такой файл уже существует"; }
                            }
                            else
                            {
                                ViewBag.Error = "Ошибка: файл создан более чем год назад";
                                return View();
                            }
                           
                            
                           
                        }
                        else
                        {
                            ViewBag.Error = "Ошибка: файл пустой или не выбран";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Ошибка: формат файла не поддерживаеться";
                        return View();
                    }
                    // redirect back to the index action to show the form once again
               
            }
            else
            {
                ViewBag.Error = "Ошибка: файл не выбран";
                return View();
            }
            
            return RedirectToAction("About");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}