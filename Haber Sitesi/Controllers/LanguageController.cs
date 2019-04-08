using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Haber_Sitesi.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Change(String LanguageSelection)
        {
            if (LanguageSelection != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageSelection);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageSelection);
            }

            HttpCookie cookie = new HttpCookie("dil");
            cookie.Value = LanguageSelection;
            Response.Cookies.Add(cookie);
           
            return View("../Home/Hakkimda");
        }
    }
}