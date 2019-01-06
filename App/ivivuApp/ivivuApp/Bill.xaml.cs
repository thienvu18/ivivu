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
using System.Data.Common;

namespace ivivuApp
{
    /// <summary>
    /// Interaction logic for Bill.xaml
    /// </summary>
    public partial class Bill : Window
    {
        public class BillInfo
        {
            public int billID { get; set; }
            public string dateCreat { get; set; }
            public string typeRoom { get; set; }
            public long price { get; set; }
            public int days { get; set; }
            public long total { get; set; }
        }

        public Bill()
        {
            InitializeComponent();
        }

        private void btn_pre_bill_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("proc_setBill", Database.connection)
            {

                // Kiểu của Command là StoredProcedure
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add(new SqlParameter("@maDatPhong", ID_book.Text));

            SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            int id = (int)returnParameter.Value;

            if (id == 0)
            {
                MessageBox.Show("Mã đặt phòng không tồn tại!");
            }
            else if(id == 1)
            {
                MessageBox.Show("Hóa đơn cho mã đặt phòng " + ID_book.Text + " đã tồn tại! Mời nhập mã đặt phòng chưa có hóa đơn.");
            }
            else // id == 2
            {
                Load_bill();
                //show bill
                canv_bill_detail.Visibility = Visibility.Visible;
            }
        }

        private BillInfo _bill = new BillInfo();
        int _idBill;
        string _dateCreate;
        long _total;
        long _price;
        string _typeRoom;
        int _days;

        private void Load_bill()
        {
            string sqlHD = "SELECT HoaDon.maHD, HoaDon.TongTien FROM HoaDon WHERE HoaDon.maDP = " + ID_book.Text;
            using (SqlCommand commandHD = new SqlCommand(sqlHD, Database.connection))
            {
                using (SqlDataReader readerHD = commandHD.ExecuteReader())
                {
                    while (readerHD.Read())
                    {
                       _idBill  = readerHD.GetInt32(0);
                       _total= readerHD.GetInt64(1);
                    }

                    _dateCreate = DateTime.Now.ToString();
                    _bill.total = _total;
                    _bill.dateCreat = _dateCreate;
                    _bill.billID = _idBill;

                }

                string sql = "SELECT DISTINCT LoaiPhong.tenLoaiPhong, LoaiPhong.donGia, DATEDIFF(DAY, DatPhong.ngayTraPhong, DatPhong.ngayBatDau) AS numDay FROM HoaDon, LoaiPhong, DatPhong WHERE DatPhong.maDP = " + ID_book.Text + " AND DatPhong.maDP = HoaDon.maDP AND DatPhong.maLoaiPhong = LoaiPhong.maLoaiPhong";
                using (SqlCommand command = new SqlCommand(sql, Database.connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                      
                        while (reader.Read())
                        {
                            _price = reader.GetInt64(1);
                            _typeRoom = reader.GetString(0);
                            _days = reader.GetInt32(2);
                        }

                        _bill.price = _price;
                        _bill.typeRoom = _typeRoom;
                        _bill.days = _days;
                    }
                } 
            }
            txt_price.Text = _bill.price.ToString();
            txt_num_day.Text = _bill.days.ToString() + " Ngày";
            txt_ID_room.Text = _bill.typeRoom;
            txt_ID_bill.Text = _bill.billID.ToString();
            txt_total_price.Text = _bill.total.ToString();
            txt_date_create.Text = _bill.dateCreat;

        }

        private void ListViewItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var left = this.Left;
            var top = this.Top;
            var height = this.Height;
            var width = this.Width;

            var window = new CheckRoomStatus
            {
                Left = left,
                Top = top,
                Width = width,
                Height = height
            };

            window.Show();
            this.Close();
        }

        private void ListViewItem_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        {
            var left = this.Left;
            var top = this.Top;
            var height = this.Height;
            var width = this.Width;

            var window = new Report
            {
                Left = left,
                Top = top,
                Width = width,
                Height = height
            };

            window.Show();
            this.Close();
        }

        private void ListViewItem_PreviewMouseDown_2(object sender, MouseButtonEventArgs e)
        {
            var left = this.Left;
            var top = this.Top;
            var height = this.Height;
            var width = this.Width;

            var window = new SearchBill
            {
                Left = left,
                Top = top,
                Width = width,
                Height = height
            };

            window.Show();
            this.Close();
        }

        private void ListViewItem_PreviewMouseDown_3(object sender, MouseButtonEventArgs e)
        {
            //đăng xuất
            if (Auth.isCustomerLogged == false && Auth.isEmployeeLogged == false)
            {
                MessageBox.Show("Bạn chưa đăng nhập!");
            }
            else
            {
                Auth.isEmployeeLogged = false;
                Auth.isCustomerLogged = false;

                var window = new MainWindow();

                window.Show();
                this.Close();
            }
        }

        private void ListViewItem_PreviewMouseDown_4(object sender, MouseButtonEventArgs e)
        {
            //thoát
            Application.Current.Shutdown();
        }
    }




    //query tim ra ma dat phong chua co hoa don
    /*
     
     select DatPhong.maDP
    from DatPhong
    where not exists (select hd.maDP
				from	HoaDon hd
				where hd.maDP = DatPhong.maDP)
    go
     
     */
}