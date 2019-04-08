
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Haber_Sitesi.Models;

namespace Haber_Sitesi.Controllers
{
   
    public class UyeController : Controller
    {
        habersitesiDB db = new habersitesiDB();
        // GET: Uye
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(Uye uye)
        {
            var login = db.Uye.Where(u => u.KullaniciAdi == uye.KullaniciAdi).SingleOrDefault();
            if (login.KullaniciAdi == uye.KullaniciAdi && login.Sifre == uye.Sifre)
            {
                Session["uyeid"] = login.UyeID;
                Session["kullaniciadi"] = login.KullaniciAdi;
                Session["yetki"] = login.Yetki;
                return RedirectToAction("Index", "Home");
            }
            else { return View(); }
        }
        //public ActionResult BlogArama(string Ara = null)
        //{
        //    var aranan = db.Yazi.Where(y => y.Baslik.Contains(Ara)).ToList();
        //    return View(aranan.OrderByDescending(y => y.Tarih));
        //}
        public ActionResult Logout()
        {
            Session["uyeid"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Uye uye)
        {
            uye.Yetki = 0;
            if (ModelState.IsValid)
            {
                
                db.Uye.Add(uye);
               
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
                return View();
        }


    }
}