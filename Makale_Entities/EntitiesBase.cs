using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities
{
    public class EntitiesBase
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public DateTime KayıtTarihi { get; set; }
        [Required]
        public DateTime DegistirmeTarihi { get; set; }
        [Required,StringLength(20)]
        public string DegistirenKullanici { get; set; }
    }
}
