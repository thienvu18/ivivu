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
    /// Interaction logic for ShowInformation_Bill.xaml
    /// </summary>
    public partial class ShowInformation_Bill : Window
    {
        public ShowInformation_Bill(int ma)
        {
            InitializeComponent();
            getData(ma);

        }
        public void getData(int ma)
        {

            SqlCommand command = new SqlCommand("select HoaDon.maHD, HoaDon.ngayThanhToan,HoaDon.tongTien,HoaDon.madp,DatPhong.maKH,DatPhong.ngayBatDau,DatPhong.ngayTraPhong  from HoaDon JOIN DatPhong ON HoaDon.maDP = DatPhong.maDP where HoaDon.maHD =" + ma, Database.connection);
            SqlDataReader sqlReader = command.ExecuteReader();
            if (sqlReader.Read())
            {

                txtmaHD.Text = sqlReader.GetInt32(0).ToString();
                txtngaylap.Text = sqlReader.GetDateTime(1).ToShortDateString();
                txttongtien.Text = sqlReader.GetInt64(2).ToString();
                txtmaDP.Text = sqlReader.GetInt32(3).ToString();
                txtmaKH.Text = sqlReader.GetInt32(4).ToString();
                txtngaybatdau.Text = sqlReader.GetDateTime(5).ToShortDateString();
                txtngayketthuc.Text = sqlReader.GetDateTime(6).ToShortDateString();


            }
            sqlReader.Close();

        }
        private void click_in(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng chưa được hỗ trợ!");
        }
    }
}
