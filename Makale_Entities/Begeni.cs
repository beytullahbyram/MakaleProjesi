using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities
{
    public class Begeni
    {
        //birden fazla makaleyi birdan fazla kişi beğenebilir o yüzden n-n ilişkilerde ara tablo yapmak durumda kalırız
        public int Id { get; set; }
        public virtual Kullanıcı Kullanıcı { get; set; }
        public virtual Makaleler Makaleler { get; set; }
    }
}
