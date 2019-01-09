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
    /// Interaction logic for TraineeViweWindow.xaml
    /// </summary>
    public partial class TraineeViweWindow : Window
    {
        public IBL my_bl;
        public TraineeViweWindow()
        {
            InitializeComponent();
            my_bl = BL.Factory_BL.getBL();
            traineeListView.DataContext = my_bl.GetAllTrainees();
          
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
