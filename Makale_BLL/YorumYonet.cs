using Makale_DatabaseLayer;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    public class YorumYonet
    {
        Repository<Yorum> rep_yorum=new Repository<Yorum>();
        public Yorum YorumBul(int id)
        {
            return rep_yorum.Find(x=>x.ID == id);
        }
        public int YorumGuncelle(Yorum yorum)
        {
            return rep_yorum.Update(yorum);
        }

        public int YorumSil(int id)
        {
            Yorum silineceyorum=rep_yorum.Find(x=>x.ID==id); 
            return rep_yorum.Delete(silineceyorum);
        }
    }
}
