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
        public void setComboBoxes()
        {
            
        }
        public IBL my_bl;
        public TraineeViweWindow()
        {
            InitializeComponent();
            my_bl = BL.Factory_BL.getBL();
            GenderComboBox.ItemsSource =Enum.GetNames(typeof( BE.MyEnum.gender));
            traineeListView.DataContext = my_bl.GetAllTrainees();
            GenderComboBox.Visibility = Visibility.Hidden;
            setComboBoxes();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void AllStudentsBySchool_Checked(object sender, RoutedEventArgs e)
        {
            traineeListView.DataContext = my_bl.TraineeBySchool(true);
            
        }

        private void TraineesByTeacherRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            traineeListView.DataContext = my_bl.TraineeByTeacher(true);
        }

        private void AllTraineesOfGenderx_Checked(object sender, RoutedEventArgs e)
        {
            GenderComboBox.Visibility = Visibility.Visible;
        }

        private void GenderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            traineeListView.DataContext = my_bl.ALLTraineeByParameter(X => 
            X.MyGender ==(BE.MyEnum.gender) Enum.Parse(typeof(BE.MyEnum.gender), GenderComboBox.SelectedValue.ToString()));
        }

        private void AllTraineesOfGenderx_Unchecked(object sender, RoutedEventArgs e)
        {
            GenderComboBox.Visibility = Visibility.Hidden;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SearchUC.IDSearchTB.Text !="")
                    traineeListView.DataContext = my_bl.ALLTraineeByParameter(x => x.Id == SearchUC.IDSearchTB.Text);
                else
                    traineeListView.DataContext = my_bl.ALLTraineeByParameter(x =>x.Name==SearchUC.NameSearchTextbox.Text ||x.FamilyName== SearchUC.FamilyNameSearchTB.Text
                    || x.PhoneNumber==SearchUC.PhoneNumberSearchTextBox.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR 404");
            }
            }
    }
}
