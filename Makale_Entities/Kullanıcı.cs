using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities
{
    [Table("Kullanici")]
    public  class Kullanıcı :EntitiesBase
    {
        [StringLength(20)]
        public string Adi { get; set; }
        [StringLength(20)]

        public string Soyad { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Email { get; set; }
        public Guid AktivasyonAnahtari { get; set; }
        public bool Aktif { get; set; }
        public bool Admin { get; set; }
        public virtual List<Makaleler> Makaleler { get; set; }
        public virtual List<Yorum> Yorumlar { get; set; }
        public virtual List<Begeni> Begeniler { get; set; }
    }
}
