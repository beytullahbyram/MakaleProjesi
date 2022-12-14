using Makale_BLL;
using Makale_Common;
using Makale_Entities;
using Makale_Entities.View_modal;
using Makale_Web.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Makale_Web.Controllers
{
	[Exc]
	public class HomeController : Controller
	{
		// GET: Home
		private MakaleYonet makaleYonet = new MakaleYonet();

		private KullaniciYonet ky = new KullaniciYonet();

		public ActionResult Index()
		{
			int xxx = 1;
			int yyy=xxx/0;
			//return View(makaleYonet.MakaleListele().OrderByDescending(x=>x.DegistirmeTarihi).ToList());
			return View(makaleYonet.ListeleQueryable().Where(x=>x.TaslakDurumu == false).OrderByDescending(x => x.DegistirmeTarihi).ToList());
		}

		public ActionResult Kategori(int? id)
		{
			if (id == null)
				return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
			List<Makaleler>makale=makaleYonet.ListeleQueryable().Where(x=>x.TaslakDurumu==false && x.Kategori.ID == id).OrderByDescending(x=> x.DegistirmeTarihi).ToList();	
			return View("Index",makale);
		}

		public ActionResult Begenilenler()
		{
			return View("Index", makaleYonet.MakaleListele().OrderByDescending(x => x.BegeniSayisi).ToList());
		}

		public ActionResult Hakkımızda()
		{
			return View();
		}

		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(Login_Modal modal)
		{
			


			if (ModelState.IsValid)
			{
				BusinessLayerSonuc<Kullanıcı> sonuc = ky.LoginKontrol(modal);
				if (sonuc.Hatalar.Count > 0)
				{
					sonuc.Hatalar.ForEach(x => ModelState.AddModelError("", x));
					return View(modal);
				}

				Session["login"] = sonuc.nesne; //Session da login olan kullanıcı bilgileri tutuldu.
				Uygulama.kullanidiad=sonuc.nesne.KullaniciAdi;//uygulama classındaki default bilgiyi değiştirdik
				return RedirectToAction("Index");//Login olduğu için indexe yönlendirildi.
			}

			return View(modal);
		}

		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Register(Register_Modal modal)
		{
			Uygulama.kullanidiad = modal.KullaniciAdi;
			if (ModelState.IsValid)
			{
				BusinessLayerSonuc<Kullanıcı> sonuc = ky.Kaydet(modal);
				if (sonuc.Hatalar.Count > 0)
				{
					sonuc.Hatalar.ForEach(x => ModelState.AddModelError("", x));
					return View(modal);
				}
				return RedirectToAction("Login");
			}

			return View(modal);
		}

		public ActionResult UserActivate(Guid id)
		{
			BusinessLayerSonuc<Kullanıcı> sonuc = ky.kullanıcıbul(id);
			if (sonuc.Hatalar.Count > 0)
			{
				TempData["aktifsonuc"] = sonuc.Hatalar;
				return RedirectToAction("UserActivateError");
			}
			else
				TempData["aktifsonuc"] = "Kullanıcı aktifleştirildi";
			return View();
		}

		public ActionResult UserActivateError()
		{
			List<string> hatalar = null;
			if (TempData["aktifsonuc"] != null)
				hatalar = (List<string>)TempData["aktifsonuc"];
			return View(hatalar);
		}
		
		public ActionResult Logout()
		{
			Session.Clear();
			return RedirectToAction("Index");
		}

		public ActionResult KayitBasarili()
		{
			return RedirectToAction("Index");
		}
		[Auth]
		public ActionResult ProfilGoster()
		{
			Kullanıcı kullanıcı = (Kullanıcı)Session["login"];
			return View(kullanıcı);
		}
		[Auth]
		public ActionResult ProfilDegistir()
		{
			Kullanıcı kullanıcı = (Kullanıcı)Session["login"];
			return View(kullanıcı);
		}
		[Auth]
		[HttpPost]
		public ActionResult ProfilDegistir(Kullanıcı model, HttpPostedFile profilresmi)
		{
			Uygulama.kullanidiad = model.KullaniciAdi;
			ModelState.Remove("DegistirenKullanici");
			if (ModelState.IsValid)
			{
				if (profilresmi != null)
				{
					string dosyaadi = $"user_{model.ID}.{profilresmi.ContentType.Split('/')[1]}";
					profilresmi.SaveAs(Server.MapPath($"~/image/{dosyaadi}"));
					model.ProfilResmi = dosyaadi;
				}
				BusinessLayerSonuc<Kullanıcı> sonuc = ky.KullaniciUpdate(model);
				if (sonuc.Hatalar.Count > 0)
				{
					sonuc.Hatalar.ForEach(h => ModelState.AddModelError("", h));
					return View(sonuc.nesne);
				}
			}
			return View(model);
		}

		[HttpPost]
		public ActionResult ProfilSil(Kullanıcı model, HttpPostedFileBase profilresmi)
		{
			if (ModelState.IsValid)
			{
				if (profilresmi != null)
				{
					string dosyaadi = $"user_{model.ID}.{profilresmi.ContentType.Split('/')[1]}";
					profilresmi.SaveAs(Server.MapPath($"~/image/{dosyaadi}"));
					model.ProfilResmi = dosyaadi;
				}
				BusinessLayerSonuc<Kullanıcı> sonuc = ky.KullaniciUpdate(model);
				if (sonuc.Hatalar.Count > 0)
				{
					sonuc.Hatalar.ForEach(h => ModelState.AddModelError("", h));
					return View(sonuc.nesne);
				}
				return RedirectToAction("ProfilGoster");
			}
			return View(model);
		}

		public ActionResult ProfilSil()
		{
            Kullanıcı kullanici = Session["login"] as Kullanıcı;

            BusinessLayerSonuc<Kullanıcı> sonuc = ky.KullaniciSil(kullanici.ID);

            if(sonuc.Hatalar.Count > 0)
            {
                //Hatalar ekranda gösterilir. Profilsil ekranı oluşturabilirsiniz.
                sonuc.Hatalar.ForEach(x => ModelState.AddModelError("", x));
                return RedirectToAction("ProfilGoster",sonuc.nesne);
            }

            Session.Clear();
            return RedirectToAction("Index");
		}
		public ActionResult ErrorPage()
        {
			return View();	
        }
	}
}