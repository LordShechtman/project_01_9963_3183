using System;
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
using DAL;
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
        /*TEMP DEMO BUTTON JUST TO RUN OUR PROJECT*/
        private void DemoModeButton_Click(object sender, RoutedEventArgs e)
        {
            /* Demo Data for check our program*/
            Random r = new Random();
            string[] names = { "Avi", "Yossi", "Tomer", "Rafi", "Noam", "Youval", "Tal", "Adi", "Ester", "Nadav", "Moshe" };
            string[] f_names = { "Hlevi", "Rabinovich", "Fox", "Madmon", "Shey", "Dibon", "Apt", "Gidon", "Potter" };
            string[] streets = { "Hsafam", "Herbert Samuel", "Galil", "Ben Gurion", "Herzel", "Valenberg" };
            bool[,] work_all_time = new bool[5, 6];
            for(int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    work_all_time[i, j] = true;
                }
            }
    string[] cities = { "Hifa", "Tel Aviv", "Malalot", "Ramat Hashron", "Jerusalem", "Ashdod" };
            Address temp_address;
            for (int i = 1; i < 10; i++)
            {
                temp_address.city = cities[r.Next(0, 6)];
                temp_address.houseNumber = r.Next(1, 120);
                temp_address.streetName = streets[r.Next(0, 6)];

                bl.AddTrainee(i.ToString("312589900"), names[r.Next(0, 11)], f_names[r.Next(0, 9)]
                    , new DateTime(1998, 01, 21), (MyEnum.gender)r.Next(0, 2), i.ToString("054-2345701"), temp_address,
                    (MyEnum.carType)r.Next(1, 5), (MyEnum.gear)r.Next(0, 2), "Hermon", names[r.Next(0, 11)]
                    , r.Next(10, 80),"1234");
                temp_address.city = cities[r.Next(0, 6)];
                temp_address.houseNumber = r.Next(1, 120);
                temp_address.streetName = streets[r.Next(0, 6)];
                bl.AddTester(i.ToString("112581900"), names[r.Next(0, 11)], f_names[r.Next(0, 9)],
                    new DateTime(1975, 01, 21), (MyEnum.gender)r.Next(0, 2), i.ToString("053-2345701")
                    ,temp_address,10,30, (MyEnum.carType)r.Next(1, 5),work_all_time,70,"1234");
                foreach (var item in bl.GetAllTrainees())
                {
                    item.Password = "1234";
                }
                foreach (var item in bl.GetAllTesters())
                {
                    item.Password = "1234";
                }
            }
            DemoModeButton.IsEnabled = false;
            //-----------------------------------------------
            //------------For the Demo We made "Fake TESTS SHOW YOU SOME CHANGES"----------
            Test test1 = new Test(bl.GetAllTesters().First().Id, bl.GetAllTrainees().First().Id, new DateTime(2019, 01, 10, 9, 0, 0), (bl.GetAllTesters().First().MyAddress));
            Idal fake_dal = Factory_dal.getDal();
            fake_dal.AddTest(test1);
        }

        private void ShowTestersButton_Click(object sender, RoutedEventArgs e)
        {
            TesterViweWindow testerViweWindow = new TesterViweWindow();
            testerViweWindow.ShowDialog();

        }

        private void showTraineesButton_Click(object sender, RoutedEventArgs e)
        {
            TraineeViweWindow traineeViwe = new TraineeViweWindow();
            traineeViwe.ShowDialog();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource testerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testerViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // testerViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource traineeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("traineeViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // traineeViewSource.Source = [generic data source]
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            PassedTodayListView.DataContext = bl.passedToday();
        }
    }
}
