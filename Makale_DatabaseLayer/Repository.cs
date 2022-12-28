using Makale_Common;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Makale_DatabaseLayer
{
	//Tüm entitiler için bu class kullanılacak(listeleme,ekle, çıkar vs.)
	public class Repository<T> : Singleton where T : class // T tipin class olduğunu söylemek için where T : class yazdık çünkü T tipine int,string değerde alabiliyor
	{
		private DbSet<T> _objectset;

		public Repository()
		{
			_objectset = db.Set<T>();//Her seferinde dbset bulmaya çalışmaması için sadece class çağrıldığında tek sefer de bulması yeterli
		}

		public List<T> List()
		{
			return _objectset.ToList();
		}

		public IQueryable<T> ListE()
		{
			return _objectset.AsQueryable();
		}

		public List<T> List(Expression<Func<T, bool>> kosul)
		{
			return _objectset.Where(kosul).ToList();
		}

		public T Find(Expression<Func<T, bool>> kosul)
		{
			return _objectset.FirstOrDefault(kosul);
		}

		public int Insert(T nesne)
		{
			_objectset.Add(nesne);

			EntitiesBase obj = nesne as EntitiesBase;
			DateTime zaman = DateTime.Now;

			if (nesne is EntitiesBase)
			{
				obj.KayıtTarihi = zaman;
				obj.DegistirmeTarihi = zaman;
				obj.DegistirenKullanici = Uygulama.kullanidiad;
			}

			return db.SaveChanges();
		}

		public int Delete(T nesne)
		{
			_objectset.Remove(nesne);
			return db.SaveChanges();
		}

		public int Update(T nesne)
		{
			EntitiesBase obj = nesne as EntitiesBase;

			if (nesne is EntitiesBase)
			{
				obj.DegistirmeTarihi = DateTime.Now;
				obj.DegistirenKullanici = Uygulama.kullanidiad;
			}

			return db.SaveChanges();
		}
	}
}