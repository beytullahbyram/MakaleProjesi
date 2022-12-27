using Makale_DatabaseLayer;
using Makale_Entities;

namespace Makale_BLL
{
	public class YorumYonet
	{
		private Repository<Yorum> rep_yorum = new Repository<Yorum>();

		public Yorum YorumBul(int id)
		{
			return rep_yorum.Find(x => x.ID == id);
		}

		public int YorumEkle(Yorum yorum)
		{
			return rep_yorum.Insert(yorum);
		}

		public int YorumGuncelle(Yorum yorum)
		{
			return rep_yorum.Update(yorum);
		}

		public int YorumSil(Yorum yorum)
		{
			return rep_yorum.Delete(yorum);
		}
	}
}