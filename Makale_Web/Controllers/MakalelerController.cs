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
    public class MakalelerController : Controller
    {
        MakaleYonet my=new MakaleYonet();

        // GET: Makaleler
        public ActionResult Index()
        {
            Kullanıcı kullanıcı=Session["login"] as Kullanıcı;

            //include join işlemi yapmamızı sağlar
            var makalelers = my.ListeleQueryable().Include(m => m.Kategori);
            if (kullanıcı != null)
                makalelers= my.ListeleQueryable().Include(m => m.Kategori).Where(x=>x.Kullanici.ID==kullanıcı.ID);
            return View(makalelers.ToList());
        }

        // GET: Makaleler/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Makaleler makaleler = my.MakaleBul(id.Value);
            if (makaleler == null)
            {
                return HttpNotFound();
            }
            return View(makaleler);
        }

        KategoriYonet ky=new KategoriYonet();
        // GET: Makaleler/Create
        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(ky.KategorileriListele(), "ID", "KategoriBaslik");
            return View();
        }

        // POST: Makaleler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Makaleler makaleler)
        {
            if (ModelState.IsValid)
            {
                my.MakaleKaydet(makaleler);
                return RedirectToAction("Index");
            }
            ViewBag.KategoriId = new SelectList(ky.KategorileriListele(), "ID", "KategoriBaslik", makaleler.KategoriId);
            return View(makaleler);
        }

        // GET: Makaleler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Makaleler makaleler = my.MakaleBul(id.Value);   
            if (makaleler == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(ky.KategorileriListele(), "ID", "KategoriBaslik", makaleler.KategoriId);
            return View(makaleler);
        }
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Makaleler makaleler)
        {
            if (ModelState.IsValid)
            {
                my.MakaleUpdate(makaleler);
                return RedirectToAction("Index");
            }
            ViewBag.KategoriId = new SelectList(ky.KategorileriListele(), "ID", "KategoriBaslik", makaleler.KategoriId);
            return View(makaleler);
        }
        public ActionResult Delete(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Makaleler makaleler = my.MakaleBul(id.Value);
            if (makaleler == null)
            {
                return HttpNotFound();
            }
            return View(makaleler);
        }

        // POST: Makaleler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Makaleler makaleler = my.MakaleBul(id);
            my.MakaleSil(makaleler);
            return RedirectToAction("Index");
        }



        public ActionResult Begendiklerim()
        {
            LikeYonet ly=new LikeYonet();
            var makalelers = my.ListeleQueryable().Include(m => m.Kategori);
            if (Session["login"] != null)
            {
                Kullanıcı kullanıcısession=Session["login"] as Kullanıcı;
                makalelers= ly.ListE().Include("Kullanıcı").Include("Begeni").Where( x=>x.Kullanıcı.ID==kullanıcısession.ID).Select(s=>s.Makaleler).Include(k=>k.Kategori);
            }
            return View("Index",makalelers.ToList());
        }

    }
}
