using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;

namespace Nexus.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            // Zaten girişliyse doğrudan Home'a gönder.
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password, bool rememberMe = false)
        {
            var user = _userService.Login(username, password);

            if (user == null)
            {
                ViewBag.Error = "Kullanıcı adı veya şifre yanlış";
                return View();
            }

            // Rolü de ticket içine gömüyoruz ki [Authorize(Roles = "Admin")] gibi
            // kontroller çalışabilsin (Global.asax.cs zaten bu veriyi okuyor).
            var ticket = new FormsAuthenticationTicket(
                1,
                user.UserName,
                DateTime.Now,
                DateTime.Now.AddMinutes(rememberMe ? 60 * 24 * 30 : 60),
                rememberMe,
                user.Role ?? string.Empty,
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL,
                Expires = rememberMe ? ticket.Expiration : DateTime.MinValue
            };

            Response.Cookies.Add(authCookie);

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult ResetPassword()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(string email, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(newPassword))
            {
                ViewBag.Error = "E-posta ve yeni şifre zorunludur.";
                return View();
            }

            if (newPassword.Length < 6)
            {
                ViewBag.Error = "Şifre en az 6 karakter olmalıdır.";
                return View();
            }

            if (newPassword != confirmPassword)
            {
                ViewBag.Error = "Girdiğiniz şifreler eşleşmiyor.";
                return View();
            }

            var user = _userService.GetByEmail(email);
            if (user == null)
            {
                ViewBag.Error = "Bu e-posta ile eşleşen bir kullanıcı bulunamadı.";
                return View();
            }

            user.Password = newPassword; // UserManager.Update bunu otomatik hash'liyor
            _userService.Update(user);

            ViewBag.Success = "Şifreniz güncellendi. Şimdi yeni şifrenizle giriş yapabilirsiniz.";
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}
