using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplicationClient.Models;
using WebApplicationClient.Operations;

namespace WebApplicationClient.Controllers
{
    public class RolesController : Controller
    {
        private readonly ApiOperations _ops;

        public RolesController()
        {
            _ops = new ApiOperations();
        }

        public IActionResult Index()
        {
            return View(_ops.GetAllRoles());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                if (_ops.AddRole(name))
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index", "Error", new { errMsg = "Не удалось добавить роль" });
        }

        public IActionResult Edit(string userId)
        {
            User user = _ops.GetUser(userId);
            if (user != null)
            {
                var userRoles = _ops.RoleUsers(user);
                var allRoles = _ops.GetAllRoles();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    Id = user.Id,
                    LoginProp = user.UserName,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(string userId, List<string> roles)
        {
            User user = _ops.GetUser(userId);
            if (user != null)
            {
                if (_ops.EditRoleUser(user, roles))
                {
                    return RedirectToAction("Index", "Users");
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (_ops.DelRole(id))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Error", new { errMsg = "Не удалось удалить роль" });
        }
    }
}
