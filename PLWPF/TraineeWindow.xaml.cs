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
        /// <summary>
        /// This windows manage add/delete and update of students in the Data base
        /// The user uses radio buttons to make his wanted choice
        /// The user also can look in the the date base to find studnet by his\her ID
        /// 
        /// </summary>
        public void clearFileds()
        {
            idTextBox.Text = null;
            nameTextBox.Text = null;
            familyNameTextBox.Text = null;
            brithDateDatePicker.Text = null;
            phoneNumberTextBox.Text = null;
            PhoneNumberPrirtyComboBox.Text = null;
            CityTB.Text = null;
            StreetNameTB.Text = null;
            HouseNumberTB.Text = null;
            myGearTextBox.Text = null;
            myGenderTextBox.Text = null;
            schoolTextBox.Text = null;
            teacherNameTextBox.Text = null;

            numberOfLessonsTextBox.Text = null;
            carTextBox.Text = null;
            passwordBox.Password = null;
        }
        public IBL My_bl;
        int ListIndex = 0;
        public TrinneWindow()
        {
            InitializeComponent();
            My_bl = BL.Factory_BL.getBL();
            NextButton.Visibility = Visibility.Collapsed;
            PreButton.Visibility = Visibility.Collapsed;
            grid1.DataContext = My_bl.GetAllTrainees(); 
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
        #region Numaric input
        /*To avoid input problems we Blocked the option from the user to type letters and chars
         where we need "int" type*/
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
        private void SearchIdTB_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
              || (e.Key >= Key.D0 && e.Key <= Key.D9));
        }
        #endregion
        private void FinshButton_Click(object sender, RoutedEventArgs e)
        {
            /* Finsh button finsh an action (add/delete/update) after the user click it
             the selected radio button will decide which case in "on"*/
            try
            {
                BE.Address Myaddress;
                Predicate<Trainee> isfound=(Trainee x)=>{ return x.Id == idTextBox.Text; };
                if (AddRbutton.IsChecked == true )
                {
                    //ADD CASE (Some input test are in the BL layer)
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
                        throw new Exception("The phone number you entered is too short!!");
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
                    /*Defult password 1234"*/
                    string my_password = passwordBox.Password != null ? passwordBox.Password:"1234";
                    My_bl.AddTrainee(idTextBox.Text, nameTextBox.Text, familyNameTextBox.Text
                        , (DateTime)brithDateDatePicker.SelectedDate,
                        (MyEnum.gender)Enum.Parse(typeof(MyEnum.gender), myGenderTextBox.Text), 
                        (PhoneNumberPrirtyComboBox.Text + phoneNumberTextBox.Text)
                        ,Myaddress,(MyEnum.carType)Enum.Parse (typeof(MyEnum.carType),carTextBox.Text)
                        ,(MyEnum.gear)Enum.Parse(typeof(MyEnum.gear) ,myGearTextBox.Text), schoolTextBox.Text,
                        teacherNameTextBox.Text,int.Parse(numberOfLessonsTextBox.Text),my_password);
                    if (My_bl.GetAllTrainees().Find(isfound) != null)
                        MessageBox.Show("The Trainee " + idTextBox.Text + "\n added to the data base");
                    
                   

                }
                if (DeleteRButton.IsChecked == true)
                {
                    //Delete case(input test is in the BL)
                    My_bl.DeleteTrainee(idTextBox.Text);
                    if (My_bl.GetAllTrainees().Find(isfound) == null)
                        MessageBox.Show("The Trainee " + idTextBox.Text + " Was removed from the data base");
                }
                if(UpdateRButton.IsChecked==true)
                {
                    //Update case: same as adding but without the option to change the 
                    //ID witch our main key
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
                    string my_password = passwordBox.Password != null ? passwordBox.Password : 
                        My_bl.GetAllTrainees().Find(t=>t.Id== idTextBox.Text).Password;
                    My_bl.UpdateTrainee(new Trainee (idTextBox.Text, nameTextBox.Text, familyNameTextBox.Text
                        , (DateTime)brithDateDatePicker.SelectedDate,
                        (MyEnum.gender)Enum.Parse(typeof(MyEnum.gender), myGenderTextBox.Text),
                        (PhoneNumberPrirtyComboBox.Text + phoneNumberTextBox.Text)
                        , Myaddress, (MyEnum.carType)Enum.Parse(typeof(MyEnum.carType), carTextBox.Text)
                        , (MyEnum.gear)Enum.Parse(typeof(MyEnum.gear), myGearTextBox.Text), schoolTextBox.Text,
                        teacherNameTextBox.Text, int.Parse(numberOfLessonsTextBox.Text), my_password));
                   
                    MessageBox.Show("Update succeeded");
                }
                clearFileds();
                AddRbutton.IsChecked = true;
                UpdateRButton.IsChecked = false;
                DeleteRButton.IsChecked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #region Radio Buttonschecked event
        private void AddRbutton_Checked(object sender, RoutedEventArgs e)
        {
            ListIndex = 0;
            grid1.DataContext = null;
            FinshButton.Content = "Save";
            SearchGrid.Visibility = Visibility.Hidden;
            idTextBox.IsEnabled = true;
            NextButton.Visibility = Visibility.Collapsed;
            PreButton.Visibility = Visibility.Collapsed;
            DeleteRButton.IsChecked = false;
            UpdateRButton.IsChecked = false;
            nameTextBox.IsEnabled = true;
            familyNameTextBox.IsEnabled = true;
            brithDateDatePicker.IsEnabled = true;
            carTextBox.IsEnabled = true;
            phoneNumberTextBox.IsEnabled = true;
            PhoneNumberPrirtyComboBox.IsEnabled = true;
            myGearTextBox.IsEnabled = true;
            numberOfLessonsTextBox.IsEnabled = true;
            schoolTextBox.IsEnabled = true;
            teacherNameTextBox.IsEnabled = true;
            clearFileds();




        }
        private void DeleteRButton_Checked(object sender, RoutedEventArgs e)
        {
            FinshButton.Content = "Delete";
            AddRbutton.IsChecked = false;
            UpdateRButton.IsChecked = false;
            SearchGrid.Visibility = Visibility.Visible;
            NextButton.Visibility = Visibility.Visible;
            PreButton.Visibility = Visibility.Visible;
            if (My_bl.GetAllTrainees().Any())
            {
                

                ListIndex = 0;
                grid1.DataContext = My_bl.GetAllTrainees()[0];
                StreetNameTB.Text = My_bl.GetAllTrainees()[ListIndex].MyAddress.streetName;
                HouseNumberTB.Text = My_bl.GetAllTrainees()[ListIndex].MyAddress.houseNumber.ToString();
                myGearTextBox.Text = My_bl.GetAllTrainees()[ListIndex].MyGear.ToString();
                myGenderTextBox.Text = My_bl.GetAllTrainees()[ListIndex].MyGender.ToString();
                carTextBox.Text = My_bl.GetAllTrainees()[ListIndex].Car.ToString();
                CityTB.Text = My_bl.GetAllTrainees()[ListIndex].MyAddress.city;
                PhoneNumberPrirtyComboBox.Text = My_bl.GetAllTrainees()[ListIndex].PhonePrefix;
                phoneNumberTextBox.Text = null;
                string suffix = My_bl.GetAllTrainees()[ListIndex].PhoneNumber;
                if (suffix[3] == '-')
                {
                    for (int i = 4; i < suffix.Length; i++)
                        phoneNumberTextBox.Text += suffix[i];
                }
                else
                {
                    for (int i = 3; i < suffix.Length; i++)
                        phoneNumberTextBox.Text += suffix[i];
                }
              

                idTextBox.IsEnabled = false;
                AddRbutton.IsChecked = false;
                UpdateRButton.IsChecked = false;
                nameTextBox.IsEnabled = false;
                familyNameTextBox.IsEnabled = false;
                brithDateDatePicker.IsEnabled = false;
                carTextBox.IsEnabled = false;
                phoneNumberTextBox.IsEnabled = false;
                PhoneNumberPrirtyComboBox.IsEnabled = false;
                myGearTextBox.IsEnabled = false;
                myGearTextBox.IsEnabled = false;
                numberOfLessonsTextBox.IsEnabled = false;
                schoolTextBox.IsEnabled = false;
                teacherNameTextBox.IsEnabled = false;
            }
        }

        private void UpdateRButton_Checked(object sender, RoutedEventArgs e)
        {
            FinshButton.Content = "Save \n changes";
            AddRbutton.IsChecked = false;
            DeleteRButton.IsChecked = false;
            NextButton.Visibility = Visibility.Visible;
            PreButton.Visibility = Visibility.Visible;
            SearchGrid.Visibility = Visibility.Visible;
            if (My_bl.GetAllTrainees().Any())
            {
                ListIndex = 0;
                
                grid1.DataContext = My_bl.GetAllTrainees()[ListIndex];
                StreetNameTB.Text = My_bl.GetAllTrainees()[ListIndex].MyAddress.streetName;
                HouseNumberTB.Text = My_bl.GetAllTrainees()[ListIndex].MyAddress.houseNumber.ToString();
                CityTB.Text = My_bl.GetAllTrainees()[ListIndex].MyAddress.city;
                myGearTextBox.Text = My_bl.GetAllTrainees()[ListIndex].MyGear.ToString();
                myGenderTextBox.Text = My_bl.GetAllTrainees()[ListIndex].MyGender.ToString();
                carTextBox.Text = My_bl.GetAllTrainees()[ListIndex].Car.ToString();
                PhoneNumberPrirtyComboBox.Text = My_bl.GetAllTrainees()[ListIndex].PhonePrefix;
                phoneNumberTextBox.Text = null;
                string suffix = My_bl.GetAllTrainees()[ListIndex].PhoneNumber;
                if (suffix[3] == '-')
                {
                    for (int i = 4; i < suffix.Length; i++)
                        phoneNumberTextBox.Text += suffix[i];
                }
                else
                {
                    for (int i = 3; i < suffix.Length; i++)
                        phoneNumberTextBox.Text += suffix[i];
                }

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
        }

        #endregion
        #region search tools events
        private void PreButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListIndex > 0)
            {
                ListIndex--;
                grid1.DataContext = My_bl.GetAllTrainees()[ListIndex];
                StreetNameTB.Text = My_bl.GetAllTrainees()[ListIndex].MyAddress.streetName;
                HouseNumberTB.Text = My_bl.GetAllTrainees()[ListIndex].MyAddress.houseNumber.ToString();
                CityTB.Text= My_bl.GetAllTrainees()[ListIndex].MyAddress.city;
                myGearTextBox.Text = My_bl.GetAllTrainees()[ListIndex].MyGear.ToString();
                myGenderTextBox.Text = My_bl.GetAllTrainees()[ListIndex].MyGender.ToString();
                carTextBox.Text = My_bl.GetAllTrainees()[ListIndex].Car.ToString();
                PhoneNumberPrirtyComboBox.Text = My_bl.GetAllTrainees()[ListIndex].PhonePrefix;
                phoneNumberTextBox.Text = null;
                string suffix = My_bl.GetAllTrainees()[ListIndex].PhoneNumber;
                if (suffix[3] == '-')
                {
                    for (int i = 4; i < suffix.Length; i++)
                        phoneNumberTextBox.Text += suffix[i];
                }
                else
                {
                    for (int i = 3; i < suffix.Length; i++)
                        phoneNumberTextBox.Text += suffix[i];
                }

            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListIndex < My_bl.GetAllTrainees().Count() - 1 && My_bl.GetAllTrainees().Any())
            {
                ListIndex++;
                grid1.DataContext = My_bl.GetAllTrainees()[ListIndex];
                StreetNameTB.Text = My_bl.GetAllTrainees()[ListIndex].MyAddress.streetName;
                HouseNumberTB.Text = My_bl.GetAllTrainees()[ListIndex].MyAddress.houseNumber.ToString();
                CityTB.Text = My_bl.GetAllTrainees()[ListIndex].MyAddress.city;
                myGearTextBox.Text = My_bl.GetAllTrainees()[ListIndex].MyGear.ToString();
                myGenderTextBox.Text = My_bl.GetAllTrainees()[ListIndex].MyGender.ToString();
                carTextBox.Text = My_bl.GetAllTrainees()[ListIndex].Car.ToString();
                PhoneNumberPrirtyComboBox.Text = My_bl.GetAllTrainees()[ListIndex].PhonePrefix;
                phoneNumberTextBox.Text = null;
                string suffix = My_bl.GetAllTrainees()[ListIndex].PhoneNumber;
                if (suffix[3] == '-')
                {
                    for (int i = 4; i < suffix.Length; i++)
                        phoneNumberTextBox.Text += suffix[i];
                }
                else
                {
                    for (int i = 3; i < suffix.Length; i++)
                        phoneNumberTextBox.Text += suffix[i];
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SearchIdTB.Text.Length < 8)
                    throw new Exception("The ID you Enter is too short");
                int searchIndex = -1;
                int count = 0;
                foreach (var v in My_bl.GetAllTrainees())
                {
                   
                    if (v.Id == SearchIdTB.Text)
                    {
                        searchIndex = count;
                        break;
                    }
                    count++;
                }
                if (searchIndex == -1)
                    throw new Exception ("Trainee "+SearchIdTB.Text+" Not Found!!!");
                ListIndex = searchIndex;
                grid1.DataContext = My_bl.GetAllTrainees()[ListIndex];
                StreetNameTB.Text = My_bl.GetAllTrainees()[ListIndex].MyAddress.streetName;
                HouseNumberTB.Text = My_bl.GetAllTrainees()[ListIndex].MyAddress.houseNumber.ToString();
                CityTB.Text = My_bl.GetAllTrainees()[ListIndex].MyAddress.city;
                myGearTextBox.Text = My_bl.GetAllTrainees()[ListIndex].MyGear.ToString();
                myGenderTextBox.Text = My_bl.GetAllTrainees()[ListIndex].MyGender.ToString();
                carTextBox.Text = My_bl.GetAllTrainees()[ListIndex].Car.ToString();
                PhoneNumberPrirtyComboBox.Text = My_bl.GetAllTrainees()[ListIndex].PhonePrefix;
                phoneNumberTextBox.Text = null;
                string suffix = My_bl.GetAllTrainees()[ListIndex].PhoneNumber;
                if (suffix[3] == '-')
                {
                    for (int i = 4; i < suffix.Length; i++)
                        phoneNumberTextBox.Text += suffix[i];
                }
                else
                {
                    for (int i = 3; i < suffix.Length; i++)
                        phoneNumberTextBox.Text += suffix[i];
                }





            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR MESSAGE");
            }
        }

        #endregion
        // to Convert the first character
        private void NAMES(object sender, TextChangedEventArgs e)
        {
            TextBox temp = (TextBox)sender;
            if (temp.Text.Length > 1)
            {
                temp.Text = temp.Text[0].ToString().ToUpper() + temp.Text.Substring(1);

            }

            if (temp.Text.Length == 1)
            {
                temp.Text = temp.Text.ToUpper();
            }

            ((TextBox)sender).Text = temp.Text;
            ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
        }
    }
}
