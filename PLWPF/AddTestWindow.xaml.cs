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
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddTestWindow.xaml
    /// </summary>
    public partial class AddTestWindow : Window
    {
        public void clearFileds()
        {
            traineeIdTB.Text = null;
            CityTextBox.Text = "City";
            StreetNameTextBox.Text = "Street";
            HouseNumberTextBox.Text = null;
            traineeIdTB.IsEnabled = true;
            CityTextBox.IsEnabled = false;
            StreetNameTextBox.IsEnabled = false;
            HouseNumberTextBox.IsEnabled = false;

            Singup.Content = "New  trainee ? ";
            SingUpWindow.Visibility = Visibility.Visible;
            YourPasswordBox.Visibility = Visibility.Hidden;
            testDateDatePicker.DisplayDateEnd = DateTime.Now.AddDays(31);
            testDateDatePicker.DisplayDateStart = DateTime.Now;
            testDateDatePicker.IsEnabled = false;
            CityTextBox.IsEnabled = false;
            HourCB.IsEnabled = false;
        }
        public IBL MyBL;
        public AddTestWindow()
        {

            InitializeComponent();
            MyBL = BL.Factory_BL.getBL();
            clearFileds();
            for (int i = 9; i < 15; i++)
                HourCB.Items.Add(i.ToString());


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource testViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // testViewSource.Source = [generic data source]
        }

        private void HouseNumberTextBox_KeyDown(object sender, KeyEventArgs e)
        {

            e.Handled = !((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                || (e.Key >= Key.D0 && e.Key <= Key.D9));
        }

       

        private void CityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void HouseNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BE.Address my_address;
                my_address.city = CityTextBox.Text;
                my_address.streetName = StreetNameTextBox.Text;
                my_address.houseNumber = int.Parse(HouseNumberTextBox.Text);
                if (traineeIdTB.Text=="")
                    throw new Exception("Please select trainee ID");
                if(testDateDatePicker.Text=="")
                    throw new Exception("Please select Date for the test");
                DateTime testDay = (DateTime)testDateDatePicker.SelectedDate;
                testDay = testDay.AddHours(double.Parse(HourCB.Text));



                MyBL.AddTest(traineeIdTB.Text, my_address,testDay);
                BE.Test isfound = MyBL.GetAllTests().Find(x => x.TraineeId == traineeIdTB.Text && x.TestDate == testDay);
                if(isfound!=null)
                {
                    BE.Tester myTester = MyBL.GetAllTesters().Find(x => x.Id == isfound.TesterId);
                    MessageBox.Show("Good Luck: your tester  phone is: " + myTester.PhoneNumber + " " + myTester.Name + " " + myTester.FamilyName,"Test number: "+isfound.TestNumber);
                }
                clearFileds();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
                clearFileds();
                
            }
        }

        private void traineeIdTB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
          

        }

        private void SingUpWindow_Click(object sender, RoutedEventArgs e)
        {
            TrinneWindow trinneWindow = new TrinneWindow();
            trinneWindow.ShowDialog();
        }

        private void CheckPasswordB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BE.Trainee trinne = MyBL.GetAllTrainees().Find(x => x.Id == traineeIdTB.Text);
                if (trinne == null)
                    throw new Exception("Trinne NOT FOUND!!!");
                if (trinne.Password == YourPasswordBox.Password)
                {
                    SingUpWindow.Visibility = Visibility.Hidden;
                    Singup.Visibility = Visibility.Visible;
                    Singup.Content = trinne.Name + " " + trinne.FamilyName;
                    testDateDatePicker.IsEnabled = true;
                    CityTextBox.IsEnabled = true;
                    StreetNameTextBox.IsEnabled = true;
                    HouseNumberTextBox.IsEnabled = true;
                    HourCB.IsEnabled = true;
                    YourPasswordBox.Visibility = Visibility.Hidden;
                    CheckPasswordB.Visibility = Visibility.Hidden;
                    PasswordLB.Visibility = Visibility.Hidden;
                }
                else
                {
                    throw new Exception("Wrong password");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Window_Activated(object sender, EventArgs e)
        {
            
        }

        private void traineeIdTB_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
              || (e.Key >= Key.D0 && e.Key <= Key.D9));
            if (traineeIdTB.Text.Length == 9)
            {
                Singup.Visibility = Visibility.Hidden;
                YourPasswordBox.Visibility = Visibility.Visible;
                YourPasswordBox.IsEnabled = true;
                CheckPasswordB.Visibility = Visibility.Visible;
                SingUpWindow.Visibility = Visibility.Hidden;

                
            }
        }
    }
}
