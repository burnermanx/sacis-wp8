﻿#pragma checksum "G:\Users\Burner\Documents\GitHub\sacis-wp8\SACIS\SACIS\AlteraSenha.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "84DB1401EA1B6DE624698DCE4A3D2332"
//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.34014
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace SACIS {
    
    
    public partial class AlteraSenha : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBox PasswordWatermark;
        
        internal System.Windows.Controls.PasswordBox Password;
        
        internal System.Windows.Controls.TextBox ConfirmPasswordWatermark;
        
        internal System.Windows.Controls.PasswordBox ConfirmPassword;
        
        internal System.Windows.Controls.TextBlock Mensagem;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/SACIS;component/AlteraSenha.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.PasswordWatermark = ((System.Windows.Controls.TextBox)(this.FindName("PasswordWatermark")));
            this.Password = ((System.Windows.Controls.PasswordBox)(this.FindName("Password")));
            this.ConfirmPasswordWatermark = ((System.Windows.Controls.TextBox)(this.FindName("ConfirmPasswordWatermark")));
            this.ConfirmPassword = ((System.Windows.Controls.PasswordBox)(this.FindName("ConfirmPassword")));
            this.Mensagem = ((System.Windows.Controls.TextBlock)(this.FindName("Mensagem")));
        }
    }
}

