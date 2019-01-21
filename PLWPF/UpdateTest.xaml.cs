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
    /// Interaction logic for UpdateTest.xaml
    /// </summary>
    
       
    public partial class UpdateTest : Window
    {
        public void LockFiled()
        {
            GenralParmetersGrid.IsEnabled = false;
            PasswordGrid.IsEnabled = false;
            AddNoteGrid.IsEnabled = false;
            studentDetails.Content = null;
            StudentIDTB.Text = null;
            TestDateDP.Text = null;
            

        }
        public int ListIndex = 0;
        public void clearFileds()
        {
            TestsNumberTB.Text = null;
            TestDateDP.Text = null;
            HourDispalyLabel.Content = null;
            studentDetails.Content = null;
            KeepDistanceUC.clear();
            WheelHandleUC.clear();
            SignalsUC.clear();
            MirorsUC.clear();
            ReverseParkUC.clear();
            PriortyRulesUC.clear();
            myTestNotes.Clear();
            allMyParameters.Clear();
        }
        IBL My_bl;
       public List<string> myTestNotes = new List<string>();
       public List<bool> allMyParameters = new List<bool>();
        /// <summary>
        /// We loced and ulocked the filed by tester login and log out
        /// </summary>
        public UpdateTest()
        {
            InitializeComponent();
            My_bl = Factory_BL.getBL();
            StudentIDTB.IsEnabled = false;
            HourDispalyLabel.Content = "Hour:";
            LockFiled();

        }

        public void ActiveFildes()
        {
            GenralParmetersGrid.IsEnabled = true;
            PasswordGrid.IsEnabled = true;
            AddNoteGrid.IsEnabled = true;
        }
            

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for(int i = 0; i < 6; i++) { allMyParameters.Add(false); }
                if ((KeepDistanceUC.PassedCB.IsChecked == false && KeepDistanceUC.FailCB.IsChecked == false)
                   || (ReverseParkUC.PassedCB.IsChecked == false && ReverseParkUC.FailCB.IsChecked == false) ||
                   (MirorsUC.PassedCB.IsChecked==false && MirorsUC.FailCB.IsChecked == false) ||
                   (SignalsUC.PassedCB.IsChecked==false && SignalsUC.FailCB.IsChecked==false) ||
                  ( WheelHandleUC.PassedCB.IsChecked==false && WheelHandleUC.FailCB.IsChecked == false) ||
                        ( PriortyRulesUC.PassedCB.IsChecked == false && PriortyRulesUC.FailCB.IsChecked == false))
                    throw new Exception("One or more test parmeters was not marked ");
                /// If one paramter is missing 
                
                if (WheelHandleUC.PassedCB.IsChecked == true)
                    allMyParameters[(int)MyEnum.testsParameters.wheelhandling]= true;
                
                if (ReverseParkUC.PassedCB.IsChecked == true)
                    allMyParameters[(int)MyEnum.testsParameters.reverseParking] =true;
                if (PriortyRulesUC.PassedCB.IsChecked == true)
                    allMyParameters[(int)MyEnum.testsParameters.priorityrules]= true;
                if (MirorsUC.PassedCB.IsChecked == true)
                    allMyParameters[(int)MyEnum.testsParameters.mirors]= true;
                if (SignalsUC.PassedCB.IsChecked == true)
                    allMyParameters[(int)MyEnum.testsParameters.signals]= true;
                if (KeepDistanceUC.PassedCB.IsChecked == true)
                    allMyParameters[(int)MyEnum.testsParameters.keepDistance]= true;
                DateTime my_date = My_bl.GetAllTests()[ListIndex].TestDate;
               
                
                Test finalGradeTest = new Test(TesterIDTextBox.Text, StudentIDTB.Text,my_date, My_bl.GetAllTests()[ListIndex].TestAddress,allMyParameters,myTestNotes);
                My_bl.UpdateTest(finalGradeTest);
                Test t = My_bl.GetAllTests().Find(x=>x.TestNumber==TestsNumberTB.Text);
                if(t!=null)
                {
                    string ShowResult = "Test Result: \n";
                    if (t.IsPass == true)
                        ShowResult += "Passed";
                    else
                        ShowResult += "Failed";
                    MessageBox.Show(ShowResult, "Update succsessed");
                    LockFiled();
                    clearFileds();
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
                
            }
        }

        

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BE.Tester tester = My_bl.GetAllTesters().Find(x=>x.Id==TesterIDTextBox.Text);
                if (tester == null)
                    throw new Exception("Tester " + TesterIDTextBox.Text + "not found!!");
                if (tester.Password == YourPasswordBox.Password)
                {
                    List<Test> myTests = My_bl.GetAllTests().FindAll(x => x.TesterId == TesterIDTextBox.Text && x.TestDate <= DateTime.Now && 
                    x.TestParameters.Any()==false);
                    if (myTests.Any() == false)
                        throw new Exception("You don't have any tests to update");
                    ListIndex = 0;
                    StudentIDTB.Text = myTests[0].TraineeId;
                    Trainee trainee = My_bl.GetAllTrainees().Find(x => x.Id == StudentIDTB.Text);
                    studentDetails.Content = trainee.Name + " " + trainee.FamilyName;
                    TestDateDP.SelectedDate = myTests[ListIndex].TestDate;
                    HourDispalyLabel.Content = "Hour:" + myTests[ListIndex].TestDate.Hour+":00";
                    TestsNumberTB.Text = myTests[ListIndex].TestNumber;
                    ActiveFildes();
                }
                else
                    throw new Exception("Worng password!!");
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyNotesTB.Text +="\n" + SingleNoteTB.Text;
            MessageBox.Show("Note added");
            myTestNotes.Add(SingleNoteTB.Text);
            SingleNoteTB.Text = null;


        }

        private void TesterIDTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
             || (e.Key >= Key.D0 && e.Key <= Key.D9));
            if(TesterIDTextBox.Text.Count()==9)
            {
                PasswordGrid.IsEnabled = true;
            }
        }
    }
}
