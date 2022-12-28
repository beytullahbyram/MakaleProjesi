﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Makale_BLL;
using Makale_Entities;
using Makale_Web.Models;

namespace Makale_Web.Controllers
{
    public class MakalelerController : Controller
    {
        MakaleYonet my=new MakaleYonet();
        KategoriYonet ky=new KategoriYonet();
        LikeYonet ly=new LikeYonet();

        // GET: Makaleler
        public ActionResult Index()
        {

            //include join işlemi yapmamızı sağlar
            var makalelers = my.ListeleQueryable().Include(m => m.Kategori);
            if (Session["login"] != null)
            {
                Kullanıcı kullanıcı = (Kullanıcı)Session["login"];
                makalelers= my.ListeleQueryable().Include(m => m.Kategori).Where(x=>x.Kullanici.ID==kullanıcı.ID);

            }
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

        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(CacheHelper.Kategoriler(), "ID", "KategoriBaslik");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Makaleler makaleler)
        {
            Kullanıcı kullanıcı=null;
            if (Session["login"] != null)
            {
                kullanıcı=Session["login"] as Kullanıcı;
            }
            makaleler.Kullanici=kullanıcı;
            ModelState.Remove("DegistirenKullanici");
            if (ModelState.IsValid)
            {
                BusinessLayerSonuc<Makaleler> sonuc=my.MakaleKaydet(makaleler);
                if (sonuc.Hatalar.Count > 0)
                {
                    sonuc.Hatalar.ForEach(x=>ModelState.AddModelError("",x));
                    return View(makaleler);
                }
                return RedirectToAction("Index");
            }
            ViewBag.KategoriId = new SelectList(CacheHelper.Kategoriler(), "ID", "KategoriBaslik", makaleler.KategoriId);
            return View(makaleler);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Makaleler makaleler = my.MakaleBul(id.Value);   
            if (makaleler == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(CacheHelper.Kategoriler(), "ID", "KategoriBaslik", makaleler.KategoriId);
            return View(makaleler);
        }
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Makaleler makaleler)
        {
            ModelState.Remove("DegistirenKullanici");

            ViewBag.KategoriId = new SelectList(CacheHelper.Kategoriler(), "ID", "KategoriBaslik", makaleler.KategoriId);
            if (ModelState.IsValid)
            {
                    BusinessLayerSonuc<Makaleler>sonuc=my.MakaleUpdate(makaleler);
                if (sonuc.Hatalar.Count>0)
                {
                    sonuc.Hatalar.ForEach(h =>ModelState.AddModelError("",h));
                    return View(sonuc);
                }
            }
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
            BusinessLayerSonuc<Makaleler> sonuc=my.MakaleSil(makaleler);
            if (sonuc.Hatalar.Count > 0)
            {
                sonuc.Hatalar.ForEach(h =>ModelState.AddModelError("",h));
                return View(sonuc);
            }
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
                makalelers = ly.ListE().Include("Kullanıcı").Include("Begeni").Where(x => x.Kullanıcı.ID == kullanıcısession.ID).Select(s => s.Makaleler).Include(k => k.Kategori);
            }
            return View("Index",makalelers.ToList());
        }
        [HttpPost]
        public ActionResult Begeni(int[] arr)
        {
            List<int> listlike=new List<int>();
            Kullanıcı kullanıcı=Session["login"] as Kullanıcı;
            
            //x=>begeni tablosu
            //begeni tablosundaki id ile giriş yapan kullanıcının idsi eşleşiyor mu ve gelen dizi begeni tablosundaki idyi içeriyor mu kontrolü yapıldı ve select ile sadece int değerleri list şekilde geri aldık
            if(kullanıcı != null)
            {
                    listlike = ly.BegeniListele(x=>x.Kullanıcı.ID == kullanıcı.ID && arr.Contains(x.Makaleler.ID)).Select(x=>x.Makaleler.ID).ToList();

            }

            return Json(new {sonuc=listlike},JsonRequestBehavior.AllowGet);//sayfaya geri gönderdik çünkü like butonların dolu olup olamayacağına bakacağız

        }


        public ActionResult LikeDuzenle(int notid,bool like)
        {

            return View();  
        }

    }
}
