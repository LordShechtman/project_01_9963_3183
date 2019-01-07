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
        public IBL MyBL;
        public AddTestWindow()
        {
            InitializeComponent();
            MyBL = BL.Factory_BL.getBL();
            if (MyBL.GetAllTrainees().Any())
            {
                foreach (var v in MyBL.GetAllTrainees())
                    traineeIdCB.Items.Add(v.Id);
            }
            YourPasswordBox.Visibility = Visibility.Hidden;
            testDateDatePicker.DisplayDateEnd = DateTime.Now.AddDays(31);
            testDateDatePicker.DisplayDateStart = DateTime.Now;
            testDateDatePicker.IsEnabled = false;
            CityTextBox.IsEnabled = false;
            HourCB.IsEnabled = false;
            for(int i=9;i<15;i++)
            {
                HourCB.Items.Add(i);
            }

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

        private void traineeIdTextBox_KeyDown(object sender, KeyEventArgs e)
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
                if (traineeIdCB.Text=="")
                    throw new Exception("Please select trainee ID");
                if(testDateDatePicker.Text=="")
                    throw new Exception("Please select Date for the test");
                MyBL.AddTest(traineeIdCB.Text, my_address,testDateDatePicker.DisplayDate);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }

        private void traineeIdCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            YourPasswordBox.Visibility = Visibility.Visible;
            SingUpWindow.Visibility = Visibility.Hidden;
            var UserName = MyBL.GetAllTrainees().Find(x => x.Id == traineeIdCB.SelectedItem.ToString());
            Singup.Content = UserName.Name + " " + UserName.FamilyName;


        }

        private void SingUpWindow_Click(object sender, RoutedEventArgs e)
        {
            TrinneWindow trinneWindow = new TrinneWindow();
            trinneWindow.ShowDialog();
        }

        private void CheckPasswordB_Click(object sender, RoutedEventArgs e)
        {
            BE.Trainee trinne = MyBL.GetAllTrainees().Find(x => x.Id == traineeIdCB.Text);
            if(trinne.Password==YourPasswordBox.Password)
            {
                testDateDatePicker.IsEnabled = true;
                CityTextBox.IsEnabled = true;
                HourCB.IsEnabled = true;
                YourPasswordBox.Visibility = Visibility.Hidden;
                CheckPasswordB.Visibility = Visibility.Hidden;
            }

        }
    }
}
