using Makale_BLL;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Makale_Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
            MakaleYonet makaleYonet = new MakaleYonet();
        public ActionResult Index()
        {
            //Test test = new Test();
            //test.InsertTest();
            //test.UpdateTest();  
            //test.InsertComment();   
            return View(makaleYonet.MakaleListele().OrderByDescending(x=>x.DegistirmeTarihi).ToList());
            //return View(makaleYonet.ListeleQueryable().OrderByDescending(x=>x.DegistirmeTarihi).ToList());
        }
        
         public ActionResult Kategori(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            KategoriYonet kategoriYonet = new KategoriYonet();  
            Kategori kategori = kategoriYonet.KategoriBul(id.Value);
            if(kategori == null)
            {
                return HttpNotFound();
            }
            return View("Index",kategori.Makaleler);
        }
        public ActionResult Begenilenler()
        {
            return View("Index",makaleYonet.MakaleListele().OrderByDescending(x=>x.BegeniSayisi).ToList());
        }


        public ActionResult Hakkımızda()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
    }
}