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
namespace ListviewFrom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public void setMet(bool[,] mat)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if ((j + 2) % 2 == 0)
                        mat[i, j] = true;
                    else
                        mat[i, j] = false;
                }
            }
        }
        IBL bl;
        public MainWindow()
        {
            InitializeComponent();
           bl = Factory_BL.getBL();
            bool[,] mat = new bool[5, 7];
            setMet(mat);
            Address a1, a2;
            a1.city = "Hifa"; a1.streetName = "Nosh"; a1.houseNumber = 4;
            a2.city = "Jersalem"; a2.streetName = "Dag"; a2.houseNumber = 6;
            try
            {
                bl.AddTester("111111111", "Nadv", "Rabinovich", new DateTime(1970, 3, 11), gender.male, "0528434091", a1, 20, 20, carType.privateCar, mat, 50);
                bl.AddTester(new Tester("111111123", "Ester", "Malka", new DateTime(1972, 4, 13), gender.female, "0528434093", a2, 20, 20, carType.privateCar, 50, mat));
                bl.AddTrainee("312589963", "Rafi", "Lev", new DateTime(1990, 3, 11), gender.male, "0525525224", a1, carType.privateCar, gear.manual, "Hermon", "Yossi", 30);
                bl.AddTrainee(new Trainee("312589964", "Noam", "Getz", new DateTime(1994, 6, 14), gender.male, "0525525224", a2, carType.privateCar, gear.manual, "Hermon", "Yossi", 28));
                bl.AddTrainee("312589967", "Tal", "Madmon", new DateTime(1990, 3, 11), gender.male, "0525525223", a1, carType.privateCar, gear.manual, "Ways", "Efi", 30);
                bl.AddTest("312589963", a1, new DateTime(2018, 12, 31, 13, 0, 0));
                bl.AddTest("312589963", a1, new DateTime(2019, 1, 7, 13, 0, 0));
                string str = "";

                foreach (var v in bl.GetAllTrainees())
                {
                    str += (v.Id + " " + v.Name + " " + v.FamilyName);
                    str += "\n";
                }
                MainLabel.Content = "Trainees:";
                showTestBox.Text = str;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }

        private void showTestBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str = "";
            foreach (var x in bl.GetAllTesters())
            {
                str += x.Id + " " + x.Name + x.FamilyName + " " + x.MaxDistance;
                str += "\n";
            }
            MainLabel.Content = "Testers:";
            showTestBox.Text = str;
        }

        private void showTest_Click(object sender, RoutedEventArgs e)
        {
            if (bl.GetAllTests().Count == 0)
            {
                
                showTestBox.Text.Insert (0,"Empty List Please try Again!!");
                showTestBox.Foreground = Brushes.OrangeRed;

            }
            string str = "";

            foreach (var v in bl.GetAllTests())
            {
                str += ("Tester ID:"+v.TesterId+" "+"STUDENT ID:"+v.TraineeId +"\n"+"Date:"+v.TestDate);
                str += "\n";
            }
            MainLabel.Content = "Test:";
            showTestBox.Text = str;
        }
    }
}
