using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Windows;
using MySql.Data.MySqlClient;

namespace demoexam_example
{
    public partial class Auth : Window
    {
        private bool _captha = false;
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
                            sw
                        }
                        if (cmd.ExecuteScalar() != null)
                            MessageBox.Show("Success Log!");
                        else
                        {
                            _captha = true;
                            MessageBox.Show("Non Success Log!");
                        }
                            
                        
                    }
                }
                catch (MySqlException)
                {
                    MessageBox.Show("Ошибка подключения к БД!");
                }
            }
            
        }
    }
}