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
using SQLite;
using CTransport.Assets;
using Windows.UI.Popups;
using System.Threading.Tasks;
using Windows.Storage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace CTransport
{

    public sealed partial class MainPage : Page
    {
       SQLiteAsyncConnection conn = new SQLiteAsyncConnection("CTransport.sqlite");
        
        
        

        public MainPage()
        {
            this.InitializeComponent();
            //Frame.Navigate(typeof(MainPage));
            Windows.Phone.UI.Input.HardwareButtons.BackPressed        += HardwareButtons_BackPressed;
            this.NavigationCacheMode = NavigationCacheMode.Disabled;
            

          /*  checkdbaysnc(conn.ToString());

            conn.CreateTableAsync<login>();
            
            

         var newlogin = new List<login>();
           
               newlogin.Add(new login() { Id = 1, UserName = "admin", Password = "password" });
               newlogin.Add(new login() { Id =2, UserName = "admin1", Password = "password1" });
               newlogin.Add(new login() { Id = 3, UserName = "aa", Password = "bb" });

               conn.InsertAllAsync(newlogin);*/

            /*login newlogin1 = new login()
            {
                UserName = "admin1",
                Password = "password1",
            };
            conn.InsertAsync(newlogin1);
            login newlogin2 = new login()
            {
                UserName = "admin2",
                Password = "password2",
            };
            conn.InsertAsync(newlogin2);*/

        }
        

        
        

      /*  private async Task<bool> checkdbaysnc(string Dbname)
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
        }*/

      /* void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            // frame.GoBack();

            // e.Handled = true;

            if (frame == null)
            {

                return;
            }

          if(frame.CanGoBack)
          {
              return;
          }
        }*/

        void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            //Application.Current.Exit();
            //e.Handled = true;
            //return;

             if (frame == null)
              {
                  
                  return;
              }
              if (frame.CanGoBack)
              {
                  
                  return;
                  //frame.GoBack();
                 // e.Handled = true;

              }
            //MessageDialog mess = new MessageDialog("back pressed");
            //await mess.ShowAsync();
            //Frame.Navigate(typeof(MainPage));*/
        }

        public async void SignIn_Click(object sender, RoutedEventArgs e)
        {
            
            if (textBox1.Text == "")
            {
                MessageDialog mess = new MessageDialog("Username cannot be left blank!");
                await mess.ShowAsync();

                textBox1.Focus(Windows.UI.Xaml.FocusState.Keyboard);
                return;
            }

            else if (Passwordbox.Password == "")
            {
                MessageDialog mess = new MessageDialog("Password cannot be left blank!");
                await mess.ShowAsync();
                return;
            }

            string Username1 = textBox1.Text.Trim();
            string password1 = Passwordbox.Password.Trim();

            var query1 = conn.Table<login>().Where(x => x.UserName.Contains(Username1));
            var Result = await query1.ToListAsync();

            string res_U = null;
            string res_P = null;
            string content = String.Empty;
            foreach (var Item in Result)
            {

               if (Username1 == Item.UserName)
                {
                    res_U = "success";
                    if (password1 == Item.Password)
                    {
                        res_P = "success";
                        textBox1.Text = "";
                        Passwordbox.Password = "";
                        //MessageDialog mess = new MessageDialog("Login Successful!!");                        
                        //await mess.ShowAsync();
                        Frame.Navigate(typeof(MenuLayout));
                        break;
                    }
                }
                
            }

            if (res_P != "success" || res_U != "success")
            {
                MessageDialog mess = new MessageDialog("Incorrect Username or Password");
                await mess.ShowAsync();
            }
        }

        private void Passwordbox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
}