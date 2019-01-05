using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ChooseRoom.xaml
    /// </summary>
    public partial class ChooseRoom : Window
    {
        private class Room
        {
            public int roomId { get; set; }
            public string roomNumber { get; set; }
            public string roomType { get; set; }
            public long price { get; set; }
            public string description { get; set; }
            public bool isChosen { get; set; }
        }

        private List<Room> _rooms = new List<Room>();
        private int _hotelId;

        public ChooseRoom(int hotelId)
        {
            this._hotelId = hotelId;
            InitializeComponent();
        }

        public ChooseRoom()
        {
            InitializeComponent();

        }

        private int BookRoom(int roomId, int customerId, DateTime startDate, DateTime endDate, string description)
        {
            SqlCommand cmd = new SqlCommand("proc_BookRoom", Database.connection);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@MaPhong", roomId));
            cmd.Parameters.Add(new SqlParameter("@MaKhachHang", customerId));
            cmd.Parameters.Add(new SqlParameter("@NgayBatDau", startDate.ToString("yyyy-MM-dd")));
            cmd.Parameters.Add(new SqlParameter("@NgayTraPhong", endDate.ToString("yyyy-MM-dd")));
            cmd.Parameters.Add(new SqlParameter("@MoTa", description));

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                return -1;
            }

            return 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

                string sql = "SELECT Phong.maPhong, Phong.soPhong , LoaiPhong.tenLoaiPhong, LoaiPhong.donGia, LoaiPhong.moTa FROM Phong JOIN LoaiPhong on Phong.loaiPhong = LoaiPhong.maLoaiPhong WHERE LoaiPhong.maKS = " + _hotelId.ToString();
                using (SqlCommand command = new SqlCommand(sql, Database.connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int roomId;
                        string roomNumber;
                        string roomType;
                        long price;
                        string description;

                        while (reader.Read())
                        {
                            roomId = reader.IsDBNull(0) ? -1 : reader.GetInt32(0);
                            roomNumber = reader.IsDBNull(1) ? "" : reader.GetString(1);
                            roomType = reader.IsDBNull(2) ? "" : reader.GetString(2);
                            price = reader.IsDBNull(3) ? -1 : reader.GetInt64(3);
                            description = reader.IsDBNull(4) ? "" : reader.GetString(4);

                            _rooms.Add(new Room() { roomId = roomId, roomNumber = roomNumber, roomType = roomType, price = price, description = description, isChosen = false });
                        }
                    }
                }
                lvRooms.ItemsSource = _rooms;
            
        }

        private void BtDone_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate;
            DateTime endDate;
            List<int> failRooms = new List<int>();
            if (dpStartDate.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn ngày nhận phòng");
                return;
            }
            startDate = (DateTime)dpStartDate.SelectedDate;

            if (dpEndDate.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn ngày trả phòng");
                return;
            }
            endDate = (DateTime)dpEndDate.SelectedDate;

            for (int i = 0; i < _rooms.Count; i++)
            {
                if (_rooms[i].isChosen)
                {
                    
                    if (BookRoom(_rooms[i].roomId, Auth.user.maKH, startDate, endDate, "") != 0)
                    {
                        failRooms.Add(_rooms[i].roomId);
                    }
                }
            }

            if (failRooms.Count > 0)
            {
                string msg = "Chưa đặt được các phòng: ";
                for (int i = 0; i < failRooms.Count - 1; i++)
                {
                    msg += failRooms[i].ToString();
                    msg += ", ";
                }
                msg += failRooms.Last<int>();

                MessageBox.Show(msg);
            }
            else MessageBox.Show("Đặt phòng thành công");
        }
    }
}
