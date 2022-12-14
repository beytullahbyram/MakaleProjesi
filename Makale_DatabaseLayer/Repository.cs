using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Makale_DatabaseLayer
{
    //Tüm entitiler için bu class kullanılacak(listeleme,ekle, çıkar vs.)
    public class Repository<T>:Singleton where T : class // T tipin class olduğunu söylemek için where T : class yazdık çünkü T tipine int,string değerde alabiliyor
    {
        DbSet<T> _objectset;
        public Repository()
        {

            _objectset= db.Set<T>();//Her seferinde dbset bulmaya çalışmaması için sadece class çağrıldığında tek sefer de bulması yeterli
        }

        public List<T> List()
        {
            return _objectset.ToList();
        }
        public IQueryable<T> ListE()
        {
            return _objectset.AsQueryable();
        }
        public List<T> List(Expression<Func<T,bool>>kosul)
        {
            return _objectset.Where(kosul).ToList();
        }
        public T Find(Expression<Func<T,bool>>kosul)
        {
            return _objectset.FirstOrDefault(kosul);
        }
        public int Insert(T nesne)
        {
            _objectset.Add(nesne);

            return db.SaveChanges();
        }
        public int Delete(T nesne)
        {
            _objectset.Remove(nesne);

            return db.SaveChanges();
        }
        public int Update()
        {
            return db.SaveChanges();
        }


    }
}
