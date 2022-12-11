using System.Windows;

namespace demoexam_example
{
    public partial class MainWindow : Window
    {
        private bool _closed = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GoAuth_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow(new Auth());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_closed == false)
            {
                var res = MessageBox.Show(classes.Variables.ExitMsg, classes.Variables.ExitTitle,
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.No)
                    e.Cancel = true;
            }            
        }

        private void ShowWindow(Window window)
        {
            _closed = true;
            this.Close();
            window.Show();
        }
    }
}
