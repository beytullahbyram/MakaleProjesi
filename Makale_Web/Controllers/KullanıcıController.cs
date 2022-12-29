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
using Makale_Web.Filter;

namespace Makale_Web.Controllers
{
    [Auth][AuthAdmin]
    public class KullanıcıController : Controller
    {
        KullaniciYonet ky = new KullaniciYonet();

        // GET: Kullanıcı
        public ActionResult Index()
        {
            return View(ky.Listele());
        }

        // GET: Kullanıcı/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanıcı kullanıcı = ky.KullaniciBul(id.Value);
            if (kullanıcı == null)
            {
                return HttpNotFound();
            }
            return View(kullanıcı);
        }

        // GET: Kullanıcı/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kullanıcı/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kullanıcı kullanıcı)
        {
            //modeldeki field kaldırmamızı sağlar
            ModelState.Remove("DegistirenKullanici");
            if (ModelState.IsValid)
            {
                BusinessLayerSonuc<Kullanıcı>sonuc=ky.KullaniciKaydet(kullanıcı);
                if (sonuc.Hatalar.Count > 0 ) 
                {
                    sonuc.Hatalar.ForEach(x=>ModelState.AddModelError("",x));
                    return View(kullanıcı);
                }
                return RedirectToAction("Index");
            }
            return View(kullanıcı);
        }

        // GET: Kullanıcı/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanıcı kullanıcı = ky.KullaniciBul(id.Value);
            if (kullanıcı == null)
            {
                return HttpNotFound();
            }
            return View(kullanıcı);
        }

        // POST: Kullanıcı/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Kullanıcı kullanıcı)
        {
            if (ModelState.IsValid)
            {
                ky.KullaniciUpdate(kullanıcı);
                return RedirectToAction("Index");
            }
            return View(kullanıcı);
        }

        // GET: Kullanıcı/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanıcı kullanıcı =ky.KullaniciBul(id.Value);
            if (kullanıcı == null)
            {
                return HttpNotFound();
            }
            return View(kullanıcı);
        }

        // POST: Kullanıcı/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ky.KullaniciSil(id);
            return RedirectToAction("Index");
        }


    }
}
