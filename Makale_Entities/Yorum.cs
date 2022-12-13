using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities
{
    public class Yorum:EntitiesBase
    {
        public string Text { get; set; }    
        public virtual Kullanıcı Kullanici { get; set; }
        public virtual Makaleler Makaleler { get; set; }
    }
}
