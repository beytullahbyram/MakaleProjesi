using Makale_Common;
using Makale_DatabaseLayer;
using Makale_Entities;
using Makale_Entities.View_modal;
using System;
using System.Collections.Generic;

namespace Makale_BLL
{
	public class KullaniciYonet
	{
		private BusinessLayerSonuc<Kullanıcı> sonuc = new BusinessLayerSonuc<Kullanıcı>();
		private Repository<Kullanıcı> rep_kul = new Repository<Kullanıcı>();

		public List<Kullanıcı> Listele()
		{
			return rep_kul.List();
		}

		public BusinessLayerSonuc<Kullanıcı> Kaydet(Register_Modal model)
		{
			Kullanıcı kullanici = new Kullanıcı();
			kullanici.KullaniciAdi = model.KullaniciAdi;
			kullanici.Email = model.Email;

			sonuc = KullaniciKontrol(kullanici);
			if (sonuc.Hatalar.Count > 0)
			{
				sonuc.nesne = kullanici;
				return sonuc;
			}
			else
			{
				int kaydet = rep_kul.Insert(new Kullanıcı()
				{
					Email = model.Email,
					KullaniciAdi = model.KullaniciAdi,
					Sifre = model.Sifre,
					AktivasyonAnahtari = Guid.NewGuid(),
					Admin = false,
					Aktif = false,
				});
				if (kaydet > 0)
				{
					sonuc.nesne = rep_kul.Find(x => x.Email == model.Email && x.KullaniciAdi == model.KullaniciAdi);

					//Aktivasyon maili gönderilecek
					string siteUrl = ConfigHelper.Get<string>("SiteRootUri");
					string activateUrl = $"{siteUrl}/Home/UserActivate/{sonuc.nesne.AktivasyonAnahtari}";
					string body = $"Merhaba {sonuc.nesne.KullaniciAdi} <br/> Hesabınızı aktifleştirmek için <a href='{activateUrl}'> tıklayınız</a> ";

					MailHelper.SendMail(body, sonuc.nesne.Email, "Hesap Aktifleştirme");
				}
			}
			return sonuc;
		}

		public BusinessLayerSonuc<Kullanıcı> KullaniciKaydet(Kullanıcı kullanıcı)
		{
			if (kullanıcı != null)
			{
				if (kullanıcı.KullaniciAdi == kullanıcı.KullaniciAdi)
				{
					sonuc.Hatalar.Add("Kullanıcı adı sistemde kayıtlı");
				}
				if (kullanıcı.Email == kullanıcı.Email)
				{
					sonuc.Hatalar.Add("Email sistemde kayıtlı");
				}
			}
			else
			{
				int kaydet = rep_kul.Insert(new Kullanıcı()
				{
					Email = kullanıcı.Email,
					KullaniciAdi = kullanıcı.KullaniciAdi,
					Sifre = kullanıcı.Sifre,
					AktivasyonAnahtari = Guid.NewGuid(),
					Admin = false,
					Aktif = false,
				});
				if (kaydet > 0)
				{
					sonuc.nesne = rep_kul.Find(x => x.Email == kullanıcı.Email && x.KullaniciAdi == kullanıcı.KullaniciAdi);

					//Aktivasyon maili gönderilecek
					string siteUrl = ConfigHelper.Get<string>("SiteRootUri");
					string activateUrl = $"{siteUrl}/Home/UserActivate/{sonuc.nesne.AktivasyonAnahtari}";
					string body = $"Merhaba {sonuc.nesne.KullaniciAdi} <br/> Hesabınızı aktifleştirmek için <a href='{activateUrl}'> tıklayınız</a> ";

					MailHelper.SendMail(body, sonuc.nesne.Email, "Hesap Aktifleştirme");
				}
			}
			return sonuc;
		}

		public Kullanıcı KullaniciBul(int id)
		{
			return rep_kul.Find(x => x.ID == id);
		}

		public BusinessLayerSonuc<Kullanıcı> LoginKontrol(Login_Modal model)
		{
			BusinessLayerSonuc<Kullanıcı> sonuc = new BusinessLayerSonuc<Kullanıcı>();

			sonuc.nesne = rep_kul.Find(x => x.KullaniciAdi == model.KullaniciAdi && x.Sifre == model.Sifre);

			if (sonuc.nesne != null)
			{
				if (!sonuc.nesne.Aktif)
				{
					sonuc.Hatalar.Add("Kullanıcı aktif değildir.Aktivasyon için e-posta adresiniz kontrol ediniz.");
					return sonuc;
				}
			}
			else
			{
				sonuc.Hatalar.Add("Kullanıcı adı ve şifre eşleşmiyor.");
			}
			return sonuc;
		}

		public BusinessLayerSonuc<Kullanıcı> kullanıcıbul(Guid id)
		{
			BusinessLayerSonuc<Kullanıcı> sonuc = new BusinessLayerSonuc<Kullanıcı>();
			sonuc.nesne = rep_kul.Find(x => x.AktivasyonAnahtari == id);
			if (sonuc.nesne != null)
			{
				if (sonuc.nesne.Aktif)
				{
					sonuc.Hatalar.Add("Kullanıcı zaten aktif");
					return sonuc;
				}
				sonuc.nesne.Aktif = true;
				rep_kul.Update(sonuc.nesne);
			}
			else
			{
				sonuc.Hatalar.Add("Aktifleştirilecek kullanıcı bulunumadı");
			}
			return sonuc;
		}

		public BusinessLayerSonuc<Kullanıcı> KullaniciUpdate(Kullanıcı kullanıcı)
		{
			sonuc = KullaniciKontrol(kullanıcı);
			if (sonuc.Hatalar.Count > 0)
			{
				sonuc.nesne = kullanıcı;
				return sonuc;
			}
			sonuc.nesne = rep_kul.Find(x => x.ID == kullanıcı.ID);
			sonuc.nesne.Adi = kullanıcı.Adi;
			sonuc.nesne.Soyad = kullanıcı.Soyad;
			sonuc.nesne.KullaniciAdi = kullanıcı.KullaniciAdi;
			sonuc.nesne.Email = kullanıcı.Email;
			sonuc.nesne.Sifre = kullanıcı.Sifre;

			if (!string.IsNullOrEmpty(kullanıcı.ProfilResmi))
				sonuc.nesne.ProfilResmi = kullanıcı.ProfilResmi;

			int updatesonuc = rep_kul.Update(sonuc.nesne);
			if (updatesonuc < 1)
				sonuc.Hatalar.Add("Kullanıcı güncellenemedi");
			return sonuc;
		}

		private BusinessLayerSonuc<Kullanıcı> KullaniciKontrol(Kullanıcı kullanıcı)
		{
			Kullanıcı k1 = rep_kul.Find(x => x.KullaniciAdi == kullanıcı.Adi);
			Kullanıcı k2 = rep_kul.Find(x => x.Email == kullanıcı.Email);
			if (k1 != null && k1.ID != kullanıcı.ID)
				sonuc.Hatalar.Add("Kullanıcı adi sistemde kayıtlı");
			if (k2 != null && k2.ID != kullanıcı.ID)
				sonuc.Hatalar.Add("Email sistemde kayıtlı");
			return sonuc;
		}

		public BusinessLayerSonuc<Kullanıcı> KullaniciSil(int id)
		{
			BusinessLayerSonuc<Kullanıcı> sonuc = new BusinessLayerSonuc<Kullanıcı>();
			sonuc.nesne = rep_kul.Find(x => x.ID == id);
			if (sonuc.nesne != null)
				rep_kul.Delete(sonuc.nesne);
			else if (sonuc.nesne == null)
				sonuc.Hatalar.Add("Kullanıcı bulunamadı");
			return sonuc;
		}
	}
}