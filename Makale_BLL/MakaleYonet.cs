using Makale_DatabaseLayer;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    public class MakaleYonet
    {
        Repository<Makaleler> repository_makaleler=new Repository<Makaleler>(); 
        public List<Makaleler> MakaleListele()
        {
           return repository_makaleler.List();
        }
        public IQueryable<Makaleler> ListeleQueryable()
        {
            return repository_makaleler.ListE();
        }
    }
}
