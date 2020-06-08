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
        public static int Number_Parameters_To_Pass = 5;    
       public  DateTime currntDate = DateTime.Now;
       public static int Maximum_Tester_age=67;
       public static int testNum = 1;
        public static  string[] phoneNumberPrefixes = { "050-","051-", "052-", "054-","055-","058-","057-" ,"02-","03-","08-","04-","09-"};
        #region Convertors(Eliran wanteed to write this here)
        public static MyEnum.gender make_gender(string value)
        {
            switch(value)
            {
                //Turn string to gender enum
                case "male":
                    return MyEnum.gender.male;
                case "female":
                    return MyEnum.gender.female;
                default:
                    break;
            }
            return MyEnum.gender.female;
        }

        public static MyEnum.carType MAKE_TYPE_CAR(string value)
        {
            switch (value)
            {
                //Turn string to carType enum
                case "motroCycle":
                    return MyEnum.carType.motroCycle;
                case "trunk":
                    return MyEnum.carType.trunk;
                case "havyTrunk":
                    return MyEnum.carType.havyTrunk;
                case "privateCar":
                    return MyEnum.carType.privateCar;
                default:
                    break;
            }
            return MyEnum.carType.privateCar;
        }

        public static Address MAKE_Address(string city, string street, int number)
        {
            Address my_address;
            my_address.city = city;
            my_address.streetName = street;
            my_address.houseNumber = number;
            return my_address;
        }

        public static MyEnum.gear MAKE_gear(string value)
        {
            //Turn string to gear  Enum
            switch(value)
            {
                case "automat":
                    return MyEnum.gear.automat;
                case "manual":
                    return MyEnum.gear.manual;
                default:
                    break;
            }
            return MyEnum.gear.automat;
        }

        public static List<bool> SetBoolLIST(string value1, string value2, string value3, string value4, string value5, string value6)
        {
            List<bool> my_list = new List<bool>();
            my_list.Add(Boolean.Parse(value1));
            my_list.Add(Boolean.Parse(value2));
            my_list.Add(Boolean.Parse(value3));
            my_list.Add(Boolean.Parse(value4));
            my_list.Add(Boolean.Parse(value5));
            my_list.Add(Boolean.Parse(value6));
            return my_list;
        }

        public static List<string> MakeNoteList(string value)
        {
            List<string> notes = new List<string>();
            foreach(var ch in value)
            {
                string str = "";
                while (ch != '.')
                    str += ch;
                notes.Add(str);
            }
            return notes;
        }
        #endregion
    }

}