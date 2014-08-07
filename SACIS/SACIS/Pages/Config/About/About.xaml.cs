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
using Mono.Security.Cryptography;
using System.Security.Cryptography;
using SACIS.Classes;

namespace SACIS.Pages
{
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();
            string mensagem = "#SEnhaCompleX4!";
            Texto.Text = mensagem;
            TextoHash.Text = Hash.hashing(mensagem);
        }

    }
}