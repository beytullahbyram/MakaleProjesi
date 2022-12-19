using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [StringLength(50)]
        public string Adi { get; set; }
        [StringLength(50)]
        public string Soyad { get; set; }
        [StringLength (50)]
        public string ProfilResmi { get; set; }
        [DisplayName("kullanıcı adı"),Required(),StringLength(50)]
        public string KullaniciAdi { get; set; }
        [Required,StringLength (50)]
        public string Sifre { get; set; }
        [Required,StringLength (60)]
        public string Email { get; set; }
        [Required]
        public Guid AktivasyonAnahtari { get; set; }
        public bool Aktif { get; set; }
        public bool Admin { get; set; }
        public virtual List<Makaleler> Makaleler { get; set; }
        public virtual List<Yorum> Yorumlar { get; set; }
        public virtual List<Begeni> Begeniler { get; set; }

        public Kullanıcı()
        {
            Makaleler=new List<Makaleler>();
            Yorumlar= new  List<Yorum>();
             Begeniler=new    List<Begeni>();
        }
    }
}
