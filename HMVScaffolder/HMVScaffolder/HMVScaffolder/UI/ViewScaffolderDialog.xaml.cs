using System.Windows;
using System.Windows.Markup;

namespace HMVScaffolder.Mvc
{
    /// <summary>
    /// Interaction logic for ViewScaffolderDialog.xaml
    /// </summary>
    public partial class ViewScaffolderDialog : ValidatingDialogWindow, IComponentConnector
    {

        public ViewScaffolderDialog()
        {
            this.InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            base.TryClose();
        }
    }
}
