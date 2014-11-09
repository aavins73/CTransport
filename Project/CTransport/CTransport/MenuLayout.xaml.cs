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
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace CTransport
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MenuLayout : Page
    {
        public MenuLayout()
        {
            this.InitializeComponent();
            
          Windows.Phone.UI.Input.HardwareButtons.BackPressed        += HardwareButtons_BackPressed;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

       void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
           
            //Application.Current.Exit();
            //return;

         if (frame == null)
            {

                return;
            }
         if (frame.CanGoBack)
          {

              frame.GoBack();
            e.Handled = true;

          }
            //MessageDialog mess = new MessageDialog("back pressed");
            //await mess.ShowAsync();
            //Frame.Navigate(typeof(MainPage));*/
        }
		

        private void LS_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LocationSearchRoute));
        }

        private void RL_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RouteList));
        }

        private void GLU_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GA_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SS_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ShuttleService));
        }
    }
}
