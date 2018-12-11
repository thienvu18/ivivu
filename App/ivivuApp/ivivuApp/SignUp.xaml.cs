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
    /// 

    public partial class SignUp : Window
    {
        bool c_fullname = true;
        bool c_username = true;
        bool c_pass = true;
        bool c_retype_pass = true;
        bool c_email = true;
        bool c_phone = true;
        bool c_cmnd = true;
        bool c_address = true;

        public SignUp()
        {
            InitializeComponent();
        }

        private void btn_confirm_Click(object sender, RoutedEventArgs e)
        {
            c_fullname = true;
            c_username = true;
            c_pass = true;
            c_retype_pass = true;
            c_email = true;
            c_phone = true;
            c_cmnd = true;
            c_address = true;

            string notify = "";
            bool checkinput = checkInfo(ref notify);

            if (checkinput)
            {
                SqlCommand cmd = new SqlCommand("proc_signUpUser", Database.connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@FullName", txt_fullname.Text));
                cmd.Parameters.Add(new SqlParameter("@UserName", txt_username.Text));
                cmd.Parameters.Add(new SqlParameter("@Pass", txt_pass.Password));
                cmd.Parameters.Add(new SqlParameter("@CMND", txt_cmnd.Text));
                cmd.Parameters.Add(new SqlParameter("@SDT", txt_phone.Text));
                cmd.Parameters.Add(new SqlParameter("@Address", txt_address.Text));
                cmd.Parameters.Add(new SqlParameter("@Mail", txt_email.Text));

                SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                int id = (int)returnParameter.Value;

                if (id == 1)
                {
                    MessageBox.Show("Đăng ký thành công!");

                    var window = new Login_user();
                    this.Close();
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập đã trùng. Mời bạn thử lại!");
                }
            }
            else
            {
                MessageBox.Show(notify + "Mời nhập lại thông tin không đúng!");
            }



        }

        private bool checkInfo(ref string notify)
        {
            bool flag = true;

            //check fullName
            if (txt_fullname.Text == "")
            {
                notify += "Họ tên không đúng. ";
                c_fullname = false;
                flag = false;
            }

            //check address
            if (txt_address.Text == "")
            {
                notify += "Địa chỉ không đúng. ";
                c_address = false;
                flag = false;
            }

            //check username
            if (txt_username.Text == "" || txt_username.Text.IndexOf(" ") != -1)
            {
                notify += "Tên đăng không bỏ trống và không có khoảng trắng. ";
                c_username = false;
                flag = false;
            }


            //check passs
            if (txt_pass.Password == "" || txt_retype_pass.Password == "" ||
                txt_pass.Password != txt_retype_pass.Password)
            {
                notify += "Mật khẩu không đúng. ";
                c_pass = false;
                c_retype_pass = false;
                flag = false;
            }

            //check sdt
            string phone = txt_phone.Text;
            if (phone == "")
            {
                notify += "Số điện thoại không đúng. ";
                flag = false;
            }
            else
            {
                foreach (var item in phone)
                {
                    if (item < '0' || item > '9')
                    {
                        notify += "Số điện thoại không đúng. ";
                        flag = false;
                        break;
                    }
                }
            }


            //check cmnd
            string cmnd = txt_cmnd.Text;
            if (cmnd == "")
            {
                notify += "CMND không đúng. ";
                flag = false;
            }
            else
            {
                foreach (var item in cmnd)
                {
                    if (item < '0' || item > '9')
                    {
                        notify += "CMND không đúng. ";
                        flag = false;
                        break;
                    }
                }
            }


            //check mail
            if (txt_email.Text.IndexOf("@gmail.com") == -1)
            {
                notify += "Email không đúng. ";
                flag = false;
            }

            if (!flag)
                return false;

            return true;

        }
    }
}
