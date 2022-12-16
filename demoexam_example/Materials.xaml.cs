using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using demoexam_example.classes;
using MySql.Data.MySqlClient;

namespace demoexam_example
{
    public partial class Materials : Window
    {
        public Materials()
        {
            InitializeComponent();
            ShowData("SELECT Image, (SELECT train.MaterialType.Title FROM train.MaterialType WHERE train.Material.MaterialTypeID = train.MaterialType.ID ) AS MaterialType, Title, CountInStock, MinCount FROM train.Material;");
        }

        private void ShowData(string sql)
        {
            using (MySqlConnection conn = new MySqlConnection(classes.Variables.ConnStr))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        List <classes.Material> material = new List<classes.Material>();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string ImageSource = reader.GetString(0);
                                if (ImageSource == String.Empty)
                                    ImageSource = "images/picture.png";
                                else
                                    ImageSource = "images" + ImageSource;
                                 
                                material.Add(new classes.Material(){ Image = ImageSource, MaterialType = reader.GetString(1),Title = reader.GetString(2), 
                                    CountInStock = reader.GetInt32(3), MinCount = reader.GetInt32(4) });
                            }
                            MaterialsList.ItemsSource = material;
                        }
                    }
                }
                catch (MySqlException)
                {
                    MessageBox.Show("Ошибка подключения к БД!");
                }
            }   
        }

        private void SearchBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ShowData($"SELECT Image, (SELECT train.MaterialType.Title FROM train.MaterialType WHERE train.Material.MaterialTypeID = train.MaterialType.ID ) AS MaterialType, Title, CountInStock, MinCount FROM train.Material WHERE Material.Title LIKE '%{SearchBox.Text}%';");
        }
    }
}