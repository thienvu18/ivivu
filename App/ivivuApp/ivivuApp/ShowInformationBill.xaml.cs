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
        public class BillInfo
        {
            public int billID { get; set; }
            public string dateCreat { get; set; }
            public string typeRoom { get; set; }
            public long price { get; set; }
            public int days { get; set; }
            public long total { get; set; }
        }
        public ShowInformation_Bill(int ma)
        {
            InitializeComponent();
            getData(ma);

        }
        private BillInfo _bill = new BillInfo();
        int _idBill;
        string _dateCreate;
        long _total;
        long _price;
        string _typeRoom;
        int _days;

        public void getData(int ma)
        {
            string sqlHD = "SELECT HoaDon.maHD, HoaDon.TongTien FROM HoaDon WHERE HoaDon.maDP = " + ma;
            using (SqlCommand commandHD = new SqlCommand(sqlHD, Database.connection))
            {
                using (SqlDataReader readerHD = commandHD.ExecuteReader())
                {
                    while (readerHD.Read())
                    {
                        _idBill = readerHD.GetInt32(0);
                        _total = readerHD.GetInt64(1);
                    }

                    _dateCreate = DateTime.Now.ToString();
                    _bill.total = _total;
                    _bill.dateCreat = _dateCreate;
                    _bill.billID = _idBill;

                }

                string sql = "SELECT DISTINCT LoaiPhong.tenLoaiPhong, LoaiPhong.donGia, DATEDIFF(DAY, DatPhong.ngayTraPhong, DatPhong.ngayBatDau) AS numDay FROM HoaDon, LoaiPhong, DatPhong WHERE DatPhong.maDP = " + ma + " AND DatPhong.maDP = HoaDon.maDP AND DatPhong.maLoaiPhong = LoaiPhong.maLoaiPhong";
                using (SqlCommand command1 = new SqlCommand(sql, Database.connection))
                {
                    using (SqlDataReader reader = command1.ExecuteReader())
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
            txt_num_day.Text = _bill.days.ToString();
            txt_ID_room.Text = _bill.typeRoom;
            txt_ID_bill.Text = _bill.billID.ToString();
            txt_total_price.Text = _bill.total.ToString();
            txt_date_create.Text = _bill.dateCreat;


        }




        private void click_in(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng chưa được hỗ trợ!");
        }
    }
}