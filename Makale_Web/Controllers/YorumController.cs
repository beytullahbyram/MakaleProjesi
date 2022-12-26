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
        public ActionResult YorumGoster(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Makaleler makale = my.MakaleBul(id.Value);
            
            return PartialView("_PartialPageYorum",makale.Yorumlar);
        }
    }
}