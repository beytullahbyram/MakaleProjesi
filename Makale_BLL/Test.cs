using Makale_DatabaseLayer;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    public class Test
    {
        //public Test()
        //{
        //    //DatabaseContext db = new DatabaseContext();
        //    ////dkb.Database.CreateIfNotExists(); //database oluşturma I
        //    //db.kategoriler.ToList();
            
        //    //Repository<Kategori> rep_kategori=new Repository<Kategori>();
        //    //List<Kategori> kategoriler= rep_kategori.List();
        //}
        //Repository<Kullanıcı> repository_kullanici=new Repository<Kullanıcı>();
        //public void InsertTest()
        //{
        //    repository_kullanici.Insert(new Kullanıcı()
        //    {
        //        Adi="test",
        //        Soyad="test",
        //        Email="test",
        //        Aktif=true,
        //        Admin=true, 
        //        AktivasyonAnahtari=Guid.NewGuid(),
        //        KullaniciAdi="test",
        //        Sifre="1234",
        //        KayıtTarihi=DateTime.Now,
        //        DegistirmeTarihi=DateTime.Now.AddMinutes(5),
        //        DegistirenKullanici="admin",
        //    });
        //}
        //public void UpdateTest()
        //{
        //    Kullanıcı findkullanıcı= repository_kullanici.Find(x=>x.KullaniciAdi=="test");
        //    if (findkullanıcı != null)
        //    {
        //        findkullanıcı.KullaniciAdi="kullanıcı ad degisti";
        //        repository_kullanici.Update();
        //    }
        //}

        //Repository<Yorum> repository_yorum= new Repository<Yorum>();
        //Repository<Makaleler> repository_makale= new Repository<Makaleler>();

        //public void InsertComment()
        //{
        //    Kullanıcı findkullanici=repository_kullanici.Find(x=>x.ID==1);
        //    Makaleler makalele=repository_makale.Find(x=>x.ID==1);

        //    repository_yorum.Insert(new Yorum() {
        //    Kullanici=findkullanici,
        //    Makaleler=makalele,
        //    Text="test makale",
        //    KayıtTarihi=DateTime.Now,
        //    DegistirmeTarihi=DateTime.Now.AddMinutes(5),
        //    DegistirenKullanici=findkullanici.KullaniciAdi
        //    });
        //}




    }
}
