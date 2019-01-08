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

namespace ivivuApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.Height = 600;
            this.Width = 800;
            InitializeComponent();
            Database.init();
        }

        private void login_admin_Click(object sender, RoutedEventArgs e)
        {
            var left = this.Left;
            var top = this.Top;
            var height = this.Height;
            var width = this.Width;

            var window = new Login_admin
            {
                Left = left,
                Top = top,
                Width = width,
                Height = height
            };

            window.Show();
            this.Close();
        }

        private void login_user_Click(object sender, RoutedEventArgs e)
        {

            var left = this.Left;
            var top = this.Top;
            var height = this.Height;
            var width = this.Width;
            var window = new Login_user
            {
                Left = left,
                Top = top,
                Width = width,
                Height = height
            };
            window.Show();
            this.Close();


        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            var left = this.Left;
            var top = this.Top;
            var height = this.Height;
            var width = this.Width;

            var window = new Search
            {
                Left = left,
                Top = top,
                Width = width,
                Height = height
            };

            window.Show();
            this.Close();

        }

        private void exit(object sender, MouseButtonEventArgs e)
        {
            //thoát
            Application.Current.Shutdown();
        }

    }
}
