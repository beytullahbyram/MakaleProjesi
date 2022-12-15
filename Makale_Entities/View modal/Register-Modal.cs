using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities.View_modal
{
    public class Register_Modal
    {
        [DisplayName("kullanıcı adı"),Required(ErrorMessage ="{0} alanı boş geçilemez"),StringLength(50)]
        public string KullaniciAdi { get; set; }

        [DisplayName("sifre"),Required(ErrorMessage ="{0} minumum 6 maximum 20 karakter olmalı"),StringLength (50),MaxLength(20),MinLength(6)]
        public string Sifre { get; set; }
        [DisplayName("sifre dogrulama"),Required(ErrorMessage ="{0} 6 maximum 20 karakter olmalı"),StringLength (50),MaxLength(20),MinLength(6),Compare(nameof(Sifre),ErrorMessage ="sifreler uyusmuyor")]
        public string Sifre2 { get; set; }

        [DisplayName("email"),Required(ErrorMessage ="{0} boş geçilemez"),StringLength (60),EmailAddress(ErrorMessage ="geçerli bir email değil")]
        public string Email { get; set; }

    }
}
