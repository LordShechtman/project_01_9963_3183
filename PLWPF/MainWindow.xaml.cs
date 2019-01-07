﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE;
using BL;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private IBL bl;
        public MainWindow()
        {
            InitializeComponent();
            bl = Factory_BL.getBL();
            /* Demo Data for check our program*/
            Random r = new Random();
            string[] names = { "Avi", "Yossi", "Tomer", "Rafi", "Noam", "Youval","Tal","Adi","Ester","Nadav","Moshe" };
            string[] f_names = {"Hlevi","Rabinovich","Fox","Madmon","Shey","Dibon","Apt","Gidon","Potter" };
            string[] streets = {"Hsafam","Herbert Samuel","Galil","Ben Gurion","Herzel" ,"Valenberg"};
            string[] cities = {"Hifa","Tel Aviv","Malalot","Ramat Hashron","Jerusalem","Ashdod" };
            Address temp_address;
            for (int i = 1; i < 10; i++)
            {
                temp_address.city = cities[r.Next(0, 6)];
                temp_address.houseNumber = r.Next(1, 120);
                temp_address.streetName = streets[r.Next(0, 6)];

                bl.AddTrainee(i.ToString("312589900"), names[r.Next(0, 11)], f_names[r.Next(0, 9)]
                    , new DateTime(1998, 01, 21), (MyEnum.gender)r.Next(0, 2), i.ToString("054-2345701"), temp_address,
                    (MyEnum.carType)r.Next(1, 5), (MyEnum.gear)r.Next(0, 2), "Hermon", names[r.Next(0, 11)]
                    ,r.Next(10,80));
               foreach (var item in bl.GetAllTrainees())
                {
                    item.Password = "1234";
                }
            }
            //---------------------------------------------------

        }

        private void TesterButton_Click(object sender, RoutedEventArgs e)
        {
            TesterWindow testerWindow = new TesterWindow();
            testerWindow.ShowDialog();

        }

        private void TrineeButton_Click(object sender, RoutedEventArgs e)
        {
            TrinneWindow trinneWindow = new TrinneWindow();
            trinneWindow.ShowDialog();
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            TestMenuxaml testMenu = new TestMenuxaml();
            testMenu.ShowDialog();
        }

        
    }
}
