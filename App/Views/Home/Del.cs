using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace App.Views.Home
{
    public class Del : Controller
    {
        // GET api/<controller>
        
        public ActionResult Delete()
        {
            if ((System.IO.File.Exists("~/Content/Images/" + ViewBag.f)))
            {
                try
                {
                    System.IO.File.Delete("~/Content/Images/" + ViewBag.f);
                    ViewBag.f = "";
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Deletion of file failed: " + ex.Message);
                    ViewBag.f = "";
                }
            }
            return View();
        }
    }
}