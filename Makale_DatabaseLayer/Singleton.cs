using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_DatabaseLayer
{
    public class Singleton
    {
        //repository classından db sürekli önlemesinin önüne geçmiş olduk
        public static DatabaseContext db=null;
        public Singleton()
        {
            if(db==null)
                db=new DatabaseContext();
        }
    }
}
