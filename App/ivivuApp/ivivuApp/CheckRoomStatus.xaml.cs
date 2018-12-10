﻿using System;
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
        public class RoomStatus
        {
            public int roomTypeId { get; set; }
            public string roomTypeName { get; set; }
            public int hotelId { get; set; }
            public long price { get; set; }
            public string description { get; set; }
            public int availableCount { get; set; }
            public bool isChosen { get; set; }
        }

        private List<RoomStatus> _roomsStatus = new List<RoomStatus>();

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
                        bool isChosen = false;

                        while (reader.Read())
                        {
                            roomTypeId = reader.GetInt32(0);
                            roomTypeName = reader.IsDBNull(1) ? "" : reader.GetString(1);
                            hotelId = reader.IsDBNull(2) ? -1 : reader.GetInt32(2);
                            price = reader.IsDBNull(3) ? -1 : reader.GetInt64(3);
                            description = reader.IsDBNull(4) ? "" : reader.GetString(4);
                            availableCount = reader.IsDBNull(5) ? -1 : reader.GetInt32(5);

                            _roomsStatus.Add(new RoomStatus() { roomTypeId = roomTypeId, roomTypeName = roomTypeName, hotelId = hotelId, price = price, description = description, availableCount = availableCount, isChosen = isChosen });
                        }
                    }
                }
                lvRoomsStatus.ItemsSource = _roomsStatus;
            }
            else
            {
                var loginWindows = new Login_admin();

                MessageBox.Show("Vui lòng đăng nhập để sử dụng tính năng này");
                this.Close();
                loginWindows.ShowDialog();
            }

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton bt = (RadioButton)sender;
            int curentId = ((RoomStatus)bt.DataContext).roomTypeId;

            for (int i = 0; i < _roomsStatus.Count; i++)
            {
                if (_roomsStatus[i].isChosen == true && _roomsStatus[i].roomTypeId != curentId)
                _roomsStatus[i].isChosen = false;
            }

            lvRoomsStatus.Items.Refresh();
        }
    }
}
