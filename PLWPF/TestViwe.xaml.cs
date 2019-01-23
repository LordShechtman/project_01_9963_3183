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
           int Index = MyBl.GetAllTests().IndexOf((BE.Test)testListView.SelectedItem);
           // MessageBox.Show(MyBl.GetAllTests()[Index].ToString());
            if(Index!=-1)
            {
               
                if (MyBl.GetAllTests()[Index].TestParameters.Any()==true)
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

        private void AllTestsbyDateRB_Checked(object sender, RoutedEventArgs e)
        {
            SelectDateDP.Visibility = Visibility.Visible;
            
        }

        private void AllTestsbyDateRB_Unchecked(object sender, RoutedEventArgs e)
        {
            SelectDateDP.Visibility = Visibility.Hidden;
        }

        private void SelectDateDP_CalendarClosed(object sender, RoutedEventArgs e)
        {
           //Returns all tests in the same Date 
            testListView.DataContext = MyBl.AllTestBy(x => x.TestDate.AddHours((int)-1*x.TestDate.Hour )
            ==SelectDateDP.SelectedDate);
        }

        private void PassedSatistic_Click(object sender, RoutedEventArgs e)
        {
            //Return the passed precent (passed/alltests)
            MessageBox.Show("Passed precent: " + (MyBl.PassStatistic()*100)+"%");
        }

        private void AllTestbyTester_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
