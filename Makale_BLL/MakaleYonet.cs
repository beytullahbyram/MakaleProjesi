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
            return repository_makaleler.ListE();//bellekte veri oluşmaz
        }

        public Makaleler MakaleBul(int value)
        {
            throw new NotImplementedException();
        }

        public void MakaleKaydet(Makaleler makaleler)
        {
            throw new NotImplementedException();
        }

        public void MakaleUpdate(Makaleler makaleler)
        {
            throw new NotImplementedException();
        }

        public void MakaleSil(Makaleler makaleler)
        {
            throw new NotImplementedException();
        }
    }
}
