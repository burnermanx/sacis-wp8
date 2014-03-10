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

namespace SACIS
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        
        //Metodo utilizado pelos botões de entrar na tela e no appbar. Apenas pra não repetir código.
        private void Entrar()
        {
            string usuario = Login.Text;
            string senha = Password.Password;
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha))
                MessageBox.Show("Favor entrar com o nome de usuário e senha");
            else
            {
                string hash=Hash.hashing(senha);
                //MessageBox.Show(hash);
                
                SacisService.Service1SoapClient WService = new SacisService.Service1SoapClient();
               
                try
                {
                    WService.consultaUsuarioAsync(usuario, hash);
                    WService.consultaUsuarioCompleted += new EventHandler<consultaUsuarioCompletedEventArgs>(consultaUsuarioCompleted); 
                }
                catch (Exception)
                {
                    MessageBox.Show("Falha de comunicação com o servidor do SACIS.");
                }
                
            }
        }

        private void Botao_Entrar(object sender, EventArgs e)
        {
            Entrar();
        }

        private void AppBar_Entrar(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AlteraSenha.xaml", UriKind.Relative));
        }

        private void Menu_Sobre(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Sobre.xaml", UriKind.Relative));
        }

        private void LoginLostFocus(object sender, RoutedEventArgs e)
        {
            CheckLoginWatermark();
        }

        public void CheckLoginWatermark()
        {
            var loginEmpty = string.IsNullOrEmpty(Login.Text);
            LoginWatermark.Opacity = loginEmpty ? 100 : 0;
            Login.Opacity = loginEmpty ? 0 : 100;
        }

        private void LoginGotFocus(object sender, RoutedEventArgs e)
        {
            LoginWatermark.Opacity = 0;
            Login.Opacity = 100;
        }

        private void PasswordLostFocus(object sender, RoutedEventArgs e)
        {
            CheckPasswordWatermark();
        }

        public void CheckPasswordWatermark()
        {
            var passwordEmpty = string.IsNullOrEmpty(Password.Password);
            PasswordWatermark.Opacity = passwordEmpty ? 100 : 0;
            Password.Opacity = passwordEmpty ? 0 : 100;
        }

        private void PasswordGotFocus(object sender, RoutedEventArgs e)
        {
            PasswordWatermark.Opacity = 0;
            Password.Opacity = 100;
        }

        //Handler que cuida do evento consultaUsuario assim que ele termina a consulta. Ele vai permitir fazer o login ou
        //não permitir a entrada e dar a mensagem de erro correspondente.
        private void consultaUsuarioCompleted(object obj, SacisService.consultaUsuarioCompletedEventArgs e)
        {
            string mensagem="";
            try
            {
                int status = Convert.ToInt32(e.Result.ToString());
                switch (status)
                {
                    case 0:
                        NavigationService.Navigate(new Uri("/Principal.xaml?user="+Login.Text, UriKind.Relative));
                        break;
                    case 1:
                        mensagem = "Você precisa alterar a senha para continuar";
                        MessageBox.Show(mensagem);
                        NavigationService.Navigate(new Uri("/AlteraSenha.xaml?user="+Login.Text, UriKind.Relative));
                        break;
                    case 2:
                        mensagem = "Chave expirada. Favor renovar a chave";
                        MessageBox.Show(mensagem);
                        break;
                    case 3:
                        mensagem = "Usuário ou senha incorretos.";
                        MessageBox.Show(mensagem);
                        break;
                    default:
                        if (status > 100 && status < 130)
                        {
                            mensagem = "Atenção! Sua chave irá expirar em " + status + " dias. Entre em contato com o administrador para enviar suas novas chaves.";
                            MessageBox.Show(mensagem);
                            break;
                        }
                        else
                        {
                            mensagem = "Erro desconhecido. Status código " + status;
                            MessageBox.Show(mensagem);
                        }
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Falha de comunicação com o servidor do SACIS. ");
            }
        }
    }
}