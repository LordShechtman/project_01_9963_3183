using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public struct Address
    //address type
    //Use in the date base for Tester,Student and so
    
    {
        public string streetName;
        public int houseNumber;
       public string city;
        public override string ToString()
        {
            return streetName + " " + houseNumber + " " + city;
        }

    }
    
    
}
