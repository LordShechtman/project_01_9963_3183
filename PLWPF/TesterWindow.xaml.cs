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
using BE;
using BL;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for TesterWindow.xaml
    /// </summary>
    public partial class TesterWindow : Window
    {
        IBL bL;
        
        public bool[,] MyworkHours;
        public TesterWindow()
        {
            InitializeComponent();
            bL = Factory_BL.getBL();
            AddBoutton.IsChecked = true;
            foreach (var v in BE.Configuration.phoneNumberPrefixes)
                phoneNumerCombobox.Items.Add(v);
            birthDateDatePicker.DisplayDateEnd = DateTime.Now.AddYears(BE.Configuration.Tester_MIN_AGE*-1);
            birthDateDatePicker.DisplayDateStart= DateTime.Now.AddYears(BE.Configuration.Maximum_Tester_age * -1);
            expiranceCarComboBox.DataContext = Enum.GetNames(typeof(BE.MyEnum.carType)).ToList();
            
            myGenderComboBox.ItemsSource= Enum.GetValues(typeof(BE.MyEnum.gender));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource testerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testerViewSource")));
           
        }

        private void AddRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            idTextBox.IsEnabled = true;
            nameTextBox.IsEnabled = true;
            familyNameTextBox.IsEnabled = true;

        }

        private void DeleteBoutton_Checked(object sender, RoutedEventArgs e)
        {
            if(bL.GetAllTesters().Any())
            grid1.DataContext = bL.GetAllTesters()[0];
            AddBoutton.IsChecked = false;
            nameTextBox.IsEnabled = false;
            familyNameTextBox.IsEnabled = false;
            phoneNumberTextBox.IsEnabled = false;
            phoneNumerCombobox.IsEnabled = false;
            birthDateDatePicker.IsEnabled = false;
            myGenderComboBox.IsEnabled = false;
            expiranceCarComboBox.IsEnabled = false;
            maxDistanceTextBox.IsEnabled = false;
            maxTestsPerWeekTextBox.IsEnabled = false;
            yearsOfExperienceTextBox.IsEnabled = false;
        }

        private void AddBoutton_Checked(object sender, RoutedEventArgs e)
        {
            
            DeleteBoutton.IsChecked = false;

            UpdateButton.IsChecked = false;
            nameTextBox.IsEnabled = true;
            familyNameTextBox.IsEnabled = true;
            phoneNumerCombobox.IsEnabled = true;
            phoneNumberTextBox.IsEnabled = true;
            birthDateDatePicker.IsEnabled = true;
            myGenderComboBox.IsEnabled = true;
            expiranceCarComboBox.IsEnabled = true;
            maxDistanceTextBox.IsEnabled = true;
            maxTestsPerWeekTextBox.IsEnabled = true;
            yearsOfExperienceTextBox.IsEnabled = true;
        }

        private void idTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
               || (e.Key >= Key.D0 && e.Key <= Key.D9));
        }

        private void phoneNumberTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
               || (e.Key >= Key.D0 && e.Key <= Key.D9));
        }

        private void maxTestsPerWeekTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
               || (e.Key >= Key.D0 && e.Key <= Key.D9));
        }

        private void maxDistanceTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
               || (e.Key >= Key.D0 && e.Key <= Key.D9));
        }

        private void yearsOfExperienceTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
               || (e.Key >= Key.D0 && e.Key <= Key.D9));
        }

        private void UpdateButton_Checked(object sender, RoutedEventArgs e)
        {
            DeleteBoutton.IsChecked = false;
           AddBoutton.IsChecked = false;
            nameTextBox.IsEnabled = true;
            familyNameTextBox.IsEnabled = true;
            phoneNumberTextBox.IsEnabled = true;
            phoneNumerCombobox.IsEnabled = true;
            birthDateDatePicker.IsEnabled = true;
            myGenderComboBox.IsEnabled = true;
            expiranceCarComboBox.IsEnabled = true;
            maxDistanceTextBox.IsEnabled = true;
            maxTestsPerWeekTextBox.IsEnabled = true;
            yearsOfExperienceTextBox.IsEnabled = true;
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Address m_address;
                if (AddBoutton.IsChecked == true)
                {
                    if (idTextBox.Text.Length < 8)
                        throw new Exception("Id Must contin at least 8 dighits!!");
                    if (birthDateDatePicker.Text == "")
                        throw new Exception("Plese choose birth date!!");
                    if(nameTextBox.Text=="")
                        throw new Exception("Plese enter your name!!");
                    if ( familyNameTextBox.Text== "")
                        throw new Exception("Plese enter your family name!!");
                    if (birthDateDatePicker.Text == "")
                        throw new Exception("please choose birth date!!");
                    if (phoneNumberTextBox.Text == "" && phoneNumerCombobox.Text== "")
                        throw new Exception("please enter your phone number");
                    if(expiranceCarComboBox.Text=="")
                        throw new Exception("please enter your car type you expert on");
                    if(maxDistanceTextBox.Text=="")
                        throw new Exception("please enter the maximum distance form \n your house  for test");
                    if (yearsOfExperienceTextBox.Text=="")
                        throw new Exception("אנא הכנס את מספר שנות הנסיון שלך");
                    if(maxTestsPerWeekTextBox.Text=="")
                        throw new Exception("please enter how many tests you can do in a week");
                    m_address.city = myAddressTextBox.Text;
                    m_address.houseNumber = 3;
                    m_address.streetName = "moshe";
                    bL.AddTester(idTextBox.Text, nameTextBox.Text, familyNameTextBox.Text,
                        birthDateDatePicker.DisplayDate, (MyEnum.gender)Enum.Parse(typeof(MyEnum.gender), myGenderComboBox.Text
                        ),phoneNumerCombobox.Text+phoneNumberTextBox.Text,m_address
                        ,int.Parse(yearsOfExperienceTextBox.Text)
                       ,int.Parse(maxTestsPerWeekTextBox.Text)
                       ,(MyEnum.carType)Enum.Parse(typeof(MyEnum.carType),expiranceCarComboBox.Text),MyworkHours,
                        int.Parse(maxDistanceTextBox.Text));

                    
                }   
                if(DeleteBoutton.IsChecked==true)
                {
                    if (idTextBox.Text.Length < 8)
                        throw new Exception("ID must contain at least 8 dights ");
                    bL.DeleteTester(idTextBox.Text);

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void phoneNumerCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
