using Makale_DatabaseLayer;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    public class KategoriYonet
    {
        Repository<Kategori> repository_kategoriler= new Repository<Kategori>();
        public List<Kategori> KategorileriListele()
        {
            return repository_kategoriler.List();
        }
        public Kategori KategoriBul(int id)
        {
            return repository_kategoriler.Find(x=>x.ID == id);
        }
            BusinessLayerSonuc<Kategori>sonuc= new BusinessLayerSonuc<Kategori>();  
        public BusinessLayerSonuc<Kategori> KategoriEkle(Kategori kategori)
        {
            sonuc.nesne=repository_kategoriler.Find(x=>x.KategoriBaslik==kategori.KategoriBaslik);
            if (sonuc.nesne != null)
                sonuc.Hatalar.Add("Kategori kayıtlı");
            else
            {
                int kayit=repository_kategoriler.Insert(kategori);
                if(kayit<1)
                    sonuc.Hatalar.Add("kategori kaydedilemedi");
            }
            return sonuc;
        }

        public BusinessLayerSonuc<Kategori> KategoriUpdate(Kategori kategori)
        {
           sonuc.nesne=repository_kategoriler.Find(x=>x.KategoriBaslik==kategori.KategoriBaslik);
            if(sonuc.nesne != null)
            {
                sonuc.nesne.KategoriBaslik=kategori.KategoriBaslik;
                sonuc.nesne.KategoriAciklama=kategori.KategoriAciklama;
                int sonucupdate=repository_kategoriler.Update(sonuc.nesne);
                if(sonucupdate<1)   
                    sonuc.Hatalar.Add("Kategori değiştirilemedi");
            }
            return sonuc;
        }

        public BusinessLayerSonuc<Kategori> KategoriSil(Kategori kategori)
        {
            BusinessLayerSonuc<Kategori>sonuc=new BusinessLayerSonuc<Kategori>();
            sonuc.nesne=repository_kategoriler.Find(x=>x.ID==kategori.ID);

            Repository<Makaleler> rep_makaleler=new Repository<Makaleler>();
            Repository<Yorum> rep_yorum=new Repository<Yorum>();
            Repository<Begeni> rep_begeni=new Repository<Begeni>(); 

            if (sonuc.nesne != null)
            {
                foreach (Makaleler makale in sonuc.nesne.Makaleler.ToList())
                {
                    foreach (Yorum yorum in makale.Yorumlar.ToList())
                    {
                        rep_yorum.Delete(yorum);
                    }
                    foreach (Begeni begeni in makale.Begeniler.ToList())
                    {
                        rep_begeni.Delete(begeni);
                    }
                    rep_makaleler.Delete(makale);
                }

                int silsonuc=repository_kategoriler.Delete(sonuc.nesne);
                if(silsonuc<1)
                    sonuc.Hatalar.Add("kullanıcı silinemedi");
            }

            else if(sonuc.nesne==null)
                sonuc.Hatalar.Add("Kategori bulunamadı");
            return sonuc;
        }
    }
}
