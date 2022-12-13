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
        
        public DbSet<Makaleler> makaleler { get; set; }
        public DbSet<Kategori> kategoriler { get; set; }
        public DbSet<Yorum> yorumlar { get; set; }
        public DbSet<Kullanıcı> kullanıcılar { get; set; } 
        public DbSet<Begeni> begeniler { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new VeriTabanıOlusturucu());
        }




    }
}
