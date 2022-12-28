using Makale_BLL;
using Makale_Entities;
using Makale_Web.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Makale_Web.Controllers
{
	public class MakalelerController : Controller
	{
		private MakaleYonet my = new MakaleYonet();
		private KategoriYonet ky = new KategoriYonet();
		private LikeYonet ly = new LikeYonet();
		private YorumYonet yy = new YorumYonet();

		// GET: Makaleler
		public ActionResult Index()
		{
			//include join işlemi yapmamızı sağlar
			var makalelers = my.ListeleQueryable().Include(m => m.Kategori);
			if (Session["login"] != null)
			{
				Kullanıcı kullanıcı = (Kullanıcı)Session["login"];
				makalelers = my.ListeleQueryable().Include(m => m.Kategori).Where(x => x.Kullanici.ID == kullanıcı.ID);
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
		public ActionResult Create(Makaleler makaleler)
		{
			Kullanıcı kullanıcı = null;
			if (Session["login"] != null)
			{
				kullanıcı = Session["login"] as Kullanıcı;
			}
			makaleler.Kullanici = kullanıcı;
			ModelState.Remove("DegistirenKullanici");
			if (ModelState.IsValid)
			{
				BusinessLayerSonuc<Makaleler> sonuc = my.MakaleKaydet(makaleler);
				if (sonuc.Hatalar.Count > 0)
				{
					sonuc.Hatalar.ForEach(x => ModelState.AddModelError("", x));
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
				BusinessLayerSonuc<Makaleler> sonuc = my.MakaleUpdate(makaleler);
				if (sonuc.Hatalar.Count > 0)
				{
					sonuc.Hatalar.ForEach(h => ModelState.AddModelError("", h));
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
			BusinessLayerSonuc<Makaleler> sonuc = my.MakaleSil(makaleler);
			if (sonuc.Hatalar.Count > 0)
			{
				sonuc.Hatalar.ForEach(h => ModelState.AddModelError("", h));
				return View(sonuc);
			}
			my.MakaleSil(makaleler);
			return RedirectToAction("Index");
		}

		public ActionResult Begendiklerim()
		{
			LikeYonet ly = new LikeYonet();
			var makalelers = my.ListeleQueryable().Include(m => m.Kategori);
			if (Session["login"] != null)
			{
				Kullanıcı kullanıcısession = Session["login"] as Kullanıcı;
				makalelers = ly.ListE().Include("Kullanıcı").Include("Begeni").Where(x => x.Kullanıcı.ID == kullanıcısession.ID).Select(s => s.Makaleler).Include(k => k.Kategori);
			}
			return View("Index", makalelers.ToList());
		}

		[HttpPost]
		public ActionResult Begeni(int[] arr)
		{
			List<int> listlike = new List<int>();
			Kullanıcı kullanıcı = Session["login"] as Kullanıcı;

			//x=>begeni tablosu
			//begeni tablosundaki id ile giriş yapan kullanıcının idsi eşleşiyor mu ve gelen dizi begeni tablosundaki idyi içeriyor mu kontrolü yapıldı ve select ile sadece int değerleri list şekilde geri aldık
			if (kullanıcı != null)
			{
				listlike = ly.BegeniListele(x => x.Kullanıcı.ID == kullanıcı.ID && arr.Contains(x.Makaleler.ID)).Select(x => x.Makaleler.ID).ToList();
			}

			return Json(new { sonuc = listlike }, JsonRequestBehavior.AllowGet);//sayfaya geri gönderdik çünkü like butonların dolu olup olamayacağına bakacağız
		}
		[HttpPost]
		public ActionResult LikeDuzenle(int notid, bool like)
		{
			int res = 0;
			Kullanıcı kullanıcı = Session["login"] as Kullanıcı;
			Makaleler makale = my.MakaleBul(notid);

			Begeni begeni = ly.BegeniBul(notid, kullanıcı.ID);//makale ve kullanıcısı belli olan beğeniye ulaştık

			if (begeni != null && like == false)//beğeni var ise kaldırılma işlemi gerçekleştiriliyor
			{
				res = ly.BegeniSil(begeni);
			}
			else if (begeni == null && like == true)//beğeni yok ise ekleme işlemi gerçekleştiriliyor
			{
				res = ly.BegeniEkle(new Begeni()
				{
					Kullanıcı = kullanıcı,
					Makaleler = makale,
				});
			}
			if (res > 0)
			{
				if (like)
					makale.BegeniSayisi++;
				else
					makale.BegeniSayisi--;
				BusinessLayerSonuc<Makaleler> makaleUpdate = my.MakaleUpdate(makale);//Beğeni sayısını güncelliyoruz
				if (makaleUpdate.Hatalar.Count > 0)
					return Json(new { hata = false, res = makale.BegeniSayisi });
			}
			return Json(new { hata = true, res = makale.BegeniSayisi });

		}
	}
}