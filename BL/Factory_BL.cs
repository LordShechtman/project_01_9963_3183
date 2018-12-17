using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public  class Factory_BL
    {
        public static IBL getBL()
        {
            return BL.Bl_imp.Instance;
        }
    }
}
