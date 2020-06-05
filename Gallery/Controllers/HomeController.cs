using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gallery.Controllers
{
    public class HomeController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {

            return View();
        }
        
        // POST: Index
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            return null;
        }

        // GET: Upload
        [Authorize]
        [HttpPost]
        public ActionResult Upload()
        {
            return View();
        }

        // POST: Upload
        public ActionResult Upload(FormCollection collection)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}