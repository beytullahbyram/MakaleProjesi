using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_DatabaseLayer
{
    public class VeriTabanıOlusturucu:CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            Kullanıcı admin=new Kullanıcı() { 
                Adi="john",
                Soyad="doe",
                Email="example@hotmail.com",
                Sifre="123456",
                Aktif=true,
                Admin=true,
                AktivasyonAnahtari=Guid.NewGuid(),
                KullaniciAdi="johndoe",
                KayıtTarihi=DateTime.Now,
                DegistirmeTarihi=DateTime.Now.AddMinutes(5),
                DegistirenKullanici="johndoe"
            };
            context.kullanıcılar.Add(admin);

            for (int i = 1; i < 7; i++)
            {
                Kullanıcı user=new Kullanıcı() { 
                Adi=FakeData.NameData.GetFirstName(),
                Soyad=FakeData.NameData.GetSurname(),
                Email=FakeData.NetworkData.GetEmail(),
                Sifre="123456",
                Aktif=true,
                Admin=false,
                AktivasyonAnahtari=Guid.NewGuid(),
                KullaniciAdi="user"+i,
                KayıtTarihi=FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1),DateTime.Now),
                DegistirmeTarihi=DateTime.Now,
                DegistirenKullanici="user"+i,
                };
                context.kullanıcılar.Add(user);
                context.SaveChanges();
            }
            context.SaveChanges();
            List<Kullanıcı>kullanıcılistesi=context.kullanıcılar.ToList();



            for (int i = 1; i < 7; i++)
            {
                Kategori kategori=new Kategori() { 
                KategoriBaslik=FakeData.PlaceData.GetCountry(),
                KategoriAciklama=FakeData.PlaceData.GetAddress(),
                KayıtTarihi=DateTime.Now,
                DegistirmeTarihi=DateTime.Now,
                DegistirenKullanici="johndoe"
                };
                context.kategoriler.Add(kategori);

                for (int j = 1; j < 6; j++)
                {
                    Makaleler makale=new Makaleler() { 
                        Baslik=FakeData.TextData.GetAlphabetical(5),
                        Text=FakeData.TextData.GetSentences(1),
                        TaslakDurumu=false,
                        BegeniSayisi=FakeData.NumberData.GetNumber(1,4),
                        Kullanici=kullanıcılistesi[FakeData.NumberData.GetNumber(1,4)],
                        KayıtTarihi=FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1),DateTime.Now),
                        DegistirmeTarihi=DateTime.Now,
                        DegistirenKullanici=kullanıcılistesi[FakeData.NumberData.GetNumber(1,4)].KullaniciAdi
                       
                    };
                    kategori.Makaleler.Add(makale);

                    //fake begeni
                    for (int l = 1; l < makale.BegeniSayisi; l++)
                    {
                        Begeni begeni=new Begeni() { 
                        Kullanıcı =kullanıcılistesi[FakeData.NumberData.GetNumber(1,5)],
                        };
                        makale.Begeniler.Add(begeni);
                    }

                    //fake yorum
                    for (int k = 1; k < 4; k++)
                    {
                        Yorum yorum=new Yorum() { 
                            Text=FakeData.TextData.GetSentences(2),
                             Kullanici=kullanıcılistesi[FakeData.NumberData.GetNumber(1,5)],
                             KayıtTarihi=FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1),DateTime.Now),
                             DegistirmeTarihi=DateTime.Now,
                             DegistirenKullanici=kullanıcılistesi[FakeData.NumberData.GetNumber(1,5)].KullaniciAdi,
                             };
                        makale.Yorumlar.Add(yorum);
                    }
                }
            }
            context.SaveChanges();
        }
    }
}
