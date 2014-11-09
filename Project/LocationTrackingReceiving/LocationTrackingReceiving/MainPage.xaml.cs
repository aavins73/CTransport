using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.Storage;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace LocationTrackingReceiving
{
    public sealed partial class MainPage : Page
    {
        Geolocator geo = null;
        DispatcherTimer timer = new DispatcherTimer();
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 10);
            bool isenable = timer.IsEnabled;
            timer.Start();

        }

        private async void timer_Tick(object sender, object e)
        {
            MapIcon MapIcon1 = new MapIcon();
            if (geo == null)
            {
                geo = new Geolocator();
            }

            Geoposition posi = await geo.GetGeopositionAsync();
            textLatitude.Text = "Latitude: " + posi.Coordinate.Point.Position.Latitude.ToString();
            textLongitude.Text = "Longitude: " + posi.Coordinate.Point.Position.Longitude.ToString();

            //Show pins
            MapIcon1.Location = new Geopoint(new BasicGeoposition()
            {
                Latitude = posi.Coordinate.Point.Position.Latitude,
                Longitude = posi.Coordinate.Point.Position.Longitude
            });

            MapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
            MapIcon1.Title = "Avi";
            MapControl1.MapElements.Add(MapIcon1);

            //Actual map display
            MapControl1.Center =
                   new Geopoint(new BasicGeoposition()
                   {
                       Latitude = posi.Coordinate.Point.Position.Latitude,
                       Longitude = posi.Coordinate.Point.Position.Longitude
                   });
            MapControl1.ZoomLevel = 15;
            MapControl1.LandmarksVisible = true;
            MapControl1.PedestrianFeaturesVisible = true;


        }

        private void textLongitude_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void textLatitude_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            textLatitude.Text = "Latitude: " + "Tracking Halted";
            textLongitude.Text = "Longitude: " + "Tracking Halted";
            textArea.Text = "Area: " + "Tracking Halted";
            timer.Stop();
        }

        private async void GetArea_Click(object sender, RoutedEventArgs e)
        {
            //Reverse Geocode throwing exception
            if (geo == null)
            {
                geo = new Geolocator();
            }

            Geoposition posi = await geo.GetGeopositionAsync();
            BasicGeoposition loc = new BasicGeoposition();
            loc.Latitude = Convert.ToDouble(posi.Coordinate.Point.Position.Latitude.ToString());
            loc.Longitude = Convert.ToDouble(posi.Coordinate.Point.Position.Longitude.ToString());
            Geopoint reversegeocode = new Geopoint(loc);
            MapLocationFinderResult result =
            await MapLocationFinder.FindLocationsAtAsync(reversegeocode);
            textArea.Text = "Area is " + result.Locations[0].Address.Street;

        }
    }
}
