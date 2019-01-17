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
            if(Index!=-1)
            {
                List <bool> result= MyBl.GetAllTests()[Index].TestParameters;
                if (result.Any() == true)
                {
                    ParmterResultsUC.keepDistanceCB.IsChecked = result[0];
                    ParmterResultsUC.reverseParkingCB.IsChecked = result[1];
                    ParmterResultsUC.mirorsCB.IsChecked = result[2];
                    ParmterResultsUC.signalsCB.IsChecked = result[3];
                    ParmterResultsUC.wheelhandlingCB.IsChecked = result[4];
                    ParmterResultsUC.wheelhandlingCB.IsChecked = result[5];
                }
                else
                {
                    MessageBox.Show("Test didn't updated Yet!!");
                }
            }
        }
    }
}
