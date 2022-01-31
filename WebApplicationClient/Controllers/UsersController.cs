using Microsoft.AspNetCore.Mvc;
using WebApplicationClient.Models;
using WebApplicationClient.Operations;

namespace WebApplicationClient.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApiOperations _ops;

        public UsersController()
        {
            _ops = new ApiOperations();
        }

        public IActionResult Index()
        {
            return View(_ops.GetAllUsers());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateUserViewModel model)
        {
            User user = _ops.AddUser(model.LoginProp, model.Password);
            if (user != null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public  IActionResult Edit(string id)
        {
            User user = _ops.GetUser(id);
            
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, LoginProp = user.UserName };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _ops.GetUser(model.Id);

                if (user != null)
                {
                    user.UserName = model.LoginProp;

                    if (_ops.EditUser(user))
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            User user = _ops.GetUser(id);
                        
            if (user != null)
            {
                if (_ops.DelUser(user))
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index", "Error", new { errMsg = "Не удалось удалить пользователя" });
        }
    }
}
