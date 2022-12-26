using Makale_BLL;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Makale_Web.Models
{
    public class CacheHelper
    {
        public static List<Kategori> Kategoriler()
        {
            
            var sonuc=WebCache.Get("Kategori-cache");
            if (sonuc == null)
            {
                KategoriYonet kategoriYonet = new KategoriYonet();
                sonuc = kategoriYonet.KategorileriListele();
                WebCache.Set("Kategori-cache",sonuc,20,true);//true => süreyi öteler, sıfırlar.
            }
            
            return sonuc;
        }



    }
}