using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Haber_Sitesi.Models;
using System.Web.Helpers;
using System.IO;

namespace Haber_Sitesi.Controllers
{
    public class AdminYaziController : Controller
    {
        habersitesiDB db = new habersitesiDB();
        // GET: AdminYazi
        public ActionResult Index()
        {
            var yazilar = db.Yazi.ToList();
            return View(yazilar);
        }

        // GET: AdminYazi/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminYazi/Create
        public ActionResult Create()
        {

            ViewBag.KategoriID = new SelectList(db.Kategori, "KategoriID", "KategoriAdi");
            return View();
        }

        // POST: AdminYazi/Create
        [HttpPost]
        public ActionResult Create(Yazi yazilar,HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                if (Foto != null)
                {
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(800, 350);
                    img.Save("~/Uploads/YaziFoto/" + newfoto);
                    yazilar.Foto = "/Uploads/YaziFoto/" + newfoto;


                }

                yazilar.UyeID = Convert.ToInt32(Session["uyeid"]);
                db.Yazi.Add(yazilar);
                db.SaveChanges();

                return RedirectToAction("Index");

            }
                return View(yazilar);
            
        }

        // GET: AdminYazi/Edit/5
        public ActionResult Edit(int id)
        {
            var yazi = db.Yazi.Where(y => y.YaziID == id).SingleOrDefault();
            if(yazi==null)
            {
                return HttpNotFound();

            }
            ViewBag.KategoriID = new SelectList(db.Kategori, "KategoriID", "KategoriAdi",yazi.KategoriID);
            return View(yazi);
        }

        // POST: AdminYazi/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, HttpPostedFileBase Foto,Yazi yazilar)
        {
            try
            {
                var yazilar1 = db.Yazi.Where(y => y.YaziID == id).SingleOrDefault();
                if (Foto != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(yazilar1.Foto)))
                    {
                        System.IO.File.Delete(Server.MapPath(yazilar1.Foto));
                    }

                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(800, 350);
                    img.Save("~/Uploads/YaziFoto/" + newfoto);
                    yazilar1.Foto = "/Uploads/YaziFoto/" + newfoto;
                    yazilar1.Baslik = yazilar.Baslik;
                        yazilar1.Metin = yazilar.Metin;
                        yazilar1.KategoriID = yazilar.KategoriID;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {

                return View(yazilar);
            }
        }

        // GET: AdminYazi/Delete/5
        public ActionResult Delete(int id)
        {
            var yazi = db.Yazi.Where(y => y.YaziID == id).SingleOrDefault();
            if(yazi==null)
            {
                return HttpNotFound();
            }
            return View(yazi);
        }

        // POST: AdminYazi/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var yazi = db.Yazi.Where(y => y.YaziID == id).SingleOrDefault();
                if (yazi == null)
                {
                    return HttpNotFound();
                }
                if (System.IO.File.Exists(Server.MapPath(yazi.Foto)))
                {
                    System.IO.File.Delete(Server.MapPath(yazi.Foto));
                }
                foreach(var i in yazi.Yorum.ToList())
                {
                    db.Yorum.Remove(i);
                }
                db.Yazi.Remove(yazi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
