using Makale_BLL;
using Makale_Entities;
using Makale_Web.Filter;
using System.Net;
using System.Web.Mvc;

namespace Makale_Web.Controllers
{
	public class YorumController : Controller
	{
		private MakaleYonet my = new MakaleYonet();
		private YorumYonet yy = new YorumYonet();

		public ActionResult YorumGoster(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
			}
			Makaleler not = my.MakaleBul(id.Value);

			return PartialView("_PartialPageYorum", not.Yorumlar);
		}
		[Auth]
		[HttpPost]
		public ActionResult Edit(int? id, string text)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Yorum yorum = yy.YorumBul(id.Value);

			if (yorum == null)
			{
				return new HttpNotFoundResult();
			}
			yorum.Text = text;

			if (yy.YorumGuncelle(yorum) > 0)
			{
				return Json(new { sonuc = true }, JsonRequestBehavior.AllowGet);
			}

			return Json(new { sonuc = false }, JsonRequestBehavior.AllowGet);
		}
		[Auth]
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Yorum yorum = yy.YorumBul(id.Value);

			if (yorum == null)
			{
				return new HttpNotFoundResult();
			}

			if (yy.YorumSil(yorum) > 0)
			{
				return Json(new { sonuc = true }, JsonRequestBehavior.AllowGet);
			}

			return Json(new { sonuc = false }, JsonRequestBehavior.AllowGet);
		}
		[Auth]
		[HttpPost]
		public ActionResult Create(Yorum yorum, int? notid)
		{
			ModelState.Remove("DegistirenKullanici");

			if (ModelState.IsValid)
			{
				if (notid == null)
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}

				Makaleler not = my.MakaleBul(notid.Value);

				if (not == null)
				{
					return new HttpNotFoundResult();
				}

				yorum.Makaleler = not;
				yorum.Kullanici = (Kullanıcı)Session["login"];
				int sonuc = yy.YorumEkle(yorum);

				if (sonuc > 0)
				{
					return Json(new { sonuc = true }, JsonRequestBehavior.AllowGet);
				}
			}

			return Json(new { sonuc = false }, JsonRequestBehavior.AllowGet);
		}


	}
}