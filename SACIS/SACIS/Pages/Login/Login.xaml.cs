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
using System.ComponentModel;

namespace SACIS.Pages
{
    public partial class Login : PhoneApplicationPage
    {

        App app = App.Current as App;
        // Constructor
        public Login()
        {
            InitializeComponent();
            ThemeManager.ToLightTheme();
            ThemeManager.SetAccentColor(Colors.Red);
            this.Loaded += new RoutedEventHandler(Login_Loaded);

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        public void Login_Loaded(object sender, EventArgs e)
        {
            SystemTray.ProgressIndicator = new ProgressIndicator();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //Metodo utilizado pelos botões de entrar na tela e no appbar. Apenas pra não repetir código.
        private void Entrar(object sender, EventArgs e)
        {
            string usuario = LoginInput.Text;
            string senha = PasswordInput.Password;
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha))
                MessageBox.Show("Favor entrar com o nome de usuário e senha");
            else
            {
                string hash = Hash.hashing(senha);
                //MessageBox.Show(hash);

                SacisService.Service1SoapClient WService = new SacisService.Service1SoapClient();

                try
                {
                    WService.consultaUsuarioAsync(usuario, hash);
                    WService.consultaUsuarioCompleted += new EventHandler<consultaUsuarioCompletedEventArgs>(consultaUsuarioCompleted);
                    showSystemTray(true);
                }
                catch (Exception)
                {
                    MessageBox.Show("Falha de comunicação com o servidor do SACIS.");
                }

            }
        }

        public void showSystemTray(Boolean isSet)
        {
            SystemTray.Opacity = 0;
            SystemTray.IsVisible = isSet;
            SystemTray.ProgressIndicator.IsIndeterminate = isSet;
            SystemTray.ProgressIndicator.IsVisible = isSet;
        }

        private void Botao_Entrar(object sender, EventArgs e)
        {
            Entrar(sender, e);
        }

        private void AppBar_Entrar(object sender, EventArgs e)
        {
            Entrar(sender, e);
        }

        private void Menu_Sobre(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Config/About/About.xaml", UriKind.Relative));
        }

        private void LoginLostFocus(object sender, RoutedEventArgs e)
        {
            CheckLoginWatermark();
        }

        public void CheckLoginWatermark()
        {
            var loginEmpty = string.IsNullOrEmpty(LoginInput.Text);
            LoginWatermark.Opacity = loginEmpty ? 100 : 0;
            LoginInput.Opacity = loginEmpty ? 0 : 100;
        }

        private void LoginGotFocus(object sender, RoutedEventArgs e)
        {
            LoginWatermark.Opacity = 0;
            LoginInput.Opacity = 100;
        }

        private void PasswordLostFocus(object sender, RoutedEventArgs e)
        {
            CheckPasswordWatermark();
        }

        public void CheckPasswordWatermark()
        {
            var passwordEmpty = string.IsNullOrEmpty(PasswordInput.Password);
            PasswordWatermark.Opacity = passwordEmpty ? 100 : 0;
            PasswordInput.Opacity = passwordEmpty ? 0 : 100;
        }

        private void PasswordGotFocus(object sender, RoutedEventArgs e)
        {
            PasswordWatermark.Opacity = 0;
            PasswordInput.Opacity = 100;
        }

        //Handler que cuida do evento consultaUsuario assim que ele termina a consulta. Ele vai permitir fazer o login ou
        //não permitir a entrada e dar a mensagem de erro correspondente.
        private void consultaUsuarioCompleted(object obj, SacisService.consultaUsuarioCompletedEventArgs e)
        {
            string mensagem = "";
            showSystemTray(false);
            try
            {
                int status = Convert.ToInt32(e.Result.ToString());
                switch (status)
                {
                    case 0:
                        app.isAuthenticated = true;
                        app.User = LoginInput.Text;
                        NavigationService.Navigate(new Uri("/Pages/Main/MainPage.xaml", UriKind.Relative));
                        break;
                    case 1:
                        mensagem = "Você precisa alterar a senha para continuar";
                        MessageBox.Show(mensagem);
                        NavigationService.Navigate(new Uri("/Pages/User/ChangePassword/ChangePassword.xaml?user=" + LoginInput.Text, UriKind.Relative));
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
        //Metodo pra não precisar preencher a senha o tempo todo
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginInput.Text = "administrador";
            PasswordInput.Password = "admin123";
            LoginInput.Opacity = 100;
            PasswordInput.Opacity = 100;
            LoginWatermark.Opacity = 0;
            PasswordWatermark.Opacity = 0;
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
                throw new Exception(); //Jogando Exception de propósito para fechar o app
            }
        }
    }


}