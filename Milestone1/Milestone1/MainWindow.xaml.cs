using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;

namespace Milestone1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Business
        {
            public string name { get; set; }
            public string state { get; set; }
            public string city { get; set; }
            public string address { get; set; }
            public string zipcode { get; set; }
            public double stars { get; set; }
            public double review_count { get; set; }
            public double is_open { get; set; }
            public double review_rating { get; set; }
            public double num_checkins { get; set; }
            public string bid { get; set; }
        }

        public class Review
        {
            public string name { get; set; }
            public string date { get; set; }
            public string text { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            addStates();
            addRating();
            addColumns2Grid();
            addColumns2Review();
        }

        private string buildConnString()
        {
            return "Host=localhost; Username=postgres; Password=postgres; Database = milestone2db";
        }

        public void addStates()
        {
            using (var conn = new NpgsqlConnection(buildConnString()))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT DISTINCT bstate FROM business ORDER BY bstate;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            statelist.Items.Add(reader.GetString(0));
                        }
                    }
                }
                conn.Close();
            }
        }
        public void addRating()
        {
            ratingdropdown.Items.Add(1);
            ratingdropdown.Items.Add(2);
            ratingdropdown.Items.Add(3);
            ratingdropdown.Items.Add(4);
            ratingdropdown.Items.Add(5);
        }

        public void addColumns2Grid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Business Name";
            col1.Binding = new Binding("name");
            col1.Width = 255;
            businessGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Header = "State";
            col2.Binding = new Binding("state");
            col2.Width = 255;
            businessGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Header = "City";
            col3.Binding = new Binding("city");
            col3.Width = 255;
            businessGrid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Header = "Zipcode";
            col4.Binding = new Binding("zipcode");
            col4.Width = 255;
            businessGrid.Columns.Add(col4);

            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Header = "Stars";
            col5.Binding = new Binding("stars");
            col5.Width = 255;
            businessGrid.Columns.Add(col5);

            DataGridTextColumn col6 = new DataGridTextColumn();
            col6.Header = "Review Count";
            col6.Binding = new Binding("review_count");
            col6.Width = 255;
            businessGrid.Columns.Add(col6);

            DataGridTextColumn col7 = new DataGridTextColumn();
            col7.Header = "Is Open";
            col7.Binding = new Binding("is_open");
            col7.Width = 255;
            businessGrid.Columns.Add(col7);

            DataGridTextColumn col8 = new DataGridTextColumn();
            col8.Header = "Review Rating";
            col8.Binding = new Binding("review_rating");
            col8.Width = 255;
            businessGrid.Columns.Add(col8);

            DataGridTextColumn col9 = new DataGridTextColumn();
            col9.Header = "Number Checkins";
            col9.Binding = new Binding("num_checkins");
            col9.Width = 255;
            businessGrid.Columns.Add(col9);
        }

        public void addColumns2Review()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Name";
            col1.Binding = new Binding("name");
            col1.Width = 255;
            reviewlist.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Header = "Date";
            col2.Binding = new Binding("date");
            col2.Width = 100;
            reviewlist.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Header = "Text";
            col3.Binding = new Binding("text");
            col3.Width = 500;
            reviewlist.Columns.Add(col3);
        }

        private void statelist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            citylist.Items.Clear();
            businessGrid.Items.Clear();
            reviewlist.Items.Clear();

            using (var conn = new NpgsqlConnection(buildConnString()))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT DISTINCT city FROM business WHERE bstate = '" + statelist.SelectedItem.ToString() + "' ORDER BY city;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            citylist.Items.Add(reader.GetString(0));
                        }
                    }
                }
                conn.Close();
            }
        }

        private void city_selectonChanged(object sender, SelectionChangedEventArgs e)
        {
            zipcodebox.Items.Clear();
            if (citylist != null && citylist.SelectedItem != null)
            {
                using (var conn = new NpgsqlConnection(buildConnString()))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
 
                        cmd.CommandText = "SELECT DISTINCT zipcode FROM business WHERE bstate = '" + statelist.SelectedItem.ToString() + "' AND city='" +
                            citylist.SelectedItem.ToString() + "' ORDER BY zipcode;";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                zipcodebox.Items.Add(reader.GetString(0));
                            }
                        }
                    }
                    conn.Close();
                }
            }
      

        }

        private void zipcode_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (statelist.SelectedIndex > -1 && zipcodebox.SelectedItem != null)
            {
                businesscat.Items.Clear();
                using (var conn = new NpgsqlConnection(buildConnString()))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        if (citylist.SelectedItem != null)
                        {
                            cmd.CommandText = "select distinct categoryname from businesscategory where businesscategory.bid in " +
                                "(select business_id from business where bstate='" + statelist.SelectedItem.ToString() + "' AND city='" + citylist.SelectedItem.ToString() + "' AND zipcode='" + zipcodebox.SelectedItem.ToString() + "');";
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    businesscat.Items.Add(reader.GetString(0));

                                }
                            }
                        }

                    }
                    conn.Close();
                }
            }
        }

        private void category_selectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BusinessGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            reviewlist.Items.Clear();
            if (businessGrid.Items.Count > 0)
            {
                var rowindex = businessGrid.SelectedIndex;
                var row = (DataGridRow)businessGrid.ItemContainerGenerator.ContainerFromIndex(rowindex);
                Business row1 = (Business)businessGrid.SelectedItems[0];
                var businessId = row1.bid;


                using (var conn = new NpgsqlConnection(buildConnString()))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;

                        cmd.CommandText = "SELECT user1.uname,review.date,review.text FROM review,user1 WHERE bid='" + businessId + "' AND user1.userid=review.uid ORDER BY review.date;";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reviewlist.Items.Add(new Review()
                                {
                                    name = reader.GetString(0),
                                    date = reader.GetString(1),
                                    text = reader.GetString(2)
                                });
                            }
                        }

                        conn.Close();
                    }
                }
            }
            
        }


        private void addCat_Clicked(object sender, RoutedEventArgs e)
        {
            if (businesscat.SelectedItem != null && !businesscatlist.Items.Contains(businesscat.SelectedItem.ToString()))
            {
                businesscatlist.Items.Add(businesscat.SelectedItem.ToString());
            }
        }

        private void removeCat_Clicked(object sender, RoutedEventArgs e)
        {
            if (businesscatlist.SelectedItem != null && businesscatlist.Items.Contains(businesscatlist.SelectedItem.ToString()))
            {
                businesscatlist.Items.Remove(businesscatlist.SelectedItem.ToString());
            }
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-1234567890";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void addReview_Clicked(object sender, RoutedEventArgs e)
        {
            if (businessGrid.Items.Count > 0)
            {
                var rowindex = businessGrid.SelectedIndex;
                var row = (DataGridRow)businessGrid.ItemContainerGenerator.ContainerFromIndex(rowindex);
                Business row1 = (Business)businessGrid.SelectedItems[0];
                var businessId = row1.bid;


                using (var conn = new NpgsqlConnection(buildConnString()))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        
                        cmd.CommandText = "INSERT INTO review(review_id, uid, bid, review_stars, date, text, useful_vote, funny_vote, cool_vote) VALUES('" + RandomString(22) + "', 'om5ZiponkpRqUNa3pVPiRg', '" + businessId + "'," + ratingdropdown.SelectedItem.ToString() + ", '2019-03-19', '" + reviewtext.Text + "', 1, 1, 1)";
                        Console.WriteLine(cmd.CommandText);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reviewlist.Items.Add(new Review()
                                {
                                    name = reader.GetString(0),
                                    date = reader.GetString(1),
                                    text = reader.GetString(2)
                                });
                            }
                        }

                        conn.Close();
                    }
                }
            }
        }

        private void reviewRating_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Searchbutton_Click(object sender, RoutedEventArgs e)
        {
            businessGrid.Items.Clear();
            if (statelist.SelectedIndex > -1)
            {
                using (var conn = new NpgsqlConnection(buildConnString()))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;

                        // DISPLAY BUSINESSES BASED ON WHATS SELECTED
                        // bname,bstate,city,address,zipcode,stars,review_count,is_open,review_rating,num_checkins
                        if (citylist.SelectedItem == null && zipcodebox.SelectedItem == null)
                        {
                            
                            cmd.CommandText = "SELECT bname,bstate,city,address,zipcode,stars,review_count,is_open,review_rating,num_checkins,business_id FROM business WHERE bstate= '" + statelist.SelectedItem.ToString()  
                                + "' ORDER BY bname;";

                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    businessGrid.Items.Add(new Business()
                                    {
                                        name = reader.GetString(0),
                                        state = reader.GetString(1),
                                        city = reader.GetString(2),
                                        address = reader.GetString(3),
                                        zipcode = reader.GetString(4),
                                        stars = reader.GetDouble(5),
                                        review_count = reader.GetDouble(6),
                                        is_open = reader.GetDouble(7),
                                        review_rating = reader.GetDouble(8),
                                        num_checkins = reader.GetDouble(9),
                                        bid = reader.GetString(10)
                                    });
                                }
                            }
                        } else if (citylist.SelectedItem != null && zipcodebox.SelectedItem == null)
                        {
                            cmd.CommandText = "SELECT bname,bstate,city,address,zipcode,stars,review_count,is_open,review_rating,num_checkins,business_id FROM business WHERE city= '" + citylist.SelectedItem.ToString() + "' AND bstate= '" + statelist.SelectedItem.ToString() + "' ORDER BY bname;";

                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    businessGrid.Items.Add(new Business()
                                    {
                                        name = reader.GetString(0),
                                        state = reader.GetString(1),
                                        city = reader.GetString(2),
                                        address = reader.GetString(3),
                                        zipcode = reader.GetString(4),
                                        stars = reader.GetDouble(5),
                                        review_count = reader.GetDouble(6),
                                        is_open = reader.GetDouble(7),
                                        review_rating = reader.GetDouble(8),
                                        num_checkins = reader.GetDouble(9),
                                        bid = reader.GetString(10)
                                    });
                                }
                            }
                        } else if (citylist.SelectedItem != null && zipcodebox.SelectedItem != null && businesscatlist.Items.Count <= 0)
                        {
                            cmd.CommandText = "SELECT bname,bstate,city,address,zipcode,stars,review_count,is_open,review_rating,num_checkins,business_id FROM business WHERE city= '" + citylist.SelectedItem.ToString() + "' AND bstate= '" + statelist.SelectedItem.ToString() + "' AND zipcode='" + zipcodebox.SelectedItem.ToString() +  "' ORDER BY bname;";

                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    businessGrid.Items.Add(new Business()
                                    {
                                        name = reader.GetString(0),
                                        state = reader.GetString(1),
                                        city = reader.GetString(2),
                                        address = reader.GetString(3),
                                        zipcode = reader.GetString(4),
                                        stars = reader.GetDouble(5),
                                        review_count = reader.GetDouble(6),
                                        is_open = reader.GetDouble(7),
                                        review_rating = reader.GetDouble(8),
                                        num_checkins = reader.GetDouble(9),
                                        bid = reader.GetString(10)
                                    });
                                }
                            }
                        } else if (citylist.SelectedItem != null && zipcodebox.SelectedItem != null && businesscatlist.Items.Count > 0)
                        {
                            var query = "SELECT bname,bstate,city,address,zipcode,stars,review_count,is_open,review_rating,num_checkins,business_id FROM business WHERE city= '" + citylist.SelectedItem.ToString() + "' AND bstate= '" + statelist.SelectedItem.ToString() + "' AND zipcode='" + zipcodebox.SelectedItem.ToString() + "' AND (";
                            for (int i = 0; i < businesscatlist.Items.Count; i++)
                            {
                                if (i == businesscatlist.Items.Count - 1)
                                {
                                    query += "'" + businesscatlist.Items[0].ToString() + "' IN (select categoryname from businesscategory WHERE business.business_id = businesscategory.bid)) ORDER BY bname";
                                }
                                else
                                {
                                    query += "'" + businesscatlist.Items[0].ToString() + "' IN (select categoryname from businesscategory WHERE business.business_id = businesscategory.bid) OR ";
                                }
                            }

                            cmd.CommandText = query;
       
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    businessGrid.Items.Add(new Business()
                                    {
                                        name = reader.GetString(0),
                                        state = reader.GetString(1),
                                        city = reader.GetString(2),
                                        address = reader.GetString(3),
                                        zipcode = reader.GetString(4),
                                        stars = reader.GetDouble(5),
                                        review_count = reader.GetDouble(6),
                                        is_open = reader.GetDouble(7),
                                        review_rating = reader.GetDouble(8),
                                        num_checkins = reader.GetDouble(9),
                                        bid = reader.GetString(10)
                                    });
                                }
                            }
                        }
        
                    }
                    conn.Close();
                }
            }      
        }

        private void price1_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void price2_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void price3_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void price4_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void acceptsCreditCards_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void takesReservations_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void wheelcharAccesible_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void outdoorSeating_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void goodForKids_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void goodForGroups_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void delivery_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void takeOut_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void freeWiFi_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void bikeParking_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void breakfast_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void brunch_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void lunch_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void dinner_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void dessert_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void lateNight_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
