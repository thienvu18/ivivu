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
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;


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

        private void exit(object sender, MouseButtonEventArgs e)
        {
            //thoát
            Application.Current.Shutdown();
        }

        private void btn_login_user_Click(object sender, RoutedEventArgs e)
        {

            SqlCommand cmd = new SqlCommand("proc_LoginCustomer", Database.connection)
            {

                // Kiểu của Command là StoredProcedure
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add(new SqlParameter("@tenDangNhap", username.Text));
            cmd.Parameters.Add(new SqlParameter("@matKhau", passwordBox.Password));
            SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            int id = (int)returnParameter.Value;
            if (id == 1)
            {
                SqlCommand cmdQuery = new SqlCommand("SELECT * FROM KhachHang WHERE tenDangNhap = @tenDangNhap", Database.connection);
                cmdQuery.Parameters.AddWithValue("@tenDangNhap", username.Text);
                
                using (DbDataReader reader = cmdQuery.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            Auth.user.maKH = Convert.ToInt32(reader.GetValue(0));
                            Auth.user.hoTen = reader.GetValue(1).ToString();
                            Auth.user.tenDangNhap = reader.GetValue(2).ToString();
                            Auth.user.matKhau = reader.GetValue(3).ToString();
                            Auth.user.soCMND = reader.GetValue(4).ToString();
                            Auth.user.diaChi = reader.GetValue(5).ToString();
                            Auth.user.soDienThoai = reader.GetValue(6).ToString();
                            Auth.user.moTa = reader.GetValue(7).ToString();
                            Auth.user.email = reader.GetValue(8).ToString();
                            break;
                        }

                    }
                }

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
                Auth.isCustomerLogged = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công");
            }
        }

        private void btn_signup_Click(object sender, RoutedEventArgs e)
        {
   
            var left = this.Left;
            var top = this.Top;
            var height = this.Height;
            var width = this.Width;

            var window = new SignUp
            {
                Left = left,
                Top = top,
                Width = width,
                Height = height
            };

            window.Show();
            this.Close();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

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
            var window = new Login_user {
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



    }
}
