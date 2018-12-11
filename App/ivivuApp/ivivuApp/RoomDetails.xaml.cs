using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for RoomDetails.xaml
    /// </summary>
    public partial class RoomDetails : Window
    {
        private int _roomId;

        public RoomDetails()
        {
            InitializeComponent();
        }

        public RoomDetails(int roomId)
        {
            _roomId = roomId;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT Phong.soPhong , LoaiPhong.tenLoaiPhong, KhachSan.tenKS, LoaiPhong.donGia, KhachSan.soNha, KhachSan.duong, KhachSan.quan, KhachSan.thanhPho FROM Phong JOIN LoaiPhong on Phong.loaiPhong = LoaiPhong.maLoaiPhong JOIN KhachSan ON LoaiPhong.maKS = KhachSan.maKS WHERE Phong.maPhong = " + _roomId.ToString();
            using (SqlCommand command = new SqlCommand(sql, Database.connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tbRoomNumber.Text = "Số phòng: " + (reader.IsDBNull(0) ? "" : reader.GetString(0));
                        tbRoomType.Text = "Loại phòng: " + (reader.IsDBNull(1) ? "" : reader.GetString(1));
                        tbHotelName.Text = "Khách sạn: " + (reader.IsDBNull(2) ? "" : reader.GetString(2));
                        tbPrice.Text = "Đơn giá: " + (reader.IsDBNull(3) ? "0" : reader.GetInt64(3).ToString());
                        tbAddress.Text = "Địa chỉ: " + (reader.GetString(4) + ", " + reader.GetString(5) + ", " + reader.GetString(6) + ", " + reader.GetString(7));
                    }
                }
            }
            tbEmployeeId.Text = "Mã nhân viên: " + Auth.employee.maNV.ToString();
            tbEmployeeName.Text = "Họ tên: " + Auth.employee.hoTen;
        }
    }
}
