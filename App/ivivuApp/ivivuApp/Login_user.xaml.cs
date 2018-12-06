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
using System.Windows.Shapes;

namespace ivivuApp
{
    /// <summary>
    /// Interaction logic for Login_user.xaml
    /// </summary>
    public partial class Login_user : Window
    {
        public Login_user()
        {
            InitializeComponent();
        }

        private void btn_login_user_Click(object sender, RoutedEventArgs e)
        {
            var window = new Home();
            this.Close();
            window.ShowDialog();
        }

        private void btn_signup_Click(object sender, RoutedEventArgs e)
        {
            var window = new SignUp();
            this.Close();
            window.ShowDialog();
        }
    }
}
