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
using System.Data.Common;


namespace ivivuApp
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void btn_confirm_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("proc_signUpUser", Database.connection);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@FullName", txt_fullname.Text));
            cmd.Parameters.Add(new SqlParameter("@UserName", txt_username.Text));
            cmd.Parameters.Add(new SqlParameter("@Pass", txt_pass.Password));
            cmd.Parameters.Add(new SqlParameter("@CMND", txt_cmnd.Text));
            cmd.Parameters.Add(new SqlParameter("@SDT", txt_phone.Text));
            cmd.Parameters.Add(new SqlParameter("@Address", txt_address.Text));
            cmd.Parameters.Add(new SqlParameter("@Email", txt_email.Text));
            var window = new Login_user();
            this.Close();
            window.ShowDialog();
        }
    }
}
