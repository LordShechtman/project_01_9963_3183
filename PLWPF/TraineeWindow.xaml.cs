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
using BL;
using BE;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for TrinneWindow.xaml
    /// </summary>
    public partial class TrinneWindow : Window
    {
        public IBL My_bl;
        public TrinneWindow()
        {
            InitializeComponent();
            My_bl = BL.Factory_BL.getBL();
            grid1.DataContext = My_bl.GetAllTrainees(); ;
            brithDateDatePicker.DisplayDateEnd = DateTime.Now.AddYears(BE.Configuration.Trainee_MIN_AGE* -1);
            myGenderTextBox.DataContext = Enum.GetNames(typeof(BE.MyEnum.gender)).ToList();
            carTextBox.DataContext = Enum.GetNames(typeof(BE.MyEnum.carType));
            myGearTextBox.DataContext = Enum.GetNames(typeof(BE.MyEnum.gear));
            foreach (var item in BE.Configuration.phoneNumberPrefixes)
                PhoneNumberPrirtyComboBox.Items.Add(item);
            AddRbutton.IsChecked = true;
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource traineeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("traineeViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // traineeViewSource.Source = [generic data source]
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

        private void numberOfLessonsTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
               || (e.Key >= Key.D0 && e.Key <= Key.D9));
        }
        private void HouseNumberTB_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                           || (e.Key >= Key.D0 && e.Key <= Key.D9));
        }
        

        private void FinshButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BE.Address Myaddress;
                Predicate<Trainee> isfound=(Trainee x)=>{ return x.Id==idTextBox.Text ; };
                if (AddRbutton.IsChecked == true)
                {
                    if (idTextBox.Text.Length < 8)
                        throw new Exception("Id Must contin at least 8 dighits!!");
                    if (brithDateDatePicker.Text == "")
                        throw new Exception("please choose birth date!!");
                    if (nameTextBox.Text == "")
                        throw new Exception("please enter your name!!");
                    if (familyNameTextBox.Text == "")
                        throw new Exception("please enter your family name!!");
                    if (phoneNumberTextBox.Text == "" && PhoneNumberPrirtyComboBox.Text == "")
                        throw new Exception("please enter your phone number");
                    if(PhoneNumberPrirtyComboBox.Text=="")
                        throw new Exception("please enter your phone number Perfix!!");
                    if (phoneNumberTextBox.Text.Length < 7)
                        throw new Exception("The phone number you enterd is too short!!");
                    if (carTextBox.Text=="")
                        throw new Exception("please enter your car type lisence request!!");
                    if(numberOfLessonsTextBox.Text=="")
                        throw new Exception("please enter the number of lessons you have made!!");
                    if(myGearTextBox.Text=="")
                        throw new Exception("please enter your Driving teacher Car gear type!!");
                    if(teacherNameTextBox.Text=="")
                        throw new Exception("please enter your Driving teacher name!!");
                    if (schoolTextBox.Text == "")
                        throw new Exception("please enter your Driving school name!!");
                    Myaddress.city = CityTB.Text;
                    Myaddress.houseNumber = int.Parse(HouseNumberTB.Text);
                    Myaddress.streetName = StreetNameTB.Text;
                    My_bl.AddTrainee(idTextBox.Text, nameTextBox.Text, familyNameTextBox.Text
                        , brithDateDatePicker.DisplayDate,
                        (MyEnum.gender)Enum.Parse(typeof(MyEnum.gender), myGenderTextBox.Text), 
                        (PhoneNumberPrirtyComboBox.Text + phoneNumberTextBox.Text)
                        ,Myaddress,(MyEnum.carType)Enum.Parse (typeof(MyEnum.carType),carTextBox.Text)
                        ,(MyEnum.gear)Enum.Parse(typeof(MyEnum.gear) ,myGearTextBox.Text), schoolTextBox.Text,
                        teacherNameTextBox.Text,int.Parse(numberOfLessonsTextBox.Text));
                    if (My_bl.GetAllTrainees().Find(isfound) != null)
                        MessageBox.Show("The Trainee " + idTextBox.Text + "Was added to the data base");
                    
                    
                }
                if (DeleteRButton.IsChecked == true)
                {
                    My_bl.DeleteTrainee(idTextBox.Text);
                    if (My_bl.GetAllTrainees().Find(isfound) == null)
                        MessageBox.Show("The Trainee" + idTextBox.Text + "Was removed from the data base");
                }
                if(UpdateRButton.IsChecked==true)
                {
                    if (idTextBox.Text.Length < 8)
                        throw new Exception("Id Must contin at least 8 dighits!!");
                    if (brithDateDatePicker.Text == "")
                        throw new Exception("please choose birth date!!");
                    if (nameTextBox.Text == "")
                        throw new Exception("please enter your name!!");
                    if (familyNameTextBox.Text == "")
                        throw new Exception("please enter your family name!!");
                    if (phoneNumberTextBox.Text == "" && PhoneNumberPrirtyComboBox.Text == "")
                        throw new Exception("please enter your phone number");
                    if (PhoneNumberPrirtyComboBox.Text == "")
                        throw new Exception("please enter your phone number Perfix!!");
                    if (phoneNumberTextBox.Text.Length < 7)
                        throw new Exception("The phone number you enterd is too short!!");
                    if (carTextBox.Text == "")
                        throw new Exception("please enter your car type lisence request!!");
                    if (numberOfLessonsTextBox.Text == "")
                        throw new Exception("please enter the number of lessons you have made!!");
                    if (myGearTextBox.Text == "")
                        throw new Exception("please enter your Driving teacher Car gear type!!");
                    if (teacherNameTextBox.Text == "")
                        throw new Exception("please enter your Driving teacher name!!");
                    if (schoolTextBox.Text == "")
                        throw new Exception("please enter your Driving school name!!");
                    Myaddress.city = CityTB.Text;
                    Myaddress.houseNumber = int.Parse(HouseNumberTB.Text);
                    Myaddress.streetName = StreetNameTB.Text;
                    My_bl.UpdateTrainee(new Trainee (idTextBox.Text, nameTextBox.Text, familyNameTextBox.Text
                        , brithDateDatePicker.DisplayDate,
                        (MyEnum.gender)Enum.Parse(typeof(MyEnum.gender), myGenderTextBox.Text),
                        (PhoneNumberPrirtyComboBox.Text + phoneNumberTextBox.Text)
                        , Myaddress, (MyEnum.carType)Enum.Parse(typeof(MyEnum.carType), carTextBox.Text)
                        , (MyEnum.gear)Enum.Parse(typeof(MyEnum.gear), myGearTextBox.Text), schoolTextBox.Text,
                        teacherNameTextBox.Text, int.Parse(numberOfLessonsTextBox.Text)));
                    if (My_bl.GetAllTrainees().Find(isfound) != null)
                        MessageBox.Show("Update succeeded");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #region Radio Buttons event
        private void AddRbutton_Checked(object sender, RoutedEventArgs e)
        {
            idTextBox.IsEnabled = true;
            DeleteRButton.IsChecked = false;
            UpdateRButton.IsChecked = false;
            nameTextBox.IsEnabled = true;
            familyNameTextBox.IsEnabled = true;
            brithDateDatePicker.IsEnabled = true;
            carTextBox.IsEnabled = true;
            phoneNumberTextBox.IsEnabled = true;
            myGearTextBox.IsEnabled = true;
            myGearTextBox.IsEnabled = true;
            numberOfLessonsTextBox.IsEnabled = true;
            schoolTextBox.IsEnabled = true;
            teacherNameTextBox.IsEnabled = true;

        }
        private void DeleteRButton_Checked(object sender, RoutedEventArgs e)
        {
            idTextBox.IsEnabled = true;
            AddRbutton.IsChecked = false;
            UpdateRButton.IsChecked = false;
            nameTextBox.IsEnabled = false;
            familyNameTextBox.IsEnabled = false;
            brithDateDatePicker.IsEnabled = false;
            carTextBox.IsEnabled = false;
            phoneNumberTextBox.IsEnabled =false;
            myGearTextBox.IsEnabled = false;
            myGearTextBox.IsEnabled = false;
            numberOfLessonsTextBox.IsEnabled = false;
            schoolTextBox.IsEnabled = false;
            teacherNameTextBox.IsEnabled = false;
        }

        private void UpdateRButton_Checked(object sender, RoutedEventArgs e)
        {
            AddRbutton.IsChecked = false;
            DeleteRButton.IsChecked = false;
            idTextBox.IsEnabled = false;
            nameTextBox.IsEnabled = true;
            familyNameTextBox.IsEnabled = true;
            brithDateDatePicker.IsEnabled = true;
            carTextBox.IsEnabled = true;
            phoneNumberTextBox.IsEnabled = true;
            myGearTextBox.IsEnabled = true;
            myGearTextBox.IsEnabled = true;
            numberOfLessonsTextBox.IsEnabled = true;
            schoolTextBox.IsEnabled = true;
            teacherNameTextBox.IsEnabled = true;
        }
        #endregion

    }
}
