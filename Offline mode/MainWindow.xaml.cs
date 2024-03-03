using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Configuration;
using System.Runtime.Remoting.Contexts;
using System.Data.Common;

namespace Offline_mode
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string conn_string = @"Data Source=(localdb)\.;Integrated Security=True";
        public static SqlConnection connection = new SqlConnection(conn_string);
        public static DbDataAdapter adapter;
        public static DataSet data_set = new DataSet();
        public static DataTable data_table = new DataTable();
        public static string table_songs = "songs", table_art = "artists", table_song_art = "song_artists";
        public bool handle = true;

        private void conservation_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();
            adapter.Update(data_set);
            connection.Close();
        }

        public MainWindow()
        {
            InitializeComponent();
            box.SelectedItem = box.Items[0];
            if (box.SelectedItem == box.Items[0]) Edit(table_art);
            else if (box.SelectedItem == box.Items[1]) Edit(table_songs);
            else if (box.SelectedItem == box.Items[2]) Edit(table_song_art);
        }

        public void Edit(string table)
        {
            connection.Open();
            adapter = new SqlDataAdapter($"select * from {table}", connection);
            new SqlCommandBuilder((SqlDataAdapter)adapter);
            Save(adapter);
        }

        private void updating_Click(object sender, RoutedEventArgs e)
        {
            if (box.SelectedItem == box.Items[0])
            {
                DataContext = null;
                Edit(table_art);
            }
            else if (box.SelectedItem == box.Items[1])
            {
                DataContext = null;
                Edit(table_songs);
            }
            else if (box.SelectedItem == box.Items[2])
            {
                DataContext = null;
                Edit(table_song_art);
            }
        }
        public void Save(DbDataAdapter adapter)
        {
            data_set.Clear();
            adapter.Fill(data_set);
            DataContext = data_set.Tables[0].DefaultView;
            adapter.Update(data_set);
            connection.Close();
        }
    }
}