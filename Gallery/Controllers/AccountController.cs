using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Gallery.Controllers
{
    public class AccountController : Controller
    {
        protected string[] Mail { get; } = new string[] {"@mail.ru","@mail.ua","@list.ru"};
        // GET: Auth
        public ActionResult Register(FormCollection collection)
        {
            /*string[] value = new string[collection.Count];
            for (int i = 0; i < collection.Count; i++)
            {
                value[i] = collection.Get(i);
            }

            for (int i = 0; i < 3; i++)
            {
                if (value[i].Contains(Mail[i]))
                    break;
            }
            // send email to (LocalDB)
            if (value[1].Contains(value[2]) && value[1].Length == value[2].Length)
            {
                //send password to (LocalDB)
            }*/
            return View();
        }

        public ActionResult _ViewTest()
        {
            return View();
        }
    }
}