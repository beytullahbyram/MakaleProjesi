using Makale_DatabaseLayer;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    public class LikeYonet
    {
        Repository<Begeni> rep_like=new Repository<Begeni>();
        public IQueryable<Begeni> ListE()
        {
            return rep_like.ListE();
        }

        public List<Begeni> BegeniListele(Expression<Func<Begeni,bool>>kosul)
        {
            return rep_like.List(kosul);
        }

		public Begeni BegeniBul(int makaleid, int kullanıcıid)
        {
            return rep_like.Find(x=>x.Makaleler.ID==makaleid && x.Kullanıcı.ID==kullanıcıid);
        }

		public int BegeniSil(Begeni begeni)
		{
			return rep_like.Delete(begeni);
		}

		public int BegeniEkle(Begeni begeni)
		{
			return rep_like.Insert(begeni); 
		}
	}
}
