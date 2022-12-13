using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities
{
    [Table("Begeniler")]
    public class Begeni
    {
        //birden fazla makaleyi birdan fazla kişi beğenebilir o yüzden n-n ilişkilerde ara tablo yapmak durumda kalırız
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual Kullanıcı Kullanıcı { get; set; }
        public virtual Makaleler Makaleler { get; set; }
    }
}
