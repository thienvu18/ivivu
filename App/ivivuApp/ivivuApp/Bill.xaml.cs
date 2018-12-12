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
            public int price { get; set; }
            public int days { get; set; }
            public int total { get; set; }
        }

        public Bill()
        {
            InitializeComponent();
        }

        private void btn_pre_bill_Click(object sender, RoutedEventArgs e)
        {

            SqlCommand cmd = new SqlCommand("proc_SetBill", Database.connection);

            // Kiểu của Command là StoredProcedure
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@maDatPhong", ID_book.Text));
            
            SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            int id = (int)returnParameter.Value;

            if(id == 0)
            {
                MessageBox.Show("Mã đặt phòng không tồn tại!");
            }
            else
            {
                
            }

            //show bill
            canv_bill_detail.Visibility = Visibility.Visible;
        }

        private void Load_bill(object sender, RoutedEventArgs e)
        {
            string sqlHD = "SELECT HoaDon.maHD, HoaDon.ngayThanhToan, HoaDon.TongTien FROM HoaDon WHERE HoaDon.maDP = @maDatPhong";
            using (SqlCommand commandHD = new SqlCommand(sqlHD, Database.connection))
            {
                using(SqlDataReader readerHD = commandHD.ExecuteReader())
                {
                    int _idBill;
                    string _dateCreat;
                    int _total;

                    while(readerHD.Read())
                    {
                        _idBill = readerHD.GetInt32(0);
                        _dateCreat = readerHD.GetString(1);
                        _total = readerHD.GetInt32(2);
                    }

                }
            

            string sql = "SELECT LoaiPhong.tenLoaiPhong, LoaiPhong.donGia, DATEDIFF(DAY, DatPhong.ngayTraPhong, DatPhong.ngayBatDau) AS numDay FROM HoaDon, LoaiPhong, DatPhong WHERE maHD = 1 AND HoaDon.maDP = DatPhong.maDP AND DatPhong.maLoaiPhong = LoaiPhong.maLoaiPhong";
            using (SqlCommand command = new SqlCommand(sql, Database.connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                { 
                    int _price;
                    string _typeRoom;
                    int _days;
                    

                    while (reader.Read())
                    {
                        _price = reader.GetInt32(1);
                        _typeRoom = reader.GetString(0);
                        _days = reader.GetInt32(2);
                    }
                }
            }
        }
    }
}
