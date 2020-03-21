using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Gallery.DAL.Models;

namespace Gallery.Controllers
{
    public class AccountController : Controller
    {
        // GET: Register
        public ActionResult Register()
        {
            return View();
        }
        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Register(RegisterModel model)
        {

            return View(model);
        }
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Login(LoginModel model)
        {

            return View(model);
        }
    }
}