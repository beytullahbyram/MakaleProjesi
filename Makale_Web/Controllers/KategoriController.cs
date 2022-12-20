using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Makale_BLL;
using Makale_Entities;

namespace Makale_Web.Controllers
{
    public class KategoriController : Controller
    {
        #region DB
        private KategoriYonet ky = new KategoriYonet();
        #endregion

        public ActionResult Index()
        {
            return View(ky.KategorileriListele());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = ky.KategoriBul(id.Value);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        #region CREATE

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,KategoriBaslik,KategoriAciklama,KayıtTarihi,DegistirmeTarihi,DegistirenKullanici")] Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                ky.KategoriEkle(kategori.ID);
                return RedirectToAction("Index");
            }

            return View(kategori);
        }
        #endregion

        #region EDIT

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = ky.KategoriBul(id.Value);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,KategoriBaslik,KategoriAciklama,KayıtTarihi,DegistirmeTarihi,DegistirenKullanici")] Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                ky.KategoriUpdate(kategori);
                return RedirectToAction("Index");
            }
            return View(kategori);
        }
        #endregion

        #region DELETE
        //GET
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = ky.KategoriBul(id.Value);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }


        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kategori kategori = ky.KategoriBul(id);
            ky.KategoriSil(kategori);
            return RedirectToAction("Index");
        }
        #endregion

    }
}
