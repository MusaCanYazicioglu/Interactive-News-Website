using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Haber_Sitesi.Models;
namespace Haber_Sitesi.Controllers
{
    public class HomeController : Controller
    {
        habersitesiDB db = new habersitesiDB();
        // GET: Home

        public ActionResult Index()
        {
            var yazi = db.Yazi.OrderByDescending(y => y.YaziID).ToList();
            return View(yazi);
        }
        //public ActionResult PuanVer(Puan puan)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        puan.Puan = 5;
        //        puan.YaziID = Convert.ToInt32(Session["yaziid"]);
        //        db.Yazi.Add(puan);
        //        db.SaveChanges();

        //        return RedirectToAction("Index");
        //    }
            
        //    return View(puan);

        //}
        public ActionResult BlogArama(string Ara=null)
        {
            var aranan = db.Yazi.Where(y => y.Baslik.Contains(Ara)).ToList();
            return View(aranan.OrderByDescending(y=>y.Tarih));
        }
        public ActionResult SonYazilar()
        { 
            
            return View(db.Yazi.OrderByDescending(y=>y.YaziID).Take(5));
        }
        public ActionResult KategoriYazi(int id)
        {
            var yazi = db.Yazi.Where(y => y.KategoriID == id).ToList();
            
                return View(yazi);
        }
        public ActionResult YaziDetay(int id)
        {
            var yazi = db.Yazi.Where(y => y.YaziID == id).SingleOrDefault();
            if(yazi==null)
            {
                return HttpNotFound();
            }
            return View(yazi);
        }
        public ActionResult Hakkimda()
        {
            return View();
        }
        public ActionResult KategoriPartial1()
        {
            return View(db.Kategori.ToList());
        }
        public ActionResult KategoriPartial()
        {
            return View(db.Kategori.ToList());
        }
        [HttpPost]
        public JsonResult YorumYap(string yorum,int yaziID)
        {
            var uyeID = Session["uyeid"];
            if(yorum!=null)
            {
                db.Yorum.Add(new Yorum { UyeID = Convert.ToInt32(uyeID), YaziID = yaziID, Metin = yorum, Tarih = DateTime.Now });
                db.SaveChanges();
            }
            return Json(false,JsonRequestBehavior.AllowGet);
        }
        //public JsonResult PuanVer(int puan, int id)
        //{
           
            
        //        db.Puan.Add(new Puan { YaziID = id, Puan = puan });
        //        db.SaveChanges();
            
        //    return Json(false, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult YorumSil(int id)
        {
            var uyeid = Session["uyeid"];
            var yorum = db.Yorum.Where(y => y.YorumID == id).SingleOrDefault();
            var yazi = db.Yazi.Where(y => y.YaziID == yorum.YaziID).SingleOrDefault();
            if(yorum.UyeID==Convert.ToInt32(uyeid))
            {
                db.Yorum.Remove(yorum);
                db.SaveChanges();
                return RedirectToAction("YaziDetay", "Home", new { id = yazi.YaziID });
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}