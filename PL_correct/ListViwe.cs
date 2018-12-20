using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BL;
using DAL;
using BE;
namespace PL_correct
{
    public partial class ListViwe : Form
    {
        public IBL MyBl;
        public ListViwe()
        {
            InitializeComponent();
              MyBl = Factory_BL.getBL();
            
    }

        private void open(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            var testers = MyBl.GetAllTesters();
            foreach(var x in testers)
            {
                var row = new string[] { x.Id, x.Name, x.FamilyName };
                var lvi = new ListViewItem(row);
                lvi.Tag = x;
                listView1.Items.Add(lvi);
            }
        }
    }
}
