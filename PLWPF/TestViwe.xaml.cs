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
    /// Interaction logic for TestViwe.xaml
    /// </summary>
    public partial class TestViwe : Window
    {
        IBL MyBl;
        public TestViwe()
        {
            InitializeComponent();
            SelectDateDP.Visibility = Visibility.Hidden;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            System.Windows.Data.CollectionViewSource testViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // testViewSource.Source = [generic data source]
            MyBl = BL.Factory_BL.getBL();
            testListView.DataContext = MyBl.GetAllTests();
        }

        private void testListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Gives the testparmeter results and the tester Notes from the test
            List<BE.Test> tests = MyBl.GetAllTests();
            int Index = tests.IndexOf(
                tests.Find(x => x.ToString() == testListView.SelectedItem.ToString()));
           // MessageBox.Show(MyBl.GetAllTests()[Index].ToString());
            if(Index!=-1)
            {
               //BUild a new bool "Is Updated"
                if (MyBl.GetAllTests()[Index].ISUpdated==true)
                {
                    ParmterResultsUC.keepDistanceCB.IsChecked =  MyBl.GetAllTests()[Index].TestParameters[0];
                    ParmterResultsUC.reverseParkingCB.IsChecked = MyBl.GetAllTests()[Index].TestParameters[1];
                    ParmterResultsUC.mirorsCB.IsChecked = MyBl.GetAllTests()[Index].TestParameters[2];
                    ParmterResultsUC.signalsCB.IsChecked = MyBl.GetAllTests()[Index].TestParameters[3];
                    ParmterResultsUC.wheelhandlingCB.IsChecked = MyBl.GetAllTests()[Index].TestParameters[4];
                    ParmterResultsUC.priorityrulesCB.IsChecked = MyBl.GetAllTests()[Index].TestParameters[5];
                    List<string> notes = MyBl.GetAllTests()[Index].TesterNotes;
                    if(notes.Any()==true)
                    {
                        int noteNumber = 1;
                        foreach(var v in notes)
                        {
                            TesterNoteLabel.Content += noteNumber + "." + v;
                            noteNumber++;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Test didn't updated Yet!!");
                }
            }
        }
        #region AllTestsbyDateRB Events
        private void AllTestsbyDateRB_Checked(object sender, RoutedEventArgs e)
        {
            SelectDateDP.Visibility = Visibility.Visible;
            
        }
      
        private void AllTestsbyDateRB_Unchecked(object sender, RoutedEventArgs e)
        {
            SelectDateDP.Visibility = Visibility.Hidden;
        }
        #endregion
        private void SelectDateDP_CalendarClosed(object sender, RoutedEventArgs e)
        {
           ///Returns all tests in the same Date 
           ///We removed the hour so we can compare by day/month/year prametrs
            testListView.DataContext = MyBl.AllTestBy(x => x.TestDate.AddHours((int)-1*x.TestDate.Hour )
            ==SelectDateDP.SelectedDate);
        }
       
        private void PassedSatistic_Click(object sender, RoutedEventArgs e)
        {
            //Return the passed precent (passed/alltests)
            MessageBox.Show("Passed precent: " + (MyBl.PassStatistic()*100)+"%");
        }
        #region AllTestbyTester Radio button EVENTS
        private void AllTestbyTester_Checked(object sender, RoutedEventArgs e)
        {
            TesterCB.Visibility = Visibility.Visible;
            foreach( var item in MyBl.GetAllTesters())
            {
                TesterCB.Items.Add(item.Id + " " + item.Name + " " + item.FamilyName);
            }
        }

        private void AllTestbyTester_Unchecked(object sender, RoutedEventArgs e)
        {
            TesterCB.Visibility = Visibility.Hidden;
        }
        #endregion
        private void TesterCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string my_id = "";
            //Get the TESTER ID
            foreach (var v in TesterCB.SelectedValue.ToString())
            {
                if (v >= '0' && v <= '9')
                    my_id += v;
            }
            testListView.DataContext = MyBl.AllTestBy(x => x.TesterId == my_id);
        }

        private void AllTestsByTrianee_Checked(object sender, RoutedEventArgs e)
        {
            TrineeIDComboBox.Visibility = Visibility.Visible;
        }

        private void AllTestsByTrianee_Unchecked(object sender, RoutedEventArgs e)
        {
            TrineeIDComboBox.Visibility = Visibility.Hidden;
            foreach(var item in MyBl.GetAllTrainees())
                TrineeIDComboBox.Items.Add(item.Id + " " + item.Name + " " + item.FamilyName);
        }

        private void TrineeIDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string my_id = "";
            //Get the TESTER ID
            foreach (var v in TrineeIDComboBox.SelectedValue.ToString())
            {
                if (v >= '0' && v <= '9')
                    my_id += v;
            }
            testListView.DataContext = MyBl.AllTestBy(x => x.TraineeId == my_id);
        }
    }
}
