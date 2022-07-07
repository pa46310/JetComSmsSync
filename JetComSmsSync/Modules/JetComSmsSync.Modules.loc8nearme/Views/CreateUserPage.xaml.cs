using System.Windows.Controls;

namespace JetComSmsSync.Modules.loc8nearme.Views
{
    /// <summary>
    /// Interaction logic for CreateUserPage
    /// </summary>
    public partial class CreateUserPage : UserControl
    {
        public CreateUserPage()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            accountIdText.Focus();
            accountIdText.SelectAll();
        }
    }
}
