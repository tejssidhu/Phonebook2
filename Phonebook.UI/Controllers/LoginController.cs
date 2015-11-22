using System;
using System.Web;
using Phonebook.Domain.Interfaces.Services;
using System.Web.Mvc;
using System.Web.Security;
using Phonebook.UI.ViewModels;
using StructureMap.Query;

namespace Phonebook.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            FormsAuthentication.SignOut();

            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            try
            {
                var user = _userService.Authenticate(loginViewModel.Username, loginViewModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("Password", "Username or password not recognised");

                    return View(new LoginViewModel { Username = loginViewModel.Username });
                }

                //setup forms authentication
                var authenticationTicket = new FormsAuthenticationTicket(1,
                    user.Id.ToString(),
                    DateTime.Now,
                    DateTime.Now.AddMinutes(60),
                    false,
                    "");

                var encTicket = FormsAuthentication.Encrypt(authenticationTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(authCookie);

                return RedirectToAction("Index", "Contact", new { UserId = user.Id});
            }
            catch (Exception)
            {
                ModelState.AddModelError("Password", "Username or password not recognised");

                return View(new LoginViewModel { Username = loginViewModel.Username });
            }


        }
    }
}
