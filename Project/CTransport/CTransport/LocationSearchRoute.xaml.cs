using LocationSearchRoute.Assets;
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
    public sealed partial class LocationSearchRoute : Page
    {
        SQLiteAsyncConnection conn = new SQLiteAsyncConnection("CTransport.sqlite");
        List<string> alpha = new List<string>();
        List<string> beta = new List<string>();
        public LocationSearchRoute()
        {
            this.InitializeComponent();
             Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            resultTextBlock.Text = "";

            this.NavigationCacheMode = NavigationCacheMode.Disabled;

            RoutebindingfromDB();

            checkdbaysnc(conn.ToString());

            conn.CreateTableAsync<LocationSearchRte>();
            var newroute = new List<LocationSearchRte>();

            newroute.Add(new LocationSearchRte() { RouteNo = "G01", BoardLocation = "Tambaram-MCC", BoardTime = "08:00 AM", EndLocation = "GMR", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "G01", BoardLocation = "Medavakkam", BoardTime = "08:25 AM", EndLocation = "GMR", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "G01", BoardLocation = "Velachery", BoardTime = "08:45 AM", EndLocation = "GMR", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "G02", BoardLocation = "T.Nagar", BoardTime = "07:55 AM", EndLocation = "GMR", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "G02", BoardLocation = "Saidapet", BoardTime = "08:15 AM", EndLocation = "GMR", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "G02", BoardLocation = "VijayaNagar", BoardTime = "08:35 AM", EndLocation = "GMR", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "G03", BoardLocation = "Santhome", BoardTime = "08:00 AM", EndLocation = "GMR", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "G03", BoardLocation = "Adayar", BoardTime = "08:20 AM", EndLocation = "GMR", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "G03", BoardLocation = "TidelPark", BoardTime = "08:40 AM", EndLocation = "GMR", DropTime = "09:00 AM" });


            newroute.Add(new LocationSearchRte() { RouteNo = "A01", BoardLocation = "Tambaram-MCC", BoardTime = "08:00 AM", EndLocation = "ASV", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "A01", BoardLocation = "Medavakkam", BoardTime = "08:25 AM", EndLocation = "ASV", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "A01", BoardLocation = "Velachery", BoardTime = "08:45 AM", EndLocation = "ASV", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "A02", BoardLocation = "T.Nagar", BoardTime = "07:55 AM", EndLocation = "ASV", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "A02", BoardLocation = "Saidapet", BoardTime = "08:15 AM", EndLocation = "ASV", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "A02", BoardLocation = "VijayaNagar", BoardTime = "08:35 AM", EndLocation = "ASV", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "A03", BoardLocation = "Santhome", BoardTime = "08:00 AM", EndLocation = "ASV", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "A03", BoardLocation = "Adayar", BoardTime = "08:20 AM", EndLocation = "ASV", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "A03", BoardLocation = "TidelPark", BoardTime = "08:40 AM", EndLocation = "ASV", DropTime = "09:00 AM" });


            /* newroute.Add(new LocationSearchRte() { RouteNo = "S01", BoardLocation = "Tambaram-MCC", BoardTime = "08:00 AM", EndLocation = "Siruseri", DropTime = "09:00 AM" });
              newroute.Add(new LocationSearchRte() { RouteNo = "S01", BoardLocation = "Medavakkam", BoardTime = "08:25 AM", EndLocation = "Siruseri", DropTime = "09:00 AM" });
              newroute.Add(new LocationSearchRte() { RouteNo = "S01", BoardLocation = "Velachery", BoardTime = "08:45 AM", EndLocation = "Siruseri", DropTime = "09:00 AM" });
              newroute.Add(new LocationSearchRte() { RouteNo = "S02", BoardLocation = "T.Nagar", BoardTime = "07:55 AM", EndLocation = "Siruseri", DropTime = "09:00 AM" });
              newroute.Add(new LocationSearchRte() { RouteNo = "S02", BoardLocation = "Saidapet", BoardTime = "08:15 AM", EndLocation = "Siruseri", DropTime = "09:00 AM" });
              newroute.Add(new LocationSearchRte() { RouteNo = "S02", BoardLocation = "VijayaNagar", BoardTime = "08:35 AM", EndLocation = "Siruseri", DropTime = "09:00 AM" });
              newroute.Add(new LocationSearchRte() { RouteNo = "S03", BoardLocation = "Santhome", BoardTime = "08:00 AM", EndLocation = "Siruseri", DropTime = "09:00 AM" });
              newroute.Add(new LocationSearchRte() { RouteNo = "S03", BoardLocation = "Adayar", BoardTime = "08:20 AM", EndLocation = "Siruseri", DropTime = "09:00 AM" });
              newroute.Add(new LocationSearchRte() { RouteNo = "S03", BoardLocation = "TidelPark", BoardTime = "08:40 AM", EndLocation = "Siruseri", DropTime = "09:00 AM" });*/

            newroute.Add(new LocationSearchRte() { RouteNo = "S01", BoardLocation = "Tambaram-MCC", BoardTime = "08:00 AM", EndLocation = "SRI", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "S01", BoardLocation = "Medavakkam", BoardTime = "08:25 AM", EndLocation = "SRI", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "S01", BoardLocation = "Velachery", BoardTime = "08:45 AM", EndLocation = "SRI", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "S02", BoardLocation = "T.Nagar", BoardTime = "07:55 AM", EndLocation = "SRI", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "S02", BoardLocation = "Saidapet", BoardTime = "08:15 AM", EndLocation = "SRI", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "S02", BoardLocation = "VijayaNagar", BoardTime = "08:35 AM", EndLocation = "SRI", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "S03", BoardLocation = "Santhome", BoardTime = "08:00 AM", EndLocation = "SRI", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "S03", BoardLocation = "Adayar", BoardTime = "08:20 AM", EndLocation = "SRI", DropTime = "09:00 AM" });
            newroute.Add(new LocationSearchRte() { RouteNo = "S03", BoardLocation = "TidelPark", BoardTime = "08:40 AM", EndLocation = "SRI", DropTime = "09:00 AM" });

            /* newroute.Add(new LocationSearchRte() {BoardLocation =" " ,EndLocation = "DLF" });
             newroute.Add(new LocationSearchRte() { BoardLocation =" ",EndLocation = "TDL" });
             newroute.Add(new LocationSearchRte() {BoardLocation =" ", EndLocation = "PKN" });
             newroute.Add(new LocationSearchRte() {BoardLocation =" ", EndLocation = "MPZ" });*/
            newroute.Add(new LocationSearchRte() { RouteNo = "", BoardLocation = "", BoardTime = "", EndLocation = "DLF", DropTime = "" });
            newroute.Add(new LocationSearchRte() { RouteNo = "", BoardLocation = "", BoardTime = "", EndLocation = "PKN", DropTime = "" });
            newroute.Add(new LocationSearchRte() { RouteNo = "", BoardLocation = "", BoardTime = "", EndLocation = "CCO", DropTime = "" });
            newroute.Add(new LocationSearchRte() { RouteNo = "", BoardLocation = "", BoardTime = "", EndLocation = "TDL", DropTime = "" });

            conn.InsertAllAsync(newroute);
        }
        void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            e.Handled = true;
           frame.Navigate(typeof(MenuLayout));
            

         /*  if (frame == null)
            {
                return;
                //frame.Navigate(typeof(MenuLayout));
            }
            if (frame.CanGoBack)
            {
                frame.GoBack();
                e.Handled = true;
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
        {
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
            if (cboRoute.SelectedItem == null || Location.Text.ToString() == "")
            {
                content = "Boarding Point/End Location cannot be left blank";
                resultTextBlock.Text = content;
                return;
            }
            //  selectedRoute = ((ComboBoxItem)cboRoute.SelectedItem).ToString();

            string selectedEndLoc = cboRoute.SelectedItem.ToString();
            string selectedBoardLoc = Location.Text.ToString();



            var query1 = conn.Table<LocationSearchRte>().Where(x => x.EndLocation.Contains(selectedEndLoc) && x.BoardLocation.Contains(selectedBoardLoc));

            var Result = await query1.ToListAsync();

            if (Result.Count > 0)
            {

                foreach (var Item in Result)
                {
                    content = String.Format(Item.RouteNo + "\n");
                    content = String.Format("Route no : " + Item.RouteNo + "\n" + "Boarding Location : " + Item.BoardLocation + " at " + Item.BoardTime + "\n" +
                                            "Drop Location : " + Item.EndLocation + " at " + Item.DropTime);
                }
            }
            else
            {
                content = "No Results Found! Kindly provide appropriate Boarding point or End Location!";
            }

            resultTextBlock.Text = content;


        }

        public async void RoutebindingfromDB()
        {

            var query1 = conn.Table<LocationSearchRte>();
            var Result = await query1.OrderBy(a => a.BoardLocation).ToListAsync();
            var dis = Result.GroupBy(q => q.BoardLocation).Select(y => y.First());

            var query2 = conn.Table<LocationSearchRte>();
            var Result1 = await query1.OrderBy(a => a.EndLocation).ToListAsync();
            var dis1 = Result.GroupBy(q => q.EndLocation).Select(y => y.First());



            foreach (var Item in dis)
            {

                alpha.Add(Item.BoardLocation.ToString());
                //Location1.DataContext = Item.RouteNo;
                // Location.DataContext = Item.RouteNo;
                //Loc.Items.Add(Item.RouteNo);
                //Loc.DataC;



            }

            Location.DataContext = alpha;

            foreach (var Item in dis1)
            {
                beta.Add(Item.EndLocation.ToString());
                //cboRoute.Items.Add(Item.EndLocation.ToString());


                //Location1.DataContext = Item.RouteNo;
                // Location.DataContext = Item.RouteNo;
                //Loc.Items.Add(Item.RouteNo);
                //Loc.DataC;

            }

            cboRoute.DataContext = beta;


        }

        public async void Suggestions_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            string userInput = Location.Text.Trim();


            if (args.Reason == AutoSuggestionBoxTextChangeReason.SuggestionChosen)
            {
                return;
            }
            if (userInput.Length == 0)
            {
                Location.ItemsSource = null;
                return;
            }
            IOrderedEnumerable<string> Result = from num in alpha
                                                where num.ToUpper().Contains(userInput.ToUpper())
                                                orderby num ascending
                                                select num;
            if (!Result.Any())
            {
                List<string> noResults = new List<string>();
                noResults.Add("No values found");
                Location.ItemsSource = noResults;
                return;
            }

            Location.ItemsSource = Result;
        }
    }
}
