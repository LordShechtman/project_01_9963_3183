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
        int ListIndex = 0;
        public bool[,] MyworkHours;
        /// <summary>
        /// Set the boolen matrix of Tester weekly work hours
        /// We use this method in the project!!
        /// </summary>
        public void setHoursMatrix()
        {
            
            MyworkHours = new bool[5, 6];
            //Sunday
            MyworkHours[0, 0] =(bool) WorkHoursUC.cb_sun_9.IsChecked;
            MyworkHours[0,1]= (bool)WorkHoursUC.cb_sun_10.IsChecked;
            MyworkHours[0, 2] = (bool)WorkHoursUC.cb_sun_11.IsChecked;
            MyworkHours[0, 3] = (bool)WorkHoursUC.cb_sun_12.IsChecked;
            MyworkHours[0, 4] = (bool)WorkHoursUC.cb_sun_13.IsChecked;
            MyworkHours[0, 5] = (bool)WorkHoursUC.cb_sun_14.IsChecked;
            //Monday
            MyworkHours[1, 0] = (bool)WorkHoursUC.cb_mon_9.IsChecked;
            MyworkHours[1, 1] = (bool)WorkHoursUC.cb_mon_10.IsChecked;
            MyworkHours[1, 2] = (bool)WorkHoursUC.cb_mon_11.IsChecked;
            MyworkHours[1, 3] = (bool)WorkHoursUC.cb_mon_12.IsChecked;
            MyworkHours[1, 4] = (bool)WorkHoursUC.cb_mon_13.IsChecked;
            MyworkHours[1, 5] = (bool)WorkHoursUC.cb_mon_14.IsChecked;
            //TUE
            MyworkHours[2, 0] = (bool)WorkHoursUC.cb_tue_9.IsChecked;
            MyworkHours[2, 1] = (bool)WorkHoursUC.cb_tue_10.IsChecked;
            MyworkHours[2, 2] = (bool)WorkHoursUC.cb_tue_11.IsChecked;
            MyworkHours[2, 3] = (bool)WorkHoursUC.cb_tue_12.IsChecked;
            MyworkHours[2, 4] = (bool)WorkHoursUC.cb_tue_13.IsChecked;
            MyworkHours[2, 5] = (bool)WorkHoursUC.cb_tue_14.IsChecked;
            //Wed
            MyworkHours[3, 0] = (bool)WorkHoursUC.cb_wed_9.IsChecked;
            MyworkHours[3, 1] = (bool)WorkHoursUC.cb_wed_10.IsChecked;
            MyworkHours[3, 2] = (bool)WorkHoursUC.cb_wed_11.IsChecked;
            MyworkHours[3, 3] = (bool)WorkHoursUC.cb_wed_12.IsChecked;
            MyworkHours[3, 4] = (bool)WorkHoursUC.cb_wed_13.IsChecked;
            MyworkHours[3, 5] = (bool)WorkHoursUC.cb_wed_14.IsChecked;
            //Thu
            MyworkHours[4, 0] = (bool)WorkHoursUC.cb_thu_9.IsChecked;
            MyworkHours[4, 1] = (bool)WorkHoursUC.cb_thu_10.IsChecked;
            MyworkHours[4, 2] = (bool)WorkHoursUC.cb_thu_11.IsChecked;
            MyworkHours[4, 3] = (bool)WorkHoursUC.cb_thu_12.IsChecked;
            MyworkHours[4, 4] = (bool)WorkHoursUC.cb_thu_13.IsChecked;
            MyworkHours[4, 5] = (bool)WorkHoursUC.cb_thu_14.IsChecked;



        }
        public void ShowMatrixbyIndex()
        {
            //SUN
            MyworkHours = new bool[5, 7];
            MyworkHours = bL.GetAllTesters()[ListIndex].WorkHours;
             WorkHoursUC.cb_sun_9.IsChecked = MyworkHours[0, 0];
             WorkHoursUC.cb_sun_10.IsChecked = MyworkHours[0, 1];
             WorkHoursUC.cb_sun_11.IsChecked= MyworkHours[0, 2];
            WorkHoursUC.cb_sun_12.IsChecked= MyworkHours[0, 3];
            WorkHoursUC.cb_sun_13.IsChecked= MyworkHours[0, 4];
            WorkHoursUC.cb_sun_14.IsChecked = MyworkHours[0, 5];
            //Monday
            WorkHoursUC.cb_mon_9.IsChecked = MyworkHours[1, 0];
            WorkHoursUC.cb_mon_10.IsChecked = MyworkHours[1, 1];
            WorkHoursUC.cb_mon_11.IsChecked= MyworkHours[1, 2];
            WorkHoursUC.cb_mon_12.IsChecked= MyworkHours[1, 3];
            WorkHoursUC.cb_mon_13.IsChecked = MyworkHours[1, 4];
            WorkHoursUC.cb_mon_14.IsChecked= MyworkHours[1, 5];
            //TUE
              WorkHoursUC.cb_tue_9.IsChecked = MyworkHours[2, 0];
              WorkHoursUC.cb_tue_10.IsChecked=MyworkHours[2, 1];
              WorkHoursUC.cb_tue_11.IsChecked=MyworkHours[2, 2];
              WorkHoursUC.cb_tue_12.IsChecked=MyworkHours[2, 3];
              WorkHoursUC.cb_tue_13.IsChecked=MyworkHours[2, 4];
              WorkHoursUC.cb_tue_14.IsChecked=MyworkHours[2, 5];
            //Wed
            WorkHoursUC.cb_wed_9.IsChecked = MyworkHours[3, 0];
            WorkHoursUC.cb_wed_10.IsChecked = MyworkHours[3, 1];
            WorkHoursUC.cb_wed_11.IsChecked = MyworkHours[3, 2];
            WorkHoursUC.cb_wed_12.IsChecked = MyworkHours[3, 3];
            WorkHoursUC.cb_wed_13.IsChecked = MyworkHours[3, 4];
            WorkHoursUC.cb_wed_14.IsChecked = MyworkHours[3, 5];
            //Thu
            WorkHoursUC.cb_thu_9.IsChecked = MyworkHours[4, 0];
            WorkHoursUC.cb_thu_10.IsChecked = MyworkHours[4, 1];
            WorkHoursUC.cb_thu_11.IsChecked = MyworkHours[4, 2];
            WorkHoursUC.cb_thu_12.IsChecked = MyworkHours[4, 3];
            WorkHoursUC.cb_thu_13.IsChecked = MyworkHours[4, 4];
            WorkHoursUC.cb_thu_14.IsChecked = MyworkHours[4, 5];
        }
       public void  clearFileds()
        {
            ListIndex = 0;
            myPasswordBox.Password = null;
            grid1.DataContext = null;
            idTextBox.Text = null;
            nameTextBox.Text = null;
            familyNameTextBox.Text = null;
            myGenderComboBox.Text = null;
            maxDistanceTextBox.Text = null;
            birthDateDatePicker.Text = null;
            expiranceCarComboBox.Text = null;
            maxTestsPerWeekTextBox.Text = null;
            yearsOfExperienceTextBox.Text = null;
            phoneNumberTextBox.Text = null;
            phoneNumerCombobox.Text = null;
            WorkHoursUC.cb_sun_9.IsChecked = false;
           WorkHoursUC.cb_sun_10.IsChecked = false;
            WorkHoursUC.cb_sun_11.IsChecked = false;
            WorkHoursUC.cb_sun_12.IsChecked = false;
           WorkHoursUC.cb_sun_13.IsChecked = false;
            WorkHoursUC.cb_sun_14.IsChecked=false;
            //Monday
            WorkHoursUC.cb_mon_9.IsChecked=false;
            WorkHoursUC.cb_mon_10.IsChecked=false;
            WorkHoursUC.cb_mon_11.IsChecked=false;
           WorkHoursUC.cb_mon_12.IsChecked=false;
           WorkHoursUC.cb_mon_13.IsChecked=false;
          WorkHoursUC.cb_mon_14.IsChecked=false;
            //TUE
          WorkHoursUC.cb_tue_9.IsChecked=false;
           WorkHoursUC.cb_tue_10.IsChecked = false;
          WorkHoursUC.cb_tue_11.IsChecked = false;
          WorkHoursUC.cb_tue_12.IsChecked = false;
            WorkHoursUC.cb_tue_13.IsChecked = false;
           WorkHoursUC.cb_tue_14.IsChecked = false;
            //Wed
           WorkHoursUC.cb_wed_9.IsChecked=false;
           WorkHoursUC.cb_wed_10.IsChecked = false;
           WorkHoursUC.cb_wed_11.IsChecked = false;
           WorkHoursUC.cb_wed_12.IsChecked=false;
           WorkHoursUC.cb_wed_13.IsChecked = false;
           WorkHoursUC.cb_wed_14.IsChecked = false;
            //The
            WorkHoursUC.cb_thu_9.IsChecked = false;
            WorkHoursUC.cb_thu_10.IsChecked = false;
            WorkHoursUC.cb_thu_11.IsChecked = false;
           WorkHoursUC.cb_thu_12.IsChecked = false;
            WorkHoursUC.cb_thu_13.IsChecked = false;
           WorkHoursUC.cb_thu_14.IsChecked = false;

        }
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
        public void getPhoneprefix()
        {
            phoneNumerCombobox.Text = bL.GetAllTesters()[ListIndex].PhonePrefix;
            

            }
            public void getPhonesuffix()
        {
            string suffix=bL.GetAllTesters()[ListIndex].PhoneNumber;
  
            if (suffix[3] == '-')
            {
                phoneNumberTextBox.Text = null;
                for (int i = 4; i < suffix.Length; i++)
                    phoneNumberTextBox.Text += suffix[i];
            }
            else
            {
                for (int i = 3; i < suffix.Length; i++)
                    phoneNumberTextBox.Text += suffix[i];
            }

            
        }
       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource testerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testerViewSource")));
           
        }

        
        #region buttons Checked
        private void DeleteBoutton_Checked(object sender, RoutedEventArgs e)
        {
            ListIndex = 0;
            if (bL.GetAllTesters().Any())
            {
                grid1.DataContext = bL.GetAllTesters()[0];
                ShowMatrixbyIndex();
                CityTB.Text = bL.GetAllTesters()[ListIndex].MyAddress.city;
                HoustNumTB.Text = bL.GetAllTesters()[ListIndex].MyAddress.houseNumber.ToString();
                StreetNameTB.Text = bL.GetAllTesters()[ListIndex].MyAddress.streetName;
                getPhoneprefix();
                getPhonesuffix();
                myGenderComboBox.Text = bL.GetAllTesters()[ListIndex].MyGender.ToString();
                expiranceCarComboBox.Text= bL.GetAllTesters()[ListIndex].ExpiranceCar.ToString();

            }
            SearchGrid.Visibility = Visibility.Visible;
            NextButton.Visibility = Visibility.Visible;
            PreButton.Visibility = Visibility.Visible;
            myPasswordBox.IsEnabled = false;
            grid1.IsEnabled = false;
            WorkHoursUC.IsEnabled = false;
            AddressGrid.IsEnabled = false;
            phoneNumerCombobox.IsEnabled = true;
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
            FinishButton.Content = "Delete";
        }
        private void UpdateButton_Checked(object sender, RoutedEventArgs e)
        {
            grid1.IsEnabled = true;
            WorkHoursUC.IsEnabled = true;
            AddressGrid.IsEnabled = true;
            SearchGrid.Visibility = Visibility.Visible;
            NextButton.Visibility = Visibility.Visible;
            PreButton.Visibility = Visibility.Visible;
            myPasswordBox.IsEnabled = true;
            if (bL.GetAllTesters().Any())
            {
                grid1.DataContext = bL.GetAllTesters()[0];
                ShowMatrixbyIndex();
                CityTB.Text = bL.GetAllTesters()[ListIndex].MyAddress.city;
                HoustNumTB.Text = bL.GetAllTesters()[ListIndex].MyAddress.houseNumber.ToString();
                StreetNameTB.Text = bL.GetAllTesters()[ListIndex].MyAddress.streetName;
                 getPhoneprefix();
                getPhonesuffix();
                myGenderComboBox.Text = bL.GetAllTesters()[ListIndex].MyGender.ToString();
                expiranceCarComboBox.Text = bL.GetAllTesters()[ListIndex].ExpiranceCar.ToString();


            }
            DeleteBoutton.IsChecked = false;
            AddBoutton.IsChecked = false;
            FinishButton.Content = "Update \n Changes";
            idTextBox.IsEnabled = false;
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
        
        private void AddBoutton_Checked(object sender, RoutedEventArgs e)
        {
            DeleteBoutton.IsChecked = false;
            UpdateButton.IsChecked = false;
            grid1.IsEnabled = true;
            WorkHoursUC.IsEnabled = true;
            AddressGrid.IsEnabled = true;
            phoneNumerCombobox.IsEnabled = true;
            grid1.DataContext = null;
            myPasswordBox.IsEnabled = true;
            clearFileds();
            SearchGrid.Visibility = Visibility.Hidden;
            NextButton.Visibility = Visibility.Hidden;
            PreButton.Visibility = Visibility.Hidden;
            FinishButton.Content = "Save";
        }
        #endregion

        #region Numeric input
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
        private void HoustNumTB_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
             || (e.Key >= Key.D0 && e.Key <= Key.D9));
        }
        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                || (e.Key >= Key.D0 && e.Key <= Key.D9));
        }
        #endregion

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
                    if (nameTextBox.Text == "")
                        throw new Exception("Plese enter your name!!");
                    if (familyNameTextBox.Text == "")
                        throw new Exception("Plese enter your family name!!");
                    if (birthDateDatePicker.Text == "")
                        throw new Exception("please choose birth date!!");
                    if (phoneNumberTextBox.Text == "" && phoneNumerCombobox.Text == "")
                        throw new Exception("please enter your phone number");
                    if (expiranceCarComboBox.Text == "")
                        throw new Exception("please enter your car type you expert on");
                    if (maxDistanceTextBox.Text == "")
                        throw new Exception("please enter the maximum distance form \n your house  for test");
                    if (yearsOfExperienceTextBox.Text == "")
                        throw new Exception("אנא הכנס את מספר שנות הנסיון שלך");
                    if (maxTestsPerWeekTextBox.Text == "")
                        throw new Exception("please enter how many tests you can do in a week");
                    setHoursMatrix();
                    m_address.city = CityTB.Text;
                    m_address.streetName = StreetNameTB.Text;
                    m_address.houseNumber = int.Parse(HoustNumTB.Text);
                    bL.AddTester(idTextBox.Text, nameTextBox.Text, familyNameTextBox.Text,
                        (DateTime)birthDateDatePicker.SelectedDate, (MyEnum.gender)Enum.Parse(typeof(MyEnum.gender), myGenderComboBox.Text
                        ), phoneNumerCombobox.Text + phoneNumberTextBox.Text, m_address
                        , int.Parse(yearsOfExperienceTextBox.Text)
                       , int.Parse(maxTestsPerWeekTextBox.Text)
                       , (MyEnum.carType)Enum.Parse(typeof(MyEnum.carType), expiranceCarComboBox.Text), MyworkHours,
                        int.Parse(maxDistanceTextBox.Text));
                    MessageBox.Show("THe tester " + idTextBox.Text + " added to the data base");
                    if(myPasswordBox.Password!=null)
                    bL.SetTesterpassword(idTextBox.Text, myPasswordBox.Password);
                    else
                        bL.SetTesterpassword(idTextBox.Text, "1234");
                }
                if (DeleteBoutton.IsChecked == true)
                {
                    if (idTextBox.Text.Length < 8)
                        throw new Exception("ID must contain at least 8 dights ");
                    bL.DeleteTester(idTextBox.Text);
                    MessageBox.Show("THe tester " + idTextBox.Text + " Removed from the data base");
                }
                if (UpdateButton.IsChecked == true)
                {
                   
                   
                        if (idTextBox.Text.Length < 8)
                            throw new Exception("Id Must contin at least 8 dighits!!");
                        if (birthDateDatePicker.Text == "")
                            throw new Exception("Plese choose birth date!!");
                        if (nameTextBox.Text == "")
                            throw new Exception("Plese enter your name!!");
                        if (familyNameTextBox.Text == "")
                            throw new Exception("Plese enter your family name!!");
                        if (birthDateDatePicker.Text == "")
                            throw new Exception("please choose birth date!!");
                        if (phoneNumberTextBox.Text == "" && phoneNumerCombobox.Text == "")
                            throw new Exception("please enter your phone number");
                        if (expiranceCarComboBox.Text == "")
                            throw new Exception("please enter your car type you expert on");
                        if (maxDistanceTextBox.Text == "")
                            throw new Exception("please enter the maximum distance form \n your house  for test");
                        if (yearsOfExperienceTextBox.Text == "")
                            throw new Exception("אנא הכנס את מספר שנות הנסיון שלך");
                        if (maxTestsPerWeekTextBox.Text == "")
                            throw new Exception("please enter how many tests you can do in a week");
                        setHoursMatrix();
                        m_address.city = CityTB.Text;
                    m_address.streetName = StreetNameTB.Text;
                        m_address.houseNumber =int.Parse( HoustNumTB.Text);
                       
                        bL.UpdateTester(new Tester(idTextBox.Text, nameTextBox.Text, familyNameTextBox.Text,
                           (DateTime)birthDateDatePicker.SelectedDate, (MyEnum.gender)Enum.Parse(typeof(MyEnum.gender), myGenderComboBox.Text
                            ), phoneNumerCombobox.Text + phoneNumberTextBox.Text, m_address
                            , int.Parse(yearsOfExperienceTextBox.Text)
                           , int.Parse(maxTestsPerWeekTextBox.Text)
                           , (MyEnum.carType)Enum.Parse(typeof(MyEnum.carType), expiranceCarComboBox.Text),
                            int.Parse(maxDistanceTextBox.Text), MyworkHours));
                    MessageBox.Show("Update succeeded");
                    if (myPasswordBox.Password != null)
                        bL.SetTesterpassword(idTextBox.Text, myPasswordBox.Password);
                }
                    clearFileds();
                AddBoutton.IsChecked = true;
                DeleteBoutton.IsChecked = false;
                UpdateButton.IsChecked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        

        private void phoneNumerCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        

        #region Search Bouttons Click events
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if(ListIndex<bL.GetAllTesters().Count-1)
            {
                ListIndex++;
                grid1.DataContext = bL.GetAllTesters()[ListIndex];
                CityTB.Text = bL.GetAllTesters()[ListIndex].MyAddress.city;
                HoustNumTB.Text = bL.GetAllTesters()[ListIndex].MyAddress.houseNumber.ToString();
               StreetNameTB.Text = bL.GetAllTesters()[ListIndex].MyAddress.streetName;
                ShowMatrixbyIndex();
                getPhoneprefix();
                getPhonesuffix();
                myGenderComboBox.Text = bL.GetAllTesters()[ListIndex].MyGender.ToString();
                expiranceCarComboBox.Text = bL.GetAllTesters()[ListIndex].ExpiranceCar.ToString();
            }
        }

        private void PreButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListIndex >0)
            {
                ListIndex--;
                grid1.DataContext = bL.GetAllTesters()[ListIndex];
                CityTB.Text = bL.GetAllTesters()[ListIndex].MyAddress.city;
                HoustNumTB.Text = bL.GetAllTesters()[ListIndex].MyAddress.houseNumber.ToString();
                StreetNameTB.Text = bL.GetAllTesters()[ListIndex].MyAddress.streetName;
                ShowMatrixbyIndex();
                getPhoneprefix();
                getPhonesuffix();
                myGenderComboBox.Text = bL.GetAllTesters()[ListIndex].MyGender.ToString();
                expiranceCarComboBox.Text = bL.GetAllTesters()[ListIndex].ExpiranceCar.ToString();
            }

        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Tester isFound=bL.GetAllTesters().Find(x=>x.Id==SearchTextBox.Text);
            int FindIndex = bL.GetAllTesters().IndexOf(isFound);
            if (FindIndex!=-1)
            {
                ListIndex = FindIndex;
                grid1.DataContext = bL.GetAllTesters()[0];
                ShowMatrixbyIndex();
                CityTB.Text = bL.GetAllTesters()[ListIndex].MyAddress.city;
                HoustNumTB.Text = bL.GetAllTesters()[ListIndex].MyAddress.houseNumber.ToString();
                StreetNameTB.Text = bL.GetAllTesters()[ListIndex].MyAddress.streetName;
                getPhoneprefix();
                getPhonesuffix();
                myGenderComboBox.Text = bL.GetAllTesters()[ListIndex].MyGender.ToString();
                expiranceCarComboBox.Text = bL.GetAllTesters()[ListIndex].ExpiranceCar.ToString();
            }
        }

        #endregion

        
    }

}
