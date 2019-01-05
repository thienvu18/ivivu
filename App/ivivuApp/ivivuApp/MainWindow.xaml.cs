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

            var window = new Login_admin();
            this.Close();
            window.ShowDialog();
        }

        private void login_user_Click(object sender, RoutedEventArgs e)
        {

            var window = new Login_user();
            this.Close();
            window.ShowDialog();
            
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            var left = Application.Current.MainWindow.Left;
            var top = Application.Current.MainWindow.Top;
            var height = Application.Current.MainWindow.Height;
            var width = Application.Current.MainWindow.Width;

            var window = new Search();
            window.Left = left;
            window.Top = top;
            window.Width = width;
            window.Height = height;

            //this.Close();
            window.ShowDialog();
        }
    }
}
