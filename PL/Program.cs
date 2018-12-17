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
                    if ((j + 2) % 2 == 0 && (i + 2) % 2 == 0)
                        mat[i, j] = true;
                    else
                        mat[i, j] = false;
                }
            }
                    
            Tester tester = new Tester("444444444", "Nadav", "Rabinovich", new DateTime(1970,12,23),(gender)0,"0522848424",a,30,17,(carType)3,60,mat);
           
            try
            {
                bl.AddTester("444444444", "Nadav", "Rabinovich", new DateTime(1970, 12, 23), (gender)0, "0522848424", a, 30, 17, (carType)3, mat, 30);
                bl.AddTester("111111111", "Nadav", "Rabinovich", new DateTime(1970, 12, 23), (gender)0, "0522848424", a, 30, 17, (carType)3, mat,30);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            bl.AddTrainee("222222222", "maoz", "sh", new DateTime(1994, 11, 3), (gender)1, "0525525223", a, (carType)3, gear.manual, "hermon", "haim", 28);
            bl.AddTrainee("333333333", "maoz", "sh", new DateTime(1994, 11, 3), (gender)1, "0525525223", a, (carType)3, gear.manual, "hermon", "haim", 28);
            Trainee trainee = new Trainee("222222222", "maoz", "sh", new DateTime(1994, 11, 3), (gender)1, "0525525223", a, (carType)3, gear.manual, "hermon", "haim", 28);
            bl.AddTest("222222222", a, new DateTime(2018, 12, 19, 13, 0, 0));
            bl.AddTest("333333333", a, new DateTime(2018, 12, 19, 13, 0, 0));
           

            foreach (Test test in bl.GetAllTests())
            {
                Console.WriteLine(test.ToString());
            }
      
            
            
        }
    }
}
