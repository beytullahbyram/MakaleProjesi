using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities.View_modal
{
    public class Login_Modal
        //Giriş ekranında giriş yaparken 
    {
        [DisplayName("kullanıcı adı"),Required(ErrorMessage ="{0} alanı boş geçilemez"),StringLength(50)]
        public string KullaniciAdi { get; set; }
        [DisplayName("sifre"),Required(ErrorMessage ="{0} alanı boş geçilemez"),StringLength(50)]
        public string Sifre { get; set; }
    }
}
