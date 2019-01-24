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



        public static MyEnum.gender make_gender(string g)
        {

            switch (g)
            {
                case "male":return MyEnum.gender.male;
                   
                case "female":
                    return MyEnum.gender.female;
                 
                default:
                    break;
            }
            return MyEnum.gender.male;
        }
        public static MyEnum.carType MAKE_TYPE_CAR(string car)
        {
            switch (car)
            {
                case "privateCar":
                    return MyEnum.carType.privateCar;
                case "motroCycle":
                    return MyEnum.carType.motroCycle;
                case "trunk":
                    return MyEnum.carType.trunk;
                case "havyTrunk":
                    return MyEnum.carType.havyTrunk;
                default:
                    break;

            }
            return MyEnum.carType.privateCar;
        }
        public static Address MAKE_adress(string city, string streetName, int houseNumber)
        {
            Address temp;
            temp.city = city;
            temp.streetName = streetName;
            temp.houseNumber = houseNumber;
            return temp;
        }
        public static MyEnum.gear MAKE_gear (string gear)
        {
            switch (gear)
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
        public static List<bool> SetBoolLIST(String ELEMENTS, String ELEMENTS1, String ELEMENTS2, String ELEMENTS3, String ELEMENTS4, String ELEMENTS5)
        {
            //Convert string to bools list
            List<bool> my_list = new List<bool>();
            my_list.Add(Boolean.Parse(ELEMENTS));
            my_list.Add(Boolean.Parse(ELEMENTS1));
            my_list.Add(Boolean.Parse(ELEMENTS2));
            my_list.Add(Boolean.Parse(ELEMENTS3));
            my_list.Add(Boolean.Parse(ELEMENTS4));
            my_list.Add(Boolean.Parse(ELEMENTS5));
            return my_list;
        }

        public static List<string> MakeNoteList(string notes)
        {
            List<string> my_notes = new List<string>();
            foreach(var ch in notes)
            {
                string item = "";
                while(ch!='.')
                {
                    item += ch;
                }
                my_notes.Add(item);

            }
            return my_notes;
        }
    }

  
     


}