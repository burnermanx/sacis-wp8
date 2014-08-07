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
    public partial class User : PhoneApplicationPage
    {
        App app = App.Current as App;
        public User()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(User_Loaded);

        }

        private void User_Loaded(object sender, RoutedEventArgs e)
        {
            if (app.isAuthenticated)
            {
                Username.Text = app.User;
            }
        }

        private void Botao_AltSenha(object sender, RoutedEventArgs e)
        {
            NavigationHelper.Instance.Navigate("/Pages/User/ChangePassword/ChangePassword.xaml");
        }
    }
}