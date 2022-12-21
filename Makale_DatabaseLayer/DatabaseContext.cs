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
        //tablolarımızı temsil ederler
        public DbSet<Makaleler> makaleler { get; set; }
        public DbSet<Kategori> kategoriler { get; set; }
        public DbSet<Yorum> yorumlar { get; set; }
        public DbSet<Kullanıcı> kullanıcılar { get; set; } 
        public DbSet<Begeni> begeniler { get; set; }

        public DatabaseContext()
        {
            //Bir veritabanına ilk kez erişmek için belirli DbContext bir tür kullanıldığında veritabanı başlatıcısı çağrılır. 
            Database.SetInitializer(new VeriTabanıOlusturucu());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Fluent api
            //Makale tablosunun yorum tablosu ile ilişki olduğunu belirttik => hasmany
            //bir yourumun mutlaka notu vardır => withrequired
            //delete işleminde cascade' izin verdik
            modelBuilder.Entity<Makaleler>().HasMany(m=>m.Yorumlar).WithRequired(y=>y.Makaleler).WillCascadeOnDelete(true);
            modelBuilder.Entity<Makaleler>().HasMany(m=>m.Begeniler).WithRequired(b=>b.Makaleler).WillCascadeOnDelete(true);
        }



    }
}
