using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
  public class DataSource :ICloneable
    {
        
        public static List<BE.Tester> testers;
        public static List<BE.Trainee> trainees;
        public static  List<BE.Test> tests;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
