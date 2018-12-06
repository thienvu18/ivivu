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

            var window = new Search();
            this.Close();
            window.ShowDialog();
        }
    }
}
