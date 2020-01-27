using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
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

            if (files != null)
            {
                FileInfo info;
                info = new FileInfo(files.InputStream.ToString());
                
                
                    if (files.ContentType == "image/jpeg")
                    {
                        // Verify that the user selected a file
                        if (files != null && files.ContentLength > 0)
                        {
                            // extract only the filename
                            var fileName = Path.GetFileName(files.FileName);
                        // store the file inside ~/App_Data/uploads folder
                            var path1 = Path.Combine(Server.MapPath("~/Content/Temp"), fileName);
                            try
                            {
                                files.SaveAs(path1);
                            }
                            catch
                            {
                            ViewBag.Error = "Запрещённое действие";
                            }
                            
                            var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                            // ViewBag.Error = path.ToString();
                            DateTime time = DateTime.Now.AddYears(-1);
                            if (info.Directory.LastAccessTime >= time)
                            {
                            Picture picture = new Picture();
                            if (picture.MD5_(path1) == false)
                            {
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