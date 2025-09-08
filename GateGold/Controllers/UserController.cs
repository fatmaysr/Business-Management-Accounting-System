using System.Linq;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using BusinessLayer.ValidationRules;
using FluentValidation.Results;


namespace GateGold.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        UserManager um = new UserManager(new EfUserDal());

        // 1. Listeleme
        public ActionResult UserList()
        {
            var users = um.GetList();
            return View(users);
        }

        // 2. Ekle - GET
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(User user)
        {
            UserValidator validator = new UserValidator();
            ValidationResult result = validator.Validate(user);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(user);
            }

            um.Add(user);
            return RedirectToAction("UserList");
        }


        // 3. Düzenle - GET
        [HttpGet]
        public ActionResult EditUser(int id)
        {
            var user = um.GetById(id);
            return View(user);
        }

        // 3. Düzenle - POST
        [HttpPost]
        public ActionResult EditUser(User user)
        {
            UserValidator validator = new UserValidator();
            ValidationResult results = validator.Validate(user);

            if (results.IsValid)
            {
                um.Update(user);
                return RedirectToAction("UserList");
            }
            else
            {
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View(user);
        }


        // 4. Sil
        public ActionResult DeleteUser(int id)
        {
            var user = um.GetById(id);
            um.Delete(user);
            return RedirectToAction("UserList");
        }
    }
}
