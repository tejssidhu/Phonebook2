using Phonebook.Domain.Interfaces.Services;
using System.Web.Mvc;

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
            var users = _userService.GetAll();

            return View(users);
        }

    }
}
