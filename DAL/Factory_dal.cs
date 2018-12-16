using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Factory_dal
    {
        
        public static Idal getDal()
        {
            return DAL.Dal_imp.instance; 
        }

    }
}
