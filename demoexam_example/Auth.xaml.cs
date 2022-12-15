using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Media.Imaging;
using MySql.Data.MySqlClient;

namespace demoexam_example
{
    public partial class Auth : Window
    {
        private bool _captha = false;
        private string _capthaStr = String.Empty;
        public Auth()
        {
            InitializeComponent();
        }

        private void Auth_OnClosing(object sender, CancelEventArgs e)
        {
            if (classes.Variables.AuthClosed == false)
            {
                var res = MessageBox.Show(classes.Variables.ExitMsg, classes.Variables.ExitTitle, 
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.No)
                    e.Cancel = true;
            }
        }

        private void GuestBtn_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Guest log!");
        }

        private void LoginBtn_OnClick(object sender, RoutedEventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(classes.Variables.ConnStr))
            {
                try
                {
                    conn.Open();

                    string sql = String.Format("SELECT UserSurname FROM User WHERE UserLogin = '{0}' AND UserPassword = '{1}';", 
                        LoginBox.Text, PassBox.Password);
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        if (_captha == true)
                        {
                            if (CapthaBox.Text != _capthaStr)
                            {
                                MessageBox.Show("Ошибка! Неверный ввод проверки Captha");
                                ShowCaptha();
                                return;
                            }
                        }
                        if (cmd.ExecuteScalar() != null)
                            MessageBox.Show("Success Log!");
                        else
                        {
                            _captha = true;
                            MessageBox.Show("Non Success Log!");
                            ShowCaptha();
                        }
                    }
                }
                catch (MySqlException)
                {
                    MessageBox.Show("Ошибка подключения к БД!");
                }
            }
        }
        private void ShowCaptha()
        {
            CapthaBox.Clear();
            AuthWindow.Height = 550;
            ShowPanelCaptha.Visibility = Visibility.Visible;
            Random rnd = new Random();
 
            int captha = rnd.Next(0, 4);
            switch (captha)
            {
                case 1:
                    CapthaImg.Source = new BitmapImage(new Uri("images/captha/1.png", UriKind.Relative));
                    _capthaStr = "f7Y/";
                    break;
                case 2:
                    CapthaImg.Source = new BitmapImage(new Uri("images/captha/2.png", UriKind.Relative));
                    _capthaStr = "b6%;";
                    break;
                case 3:
                    CapthaImg.Source = new BitmapImage(new Uri("images/captha/3.png", UriKind.Relative));
                    _capthaStr = "jJk3";
                    break;
                default:
                    CapthaImg.Source = new BitmapImage(new Uri("images/captha/1.png", UriKind.Relative));
                    break;
            }
        }
    }
}