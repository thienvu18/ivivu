﻿using System;
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

        private void ListViewItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var left = this.Left;
            var top = this.Top;
            var height = this.Height;
            var width = this.Width;

            var window = new Bill
            {
                Left = left,
                Top = top,
                Width = width,
                Height = height
            };

            window.Show();
            this.Close();
        }

        private void ListViewItem_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        {
            var left = this.Left;
            var top = this.Top;
            var height = this.Height;
            var width = this.Width;

            var window = new Report
            {
                Left = left,
                Top = top,
                Width = width,
                Height = height
            };

            window.Show();
            this.Close();
        }

        private void ListViewItem_PreviewMouseDown_2(object sender, MouseButtonEventArgs e)
        {
            var left = this.Left;
            var top = this.Top;
            var height = this.Height;
            var width = this.Width;

            var window = new SearchBill
            {
                Left = left,
                Top = top,
                Width = width,
                Height = height
            };

            window.Show();
            this.Close();
        }

        private void ListViewItem_PreviewMouseDown_5(object sender, MouseButtonEventArgs e)
        {
            var left = this.Left;
            var top = this.Top;
            var height = this.Height;
            var width = this.Width;

            var window = new CheckRoomStatus
            {
                Left = left,
                Top = top,
                Width = width,
                Height = height
            };

            window.Show();
            this.Close();
        }

        private void ListViewItem_PreviewMouseDown_3(object sender, MouseButtonEventArgs e)
        {
            //đăng xuất
            if (Auth.isCustomerLogged == false && Auth.isEmployeeLogged == false)
            {
                MessageBox.Show("Bạn chưa đăng nhập!");
            }
            else
            {
                Auth.isEmployeeLogged = false;
                Auth.isCustomerLogged = false;

                var window = new MainWindow();

                window.ShowDialog();
                this.Close();
            }
        }

        private void ListViewItem_PreviewMouseDown_4(object sender, MouseButtonEventArgs e)
        {
            //thoát
            Application.Current.Shutdown();
        }


    }
}
