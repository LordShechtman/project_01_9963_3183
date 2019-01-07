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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for TestParmeterUC.xaml
    /// </summary>
    public partial class TestParmeterUC : UserControl
    {
        public TestParmeterUC()
        {
            InitializeComponent();
        }

        private void PassedCB_Checked(object sender, RoutedEventArgs e)
        {
            FailCB.IsChecked = false;
        }

        private void FailCB_Checked(object sender, RoutedEventArgs e)
        {
            PassedCB.IsChecked = false;
        }
    }
}
