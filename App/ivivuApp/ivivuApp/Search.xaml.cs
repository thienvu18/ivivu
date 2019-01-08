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
using System.Resources;
using System.Data.Common;
using System.Collections.ObjectModel; // 


namespace ivivuApp
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>

    public partial class Search : Window
    {


        ObservableCollection<KhachSan> listKS1 = new ObservableCollection<KhachSan>();
        ObservableCollection<KhachSan> listKSSearch1 = new ObservableCollection<KhachSan>();
        ObservableCollection<KhachSan> listKS2 = new ObservableCollection<KhachSan>();
        ObservableCollection<KhachSan> listKSSearch2 = new ObservableCollection<KhachSan>();
        public Search()
        {

            InitializeComponent();
            getData();
            dttimkiem2.ItemsSource = listKS2;
            dttimkiem1.ItemsSource = listKS1;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(dttimkiem2.ItemsSource);


        }



        // ---------------hiện thị danh sách khách sạn trên datagrid------------
        public void getData()
        {

            SqlCommand command = new SqlCommand("select top (100) * from KhachSan", Database.connection);
            SqlDataReader sqlReader = command.ExecuteReader();
            while (sqlReader.Read())
            {
                KhachSan sp = new KhachSan
                {
                    maKS = sqlReader.IsDBNull(0) ? -1 : sqlReader.GetInt32(0),
                    tenKS = sqlReader.IsDBNull(1) ? "" : sqlReader.GetString(1),
                    thanhPho = sqlReader.IsDBNull(6) ? "" : sqlReader.GetString(6),
                    giaTB = sqlReader.IsDBNull(7) ? -1 : sqlReader.GetInt64(7),
                    diaChi = (sqlReader.IsDBNull(3) ? "" : sqlReader.GetString(3)) + ',' + (sqlReader.IsDBNull(4) ? "" : sqlReader.GetString(4)) + ',' + (sqlReader.IsDBNull(5) ? "" : sqlReader.GetString(5)) + ',' + (sqlReader.IsDBNull(6) ? "" : sqlReader.GetString(6))
                };

                if (sp != null)
                {
                    listKS1.Add(sp);
                    listKS2.Add(sp);

                }


            }
            sqlReader.Close();

        }


        //==========TÌM KIẾM THEO GIÁ TRUNG BÌNH==============  


        // tab 1 tim kiem theo gia

        private void textBox_TextChanged_tab1(object sender, TextChangedEventArgs e)
        {

            resetTxtSearch.Visibility = Visibility.Visible;

            if (listKSSearch1.Count > 0)
            {
                //Nếu tìm kiếm sản phẩm khác thì reset lại list sản phẩm tìm kiếm
                listKSSearch1.Clear();
            }
            if (txtenthanhpho1.Text.Trim() == "")
            {
                resetTxtSearch.Visibility = Visibility.Collapsed;
                dttimkiem1.ItemsSource = null;
                dttimkiem1.ItemsSource = listKS1;

            }
            else
            {
                for (int i = 0; i < listKS1.Count; i++)
                {
                    string str = listKS1[i].diaChi.Trim().ToLower().Replace(" ", "");
                    //Tách chuỗi nhập vào thành các từ
                    string expression = txtenthanhpho1.Text.ToLower();
                    string[] token = expression.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    int index = 0;
                    //Duyệt qua mảng các từ trong chuỗi nhập
                    for (; index < token.Length; index++)
                    {
                        //Xóa khoảng trắng
                        token[index] = token[index].Trim();
                        //Kiểm tra từ đó xuất hiện trong tên sản phẩm không
                        int pos = listKS1[i].diaChi.ToLower().IndexOf(token[index]);
                        //Nếu không xuất hiện thì
                        if (pos < 0)
                        {
                            break;
                        }
                    }
                    if (index == token.Length)
                    {
                        listKSSearch1.Add(listKS1[i]);
                    }
                }

                dttimkiem1.ItemsSource = null;
                dttimkiem1.ItemsSource = listKSSearch1;


            }





        }
        private void resetTxtSearch_Click_tab1(object sender, RoutedEventArgs e)
        {
            txtenthanhpho1.Text = "";
        }

        private void resetTxtSearch_Click_gia(object sender, RoutedEventArgs e)
        {
            txtgia.Text = "";
        }

        private void textBox_TextChanged_gia(object sender, TextChangedEventArgs e)
        {

            resetTxtSearch_gia.Visibility = Visibility.Visible;
            if (txtgia.Text == "")
            {
                resetTxtSearch.Visibility = Visibility.Collapsed;


            }



        }

        //-------buton tìm kiếm theo giá
        private void timkiemtab1_Click(object sender, RoutedEventArgs e)
        {

            if (txtgia.Text == "" && txtenthanhpho1.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập thông tin cần tìm");
            }

            if (txtgia.Text == "" && txtenthanhpho1.Text != "")
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM KhachSan WHERE thanhPho = @thanhPhoTimKiem", Database.connection)
                {
                    CommandType = CommandType.Text
                };
                cmd.Parameters.Add(new SqlParameter("@thanhPhoTimKiem", txtenthanhpho1.Text));
                SqlDataReader sqlReader = cmd.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    listKS1.Clear();
                    while (sqlReader.Read())
                    {
                        KhachSan sp = new KhachSan
                        {

                            maKS = sqlReader.IsDBNull(0) ? -1 : sqlReader.GetInt32(0),
                            tenKS = sqlReader.IsDBNull(1) ? "" : sqlReader.GetString(1),
                            giaTB = sqlReader.IsDBNull(7) ? -1 : sqlReader.GetInt64(7),
                            diaChi = (sqlReader.IsDBNull(3) ? "" : sqlReader.GetString(3)) + ',' + (sqlReader.IsDBNull(4) ? "" : sqlReader.GetString(4)) + ',' + (sqlReader.IsDBNull(5) ? "" : sqlReader.GetString(5)) + ',' + (sqlReader.IsDBNull(6) ? "" : sqlReader.GetString(6))

                        };

                        if (sp != null)
                        {

                            dttimkiem1.ItemsSource = null;
                            listKS1.Add(sp);
                        }

                    }
                    dttimkiem1.ItemsSource = listKS1;
                    sqlReader.Close();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách sạn nằm trong thành phố " + txtenthanhpho1.Text);
                    sqlReader.Close();
                }

            }
            else

                if (txtenthanhpho1.Text == "" && txtgia.Text != "")
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM KhachSan WHERE giaTB = @giaTB", Database.connection)
                {
                    CommandType = CommandType.Text
                };
                cmd.Parameters.Add(new SqlParameter("@giaTB", txtgia.Text));
                SqlDataReader sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    listKS1.Clear();
                    while (sqlReader.Read())
                    {
                        KhachSan sp = new KhachSan
                        {
                            maKS = sqlReader.IsDBNull(0) ? -1 : sqlReader.GetInt32(0),
                            tenKS = sqlReader.IsDBNull(1) ? "" : sqlReader.GetString(1),
                            giaTB = sqlReader.IsDBNull(7) ? -1 : sqlReader.GetInt64(7),
                            diaChi = (sqlReader.IsDBNull(3) ? "" : sqlReader.GetString(3)) + ',' + (sqlReader.IsDBNull(4) ? "" : sqlReader.GetString(4)) + ',' + (sqlReader.IsDBNull(5) ? "" : sqlReader.GetString(5)) + ',' + (sqlReader.IsDBNull(6) ? "" : sqlReader.GetString(6))

                        };

                        if (sp != null)
                        {

                            dttimkiem1.ItemsSource = null;
                            listKS1.Add(sp);
                        }

                    }
                    dttimkiem1.ItemsSource = listKS1;
                    sqlReader.Close();
                }


                else
                {
                    MessageBox.Show("Không tìm thấy khách sạn có  giá trung bình là: " + txtgia.Text);
                    sqlReader.Close();
                }
            }



            if (txtgia.Text != "" && txtenthanhpho1.Text != "")
            {
                SqlCommand cmd = new SqlCommand("usp_timKiemThongTinhKS_giaCa_Tp", Database.connection)
                {

                    // Kiểu của Command là StoredProcedure
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(new SqlParameter("@thanhphotimkiem", txtenthanhpho1.Text));
                cmd.Parameters.Add(new SqlParameter("@giaTB", txtgia.Text));

                SqlDataReader sqlReader = cmd.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    listKS1.Clear();
                    while (sqlReader.Read())
                    {
                        KhachSan sp = new KhachSan
                        {
                            maKS = sqlReader.IsDBNull(0) ? -1 : sqlReader.GetInt32(0),
                            tenKS = sqlReader.IsDBNull(1) ? "" : sqlReader.GetString(1),
                            giaTB = sqlReader.IsDBNull(2) ? -1 : sqlReader.GetInt64(2),
                            diaChi = (sqlReader.IsDBNull(3) ? "" : sqlReader.GetString(3)) + ',' + (sqlReader.IsDBNull(4) ? "" : sqlReader.GetString(4)) + ',' + (sqlReader.IsDBNull(5) ? "" : sqlReader.GetString(5)) + ',' + (sqlReader.IsDBNull(6) ? "" : sqlReader.GetString(6))


                        };

                        if (sp != null)
                        {

                            dttimkiem1.ItemsSource = null;
                            listKS1.Add(sp);
                        }

                    }
                    sqlReader.Close();
                    dttimkiem1.ItemsSource = listKS1;
                }
                else
                {
                    sqlReader.Close();
                    MessageBox.Show("Không tìm thấy khách sạn nằm trong thành phố " + txtenthanhpho1.Text + " và có giá là: " + txtgia.Text);
                }


            }

        }

        // lấy dữ liệu trên datagrid 1
        private void DataGridCell_PreviewMouseLeftButtonDown_gia(object sender, MouseButtonEventArgs e)
        {
            DataGridCell myCell = sender as DataGridCell;
            DataGridRow row = DataGridRow.GetRowContainingElement(myCell);

            KhachSan temp = row.Item as KhachSan;

            try
            {


                for (int i = 0; i < listKS1.Count; i++)
                {
                    if (temp == listKS1[i])
                    {
                        //MessageBox.Show(listSP[i].isChecked.ToString());

                        dttimkiem1.ItemsSource = null;
                        dttimkiem1.ItemsSource = listKS1;
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
                ShowInformation window = new ShowInformation(temp.maKS);
                window.ShowDialog();
                if (Auth.isCustomerLogged == false)
                {
                    this.Close();
                }
            }

        }


        //-------------------------------------------------------------------------------------------------------------------      
        //---------------------------- TÌM KIẾM THEO SỐ SAO--------------------------


        //buton tìm kiếm theo số sao
        private void timkiemtab2_Click(object sender, RoutedEventArgs e)
        {
            if (txtenthanhpho.Text == "")
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM KhachSan WHERE soSao = @soSao", Database.connection)
                {
                    CommandType = CommandType.Text
                };
                cmd.Parameters.Add(new SqlParameter("@soSao", sosao.Value));
                SqlDataReader sqlReader = cmd.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    listKS2.Clear();
                    while (sqlReader.Read())
                    {
                        KhachSan sp = new KhachSan
                        {
                            maKS = sqlReader.IsDBNull(0) ? -1 : sqlReader.GetInt32(0),
                            tenKS = sqlReader.IsDBNull(1) ? "" : sqlReader.GetString(1),
                            giaTB = sqlReader.IsDBNull(7) ? -1 : sqlReader.GetInt64(7),
                            diaChi = (sqlReader.IsDBNull(3) ? "" : sqlReader.GetString(3)) + ',' + (sqlReader.IsDBNull(4) ? "" : sqlReader.GetString(4)) + ',' + (sqlReader.IsDBNull(5) ? "" : sqlReader.GetString(5)) + ',' + (sqlReader.IsDBNull(6) ? "" : sqlReader.GetString(6))

                        };

                        if (sp != null)
                        {

                            dttimkiem2.ItemsSource = null;
                            listKS2.Add(sp);
                        }

                    }
                    sqlReader.Close();
                    dttimkiem2.ItemsSource = listKS2;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách sạn có số sao là: " + sosao.Value);
                    sqlReader.Close();
                }
            }

            if (txtenthanhpho.Text != "")
            {
                SqlCommand cmd = new SqlCommand("usp_timKiemThongTinhKS_sao_Tp", Database.connection)
                {

                    // Kiểu của Command là StoredProcedure
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(new SqlParameter("@thanhPhoTimKiem", txtenthanhpho.Text));
                cmd.Parameters.Add(new SqlParameter("@soSaoTimKiem", sosao.Value));
                SqlDataReader sqlReader = cmd.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    listKS2.Clear();
                    while (sqlReader.Read())
                    {
                        KhachSan sp = new KhachSan
                        {
                            maKS = sqlReader.IsDBNull(0) ? -1 : sqlReader.GetInt32(0),
                            tenKS = sqlReader.IsDBNull(1) ? "" : sqlReader.GetString(1),
                            giaTB = sqlReader.IsDBNull(2) ? -1 : sqlReader.GetInt64(2),
                            diaChi = (sqlReader.IsDBNull(3) ? "" : sqlReader.GetString(3)) + ',' + (sqlReader.IsDBNull(4) ? "" : sqlReader.GetString(4)) + ',' + (sqlReader.IsDBNull(5) ? "" : sqlReader.GetString(5)) + ',' + (sqlReader.IsDBNull(6) ? "" : sqlReader.GetString(6))

                        };

                        if (sp != null)
                        {

                            dttimkiem2.ItemsSource = null;
                            listKS2.Add(sp);
                        }

                    }
                    dttimkiem2.ItemsSource = listKS2;
                    sqlReader.Close();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách sạn nằm trong thành phố " + txtenthanhpho.Text + " và có số sao là: " + sosao.Value);
                    sqlReader.Close();
                }
            }




        }

        //-------
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            resetTxtSearch.Visibility = Visibility.Visible;

            if (listKSSearch2.Count > 0)
            {
                //Nếu tìm kiếm sản phẩm khác thì reset lại list sản phẩm tìm kiếm
                listKSSearch2.Clear();
            }
            if (txtenthanhpho.Text.Trim() == "")
            {
                resetTxtSearch.Visibility = Visibility.Collapsed;
                dttimkiem2.ItemsSource = null;
                dttimkiem2.ItemsSource = listKS2;

            }
            else
            {
                for (int i = 0; i < listKS2.Count; i++)
                {
                    string str = listKS2[i].diaChi.Trim().ToLower().Replace(" ", "");
                    //Tách chuỗi nhập vào thành các từ
                    string expression = txtenthanhpho.Text.ToLower();
                    string[] token = expression.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    int index = 0;
                    //Duyệt qua mảng các từ trong chuỗi nhập
                    for (; index < token.Length; index++)
                    {
                        //Xóa khoảng trắng
                        token[index] = token[index].Trim();
                        //Kiểm tra từ đó xuất hiện trong tên sản phẩm không
                        int pos = listKS2[i].diaChi.ToLower().IndexOf(token[index]);
                        //Nếu không xuất hiện thì
                        if (pos < 0)
                        {
                            break;
                        }
                    }
                    if (index == token.Length)
                    {
                        listKSSearch2.Add(listKS2[i]);
                    }
                }

                dttimkiem2.ItemsSource = null;
                dttimkiem2.ItemsSource = listKSSearch2;


            }



        }
        // ---------hàm lấy dữ liệu trên từng dòng của datagrid 2
        private void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataGridCell myCell = sender as DataGridCell;
            DataGridRow row = DataGridRow.GetRowContainingElement(myCell);

            KhachSan temp = row.Item as KhachSan;

            try
            {


                for (int i = 0; i < listKS2.Count; i++)
                {
                    if (temp == listKS2[i])
                    {
                        //MessageBox.Show(listSP[i].isChecked.ToString());

                        dttimkiem2.ItemsSource = null;
                        dttimkiem2.ItemsSource = listKS2;
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
                ShowInformation window = new ShowInformation(temp.maKS);
                window.ShowDialog();
            }

        }

        private void resetTxtSearch_Click(object sender, RoutedEventArgs e)
        {
            txtenthanhpho.Text = "";
        }


        private void ListViewItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var left = this.Left;
            var top = this.Top;
            var height = this.Height;
            var width = this.Width;

            var window = new Search
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
            if (Auth.isCustomerLogged)
            {
                MessageBox.Show("Bạn đã đăng nhập!");
            }
            else
            {
                var left = this.Left;
                var top = this.Top;
                var height = this.Height;
                var width = this.Width;

                var window = new Login_user
                {
                    Left = left,
                    Top = top,
                    Width = width,
                    Height = height
                };

                window.Show();
                this.Close();
            }

        }
        private void ListViewItem_PreviewMouseDown_3(object sender, MouseButtonEventArgs e)
        {
            //đăng xuất
            if (!Auth.isCustomerLogged)
            {
                MessageBox.Show("Bạn chưa đăng nhập!");
            }
            else
            {
                Auth.isEmployeeLogged = false;
                Auth.isCustomerLogged = false;

                var left = this.Left;
                var top = this.Top;
                var height = this.Height;
                var width = this.Width;

                var window = new MainWindow
                {
                    Left = left,
                    Top = top,
                    Width = width,
                    Height = height
                };

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
}







