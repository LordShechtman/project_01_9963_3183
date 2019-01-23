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
    /// Interaction logic for TesterViweWindow.xaml
    /// </summary>
    public partial class TesterViweWindow : Window
    {
        public IBL my_bl;
        public TesterViweWindow()
        {
            InitializeComponent();
            my_bl = Factory_BL.getBL();
            testerListView.DataContext = my_bl.GetAllTesters();
            WorkHoursUC.IsEnabled = false;
            DayAndHourBU.Visibility = Visibility.Hidden;
            DayOfWeekCB.IsEnabled = false;
            HourCB.IsEnabled = false;
            for (int i = 9; i < 15; i++)
            { HourCB.Items.Add(i.ToString()); }
            for (int i=0;i<5;i++)
            {
                DayOfWeekCB.Items.Add((DayOfWeek)i);
            }
            LicenceTypeCB.Visibility = Visibility.Hidden;
            NumberOfTestsComboBox.Visibility = Visibility.Hidden;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource testerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testerViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // testerViewSource.Source = [generic data source]
        }

        private void testerListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Index = my_bl.GetAllTesters().IndexOf(((Tester)testerListView.SelectedItem));
            if (Index != -1)
            { 
                bool[,] MyworkHours = my_bl.GetAllTesters()[Index].WorkHours;
            WorkHoursUC.cb_sun_9.IsChecked = MyworkHours[0, 0];
            WorkHoursUC.cb_sun_10.IsChecked = MyworkHours[0, 1];
            WorkHoursUC.cb_sun_11.IsChecked = MyworkHours[0, 2];
            WorkHoursUC.cb_sun_12.IsChecked = MyworkHours[0, 3];
            WorkHoursUC.cb_sun_13.IsChecked = MyworkHours[0, 4];
            WorkHoursUC.cb_sun_14.IsChecked = MyworkHours[0, 5];
            //Monday
            WorkHoursUC.cb_mon_9.IsChecked = MyworkHours[1, 0];
            WorkHoursUC.cb_mon_10.IsChecked = MyworkHours[1, 1];
            WorkHoursUC.cb_mon_11.IsChecked = MyworkHours[1, 2];
            WorkHoursUC.cb_mon_12.IsChecked = MyworkHours[1, 3];
            WorkHoursUC.cb_mon_13.IsChecked = MyworkHours[1, 4];
            WorkHoursUC.cb_mon_14.IsChecked = MyworkHours[1, 5];
            //TUE
            WorkHoursUC.cb_tue_9.IsChecked = MyworkHours[2, 0];
            WorkHoursUC.cb_tue_10.IsChecked = MyworkHours[2, 1];
            WorkHoursUC.cb_tue_11.IsChecked = MyworkHours[2, 2];
            WorkHoursUC.cb_tue_12.IsChecked = MyworkHours[2, 3];
            WorkHoursUC.cb_tue_13.IsChecked = MyworkHours[2, 4];
            WorkHoursUC.cb_tue_14.IsChecked = MyworkHours[2, 5];
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
           
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SearchUC1.IDSearchTB.Text != "")
                   testerListView.DataContext = my_bl.AllTestersByCondiion(x => x.Id == SearchUC1.IDSearchTB.Text);
                else
                    testerListView.DataContext = my_bl.AllTestersByCondiion(x => x.Name == SearchUC1.NameSearchTextbox.Text || x.FamilyName == SearchUC1.FamilyNameSearchTB.Text
                    || x.PhoneNumber == SearchUC1.PhoneNumberSearchTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR 404");
            }
        
    }

        private void AllTestersByHourButton_Checked(object sender, RoutedEventArgs e)
        {
            DayOfWeekCB.IsEnabled = true;
            HourCB.IsEnabled = true;
             
           
        }

        private void AllTestersByHourButton_Unchecked(object sender, RoutedEventArgs e)
        {
            DayOfWeekCB.IsEnabled = false;
            HourCB.IsEnabled = false;
            DayOfWeekCB.SelectedIndex = -1;
            HourCB.SelectedIndex = -1;
            DayAndHourBU.Visibility = Visibility.Hidden;
        }

        private void HourCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DayAndHourBU.Visibility = Visibility.Visible;
        }

        private void DayAndHourBU_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Tester> results = my_bl.AllTestersByCondiion(x => x.WorkHours[
                 DayOfWeekCB.SelectedIndex, HourCB.SelectedIndex]==true);
            testerListView.DataContext = results;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            LicenceTypeCB.Visibility = Visibility.Visible;
            LicenceTypeCB.ItemsSource= Enum.GetNames(  typeof(BE.MyEnum.carType)).ToList();
        }

        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            LicenceTypeCB.Visibility = Visibility.Hidden;
        }

        private void LicenceTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IEnumerable<Tester> results = my_bl.AllTestersByCondiion(x => x.ExpiranceCar == (BE.MyEnum.carType)LicenceTypeCB.SelectedIndex+1);
            testerListView.DataContext = results;
        }

        private void AllTestersByNUMtESTSRB_Checked(object sender, RoutedEventArgs e)
        {
            NumberOfTestsComboBox.Visibility = Visibility.Visible;
           // NumberOfTestsComboBox.ItemsSource=my_bl.tes

        }
    }
    }

