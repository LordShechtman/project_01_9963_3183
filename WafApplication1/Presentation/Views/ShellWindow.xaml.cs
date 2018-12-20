using System.ComponentModel.Composition;
using System.Windows;
using WafApplication1.Applications.Views;

namespace WafApplication1.Presentation.Views
{
    [Export(typeof(IShellView))]
    public partial class ShellWindow : Window, IShellView
    {
        public ShellWindow()
        {
            InitializeComponent();
        }
    }
}
