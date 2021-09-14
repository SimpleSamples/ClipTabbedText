using Microsoft.Data.Sqlite;
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

namespace ClipTabbedText
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string ConnectionStringTemplate = @"Data Source={path}ClipText.db";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<ClipText> ctl = new List<ClipText>();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + '\\';
            string cs = $"Data Source={path}ClipText.db";
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT Description, Data FROM ClipText";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);
                        ClipText ct = new ClipText();
                        ct.Description = reader["Description"].ToString();
                        ct.Data = reader["Data"].ToString();
                        ctl.Add(ct);
                    }
                }
            }
            if (ctl.Count == 0)
            {
                Message.Content = "No data";
                return;
            }
            TextGrid.DataContext = ctl;
        }

        private void MenuItemClip_Click(object sender, RoutedEventArgs e)
        {
            Message.Content = string.Empty;
            if (TextGrid.SelectedIndex == -1)
                return;
            ClipText ct = (ClipText)TextGrid.SelectedItem;
            if (ct == null)
                return;
            Clipboard.SetText(ct.Data);
            Message.Content = "Clipboard has: " + ct.Data;
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    class ClipText
    {
        public string Description { get; set; }
        public string Data { get; set; }
    }
}
