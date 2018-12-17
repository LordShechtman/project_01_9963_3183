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
            Tester tester1 = new Tester("444444444", "Nadav", "Rabinovich", new DateTime(1970, 12, 23), (gender)0, "0522848424", a, 30, 17, (carType)3, 60, mat);
            try
            {
                bl.AddTester("444444444", "Nadav", "Rabinovich", new DateTime(1970, 12, 23), (gender)0, "0522848424", a, 30, 17, (carType)3, mat, 30);
                bl.AddTester("111111111", "Nadav", "Rabinovich", new DateTime(1970, 12, 23), (gender)0, "0522848424", a, 30, 17, (carType)3, mat,30);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            List<Tester> t = bl.GetAllTesters();
      
            
            
        }
    }
}
