using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace ivivuApp
{
    /// <summary>
    /// Interaction logic for Reservation.xaml
    /// </summary>
    public partial class Reservation : Window
    {
        public Reservation()
        {
          InitializeComponent();
        }

        private ListViewItem MakeNewHotel(ListViewItem sample, int id, string name, string description, int rating, long price, string address)
        {
            if (sample == null)
                return (null);
            string s = XamlWriter.Save(sample);
            StringReader stringReader = new StringReader(s);
            XmlReader xmlReader = XmlTextReader.Create(stringReader, new XmlReaderSettings());

            var newHotel = (ListViewItem)XamlReader.Load(xmlReader);
            var card = (MaterialDesignThemes.Wpf.Card)newHotel.Content;
            var stackPanel = (StackPanel)card.Content;

            var sampleName = (TextBlock)stackPanel.Children[0];
            var sampleDesciption = (TextBlock)stackPanel.Children[1];
            var sampleRating = (MaterialDesignThemes.Wpf.RatingBar)stackPanel.Children[2];
            var priceDock = (DockPanel)stackPanel.Children[3];
            var addressDock = (DockPanel)stackPanel.Children[4];
            var sampleId = (TextBlock)stackPanel.Children[5];

            var samplePrice = (TextBlock)priceDock.Children[1];
            var sampleAddress = (TextBlock)addressDock.Children[1];

            sampleId.Text = id.ToString();
            sampleName.Text = name;
            sampleRating.Value = rating;
            samplePrice.Text = price.ToString();
            sampleAddress.Text = address;

            newHotel.Selected += ListViewItem_Selected;

            return newHotel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (Auth.isCustomerLogged != false)
            {
                var loginWindows = new Login_user();

                MessageBox.Show("Vui lòng đăng nhập để sử dụng tính năng này");
                this.Close();
                loginWindows.ShowDialog();
            }
            else
            {
                var window = sender as Window;
                var mainPanel = (Grid)window.Content;
                var hotelList = (ListView)mainPanel.Children[1];

                mainPanel.Visibility = Visibility.Visible;

                ListViewItem sampleHotel = (ListViewItem)hotelList.Items.GetItemAt(0);
                
                int maKS = -1;

                if (maKS == -1)
                {
                    string sql = "SELECT TOP (10) * FROM KhachSan;";
                    using (SqlCommand command = new SqlCommand(sql, Database.connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int id;
                            string hotelName;
                            string hotelDescription;
                            int hotelrating;
                            long hotelPrice;
                            string hotelAddress;

                            while (reader.Read())
                            {
                                id = reader.GetInt32(0);
                                hotelName = reader.GetString(1);
                                hotelrating = reader.GetInt32(2);
                                hotelAddress = reader.GetString(3) + ", " + reader.GetString(4) + ", " + reader.GetString(5) + ", " + reader.GetString(6);
                                hotelPrice = reader.GetInt64(7);
                                hotelDescription = reader.GetString(8);

                                hotelList.Items.Add(MakeNewHotel(sampleHotel, id, hotelName, hotelDescription, hotelrating, hotelPrice, hotelAddress));
                            }

                            hotelList.Items.RemoveAt(0);
                        }
                    }
                }
                else
                {
                    //string sql = "SELECT * FROM KhachSan;";
                    //using (SqlCommand command = new SqlCommand(sql, connection))
                    //{
                    //    using (SqlDataReader reader = command.ExecuteReader())
                    //    {

                    //        while (reader.Read())
                    //        {
                    //            //Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                    //        }
                    //    }
                    //}
                }
            }
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            var hotel = sender as ListViewItem;
            var card = (MaterialDesignThemes.Wpf.Card)hotel.Content;
            var stackPanel = (StackPanel)card.Content;
            var idTextBlock = (TextBlock)stackPanel.Children[5];
            var id = Int32.Parse(idTextBlock.Text);

            var chooseRoom = new ChooseRoom(id);
            chooseRoom.ShowDialog();
        }
    }
}
