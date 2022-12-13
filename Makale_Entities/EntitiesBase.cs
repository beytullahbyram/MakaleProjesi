using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities
{
    public class EntitiesBase
    {

        public int ID { get; set; }
        public DateTime KayıtTarihi { get; set; }
        public DateTime DegistirmeTarihi { get; set; }
        public string DegistirenKullanici { get; set; }
    }
}
