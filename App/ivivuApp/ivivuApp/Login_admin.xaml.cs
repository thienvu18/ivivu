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
    /// Interaction logic for Login_admin.xaml
    /// </summary>
    public partial class Login_admin : Window
    {
        public Login_admin()
        {
            InitializeComponent();
        }

        private void btn_login_admin_Click(object sender, RoutedEventArgs e)
        {
            var window = new Home_admin();
            this.Close();
            window.ShowDialog();
        }
    }
}
