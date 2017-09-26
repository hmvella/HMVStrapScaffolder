using System.Windows.Markup;

namespace HMVScaffolder.Mvc
{
    /// <summary>
    /// Interaction logic for AreaScaffolderDialog.xaml
    /// </summary>
    public partial class AreaScaffolderDialog : ValidatingDialogWindow, IComponentConnector
    {

        public AreaScaffolderDialog()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            base.TryClose();
        }
    }
}
