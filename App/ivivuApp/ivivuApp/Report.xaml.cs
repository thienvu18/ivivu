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
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        public Report()
        {
            InitializeComponent();
        }

        private void button_Click1(object sender, RoutedEventArgs e)
        {
            var window = new ReportRevenueByMonthWindow();
            window.Show();
        }
        private void button_Click2(object sender, RoutedEventArgs e)
        {
            var window = new ReportRevenueByYearWindow();
            window.Show();
        }

        private void button_Click3(object sender, RoutedEventArgs e)
        {
            var window = new ReportRevenueByTypeRoomWindow();
            window.Show();
        }

        private void button_Click4(object sender, RoutedEventArgs e)
        {
                 var window = new ReportStatusRoomrWindow();
            window.Show();
        }

        private void button_Click5(object sender, RoutedEventArgs e)
        {
            var window = new ReportEmptyRoomWindow();
            window.Show();
        }


    }
}
