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
    /// Interaction logic for WindowMusicStore.xaml
    /// </summary>
    public partial class WindowMusicStore : Window
    {
        private readonly string TOKEN_JWT;
        public WindowMusicStore(string tokenJWT)
        {
            InitializeComponent();
            TOKEN_JWT = tokenJWT;
        }


        private void Button_SingOut(object sender, RoutedEventArgs e)
        {
            MainWindowLogin windowLogin = new MainWindowLogin();
            this.Visibility = Visibility.Hidden;
            windowLogin.Show();
        }
    }
}
