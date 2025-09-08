using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GateGold.Controllers
{
    public class SettingsController : Controller
    {
        public ActionResult General()
        {
            return View();
        }

        public ActionResult Currency()
        {
            return View();
        }

    }
}