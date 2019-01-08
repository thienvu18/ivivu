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
using System.Globalization;

using System.Data.Common;
using System.Collections.ObjectModel;

namespace ivivuApp
{
    /// <summary>
    /// Interaction logic for SearchBill.xaml
    /// </summary>
    public partial class SearchBill : Window
    {
        ObservableCollection<HoaDon> listHD = new ObservableCollection<HoaDon>();

        public SearchBill()
        {
            InitializeComponent();
            dttimkiemhoadon.ItemsSource = listHD;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(dttimkiemhoadon.ItemsSource);
        }


        //hàm lấy danh sách hóa đơn hiện thị lên 
        public void getData()
        {

            SqlCommand command = new SqlCommand("select top (100) HoaDon.maHD, HoaDon.ngayThanhToan,HoaDon.tongTien,HoaDon.madp,DatPhong.maKH  from HoaDon JOIN DatPhong ON HoaDon.maDP = DatPhong.maDP", Database.connection);
            SqlDataReader sqlReader = command.ExecuteReader();
            while (sqlReader.Read())
            {
                HoaDon hd = new HoaDon
                {
                    maHD = sqlReader.IsDBNull(0) ? -1 : sqlReader.GetInt32(0),
                    ngayThanhToan = sqlReader.IsDBNull(1) ? new DateTime() : sqlReader.GetDateTime(1),
                    tongTien = sqlReader.IsDBNull(2) ? -1 : sqlReader.GetInt64(2),
                    maDP = sqlReader.IsDBNull(3) ? -1 : sqlReader.GetInt32(3),
                    maKH = sqlReader.IsDBNull(4) ? -1 : sqlReader.GetInt32(4)
                };

                string date1 = hd.ngayThanhToan.ToString();
                for (int i = 0; i < date1.Length; i++)
                {
                    if (date1[i] == ' ')
                    {
                        hd.date = date1.Remove(i);
                        break;
                    }
                }
                if (hd != null)
                {
                    listHD.Add(hd);

                }


            }
            sqlReader.Close();

        }

        //=====hàm lấy dữ liệu trên datagrid
        private void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataGridCell myCell = sender as DataGridCell;
            DataGridRow row = DataGridRow.GetRowContainingElement(myCell);

            HoaDon temp = row.Item as HoaDon;

            try
            {


                for (int i = 0; i < listHD.Count; i++)
                {
                    if (temp == listHD[i])
                    {
                        dttimkiemhoadon.ItemsSource = null;
                        dttimkiemhoadon.ItemsSource = listHD;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }



            if (temp != null)
            {
                ShowInformation_Bill window = new ShowInformation_Bill(temp.maHD);
                window.ShowDialog();
            }

        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            resetTxtMakh.Visibility = Visibility.Visible;
            if (txtmakh.Text == "")
            {
                resetTxtMakh.Visibility = Visibility.Collapsed;


            }
        }

        private void resetTxtMakh_Click(object sender, RoutedEventArgs e)
        {
            txtmakh.Text = "";
        }

        private void txtthanhtien_TextChanged(object sender, TextChangedEventArgs e)
        {
            resetTxtThanhtien.Visibility = Visibility.Visible;
            if (txtthanhtien.Text == "")
            {
                resetTxtThanhtien.Visibility = Visibility.Collapsed;


            }
        }

        private void resetTxtThanhtien_Click(object sender, RoutedEventArgs e)
        {
            txtthanhtien.Text = "";
        }


        private void bttimkiem_Click(object sender, RoutedEventArgs e)
        {
            string ngayThanhToan = "";
            if (ngaylap.SelectedDate != null) {
                ngayThanhToan = ((DateTime)ngaylap.SelectedDate).ToString("yyyy-MM-dd");
            }
            //================================================
            if (txtthanhtien.Text == "" && ngayThanhToan == "" && txtmakh.Text != "")
            {
                SqlCommand cmd = new SqlCommand("usp_timKiemTTHD_MaKH", Database.connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@maKH", txtmakh.Text));
                SqlDataReader sqlReader = cmd.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    listHD.Clear();
                    while (sqlReader.Read())
                    {
                        HoaDon hd = new HoaDon
                        {

                            maHD = sqlReader.IsDBNull(0) ? -1 : sqlReader.GetInt32(0),
                            ngayThanhToan = sqlReader.IsDBNull(1) ? new DateTime(): sqlReader.GetDateTime(1),
                            tongTien = sqlReader.IsDBNull(2) ? -1 : sqlReader.GetInt64(2),
                            maDP = sqlReader.IsDBNull(3) ? -1 : sqlReader.GetInt32(3),
                            maKH = sqlReader.IsDBNull(4) ? -1 : sqlReader.GetInt32(4)
                        };
                        string date1 = hd.ngayThanhToan.ToString();
                        for (int i = 0; i < date1.Length; i++)
                        {
                            if (date1[i] == ' ')
                            {
                                hd.date = date1.Remove(i);
                                break;
                            }
                        }
                        if (hd != null)
                        {
                            dttimkiemhoadon.ItemsSource = null;
                            listHD.Add(hd);

                        }

                    }
                    sqlReader.Close();
                    dttimkiemhoadon.ItemsSource = listHD;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hóa đơn có thông tin đã nhập");
                    sqlReader.Close();
                }
            }

            //===================================
            if (txtthanhtien.Text != "" && ngayThanhToan == "" && txtmakh.Text == "")
            {
                SqlCommand cmd = new SqlCommand("usp_timKiemTTHD_ThanhTien", Database.connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@thanhtien", txtthanhtien.Text));
                SqlDataReader sqlReader = cmd.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    listHD.Clear();
                    while (sqlReader.Read())
                    {
                        HoaDon hd = new HoaDon
                        {
                            maHD = sqlReader.IsDBNull(0) ? -1 : sqlReader.GetInt32(0),
                            ngayThanhToan = sqlReader.IsDBNull(1) ? new DateTime() : sqlReader.GetDateTime(1),
                            tongTien = sqlReader.IsDBNull(2) ? -1 : sqlReader.GetInt64(2),
                            maDP = sqlReader.IsDBNull(3) ? -1 : sqlReader.GetInt32(3),
                            maKH = sqlReader.IsDBNull(4) ? -1 : sqlReader.GetInt32(4)
                        };
                        string date1 = hd.ngayThanhToan.ToString();
                        for (int i = 0; i < date1.Length; i++)
                        {
                            if (date1[i] == ' ')
                            {
                                hd.date = date1.Remove(i);
                                break;
                            }
                        }
                        if (hd != null)
                        {
                            dttimkiemhoadon.ItemsSource = null;
                            listHD.Add(hd);

                        }

                    }
                    sqlReader.Close();
                    dttimkiemhoadon.ItemsSource = listHD;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hóa đơn có thông tin đã nhập");
                    sqlReader.Close();
                }
            }

            //=====================================
            if (txtthanhtien.Text == "" && ngayThanhToan != "" && txtmakh.Text == "")
            {
                SqlCommand cmd = new SqlCommand("usp_timKiemTTHD_NgayLap", Database.connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ngayThanhToan", ngayThanhToan));
                SqlDataReader sqlReader = cmd.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    listHD.Clear();
                    while (sqlReader.Read())
                    {
                        HoaDon hd = new HoaDon
                        {
                            maHD = sqlReader.IsDBNull(0) ? -1 : sqlReader.GetInt32(0),
                            ngayThanhToan = sqlReader.IsDBNull(1) ? new DateTime() : sqlReader.GetDateTime(1),
                            tongTien = sqlReader.IsDBNull(2) ? -1 : sqlReader.GetInt64(2),
                            maDP = sqlReader.IsDBNull(3) ? -1 : sqlReader.GetInt32(3),
                            maKH = sqlReader.IsDBNull(4) ? -1 : sqlReader.GetInt32(4)
                        };
                        string date1 = hd.ngayThanhToan.ToString();
                        for (int i = 0; i < date1.Length; i++)
                        {
                            if (date1[i] == ' ')
                            {
                                hd.date = date1.Remove(i);
                                break;
                            }
                        }
                        if (hd != null)
                        {
                            dttimkiemhoadon.ItemsSource = null;
                            listHD.Add(hd);

                        }

                    }
                    sqlReader.Close();
                    dttimkiemhoadon.ItemsSource = listHD;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hóa đơn có thông tin đã nhập");
                    sqlReader.Close();
                }
            }

            //=====================================
            if (txtthanhtien.Text != "" && ngayThanhToan != "" && txtmakh.Text == "")
            {
                SqlCommand cmd = new SqlCommand("usp_timKiemTTHD_ngaylap_tongtien", Database.connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ngayLap", ngayThanhToan));
                cmd.Parameters.Add(new SqlParameter("@thanhTien", txtthanhtien.Text));
                SqlDataReader sqlReader = cmd.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    listHD.Clear();
                    while (sqlReader.Read())
                    {
                        HoaDon hd = new HoaDon
                        {
                            maHD = sqlReader.IsDBNull(0) ? -1 : sqlReader.GetInt32(0),
                            ngayThanhToan = sqlReader.IsDBNull(1) ? new DateTime() : sqlReader.GetDateTime(1),
                            tongTien = sqlReader.IsDBNull(2) ? -1 : sqlReader.GetInt64(2),
                            maDP = sqlReader.IsDBNull(3) ? -1 : sqlReader.GetInt32(3),
                            maKH = sqlReader.IsDBNull(4) ? -1 : sqlReader.GetInt32(4)
                        };
                        string date1 = hd.ngayThanhToan.ToString();
                        for (int i = 0; i < date1.Length; i++)
                        {
                            if (date1[i] == ' ')
                            {
                                hd.date = date1.Remove(i);
                                break;
                            }
                        }
                        if (hd != null)
                        {
                            dttimkiemhoadon.ItemsSource = null;
                            listHD.Add(hd);

                        }

                    }
                    sqlReader.Close();
                    dttimkiemhoadon.ItemsSource = listHD;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hóa đơn có thông tin đã nhập");
                    sqlReader.Close();
                }
            }

            //=====================================
            if (txtthanhtien.Text != "" && ngayThanhToan == "" && txtmakh.Text != "")
            {
                SqlCommand cmd = new SqlCommand("usp_timKiemTTHD_maKH_ThanhTien", Database.connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@maKH", txtmakh.Text));
                cmd.Parameters.Add(new SqlParameter("@thanhTien", txtthanhtien.Text));
                SqlDataReader sqlReader = cmd.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    listHD.Clear();
                    while (sqlReader.Read())
                    {
                        HoaDon hd = new HoaDon
                        {
                            maHD = sqlReader.IsDBNull(0) ? -1 : sqlReader.GetInt32(0),
                            ngayThanhToan = sqlReader.IsDBNull(1) ? new DateTime() : sqlReader.GetDateTime(1),
                            tongTien = sqlReader.IsDBNull(2) ? -1 : sqlReader.GetInt64(2),
                            maDP = sqlReader.IsDBNull(3) ? -1 : sqlReader.GetInt32(3),
                            maKH = sqlReader.IsDBNull(4) ? -1 : sqlReader.GetInt32(4)
                        };
                        string date1 = hd.ngayThanhToan.ToString();
                        for (int i = 0; i < date1.Length; i++)
                        {
                            if (date1[i] == ' ')
                            {
                                hd.date = date1.Remove(i);
                                break;
                            }
                        }
                        if (hd != null)
                        {
                            dttimkiemhoadon.ItemsSource = null;
                            listHD.Add(hd);

                        }

                    }
                    sqlReader.Close();
                    dttimkiemhoadon.ItemsSource = listHD;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hóa đơn có thông tin đã nhập");
                    sqlReader.Close();
                }
            }
            if (txtthanhtien.Text == "" && ngayThanhToan != "" && txtmakh.Text != "")
            {
                SqlCommand cmd = new SqlCommand("usp_timKiemTTHD_maKH_ngaylap", Database.connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@maKH", txtmakh.Text));
                cmd.Parameters.Add(new SqlParameter("@ngayLap", ngayThanhToan));
                SqlDataReader sqlReader = cmd.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    listHD.Clear();
                    while (sqlReader.Read())
                    {
                        HoaDon hd = new HoaDon
                        {
                            maHD = sqlReader.IsDBNull(0) ? -1 : sqlReader.GetInt32(0),
                            ngayThanhToan = sqlReader.IsDBNull(1) ? new DateTime() : sqlReader.GetDateTime(1),
                            tongTien = sqlReader.IsDBNull(2) ? -1 : sqlReader.GetInt64(2),
                            maDP = sqlReader.IsDBNull(3) ? -1 : sqlReader.GetInt32(3),
                            maKH = sqlReader.IsDBNull(4) ? -1 : sqlReader.GetInt32(4)
                        };
                        string date1 = hd.ngayThanhToan.ToString();
                        for (int i = 0; i < date1.Length; i++)
                        {
                            if (date1[i] == ' ')
                            {
                                hd.date = date1.Remove(i);
                                break;
                            }
                        }
                        if (hd != null)
                        {
                            dttimkiemhoadon.ItemsSource = null;
                            listHD.Add(hd);

                        }

                    }
                    sqlReader.Close();
                    dttimkiemhoadon.ItemsSource = listHD;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hóa đơn có thông tin đã nhập");
                    sqlReader.Close();
                }
            }

            if (txtthanhtien.Text != "" && ngaylap.SelectedDate != null && txtmakh.Text != "")
            {
                SqlCommand cmd = new SqlCommand("usp_timKiemTTHD_MaKH_NgayLap_ThanhTien", Database.connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(new SqlParameter("@maKH", txtmakh.Text));
                cmd.Parameters.Add(new SqlParameter("@ngayThanhToan", ngayThanhToan));
                cmd.Parameters.Add(new SqlParameter("@thanhTien", txtthanhtien.Text));
                SqlDataReader sqlReader = cmd.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    listHD.Clear();
                    while (sqlReader.Read())
                    {
                        HoaDon hd = new HoaDon
                        {
                            maHD = sqlReader.IsDBNull(0) ? -1 : sqlReader.GetInt32(0),
                            ngayThanhToan = sqlReader.IsDBNull(1) ? new DateTime() : sqlReader.GetDateTime(1),
                            tongTien = sqlReader.IsDBNull(2) ? -1 : sqlReader.GetInt64(2),
                            maDP = sqlReader.IsDBNull(3) ? -1 : sqlReader.GetInt32(3),
                            maKH = sqlReader.IsDBNull(4) ? -1 : sqlReader.GetInt32(4)
                        };
                        string date1 = hd.ngayThanhToan.ToString();
                        for (int i = 0; i < date1.Length; i++)
                        {
                            if (date1[i] == ' ')
                            {
                                hd.date = date1.Remove(i);
                                break;
                            }
                        }
                        if (hd != null)
                        {
                            dttimkiemhoadon.ItemsSource = null;
                            listHD.Add(hd);

                        }

                    }
                    sqlReader.Close();
                    dttimkiemhoadon.ItemsSource = listHD;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hóa đơn có thông tin đã nhập");
                    sqlReader.Close();
                }
            }
            if (txtthanhtien.Text == "" && ngayThanhToan == "" && txtmakh.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập thông tin cần tìm");
            }
        }









        private void ListViewItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var left = this.Left;
            var top = this.Top;
            var height = this.Height;
            var width = this.Width;

            var window = new Bill
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getData();
        }
    }
}
