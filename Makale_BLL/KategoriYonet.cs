using Makale_DatabaseLayer;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    public class KategoriYonet
    {
        Repository<Kategori> repository_kategoriler= new Repository<Kategori>();
        public List<Kategori> KategorileriListele()
        {
            return repository_kategoriler.List();
        }
        public Kategori KategoriBul(int id)
        {
            return repository_kategoriler.Find(x=>x.ID == id);
        }
        public Kategori KategoriEkle(int id)
        {
            return repository_kategoriler.Find(x=>x.ID == id);
        }

        public object KategoriUpdate(Kategori kategori)
        {
            throw new NotImplementedException();
        }

        public void KategoriSil(Kategori kategori)
        {
            throw new NotImplementedException();
        }
    }
}
