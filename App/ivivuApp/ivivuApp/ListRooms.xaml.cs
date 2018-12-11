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
    /// Interaction logic for ListRooms.xaml
    /// </summary>
    public partial class ListRooms : Window
    {
        public class RoomStatus
        {
            public int roomId { get; set; }
            public string roomNumber { get; set; }
            public string status { get; set; }
        }

        private List<RoomStatus> _roomsStatus = new List<RoomStatus>();
        private int _roomTypeId;
        private DateTime _date;

        public ListRooms()
        {
            _roomTypeId = -1;
            _date = new DateTime(0);
            InitializeComponent();
        }

        public ListRooms(int roomTypeId, DateTime date)
        {
            _roomTypeId = roomTypeId;
            _date = date;
            InitializeComponent();
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT Phong.maPhong, soPhong, tinhTrang FROM Phong JOIN TrangThaiPhong ON Phong.maPhong = TrangThaiPhong.maPhong WHERE Phong.loaiPhong = " + _roomTypeId + " AND TrangThaiPhong.ngay = '" + _date.ToString("yyyy-MM-dd") + "'";
            using (SqlCommand command = new SqlCommand(sql, Database.connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    int roomId;
                    string roomNumber;
                    string status;

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            roomId = reader.GetInt32(0);
                            roomNumber = reader.IsDBNull(1) ? "" : reader.GetString(1);
                            status = reader.IsDBNull(2) ? "" : reader.GetString(2);

                            _roomsStatus.Add(new RoomStatus() { roomId = roomId, roomNumber = roomNumber, status = status });
                        }
                    }
                    else
                    {
                        MessageBox.Show("No room");
                    }
                }
            }
            lvRoomsStatus.ItemsSource = _roomsStatus;
        }

        private void LvRoomsStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RoomStatus roomStatus = lvRoomsStatus.SelectedItem as RoomStatus;

            MessageBox.Show(roomStatus.roomId.ToString());
            return;
        }
    }
}
