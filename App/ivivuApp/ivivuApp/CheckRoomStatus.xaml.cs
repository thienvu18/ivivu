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
    /// Interaction logic for CheckRoomStatus.xaml
    /// </summary>
    public partial class CheckRoomStatus : Window
    {
        public class RoomType
        {
            public int roomTypeId { get; set; }
            public string roomTypeName { get; set; }
            public int hotelId { get; set; }
            public long price { get; set; }
            public string description { get; set; }
            public int availableCount { get; set; }
        }

        private List<RoomType> _roomTypes = new List<RoomType>();

        public CheckRoomStatus()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Auth.isEmployeeLogged != false)
            {
                string sql = "SELECT * FROM LoaiPhong;";
                using (SqlCommand command = new SqlCommand(sql, Database.connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int roomTypeId;
                        string roomTypeName;
                        int hotelId;
                        long price;
                        string description;
                        int availableCount;

                        while (reader.Read())
                        {
                            roomTypeId = reader.GetInt32(0);
                            roomTypeName = reader.IsDBNull(1) ? "" : reader.GetString(1);
                            hotelId = reader.IsDBNull(2) ? -1 : reader.GetInt32(2);
                            price = reader.IsDBNull(3) ? -1 : reader.GetInt64(3);
                            description = reader.IsDBNull(4) ? "" : reader.GetString(4);
                            availableCount = reader.IsDBNull(5) ? -1 : reader.GetInt32(5);

                            _roomTypes.Add(new RoomType() { roomTypeId = roomTypeId, roomTypeName = roomTypeName, hotelId = hotelId, price = price, description = description, availableCount = availableCount });
                        }
                    }
                }
                lvRoomTypes.ItemsSource = _roomTypes;
            }
            else
            {
                var loginWindows = new Login_admin();

                MessageBox.Show("Vui lòng đăng nhập với tư cách nhân viên để sử dụng tính năng này");
                this.Close();
                loginWindows.ShowDialog();
            }

        }

        private void BtCheck_Click(object sender, RoutedEventArgs e)
        {
            if (dpDate.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn ngày để kiểm tra");
                return;
            }
            else
            {
                DateTime date = (DateTime)dpDate.SelectedDate;
                var roomTypeId = ((RoomType)lvRoomTypes.SelectedItem).roomTypeId;

                var listRooms = new ListRooms(roomTypeId, date);
                listRooms.ShowDialog();
            }
        }
    }
}
