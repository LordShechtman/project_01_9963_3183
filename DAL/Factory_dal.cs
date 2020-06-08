using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Factory_dal
    {
        
            protected static Idal instance = null;

            protected Factory_dal() { }

            public static Idal getInstance()
            {
                if (instance == null)
                {
                    instance = new DAL_XML();
                    
                }
                return instance;
            }
            public static Idal getDal()
        {
            



          return DAL_XML.Instance; ;

        }
        

    }
}
