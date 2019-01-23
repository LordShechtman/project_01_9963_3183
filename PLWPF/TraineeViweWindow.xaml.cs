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
            TeacherComboBox.Visibility = Visibility.Hidden;
            carTypeCB.Visibility = Visibility.Hidden;
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
            TeacherComboBox.Visibility = Visibility.Visible;
            traineeListView.DataContext = null;
            foreach(var item in my_bl.TraineeByTeacher(true))
            {
                TeacherComboBox.Items.Add(item.Key);
                

            }
        }

        private void AllTraineesOfGenderx_Checked(object sender, RoutedEventArgs e)
        {
            GenderComboBox.Visibility = Visibility.Visible;
            GenderComboBox.ItemsSource = Enum.GetNames(typeof(BE.MyEnum.gender)).ToList();
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

        private void TeacherComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<BE.Trainee> results=new List<BE.Trainee>();
            foreach(var key in my_bl.TraineeByTeacher(true))
            {
                if(key.Key==TeacherComboBox.SelectedValue.ToString())
                {
                    foreach(BE.Trainee element in key)
                    {
                        results.Add(element);
                    }
                }
            }
            traineeListView.DataContext = results;
            
            
        }

        private void traineeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Show Trainee number of tests and 
            int Index = my_bl.GetAllTrainees().IndexOf((BE.Trainee)traineeListView.SelectedItem);
            if (Index != -1)
            {
                BE.Trainee Selected_Trainee = my_bl.GetAllTrainees()[Index];
                MessageBox.Show("Name: " + Selected_Trainee.Name + " " + Selected_Trainee.FamilyName
                  +"\n"+"Number of Tests: "+my_bl.numberOfTests(Selected_Trainee));
            }
        }

        private void TraineesByTeacherRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            TeacherComboBox.Visibility = Visibility.Hidden;
        }

        private void AllTraineesbyLicnsceRB_Unchecked(object sender, RoutedEventArgs e)
        {
            carTypeCB.Visibility = Visibility.Hidden;
        }

        private void carTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IEnumerable<BE.Trainee> results = my_bl.ALLTraineeByParameter(x =>
              x.Car == (BE.MyEnum.carType)carTypeCB.SelectedIndex + 1);
            traineeListView.DataContext = results;
        }

        private void AllTraineesbyLicnsceRB_Checked(object sender, RoutedEventArgs e)
        {
            carTypeCB.Visibility = Visibility.Visible;
            carTypeCB.ItemsSource = Enum.GetNames(typeof(BE.MyEnum.carType)).ToList();
        }
    }
}
