using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SACIS.Classes.Entidades;
using SACIS.Classes;
using SACIS.Helpers;
using System.ComponentModel;

namespace SACIS.Pages
{
    public partial class Inbox : PhoneApplicationPage
    {

        //Chamando o SacisService
        SacisService.Service1SoapClient WService = new SacisService.Service1SoapClient();
        //Criando a lista de mensagens
        List<mensagemCabecalho> mensagens = new List<mensagemCabecalho>();
        App app = App.Current as App;

              
        public Inbox()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Inbox_Loaded);
        }

        public void Inbox_Loaded(object sender, EventArgs e)
        {
            SystemTray.ProgressIndicator = new ProgressIndicator();
            //nav.setUsername(app.User);
            listaMensagens();
        }

        public void showSystemTray(Boolean isSet)
        {
            SystemTray.Opacity = 0;
            SystemTray.IsVisible = isSet;
            SystemTray.ProgressIndicator.IsIndeterminate = isSet;
            SystemTray.ProgressIndicator.IsVisible = isSet;
        }

        public void listaMensagens()
        {
            string user = app.User;
            WService.retornaCabecalhoAsync(user, "ENTRADA");
            WService.retornaCabecalhoCompleted += new EventHandler<SacisService.retornaCabecalhoCompletedEventArgs>(listaMensagensCompleted);
            showSystemTray(true);
            
        }

        //Metodo para lidar com o resultado do Async de cabeçalhos
        private void listaMensagensCompleted(object obj, SacisService.retornaCabecalhoCompletedEventArgs e)
        {
            string xml = e.Result;
            //MessageBox.Show(xml);
            showSystemTray(false);
            if (!string.IsNullOrEmpty(xml))
            {
                mensagens = Serial.Deserializar(xml, typeof(List<mensagemCabecalho>)) as List<mensagemCabecalho>;
                LLsMensagens.ItemsSource = mensagens;
            }
        }


        //Implementacao das AppBars no Pivot
        private void ab_Atualizar(object sender, EventArgs e)
        {
            listaMensagens();
        }

        private void ab_Contatos(object sender, EventArgs e)
        {
            NavigationHelper.Instance.Navigate("/Pages/Contacts/Contacts.xaml");
        }

        private void ab_NovaMensagem(object sender, EventArgs e)
        {
            NavigationHelper.Instance.Navigate("/Pages/Messages/NewMessage/NewMessage.xaml");
        }

        private void AlterarSenha_Click(object sender, EventArgs e)
        {
            NavigationHelper.Instance.Navigate("/Pages/User/ChangePassword/ChangePassword.xaml");
        }

        private void MensagemTap(object sender, EventArgs e)
        {
            MessageBox.Show("OK!");
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (MessageBox.Show("Você deseja mesmo sair?", "Sair do SACIS?",
                                    MessageBoxButton.OKCancel) != MessageBoxResult.OK)
            {
                e.Cancel = true;
                
            }
            else
            {
                app.logoff(); //Fazendo o Logoff
                NavigationHelper.Instance.Navigate("/Pages/Main/MainPage.xaml");
            }
        }
     
    }
}