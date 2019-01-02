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
        IBL My_bl;
        public UpdateTest()
        {
            InitializeComponent();
            My_bl = Factory_BL.getBL();
            foreach (var v in My_bl.GetAllTesters())
                TesterIDCombobox.Items.Add(v.Id + " " + v.Name + " " + v.FamilyName);
           
        }

        
    }
}
