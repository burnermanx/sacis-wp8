using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SACIS.Helpers;

namespace SACIS.Pages
{
    public partial class Navigation : UserControl
    {
        App app = App.Current as App;
        public Navigation()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ChangeUsername);
        }

        private void User_OnTap(object sender, RoutedEventArgs e)
        {
            NavigationHelper.Instance.Navigate("/Pages/User/User.xaml");
        }

        private void Inbox_OnTap(object sender, RoutedEventArgs e)
        {
            NavigationHelper.Instance.Navigate("/Pages/Messages/Inbox/Inbox.xaml");
        }

        private void Sent_OnTap(object sender, RoutedEventArgs e)
        {
            NavigationHelper.Instance.Navigate("/Pages/Messages/Sent/Sent.xaml");
        }

        private void Storage_OnTap(object sender, RoutedEventArgs e)
        {
            NavigationHelper.Instance.Navigate("/Pages/FileStorage/FileStorage.xaml");
        }

        private void Settings_OnTap(object sender, RoutedEventArgs e)
        {
            NavigationHelper.Instance.Navigate("/Pages/Config/Config.xaml");
        }

        private void ChangeUsername(object sender, RoutedEventArgs e)
        {
            if (app.isAuthenticated)
            {
                username.Text = app.User;
            }
        }

    }
}