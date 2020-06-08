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
    /// View detials of Trinnes 
    /// Filterd date (by driving school and so on)
    /// is accessable with radio butoons
    /// </summary>
    public partial class TraineeViweWindow : Window
    {
        
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
           
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void AllStudentsBySchool_Checked(object sender, RoutedEventArgs e)
        {
            DrivingSchoolCB.Visibility = Visibility.Visible;
            foreach (var key in my_bl.TraineeBySchool(true))
                DrivingSchoolCB.Items.Add(key.Key);
            
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
        #region AllTrianieesbyGender un/check events
        private void AllTraineesOfGenderx_Checked(object sender, RoutedEventArgs e)
        {
            GenderComboBox.Visibility = Visibility.Visible;
            GenderComboBox.ItemsSource = Enum.GetNames(typeof(BE.MyEnum.gender)).ToList();
        }
        private void AllTraineesOfGenderx_Unchecked(object sender, RoutedEventArgs e)
        {
            GenderComboBox.Visibility = Visibility.Hidden;
        }
        #endregion
        private void GenderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ///Gives the all the trniees by the selected gender
            traineeListView.DataContext = my_bl.ALLTraineeByParameter(X => 
            X.MyGender ==(BE.MyEnum.gender) Enum.Parse(typeof(BE.MyEnum.gender), GenderComboBox.SelectedValue.ToString()));
        }

        

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ///Find trinee/s by prmeters of the serch user controll
            ///first we look by the Main key(ID) 
            ///  and then by others paramters
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
            //Show Trainee number of tests by clicking the selected trniee
            if (traineeListView.SelectedIndex != -1)
            {
               
                List<BE.Trainee> trainees = my_bl.GetAllTrainees();
                int Index = trainees.IndexOf(trainees.Find(X => X.ToString() == traineeListView.SelectedItem.ToString()));
                if (Index != -1)
                {
                    BE.Trainee Selected_Trainee = my_bl.GetAllTrainees()[Index];
                    MessageBox.Show("Name: " + Selected_Trainee.Name + " " + Selected_Trainee.FamilyName
                      + "\n" + "Number of Tests: " + my_bl.numberOfTests(Selected_Trainee));
                }
            }
        }
        #region TraineesByTeacherRadioButton un/checked events
        private void TraineesByTeacherRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            TeacherComboBox.Visibility = Visibility.Hidden;
        }

        private void AllTraineesbyLicnsceRB_Unchecked(object sender, RoutedEventArgs e)
        {
            carTypeCB.Visibility = Visibility.Hidden;
           
        }
        #endregion
        private void carTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IEnumerable<BE.Trainee> results = my_bl.ALLTraineeByParameter(x =>
              x.Car == (BE.MyEnum.carType)carTypeCB.SelectedIndex + 1);
            traineeListView.DataContext = results;
        }
        #region AllTraineesbyLicnsceRB un/checked events
        private void AllTraineesbyLicnsceRB_Checked(object sender, RoutedEventArgs e)
        {
            carTypeCB.Visibility = Visibility.Visible;
            carTypeCB.ItemsSource = Enum.GetNames(typeof(BE.MyEnum.carType)).ToList();
        }
        private void AllStudentsBySchool_Unchecked(object sender, RoutedEventArgs e)
        {
            DrivingSchoolCB.Visibility = Visibility.Hidden;

        }
        #endregion
        private void AllTraineesByNumberOfTestsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
           ///Removed in the final project
        }

        private void DrivingSchoolCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ///Driving school combo box keeps all the schools names 
            ///the list view will give us all the trinnes of the selcted school
            ///
            List<BE.Trainee> results = new List<BE.Trainee>();
            foreach(var group in my_bl.TraineeBySchool(true))
            {
               if(group.Key== DrivingSchoolCB.SelectedValue.ToString())
                {
                    foreach (var item in group)
                        results.Add(item);
                }
            }
            traineeListView.DataContext = results;
        }

        

       
    }
}
