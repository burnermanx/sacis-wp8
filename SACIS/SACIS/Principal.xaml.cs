using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SACIS.SacisService;
using SACIS.Classes;
using SACIS.Classes.Entidades;

namespace SACIS
{
    public partial class PivotPage1 : PhoneApplicationPage
    {
        //Variavel para receber o parametro vindo da tela inicial
        string user = string.Empty;
        //Bool pra impedir que o programa escreva várias vezes o nome do usuário no titulo do programa
        bool userset;
        //Chamando o SacisService
        SacisService.Service1SoapClient WService = new SacisService.Service1SoapClient();
        //Criando a lista de mensagens
        List<MensagemCabecalho> mensagens = new List<MensagemCabecalho>();

        public PivotPage1()
        {
            InitializeComponent();
            ApplicationBar = ((ApplicationBar)Application.Current.Resources["MsgBar"]);

        }

        private void Pivot_Loaded(object sender, RoutedEventArgs e)
        {
            
            if (NavigationContext.QueryString.TryGetValue("user", out user) && (!userset))
            {
                //Mudando o titulo para adicionar o nome do usuario
                Pivot.Title = Pivot.Title + " - " + user.ToUpper();
                //O nome do usuário já está setado. Talvez mude isso por uma melhor implementacao.
                userset = true;
            }
            listaMensagens("ENTRADA");
            LLsMensagens.ItemsSource = mensagens;
        }
        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((Pivot)sender).SelectedIndex)
            {
                case 0:
                    ApplicationBar = ((ApplicationBar)Application.Current.Resources["MsgBar"]);
                    break;

                case 1:
                    ApplicationBar = ((ApplicationBar)Application.Current.Resources["StorBar"]);
                    break;
            }
        }

        public void AlterarSenha_Nav()
        {
            NavigationService.Navigate(new Uri("/AlteraSenha.xaml", UriKind.Relative));
        }
       //Metodo para chamar a listagem da caixa de entrada ou enviados
        private void listaMensagens(string tipo)
        {
            if (tipo == "ENTRADA")
                WService.retornaCabecalhoAsync(user, "ENTRADA");
            else if (tipo == "ENVIADOS")
                WService.retornaCabecalhoAsync(user, "ENVIADOS");
            WService.retornaCabecalhoCompleted += new EventHandler<retornaCabecalhoCompletedEventArgs>(listaMensagensCompleted); 
         
        }
        //Metodo para lidar com o resultado do Async de cabeçalhos
        private void listaMensagensCompleted(object obj, SacisService.retornaCabecalhoCompletedEventArgs e)
        {
            //se tudo der certo, aqui já tenho um list de mensagens
            mensagens = listarMensagens(e);
        }

        public List<MensagemCabecalho> listarMensagens(SacisService.retornaCabecalhoCompletedEventArgs e)
        {
            string xml = e.Result;
            if (xml != null)
                //MessageBox.Show(xml);
                return Serial.Deserializar(xml, typeof(List<MensagemCabecalho>)) as List<MensagemCabecalho>;
            return null;
        }


    }
}