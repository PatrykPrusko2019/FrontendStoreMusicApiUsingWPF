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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace FrontEndStoreMusicAPI.View
{
    /// <summary>
    /// Interaction logic for MusicStoreWindow.xaml
    /// </summary>
    public partial class MusicStoreWindow : Window
    {
        public static UserDto detailsUser = new UserDto();


        public MusicStoreWindow()
        {
            InitializeComponent();
            DescriptionWindowMusicStore.Text += $" {detailsUser.FirstName} {detailsUser.LastName}";
        }


        private void Button_SingOut(object sender, RoutedEventArgs e)
        {
            detailsUser = null;
            MainWindowLogin windowLogin = new MainWindowLogin();
            this.Visibility = Visibility.Hidden;
            windowLogin.Show();
        }

        private void Button_UserDetails(object sender, RoutedEventArgs e)
        {

        }

        private void Button_ShowArtists(object sender, RoutedEventArgs e)
        {
            ArtistWindow artistWindow = new ArtistWindow();
            this.Visibility= Visibility.Hidden;
            artistWindow.Show();
        }

        private void Button_ShowAlbums(object sender, RoutedEventArgs e)
        {

        }

        private void Button_ShowSongs(object sender, RoutedEventArgs e)
        {

        }
    }
}
