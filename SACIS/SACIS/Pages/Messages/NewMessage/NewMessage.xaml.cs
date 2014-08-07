using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace SACIS.Pages
{
    public partial class NewMessage : PhoneApplicationPage
    {
        public NewMessage()
        {
            InitializeComponent();
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Mensagem Enviada");
            NavigationService.GoBack();
        }
    }
}