using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplicationClient.Models;
using WebApplicationClient.Operations;

namespace WebApplicationClient.Controllers
{
    public class PhoneBookController : Controller
    {
        private readonly ApiOperations _ops;

        public PhoneBookController()
        {
            _ops = new ApiOperations();
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.PhoneBook = await _ops.GetAllPhoneBooks();
            return View();
        }

        public IActionResult Record(int id)
        {
            PhoneBook record = _ops.GetPhoneBook(id);
            if (record == null)
            {
                return Redirect("~/");
            }

            return View(record);
        }

        [HttpGet]
        public IActionResult Add(int? id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRecord(string recordLastName, string recordFirstName, string recordMiddleName, string recordNumberPhone, string recordAddress, string recordDesc)
        {
            PhoneBook newPhoneBook = new PhoneBook
            {
                LastName = recordLastName,
                FirstName = recordFirstName,
                MiddleName = recordMiddleName,
                NumberPhone = recordNumberPhone,
                Address = recordAddress,
                Desc = recordDesc
            };

            if (_ops.AddPhoneBook(newPhoneBook))
            {
                return Redirect("~/");
            }
            return RedirectToAction("Index", "Error", new{ errMsg = "Не удалось добавить запись"});
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            PhoneBook record = _ops.GetPhoneBook(id);
            if (record == null)
            {
                return Redirect("~/");
            }

            return View(record);
        }

        [HttpPost]
        public IActionResult EditRecord(string recordLastName, string recordFirstName, string recordMiddleName, string recordNumberPhone, string recordAddress, string recordDesc, string recordId)
        {
            var tmp = _ops.GetPhoneBook(int.Parse(recordId));
            tmp.LastName = recordLastName;
            tmp.FirstName = recordFirstName;
            tmp.MiddleName = recordMiddleName;
            tmp.NumberPhone = recordNumberPhone;
            tmp.Address = recordAddress;
            tmp.Desc = recordDesc;

            if (_ops.EditPhoneBook(tmp))
            {
                return Redirect($"/PhoneBook/record?id={recordId}");
            }

            return RedirectToAction("Index", "Error", new { errMsg = "Не удалось отредактировать запись" });
        }

        public IActionResult Del(int id)
        {
            PhoneBook record = _ops.GetPhoneBook(id);
            if (record == null)
            {
                return Redirect("~/");
            }

            return View(record);
        }

        [HttpPost]
        public IActionResult DelRecord(int id)
        {
            PhoneBook recorDel = new PhoneBook() { Id = id };

            if (_ops.DelPhoneBook(recorDel))
            {
                return Redirect("~/");
            }

            return RedirectToAction("Index", "Error", new { errMsg = "Не удалось удалить запись" });
        }
    }
}
