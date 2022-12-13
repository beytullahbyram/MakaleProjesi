using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities
{
    [Table("Makaleler")]
    public class Makaleler:EntitiesBase
    {
        public string Baslik { get; set; }
        public string Text { get; set; }
        public bool TaslakDurumu { get; set; }
        public int BegeniSayisi { get; set; }
        public int KategoriId { get; set; }//foregein anahtarının ismini verdik
        public virtual Kategori Kategori { get; set; }
        public virtual Kullanıcı Kullanici { get; set; }
        public virtual List<Yorum> Yorumlar { get; set; }
        public virtual List<Begeni> Begeniler { get; set; }
       
    }
}
