using ShuttleService.Assets;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace CTransport
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShuttleService : Page
    {
        SQLiteAsyncConnection conn = new SQLiteAsyncConnection("CTransport.sqlite");
        List<string> alpha = new List<string>();
        List<string> beta = new List<string>();

        public ShuttleService()
        {
            this.InitializeComponent();
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            resultTextBlock.Text = "";

            this.NavigationCacheMode = NavigationCacheMode.Disabled;

            RoutebindingfromDB();

            checkdbaysnc(conn.ToString());

            conn.CreateTableAsync<ShuttleSearch>();
            var newroute = new List<ShuttleSearch>();

            newroute.Add(new ShuttleSearch() { Service = "Service 1", FromLocation = "PRN", PickUpTime = "10:15 AM", EndLocation = "ASV", DropTime = "10.40AM" });
            newroute.Add(new ShuttleSearch() { Service = "Service 2", FromLocation = "PRN", PickUpTime = "11:15 AM", EndLocation = "ASV", DropTime = "11.40 AM" });
            newroute.Add(new ShuttleSearch() { Service = "Service 3", FromLocation = "PRN", PickUpTime = "1:15 PM", EndLocation = "ASV", DropTime = "01.40 PM" });
            newroute.Add(new ShuttleSearch() { Service = "Service 1", FromLocation = "PRN", PickUpTime = "10:15 AM", EndLocation = "SRI", DropTime = "11.45AM" });
            newroute.Add(new ShuttleSearch() { Service = "Service 2", FromLocation = "PRN", PickUpTime = "11:15 AM", EndLocation = "SRI", DropTime = "12.40 AM" });
            newroute.Add(new ShuttleSearch() { Service = "Service 3", FromLocation = "PRN", PickUpTime = "1:15 PM", EndLocation = "SRI", DropTime = "02.15 PM" });

            newroute.Add(new ShuttleSearch() { Service = "Service 1", FromLocation = "ASV", PickUpTime = "10:15 AM", EndLocation = "PRN", DropTime = "10.40AM" });
            newroute.Add(new ShuttleSearch() { Service = "Service 2", FromLocation = "ASV", PickUpTime = "11:15 AM", EndLocation = "PRN", DropTime = "11.40 AM" });
            newroute.Add(new ShuttleSearch() { Service = "Service 3", FromLocation = "ASV", PickUpTime = "1:15 PM", EndLocation = "PRN", DropTime = "01.40 PM" });
            newroute.Add(new ShuttleSearch() { Service = "Service 1", FromLocation = "ASV", PickUpTime = "10:15 AM", EndLocation = "SRI", DropTime = "11.45AM" });
            newroute.Add(new ShuttleSearch() { Service = "Service 2", FromLocation = "ASV", PickUpTime = "11:15 AM", EndLocation = "SRI", DropTime = "12.40 AM" });
            newroute.Add(new ShuttleSearch() { Service = "Service 3", FromLocation = "ASV", PickUpTime = "1:15 PM", EndLocation = "SRI", DropTime = "02.15 PM" });

            newroute.Add(new ShuttleSearch() { Service = "Service 1", FromLocation = "SRI", PickUpTime = "10:15 AM", EndLocation = "ASV", DropTime = "10.40AM" });
            newroute.Add(new ShuttleSearch() { Service = "Service 2", FromLocation = "SRI", PickUpTime = "11:15 AM", EndLocation = "ASV", DropTime = "11.40 AM" });
            newroute.Add(new ShuttleSearch() { Service = "Service 3", FromLocation = "SRI", PickUpTime = "1:15 PM", EndLocation = "ASV", DropTime = "01.40 PM" });
            newroute.Add(new ShuttleSearch() { Service = "Service 1", FromLocation = "SRI", PickUpTime = "10:15 AM", EndLocation = "PRN", DropTime = "11.45AM" });
            newroute.Add(new ShuttleSearch() { Service = "Service 2", FromLocation = "SRI", PickUpTime = "11:15 AM", EndLocation = "PRN", DropTime = "12.40 AM" });
            newroute.Add(new ShuttleSearch() { Service = "Service 3", FromLocation = "SRI", PickUpTime = "1:15 PM", EndLocation = "PRN", DropTime = "02.15 PM" });

            newroute.Add(new ShuttleSearch() { Service = "", FromLocation = "MPZ", PickUpTime = "", EndLocation = "NAV", DropTime = "" });
            newroute.Add(new ShuttleSearch() { Service = "", FromLocation = "NAV", PickUpTime = "", EndLocation = "MPZ", DropTime = "" });
            newroute.Add(new ShuttleSearch() { Service = "", FromLocation = "DLF", PickUpTime = "", EndLocation = "CCO", DropTime = "" });

            conn.InsertAllAsync(newroute);
        }

        void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            e.Handled = true;
            frame.Navigate(typeof(MenuLayout));


            /*if (frame == null)
            {
               // return;
                frame.Navigate(typeof(MenuLayout));
            }
            if (frame.CanGoBack)
            {

                e.Handled = true;
                frame.GoBack();
               
                //frame.Navigate(typeof(MenuLayout));
               // e.Handled = true;

            }*/
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {//
        }

        private async Task<bool> checkdbaysnc(string Dbname)
        {
            bool dbExist = true;
            try
            {
                StorageFile sf = await ApplicationData.Current.LocalFolder.GetFileAsync(Dbname);
            }
            catch (Exception)
            {
                dbExist = false;
            }
            return dbExist;
        }
        public void cboRoute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



        public async void Button_Click(object sender, RoutedEventArgs e)
        {
            string content = String.Empty;
            resultTextBlock.Text = "";
            if (fromroute.SelectedItem == null || endroute.SelectedItem == null)
            {
                content = "From/End Location cannot be left blank";
                resultTextBlock.Text = content;
                return;
            }

            //  selectedRoute = ((ComboBoxItem)cboRoute.SelectedItem).ToString();

            string selectedEndLoc = endroute.SelectedItem.ToString();
            string selectedfromLoc = fromroute.SelectedItem.ToString();
            var query1 = conn.Table<ShuttleSearch>().Where(x => x.EndLocation.Contains(selectedEndLoc) && x.FromLocation.Contains(selectedfromLoc));
            var Result = await query1.ToListAsync();
            var dis = Result.GroupBy(q => q.Service).Select(y => y.First());
            if (Result.Count > 0)
            {

                foreach (var Item in dis)
                {
                    // content = String.Format(Item.FromLocation + "\n");
                    content += String.Format(Item.Service + "\n" + "----------------------------------------" +
                                            "PickUp Location : " + Item.FromLocation + " at " + Item.PickUpTime + "\n" +
                                            "Drop Location : " + Item.EndLocation + " at " + Item.DropTime + "\n" + "\n");

                }
            }
            else
            {
                content = "No Results Found! Kindly provide appropriate PickUp & Drop facility!";
            }

            resultTextBlock.Text = content;


        }

        public async void RoutebindingfromDB()
        {

            var query1 = conn.Table<ShuttleSearch>();
            var Result = await query1.OrderBy(a => a.FromLocation).ToListAsync();
            var dis = Result.GroupBy(q => q.FromLocation).Select(y => y.First());

            var query2 = conn.Table<ShuttleSearch>();
            var Result1 = await query1.OrderBy(a => a.EndLocation).ToListAsync();
            var dis1 = Result.GroupBy(q => q.EndLocation).Select(y => y.First());



            foreach (var Item in dis)
            {

                alpha.Add(Item.FromLocation.ToString());
                //Location1.DataContext = Item.RouteNo;
                // Location.DataContext = Item.RouteNo;
                //Loc.Items.Add(Item.RouteNo);
                //Loc.DataC;



            }

            fromroute.DataContext = alpha;

            foreach (var Item in dis1)
            {
                beta.Add(Item.EndLocation.ToString());
                //cboRoute.Items.Add(Item.EndLocation.ToString());


                //Location1.DataContext = Item.RouteNo;
                // Location.DataContext = Item.RouteNo;
                //Loc.Items.Add(Item.RouteNo);
                //Loc.DataC;

            }

            endroute.DataContext = beta;


        }

        private void fromroute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
