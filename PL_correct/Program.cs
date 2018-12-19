using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE;
using BL;
// ALL the cheks and found in the BL becuse we dont have a perfect UL layer (in the 2ND we will change those things)
namespace PL_correct
{
    class Program
    {
        //temp valid functions
        bool all_fileds_full(List<object> lst)
        {
            var is_null = lst.FindAll(x => x != null);

            return is_null.Count == lst.Count();
        }
        public static Address setAddress(string _city, string _street, int num)
        {
            Address a;
            a.city = _city;
            a.streetName = _street;
            a.houseNumber = num;
            return a;
        }
        public static void setMet(bool[,] mat)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if ((j + 2) % 2 == 0)
                        mat[i, j] = true;
                    else
                        mat[i, j] = false;
                }
            }
        }
        static void Main(string[] args)
        {
            try
            {
                bool[,] mat = new bool[5, 6];
                setMet(mat);
                IBL bl = Factory_BL.getBL();
                bl.AddTester("111111111", "Nadv", "Rabinovich", new DateTime(1970, 3, 11), gender.male, "0528434091", setAddress("Hifa", "The Pool", 4), 20, 20, carType.privateCar, mat, 50);
                bl.AddTester(new Tester("111111123", "Ester", "Malka", new DateTime(1972, 4, 13), gender.female, "0528434093", setAddress("Hifa", "The Wood", 6), 20, 20, carType.privateCar, 50, mat));
                bl.AddTrainee("312589963", "Rafi", "Lev", new DateTime(1990, 3, 11), gender.male, "0525525224", setAddress("Hifa", "Kalil", 52), carType.privateCar, gear.manual, "Hermon", "Yossi", 30);
                bl.AddTrainee(new Trainee("312589964", "Noam", "Getz", new DateTime(1994, 6, 14), gender.male, "0525525224", setAddress("Hifa", "Kalil", 56), carType.privateCar, gear.manual, "Hermon", "Yossi", 28));
                bl.AddTrainee("312589961", "Tal", "Madmon", new DateTime(1990, 3, 11), gender.male, "0525525223", setAddress("Hifa", "Hglil", 93), carType.privateCar, gear.manual, "Ways", "Efi", 30);
                bl.AddTest("312589963", setAddress("Hifa", "Hglil", 93), new DateTime(2018, 12, 30, 13, 0, 0));
                bl.AddTest("312589963", setAddress("Hifa", "Hglil", 93), new DateTime(2018, 12, 30, 15, 0, 0));
                
            }
            catch( Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
        }

    }
}
