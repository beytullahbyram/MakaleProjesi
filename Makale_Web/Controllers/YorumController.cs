using Makale_BLL;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Makale_Web.Controllers
{
    public class YorumController : Controller
    {
        // GET: Yorum
        MakaleYonet my = new MakaleYonet(); 
        YorumYonet yy= new YorumYonet();
        public ActionResult YorumGoster(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Makaleler makale = my.MakaleBul(id.Value);
            
            return PartialView("_PartialPageYorum",makale.Yorumlar);
        }
        [HttpPost]
        public ActionResult Edit(int? id,string text)
        {
            if(id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            
            Yorum bulunanyorum= yy.YorumBul(id.Value);  
            if(bulunanyorum == null)
                return new HttpNotFoundResult();

            bulunanyorum.Text = text;  
            if(yy.YorumGuncelle(bulunanyorum) > 0)
            {
                //AllowGet=> İstemciden gelen HTTP GET isteklerine izin verir 
                return Json(new {sonuc=true},JsonRequestBehavior.AllowGet);
            }
            return Json(new {sonuc=false},JsonRequestBehavior.AllowGet);
        }
    }
}