using Makale_DatabaseLayer;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    public class MakaleYonet
    {
        Repository<Makaleler> repository_makaleler=new Repository<Makaleler>(); 
        BusinessLayerSonuc<Makaleler> businessLayerSonuc=new BusinessLayerSonuc<Makaleler>();
        public List<Makaleler> MakaleListele()
        {
           return repository_makaleler.List();
        }
        public IQueryable<Makaleler> ListeleQueryable()
        {
            return repository_makaleler.ListE();//bellekte veri oluşmaz
        }

        public Makaleler MakaleBul(int value)
        {
            return repository_makaleler.Find(x=>x.ID==value);
        }

        public BusinessLayerSonuc<Makaleler> MakaleKaydet(Makaleler makaleler)
        {
            businessLayerSonuc.nesne=repository_makaleler.Find(x=>x.Baslik == makaleler.Baslik && x.KategoriId==makaleler.KategoriId);
            if(businessLayerSonuc.nesne != null)
                businessLayerSonuc.Hatalar.Add("Bu kategoride bu isimli makale kayıtlıdır");
            else
            {  
                int sonuc =repository_makaleler.Insert(makaleler);
                if(sonuc<1)
                    businessLayerSonuc.Hatalar.Add("Eklenemedi");
            }
            return businessLayerSonuc;
            
        }

        public BusinessLayerSonuc<Makaleler> MakaleUpdate(Makaleler makaleler)
        {
            businessLayerSonuc.nesne= repository_makaleler.Find(x=>x.ID==makaleler.ID);
            if(businessLayerSonuc.nesne!= null)
            {
                businessLayerSonuc.nesne.Baslik=makaleler.Baslik;
                businessLayerSonuc.nesne.Text=makaleler.Baslik;
                businessLayerSonuc.nesne.TaslakDurumu=makaleler.TaslakDurumu;
                businessLayerSonuc.nesne.KategoriId=makaleler.KategoriId;
                if(repository_makaleler.Update(makaleler) < 1)
                {
                    businessLayerSonuc.Hatalar.Add("Güncellenemedi");
                }
            }
            return businessLayerSonuc;
        }

        public BusinessLayerSonuc<Makaleler> MakaleSil(Makaleler makaleler)
        {
            businessLayerSonuc.nesne= repository_makaleler.Find(x=>x.ID==makaleler.ID);
            if(businessLayerSonuc != null)
            {
                int sonuc=repository_makaleler.Delete(makaleler);
                if(sonuc < 1)
                {
                    businessLayerSonuc.Hatalar.Add("Kayit silinemedi");
                }
            }
            else
            {
                businessLayerSonuc.Hatalar.Add("Kayit bulunamadı");
            }
            return businessLayerSonuc;
        }
    }
}
