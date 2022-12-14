using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities
{
    [Table("Makaleler")]
    public class Makaleler:EntitiesBase
    {
        [Required,StringLength(20)]
        public string Baslik { get; set; }
        [Required,StringLength(250)]
        public string Text { get; set; }
        public bool TaslakDurumu { get; set; }
        public int BegeniSayisi { get; set; }
        public int KategoriId { get; set; }//foregein anahtarının ismini verdikkk
        public virtual Kategori Kategori { get; set; }
        public virtual Kullanıcı Kullanici { get; set; }
        public virtual List<Yorum> Yorumlar { get; set; }
        public virtual List<Begeni> Begeniler { get; set; }

        public Makaleler()
        {
            Yorumlar = new List<Yorum>();
            Begeniler = new List<Begeni>(); 

        }


    }
}
