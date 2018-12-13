using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
            Address a;
                       a.city="Hifa";a.streetName = "Pool";a.houseNumber = 3;
            Trainee T=new Trainee("111111111","Maoz","Smith",new DateTime(1994,3,11),(gender)1,"0525525223",a,(carType)2,(gear)1,"kolo","moshe",100);
            Console.WriteLine(T.ToString());
            bool[,] mat = new bool[5, 6];
            for (int i = 0; i < 5; i++)
                for (int j=0; j < 6; j++)
                    mat[i, j] = true;
            Tester tester = new Tester("444444444", "Nadav", "Rabinovich", new DateTime(1970,12,23),(gender)2,"0522848424",a,30,17,(carType)3,60,mat);
           string str= tester.ToString();
            Console.WriteLine(str);
            //להוסיף שדה של ססמא לטסטר
        }
    }
}
