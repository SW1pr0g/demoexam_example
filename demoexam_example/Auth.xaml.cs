using System.ComponentModel;
using System.Windows;

namespace demoexam_example
{
    public partial class Auth : Window
    {
        public Auth()
        {
            InitializeComponent();
        }

        private void Auth_OnClosing(object sender, CancelEventArgs e)
        {
            var res = MessageBox.Show(classes.Variables.ExitMsg, classes.Variables.ExitTitle, MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
                e.Cancel = true;
        }

        private void GuestBtn_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Guest log!");
        }

        private void LoginBtn_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Log!");
        }
    }
}