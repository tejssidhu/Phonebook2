using Phonebook.Domain.Interfaces.Services;
using System.Web.Mvc;
using System.Web.Security;
using Phonebook.UI.ViewModels;

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


        }
    }
}
