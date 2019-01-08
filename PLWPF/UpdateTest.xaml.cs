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
        public int ListIndex = 0;

        IBL My_bl;
       public List<string> myTestNotes = new List<string>();
       public List<bool> allMyParameters = new List<bool>();
        public UpdateTest()
        {
            InitializeComponent();
            My_bl = Factory_BL.getBL();
            foreach (var v in My_bl.GetAllTesters())
                TesterIDCombobox.Items.Add(v.Id + " " + v.Name + " " + v.FamilyName);
            for(int i = 0; i < 6; i++) { allMyParameters.Add(false); }
            PasswordBox.IsEnabled = false;
            OkButton.IsEnabled = false;
            
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((KeepDistanceUC.PassedCB.IsChecked == false && KeepDistanceUC.FailCB.IsChecked == false)
                   || (ReverseParkUC.PassedCB.IsChecked == false && ReverseParkUC.FailCB.IsChecked == false) ||
                   (MirorsUC.PassedCB.IsChecked==false && MirorsUC.FailCB.IsChecked == false) ||
                   (SignalsUC.PassedCB.IsChecked==false && SignalsUC.FailCB.IsChecked==false) ||
                  ( WheelHandleUC.PassedCB.IsChecked==false && WheelHandleUC.FailCB.IsChecked == false) ||
                        ( PriortyRulesUC.PassedCB.IsChecked == false && PriortyRulesUC.FailCB.IsChecked == false))
                    throw new Exception("One or more test parmeters was not marked ");
                if (WheelHandleUC.PassedCB.IsChecked == true)
                    allMyParameters.Insert((int)MyEnum.testsParameters.wheelhandling, true);
                
                if (ReverseParkUC.PassedCB.IsChecked == true)
                    allMyParameters.Insert((int)MyEnum.testsParameters.reverseParking, true);
                if (PriortyRulesUC.PassedCB.IsChecked == true)
                    allMyParameters.Insert((int)MyEnum.testsParameters.priorityrules, true);
                if (MirorsUC.PassedCB.IsChecked == true)
                    allMyParameters.Insert((int)MyEnum.testsParameters.mirors, true);
                if (SignalsUC.PassedCB.IsChecked == true)
                    allMyParameters.Insert((int)MyEnum.testsParameters.signals, true);
                if (KeepDistanceUC.PassedCB.IsChecked == true)
                    allMyParameters.Insert((int)MyEnum.testsParameters.keepDistance, true);
               


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }

        private void TesterIDCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PasswordBox.IsEnabled = true;
            OkButton.IsEnabled = true;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BE.Tester tester = My_bl.GetAllTesters().Find(x=>x.Id==TesterIDCombobox.Text);
                if (tester.Password == PasswordBox.Password)
                {
                    List<Test> myTests = My_bl.GetAllTests().FindAll(x => x.TesterId == TesterIDCombobox.Text);
                    StudentIDTB.Text = myTests[0].TraineeId;
                    Trainee trainee = My_bl.GetAllTrainees().Find(x => x.Id == StudentIDTB.Text);
                    studentDetails.Content = trainee.Name + " " + trainee.FamilyName;
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
    }
}
