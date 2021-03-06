﻿using CTransport.Assets;
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
    public sealed partial class RouteList : Page
    {
        SQLiteAsyncConnection conn = new SQLiteAsyncConnection("CTransport.sqlite");
        // public ObservableCollection<Rno> Myroute = new ObservableCollection<Rno>(); //one more way to bind data
        // public List<string> Suggestions { get; set; }
        //List<string> dayresult = new List<string>();
        List<string> alpha = new List<string>();
        string selectedRoute;

        public RouteList()
        {
            this.InitializeComponent();
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            resultTextBlock.Text = "";
            // Suggestions = new List<string>();
            // Location1.ItemsSource = Suggestions;
            this.NavigationCacheMode = NavigationCacheMode.Disabled;



            // one more way to bind data

            /* Myroute.Add(new Rno("R01"));
             Myroute.Add(new Rno("R02"));
             Myroute.Add(new Rno("R03"));
             Myroute.Add(new Rno("R04"));
             Myroute.Add(new Rno("R05"));
             Myroute.Add(new Rno("R06"));
             Myroute.Add(new Rno("R07"));
             Myroute.Add(new Rno("R08"));
             Myroute.Add(new Rno("R09"));
             Myroute.Add(new Rno("R10"));
             Myroute.Add(new Rno("R11"));
             Myroute.Add(new Rno("R12"));
             Myroute.Add(new Rno("R13"));
             Myroute.Add(new Rno("R14"));
             Myroute.Add(new Rno("R15"));
             Myroute.Add(new Rno("R16"));
             Myroute.Add(new Rno("R17"));
             Myroute.Add(new Rno("R18"));
             Myroute.Add(new Rno("R19"));
             Myroute.Add(new Rno("R20"));
             Myroute.Add(new Rno("M45"));*/

            RoutebindingfromDB();




            // cboRoute.DataContext = Myroute;
            // cboRoute.ItemsSource = Myroute;

            // Location1.DataContext = Myroute;





            checkdbaysnc(conn.ToString());

            conn.CreateTableAsync<route>();
            var newroute = new List<route>();

            newroute.Add(new route() { RouteNo = "R01", Location = "08:00 AM - Tambaram-MCC" });
            newroute.Add(new route() { RouteNo = "R01", Location = "08:10 AM - Selaiyur" });
            newroute.Add(new route() { RouteNo = "R01", Location = "08:15 AM - Camproad" });
            newroute.Add(new route() { RouteNo = "R01", Location = "08:25 AM - KMPR" });
            newroute.Add(new route() { RouteNo = "R01", Location = "08:35 AM - Medavakkam" });
            newroute.Add(new route() { RouteNo = "R01", Location = "08:45 AM - PRN" });
            newroute.Add(new route() { RouteNo = "R02", Location = "08:00 AM - T Nagar" });
            newroute.Add(new route() { RouteNo = "R02", Location = "08:10 AM - Saidapet" });
            newroute.Add(new route() { RouteNo = "R02", Location = "08:25 AM - Adyar" });
            newroute.Add(new route() { RouteNo = "R02", Location = "08:35 AM - Velachery" });
            newroute.Add(new route() { RouteNo = "R02", Location = "08:45 AM - Vijayanagar" });
            newroute.Add(new route() { RouteNo = "R02", Location = "08:55 AM - Taramani" });
            newroute.Add(new route() { RouteNo = "R02", Location = "09:00 AM - PRN" });
            newroute.Add(new route() { RouteNo = "R03", Location = "09:00 AM - PRN" });
            newroute.Add(new route() { RouteNo = "R04", Location = "09:00 AM - PRN" });
            newroute.Add(new route() { RouteNo = "R05", Location = "09:00 AM - PRN" });
            newroute.Add(new route() { RouteNo = "R06", Location = "09:00 AM - PRN" });
            newroute.Add(new route() { RouteNo = "R07", Location = "09:00 AM - PRN" });
            newroute.Add(new route() { RouteNo = "R08", Location = "09:00 AM - PRN" });
            newroute.Add(new route() { RouteNo = "R09", Location = "09:00 AM - PRN" });
            newroute.Add(new route() { RouteNo = "R10", Location = "09:00 AM - PRN" });
            newroute.Add(new route() { RouteNo = "R11", Location = "09:00 AM - PRN" });
            newroute.Add(new route() { RouteNo = "R12", Location = "09:00 AM - PRN" });

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
            //MessageDialog mess = new MessageDialog("back pressed");
            //await mess.ShowAsync();
            //Frame.Navigate(typeof(MainPage));
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
            //  selectedRoute = ((ComboBoxItem)cboRoute.SelectedItem).ToString();
            selectedRoute = cboRoute.SelectedItem.ToString();

            var query1 = conn.Table<route>().Where(x => x.RouteNo.Contains(selectedRoute));
            var Result = await query1.ToListAsync();
            var dis = Result.GroupBy(q => q.Location).Select(y => y.First());

            foreach (var Item in dis)
            {
                content += String.Format(Item.Location + "\n");
            }

            resultTextBlock.Text = content;
        }

        public async void RoutebindingfromDB()
        {

            var query1 = conn.Table<route>();
            var Result = await query1.OrderBy(a => a.RouteNo).ToListAsync();
            var dis = Result.GroupBy(q => q.RouteNo).Select(y => y.First());



            foreach (var Item in dis)
            {
                cboRoute.Items.Add(Item.RouteNo);
                alpha.Add(Item.RouteNo.ToString());
                //Location1.DataContext = Item.RouteNo;
                // Location.DataContext = Item.RouteNo;
                //Loc.Items.Add(Item.RouteNo);
                //Loc.DataC;



            }

           // Location1.DataContext = alpha;


        }

        public async void Suggestions_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
           // string userInput = Location1.Text.Trim();
            //  string userInput = "jack";

            // string[] alpha = new string[3]{"jack","jill","hill" };
            // List<string> alpha = new List<string>();



            //foreach (Rno item in Myroute)
            //{
            //    alpha.Add(item.Routenumber.ToString());

            //}


            if (args.Reason == AutoSuggestionBoxTextChangeReason.SuggestionChosen)
            {
                return;
            }
           
      
            //if (!Result.Any())
            //{
            //    List<string> noResults = new List<string>();
            //    noResults.Add("No values found");
            //   // Location1.ItemsSource = noResults;
            //    return;
            //}

          //  Location1.ItemsSource = Result;
        }

    }
}
