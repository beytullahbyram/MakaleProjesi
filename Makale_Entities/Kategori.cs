using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities
{
    public class Kategori:EntitiesBase
    {
        public string KategoriBaslik { get; set; }
        public string KategoriAciklama { get; set; }
        public virtual List<Makaleler> Makaleler { get; set; }//bir kategorinin birden fazla makale olur.


    }
}
