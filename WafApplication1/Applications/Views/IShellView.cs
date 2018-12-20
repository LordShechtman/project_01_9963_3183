using System.Waf.Applications;

namespace WafApplication1.Applications.Views
{
    internal interface IShellView : IView
    {
        void Show();

        void Close();
    }
}
