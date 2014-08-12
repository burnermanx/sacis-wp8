using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SACIS.Resources;
using SACIS.SacisService;
using SACIS.Classes;
using System.Windows.Media;
using SACIS.Helpers;

namespace SACIS.Pages
{
    public partial class MainPage : PhoneApplicationPage
    {
        App app = App.Current as App;
        
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            ThemeManager.ToLightTheme();
            ThemeManager.SetAccentColor(Colors.Red);
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            
            
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var rootframe = App.RootFrame;
            rootframe.IsSlideEnabled = true;
            rootframe.RightContent = null;
            if (!app.isAuthenticated)
            {
                //MessageBox.Show(" Não Autenticado");
                NavigationService.Navigate(new Uri("/Pages/Login/Login.xaml", UriKind.Relative));
            }

            if (app.isAuthenticated)
            {
                //MessageBox.Show("Autenticado");
                NavigationHelper.Instance.Navigate("/Pages/Messages/Inbox/Inbox.xaml");
            }
        }
    }


}