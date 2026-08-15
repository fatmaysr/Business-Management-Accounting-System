using System.Web.Mvc;
using GateGold.Helpers;
using GateGold.Models;

namespace GateGold.Controllers
{
    public class SettingsController : Controller
    {
        public ActionResult General()
        {
            var model = SettingsStore.Load();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult General(GeneralSettingsViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            SettingsStore.Save(model);
            TempData["Success"] = "Ayarlar kaydedildi.";
            return RedirectToAction("General");
        }
    }
}