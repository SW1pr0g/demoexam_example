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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace demoexam_example
{
    public partial class MainWindow : Window
    {
        private bool _closing = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void First_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_closing == false)
            {
                var res = MessageBox.Show("Вы действительно хотите закрыть приложение?", "Выход из приложения",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.No)
                    e.Cancel = true;
                var str = classes.MySqlConn.ConnStr;
            }            
        }
    }
}
