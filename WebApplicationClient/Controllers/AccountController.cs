using Microsoft.AspNetCore.Mvc;
using WebApplicationClient.Models;
using WebApplicationClient.Operations;

namespace WebApplicationClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApiOperations _ops;

        public AccountController()
        {
            _ops = new ApiOperations();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User result = _ops.RegisterUser(model.LoginProp, model.Password);

                if (result != null)
                {
                    return RedirectToAction("Index", "PhoneBook");
                }
                ModelState.AddModelError("", "Не удалось зарегистрироваться");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _ops.AuthenticateUser(model.LoginProp, model.Password);
                if (user != null)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    Globals.LoggedInUser = user;
                    Globals.RoleUser = _ops.RoleUsers(user);

                    return RedirectToAction("Index", "PhoneBook");
                }

                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            Globals.LoggedInUser = null;
            Globals.RoleUser = null;
            return RedirectToAction("Index", "PhoneBook");
        }

        public IActionResult AccessDenied(string returnUrl = null)
        {
            return View();
        }
    }
}
