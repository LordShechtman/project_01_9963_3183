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
            if(Index!=-1)
                {
                Tester selectedTester = my_bl.GetAllTesters()[Index];
               
                
            }
            
        }

        private void SearchButtons_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Tester> searchResults;
            if (SearchUC.IDSearchTB.Text != "")
                searchResults = my_bl.GetAllTesters().FindAll(x => x.Id == SearchUC.IDSearchTB.Text);
            else
                searchResults = my_bl.GetAllTesters().FindAll(
                   x=>x.Name==SearchUC.NameSearchTextbox.Text ||
                   x.FamilyName==SearchUC.FamilyNameSearchTB.Text
                   || x.PhoneNumber==SearchUC.PhoneNumberSearchTextBox.Text);
            testerListView.DataContext = searchResults;

        }
    }
}
