using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Notifications;
using Windows.Phone.Devices.Notification;
using Windows.Devices.Enumeration;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace SendAlert
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // readonly GeofenceMonitor _monitor = GeofenceMonitor.Current;
        Geolocator geo = null;
        DispatcherTimer timer = new DispatcherTimer();
        private static IAsyncOperation<IUICommand> messageDialogCommand = null;
        int msgsent= 0;
        
        public MainPage()
        {
            this.InitializeComponent();
            msgsent = 0;
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            /*_monitor.GeofenceStateChanged += MonitorOnGeofenceStateChanged;

            //Microsoft Redmond building 9
            BasicGeoposition pos = new BasicGeoposition { Latitude = 47.6397, Longitude = -122.1289 };

            Geofence fence = new Geofence("building9", new Geocircle(pos, 100));

            try
            {
                _monitor.Geofences.Add(fence);
            }
            catch (Exception)
            {
                //geofence already added to system
            }*/


            
            msgsent = 0;
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Tick += timer_Tick;
            bool isenable = timer.IsEnabled;
            timer.Start();
           }

        private async void timer_Tick(object sender, object e)
        {

            

            if (geo == null)
            {
                geo = new Geolocator();
            }

            Geoposition posi = await geo.GetGeopositionAsync();
            VibrationDevice sendalert = VibrationDevice.GetDefault();

            if(msgsent >=1)
            {
                timer.Stop();
            }
            
    else if (posi.Coordinate.Point.Position.Latitude <= 12.9227 && posi.Coordinate.Point.Position.Longitude >= 080.1320)
                {
                    if (msgsent <=1)
                    {
                        msgsent = msgsent + 1;
                       //ShowDialog(new MessageDialog("Your Bus has crossed OBC bank near selaiyur"));
                       sendalert.Vibrate(TimeSpan.FromSeconds(3));
                       Theme.Play();
                       return;
                    }
                    
                } 
            
        }

        public async static Task<bool> ShowDialog(MessageDialog dlg)
        {

            // Close the previous one out
            if (messageDialogCommand != null)
            {
                messageDialogCommand.Cancel();
                messageDialogCommand = null;
            }

            messageDialogCommand = dlg.ShowAsync();
            await messageDialogCommand;
            return true;
        }
        /* private void MonitorOnGeofenceStateChanged(GeofenceMonitor sender, object args)
        {
            var fences = sender.ReadReports();

            foreach (var report in fences)
            {
                if (report.Geofence.Id != "building9")
                    continue;

                switch (report.NewState)
                {
                    case GeofenceState.Entered:
                        Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                        {
                            MessageDialog dialog = new MessageDialog("Welcome to building 9");

                            await dialog.ShowAsync();
                        });
                        break;
                    case GeofenceState.Exited:
                        Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                        {
                            MessageDialog dialog = new MessageDialog("Leaving building 9");

                            await dialog.ShowAsync();
                        });
                        break;
                }
            }
         }*/

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }
        
    }


}


