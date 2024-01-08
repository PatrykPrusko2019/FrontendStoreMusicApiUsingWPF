using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FrontEndStoreMusicAPI.View
{
    /// <summary>
    /// Interaction logic for MainWindowLogin.xaml
    /// </summary>
    public partial class MainWindowLogin : Window
    {
        public MainWindowLogin()
        {
            InitializeComponent();
        }

        private void Button_Register(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            this.Visibility = Visibility.Hidden;
            register.Show();
        }

        private void Button_SignIn(object sender, RoutedEventArgs e)
        {
            LoginDto loginDto = new LoginDto()
            {
                Email = LoginEmail.Text,
                Password = LoginPassword.Text
            };
            ILoginService loginService = new LoginService();
            loginService.LoginUser(loginDto);

        }
    }
}
