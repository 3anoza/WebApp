using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Gallery.BLL.Contracts;
using Gallery.BLL.Interfaces;
using Gallery.DAL.Models;
using Gallery.Filters.MVC;
using Gallery.Interfaces;
using Microsoft.Owin.Security;

namespace Gallery.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        
        public AccountController(IUserService user, IAuthenticationService authentication)
        {
            _userService = user ?? throw new ArgumentNullException(nameof(user));
            _authenticationService = authentication ?? throw new ArgumentNullException(nameof(authentication));
        }
        // GET: Register
        public ActionResult Register()
        {
            return View();
        }
        // POST: Register
        [LogFilter]
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password == model.ConfirmPassword)
                {
                    bool UserExist = await _userService.IsUserExistAsync(model.Email, model.Password);
                    if (!UserExist)
                    {
                        AddUserDto userDto = new AddUserDto(model.Email, model.Password);
                        await _userService.AddUserAsync(userDto);

                        string PersonId = _userService.GetPersonId(model.Email).ToString();
                        _authenticationService.AuthorizeContext(HttpContext.GetOwinContext(),_authenticationService.CreateClaim(PersonId));
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("","User with this Email already exist!");
                    }
                }
            }
            return View(model);
        }
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        // POST: Login
        [LogFilter]
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                bool isUserExist = await _userService.IsUserExistAsync(model.Email, model.Password);
                if (isUserExist)
                {
                    var PersonId = _userService.GetPersonId(model.Email).ToString();
                    _authenticationService.AuthorizeContext(HttpContext.GetOwinContext(), _authenticationService.CreateClaim(PersonId));
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "User is not found");
                }
            }
            return View(model);
        }
        private IAuthenticationManager manager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Logout()
        {
            manager.SignOut();
            return RedirectToAction("Index","Home");
        }
    }
}