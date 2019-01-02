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
    /// Interaction logic for TestMenuxaml.xaml
    /// </summary>
    public partial class TestMenuxaml : Window
    {
        public TestMenuxaml()
        {
            InitializeComponent();
        }

        private void AddTestBoutton_Click(object sender, RoutedEventArgs e)
        {
            AddTestWindow addTestWindow = new AddTestWindow();
            addTestWindow.ShowDialog();
        }

        private void UpdateTestBoutton_Click(object sender, RoutedEventArgs e)
        {
            UpdateTest updateTest = new UpdateTest();
            updateTest.ShowDialog();
        }
    }
}
