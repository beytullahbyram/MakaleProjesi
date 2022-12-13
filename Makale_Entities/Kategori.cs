using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities
{
    [Table("Kategoriler")]
    public class Kategori:EntitiesBase
    {
        [Required,StringLength(50)]
        public string KategoriBaslik { get; set; }
        [StringLength(150)]
        public string KategoriAciklama { get; set; }
        public virtual List<Makaleler> Makaleler { get; set; }//bir kategorinin birden fazla makale olur.


    }
}
