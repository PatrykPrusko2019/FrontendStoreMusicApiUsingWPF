using FrontEndStoreMusicAPI.Models;
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
        private UserDto detailsUser;
        public WindowMusicStore(UserDto loginUser)
        {
            InitializeComponent();
            detailsUser = loginUser;
            DescriptionWindowMusicStore.Text += $" {detailsUser.FirstName} {detailsUser.LastName}";
        }


        private void Button_SingOut(object sender, RoutedEventArgs e)
        {
            MainWindowLogin windowLogin = new MainWindowLogin();
            this.Visibility = Visibility.Hidden;
            windowLogin.Show();
        }

        private void Button_UserDetails(object sender, RoutedEventArgs e)
        {

        }

        private void Button_ShowArtist(object sender, RoutedEventArgs e)
        {

        }

        private void Button_ShowAlbums(object sender, RoutedEventArgs e)
        {

        }

        private void Button_ShowSongs(object sender, RoutedEventArgs e)
        {

        }
    }
}
