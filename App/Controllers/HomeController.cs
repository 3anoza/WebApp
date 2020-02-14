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
using App.Constants;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        // before downloading/deleting the user must be authorized
        [Authorize]
        [HttpPost]
        
        // get a collection of pressed input-buttons and get the value from the desired 
        public ActionResult Index(FormCollection form)
        {
            // get first input-button id where contain full path
            string filePath = form.GetKey(0);
            // start download/delete logging (get full file path)
            ViewBag.checkCorrectPath = filePath;
            MessageBox.Show(ViewBag.checkCorrectPath);
            // check if the file is in the download mode
            if (filePath[filePath.Length - 1] == ConstantProvider.downloadMode)
            {
                // logging mod check result
                ViewBag.WriteLine = filePath.Remove(filePath.Length - 1);
                MessageBox.Show(ViewBag.WriteLine);
                filePath = ViewBag.WriteLine;
                // set response header
                Response.AddHeader(
                "Content-Disposition", "attachment; filename=\"" + Path.GetFileName(filePath) + "\"");
                try
                {
                    // start download
                    Response.WriteFile(filePath);
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
            else
            {
                ViewBag.WriteLine = filePath;
                MessageBox.Show(ViewBag.WriteLine);
                // logging mod check result
                // [here]
                // Before deleting, check if the file exists at the specified path
                if ((System.IO.File.Exists(ViewBag.WriteLine))) //+ ViewBag.WriteLine)))
                {
                    try
                    {
                        // delete file
                        System.IO.File.Delete(ViewBag.WriteLine);// + ViewBag.WriteLine);
                    }
                    catch (Exception error)
                    {
                        // logging exception
                        Debug.WriteLine(ConstantProvider.FILE_DELETION_FAILED + error.Message);
                    }
                }
            }            
            return View();
        }

        public ActionResult About()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult About(HttpPostedFileBase files)
        {
            if (files != null)
            {
                FileInfo info;
                info = new FileInfo(files.InputStream.ToString());
                //***Debug messages*** 
                //*MessageBox.Show(ConfigurationManager.AppSettings.Get("FileType") + files.ContentType + "\n" + ConfigurationManager.AppSettings.Get("FileType").Contains(files.ContentType));
                if (ConfigurationManager.AppSettings.Get("FileType").Contains(files.ContentType))
                {
                    //*MessageBox.Show("Type Correct");
                    // Verify that the user selected a file
                    if (files != null && files.ContentLength > 0)
                    {
                        //*MessageBox.Show("File isn't empty");
                        // extract only the filename
                        var fileName = Path.GetFileName(files.FileName);
                        // store the file inside ~/App_Data/uploads folder
                            var path1 = Path.Combine(Server.MapPath("~/Content/Temp"), fileName);
                        //*MessageBox.Show("File add in temp directory");
                        try
                        {
                                files.SaveAs(path1);
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
                            Picture picture = new Picture();
                            //*MessageBox.Show("Pass: problem in copy protect");
                            if (picture.MD5_(path1,Server.MapPath(ConfigurationManager.AppSettings.Get("ImageDirectory"))) == false)
                            {
                               //*MessageBox.Show("EXIF correct");

                                files.SaveAs(path);
                                try
                                {
                                    System.IO.File.Delete(path1);
                                }
                                catch(Exception error)
                                {
                                    ViewBag.Error = error.Message;
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