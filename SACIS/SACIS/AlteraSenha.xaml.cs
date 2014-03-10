using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Media;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SACIS.Classes;
using SACIS.SacisService;


namespace SACIS
{
    public partial class AlteraSenha : PhoneApplicationPage
    {
        //Criando botão confirmar na classe. Não sei se é a melhor prática, mas funciona.
        ApplicationBarIconButton btnConfirmar = new ApplicationBarIconButton();
        public AlteraSenha()
        {
            InitializeComponent();
            ApplicationBar = new ApplicationBar();
            ApplicationBar.Mode = ApplicationBarMode.Default;
            ApplicationBar.Opacity = 1.0;
            ApplicationBar.IsVisible = true;
            ApplicationBar.IsMenuEnabled = false;

            btnConfirmar.IsEnabled = false;
            btnConfirmar.IconUri = new Uri("/Images/check.png", UriKind.Relative);
            btnConfirmar.Text = "confirmar";
            ApplicationBar.Buttons.Add(btnConfirmar);

            
            
        }
        /*Metodo para verificação da força da senha. Nesse caso ele vai colorir o appbar de acordo com a força da senha, para um melhor e mais rápido impacto visual 
        -Mantém a cor e troca por um botão X impedindo de avançar, caso a senha seja insuficiente.
        -Colore o appbar de vermelho caso a senha seja fraca.
        -Colore o appbar de amarelo caso a senha seja boa.
        -Colore o appbar de verde caso a senha seja ótima.
         Também será mostrado uma mensagem avisando sobre a força da senha.
        */
        public void verificaSenha()
        {
            if (Password.Password != ConfirmPassword.Password || String.IsNullOrEmpty(Password.Password) || String.IsNullOrEmpty(ConfirmPassword.Password) || Password.Password.Length < 8)
            {
                mudarAppbar(0);
            }
            else
            {
                PasswordChecker.Current.Password = Password.Password;
                switch (PasswordChecker.Current.PasswordStrength)
                {
                    case "Very Weak":
                        mudarAppbar(1);
                        break;
                    case "Weak":
                        mudarAppbar(1);
                        break;
                    case "Good":
                        mudarAppbar(2);
                        break;
                    case "Strong":
                        mudarAppbar(3);
                        break;
                    case "Very Strong":
                        mudarAppbar(3);
                        break;
                    default:
                        mudarAppbar(0);
                        break;
                }
            }
        }

        public void PasswordChanged(object sender, RoutedEventArgs e)
        {
            verificaSenha();
        }
        public void CheckPasswordChanged(object sender, RoutedEventArgs e)
        {
            verificaSenha();
        }

        //Metodo que vai mudar a Appbar de acordo com a situação
        public void mudarAppbar(int s)
        {          
            switch (s)
            {
                case 1: //Senha Fraca
                    Mensagem.Foreground = new SolidColorBrush(Color.FromArgb(255, 179, 121, 121));
                    Mensagem.Text = "SENHA FRACA - Tente usar símbolos (ex: $%#), números e letras maiúsculas para melhorar a senha. Evite repetir caracteres ou usa-los em sequencia (ex: 123abc, aaaa, 1111)";
                    ApplicationBar.BackgroundColor = Color.FromArgb(255, 179, 121, 121);
                    btnConfirmar.IsEnabled = true;
                    break;
                case 2: //Senha Media
                    Mensagem.Foreground = new SolidColorBrush(Color.FromArgb(255, 213, 222, 155));
                    Mensagem.Text = "SENHA MÉDIA - Tente usar símbolos (ex: $%#). Evite repetir caracteres ou usa-los em sequencia (ex: 123abc, aaaa, 1111)";
                    ApplicationBar.BackgroundColor = Color.FromArgb(255, 213, 222, 155);
                    btnConfirmar.IsEnabled = true;
                    break;
                case 3: //Senha Forte
                    Mensagem.Foreground = new SolidColorBrush(Color.FromArgb(255, 121, 179, 133));
                    Mensagem.Text = "SENHA FORTE - Senha ok";
                    ApplicationBar.BackgroundColor = Color.FromArgb(255, 121, 179, 133);
                    btnConfirmar.IsEnabled = true;
                    break;
                default: //Nao serve como senha 
                    Mensagem.Foreground = null;
                    Mensagem.Text = "";
                    ApplicationBar.BackgroundColor = Color.FromArgb(255, 31, 31, 31);  
                    btnConfirmar.IsEnabled = false;
                    break;
            }
        }
        
        //Metodos para fazer o password watermark funcionar
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
        private void ConfirmPasswordLostFocus(object sender, RoutedEventArgs e)
        {
            ConfirmCheckPasswordWatermark();
        }

        public void ConfirmCheckPasswordWatermark()
        {
            var confirmPasswordEmpty = string.IsNullOrEmpty(ConfirmPassword.Password);
            ConfirmPasswordWatermark.Opacity = confirmPasswordEmpty ? 100 : 0;
            ConfirmPassword.Opacity = confirmPasswordEmpty ? 0 : 100;
        }

        private void ConfirmPasswordGotFocus(object sender, RoutedEventArgs e)
        {
            ConfirmPasswordWatermark.Opacity = 0;
            ConfirmPassword.Opacity = 100;
        }

        //Metodo de handle do botão de enviar nova senha
        private void hBtnConfirmar(object sender, RoutedEventArgs e)
        {
            string login = ""; //TO-DO apenas deixei aqui pra não dar erro.
            string hash = Hash.hashing(Password.Password);
            SacisService.Service1SoapClient WService = new SacisService.Service1SoapClient();
            WService.alteraSenhaAsync(login,hash);
            WService.alteraSenhaCompleted += new EventHandler<alteraSenhaCompletedEventArgs>(alteraSenhaCompleted);
        }
        //Metodo com a resposta do comando de alterar a senha
        private void alteraSenhaCompleted(object obj, SacisService.alteraSenhaCompletedEventArgs e)
        {
            
        }
    }
}