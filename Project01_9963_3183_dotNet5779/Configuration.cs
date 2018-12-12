using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    //Includes all globals static variables for our project
   public  class Configuration
    {
        
        public static int  Tester_MIN_AGE = 40;
        public static int Trainee_MIN_AGE = 18;
        public static int MIN_LESSONS_FOR_TEST = 20;
        public static int DAYS_Between_TESTS = 7;
       public  DateTime currntDate = DateTime.Now;

        public static int testNum = 1;
    }
}
