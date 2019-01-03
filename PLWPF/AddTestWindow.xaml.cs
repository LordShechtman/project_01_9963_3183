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
            testDateDatePicker.DisplayDateEnd = DateTime.Now.AddDays(31);
            testDateDatePicker.DisplayDateStart = DateTime.Now;


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
                if (traineeIdTextBox.Text.Length < 8)
                    throw new Exception("The ID you have entered is too short");
                MyBL.AddTest(traineeIdTextBox.Text,my_address,testDateDatePicker.DisplayDate);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }
    }
}
