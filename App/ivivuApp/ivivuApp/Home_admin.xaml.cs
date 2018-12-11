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
    /// Interaction logic for Home_admin.xaml
    /// </summary>
    public partial class Home_admin : Window
    {
        public Home_admin()
        {
            InitializeComponent();
        }

        private void btn_create_bill_Click(object sender, RoutedEventArgs e)
        {

            var window = new Bill();
            this.Close();
            window.ShowDialog();
        }

        private void btn_report_click(object sender, RoutedEventArgs e)
        {
            var window = new Report();
            this.Close();
            window.ShowDialog();
        }

        private void BtnCheckRoomStatus_Click(object sender, RoutedEventArgs e)
        {
            var window = new CheckRoomStatus();
            this.Close();
            window.ShowDialog();
        }
    }
}
