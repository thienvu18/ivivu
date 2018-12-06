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

            SqlCommand cmd = new SqlCommand("proc_LoginCustomer", Database.connection);

            // Kiểu của Command là StoredProcedure
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@tenDangNhap", username.Text));
            cmd.Parameters.Add(new SqlParameter("@matKhau", passwordBox.Password));
            SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            int id = (int)returnParameter.Value;
            if (id == 1)
            {
                var window = new Home();
                Auth.isCustomerLogged = true;
                this.Close();
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công");
            }
        }

        private void btn_signup_Click(object sender, RoutedEventArgs e)
        {
            var window = new SignUp();
            this.Close();
            window.ShowDialog();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
