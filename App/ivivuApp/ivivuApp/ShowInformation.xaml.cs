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
using System.Data;
using System.Data.SqlClient;

using System.Data.Common;

namespace ivivuApp
{
    /// <summary>
    /// Interaction logic for ShowInformation.xaml
    /// </summary>
    public partial class ShowInformation : Window
    {
        public int maks { get; set; }
        public ShowInformation(int value)
        {
            InitializeComponent();
            maks = value;
            getDada();

        }
        public void getDada()
        {

            SqlCommand command = new SqlCommand("select * from KhachSan where maKS =" + maks, Database.connection);
            SqlDataReader sqlReader = command.ExecuteReader();
            if (sqlReader.Read())
            {
                txmaks.Text = sqlReader.GetInt32(0).ToString();
                txtenks.Text = sqlReader.GetString(1);
                sosao.Value = sqlReader.GetInt32(2);
                txdiachi.Text = sqlReader.GetString(3) + ',' + sqlReader.GetString(4) + ',' + sqlReader.GetString(5) + ',' + sqlReader.GetString(6);
                txgia.Text = sqlReader.GetInt64(7).ToString();
                txtmota.Text = sqlReader.GetString(8);
            }
            sqlReader.Close();

        }

        private void click_datphong(object sender, RoutedEventArgs e)
        {
            Auth.employee.maKS = maks;
            if (Auth.isCustomerLogged == false)
            {
                var loginWindows = new Login_user()
                {
                    Left = 250 ,
                    Top = 130,
                    Width = 1000,
                    Height = 600
                };

                MessageBox.Show("Vui lòng đăng nhập để sử dụng tính năng này");
                loginWindows.Show();
                this.Close();
            }
            else
            {
                var left = this.Left;
                var top = this.Top;
                var height = this.Height;
                var width = this.Width;

                var window = new ChooseRoom(maks)
                {
                    Left = left,
                    Top = top,
                    Width = width,
                    Height = height
                };

                window.Show();
            }
        }
    }

}
