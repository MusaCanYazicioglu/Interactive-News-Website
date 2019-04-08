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
    public class UyeYaziController : Controller
    {
        habersitesiDB db = new habersitesiDB();

        // GET: UyeYazi
        public ActionResult Index()
        {
            var yazilar = db.Yazi.ToList();
            return View(yazilar);
           
        }

        // GET: UyeYazi/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UyeYazi/Create
        public ActionResult Create()
        {
            ViewBag.KategoriID = new SelectList(db.Kategori, "KategoriID", "KategoriAdi");
            return View();
        }

        // POST: UyeYazi/Create
        [HttpPost]
        public ActionResult Create(Yazi yazilar, HttpPostedFileBase Foto)
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


        // GET: UyeYazi/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UyeYazi/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UyeYazi/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UyeYazi/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
