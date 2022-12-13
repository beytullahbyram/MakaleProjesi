using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Makale_DatabaseLayer
{
    public class DatabaseContext : DbContext
    {
        DbSet<Makaleler> makaleler { get; set; }
        DbSet<Kategori> kategoriler { get; set; }
        DbSet<Yorum> yorumlar { get; set; }
        DbSet<Kullanıcı> kullanıcılar { get; set; } 
        DbSet<Begeni> begeniler { get; set; }






    }
}
