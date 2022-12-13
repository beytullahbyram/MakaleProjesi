using Makale_DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    public class Test
    {
        public Test()
        {
            DatabaseContext db = new DatabaseContext();
            //dkb.Database.CreateIfNotExists(); //database oluşturma I
            db.kategoriler.ToList();
            
        }
    }
}
