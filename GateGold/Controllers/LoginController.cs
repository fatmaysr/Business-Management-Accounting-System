using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GateGold.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password, bool rememberMe = false)
        {
            if (username == "admin" && password == "admin123")
            {
                // rememberMe true ise cookie kalıcı olacak
                FormsAuthentication.SetAuthCookie(username, rememberMe);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Kullanıcı adı veya şifre yanlış";
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
