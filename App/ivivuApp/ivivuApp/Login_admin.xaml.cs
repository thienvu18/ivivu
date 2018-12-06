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
using System.Data;
using System.Data.SqlClient;

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
            SqlCommand cmd = new SqlCommand("proc_LoginEmployee", Database.connection);

            // Kiểu của Command là StoredProcedure
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@tenDangNhap", username.Text));
            cmd.Parameters.Add(new SqlParameter("@matKhau", password.Password));
            SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            int id = (int)returnParameter.Value;
            if (id == 1)
            {
                var window = new Home();
                Auth.isEmployeeLogged = true;
                this.Close();
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công");
            }
        }
    }
}
