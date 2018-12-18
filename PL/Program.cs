using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BE;
using BL;
namespace PL
{
    class Program
    {
        


       
      static void Main(string[] args)
        {
           IBL bl = Factory_BL.getBL();




        Address a;
                       a.city="Hifa";a.streetName = " The Pool";a.houseNumber = 3;
            Trainee T = new Trainee("111111111", "Maoz", "Smith", new DateTime(2005, 3, 11), (gender)1, "0525525223", a, (carType)2, (gear)1, "kolo", "moshe", 100);
            // Console.WriteLine(T.ToString());
            bool[,] mat = new bool[5, 6];
            for (int i = 0; i <5; i++)
            {
              for(int j=0;j<6;j++)
                {
                   if((j+2)%2==0)
                        mat[i, j] = true;
                    else
                        mat[i, j] = false;
                }
            }
                    
            Tester tester = new Tester("444444444", "Nadav", "Rabinovich", new DateTime(1970,12,23),(gender)1,"0522848424",a,30,17,(carType)3,60,mat);

            try
            {
                bl.AddTester("444444444", "Nadav", "Rabinovich", new DateTime(1970, 12, 23), (gender)0, "0522848424", a, 30, 17, (carType)3, mat, 30);
                bl.AddTester("111111111", "Ester", "Malka", new DateTime(1970, 12, 23), (gender)0, "0522848424", a, 30, 17, (carType)3, mat, 30);
                bl.AddTrainee("312589963", "Maoz", "Lex", new DateTime(1994,3, 11), (gender)1, "0525525223", a, (carType)4, gear.manual, "hermon", "avi", 28);
                bl.AddTrainee("312589964", "Rafi", "Lev", new DateTime(1995, 3, 3), (gender)1, "0525525243", a, (carType)4, gear.manual, "hermon", "avi", 36);
                foreach (var v in bl.GetAllTesters()) { Console.WriteLine(v.ToString()); }
                bl.AddTest("312589963", a, new DateTime(2018, 12, 18, 11, 0, 0));
                bl.AddTest("312589964", a, new DateTime(2019, 12, 18, 9, 0, 0));
                foreach (Test test in bl.GetAllTests()) { Console.WriteLine(test.ToString()); }
            }
            catch( Exception ex)
            { Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Data);
            }

           





        }
    }
}
