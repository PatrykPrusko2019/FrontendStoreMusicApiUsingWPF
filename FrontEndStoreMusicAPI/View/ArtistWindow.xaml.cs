using FrontEndStoreMusicAPI.View.Artist_Sub_Windows;
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
    /// Interaction logic for ArtistWindow.xaml
    /// </summary>
    public partial class ArtistWindow : Window
    {
        public ArtistWindow()
        {
            InitializeComponent();
        }

        private void Button_ReturnToMusicStoreWindow(object sender, RoutedEventArgs e)
        {
            MusicStoreWindow musicStoreWindow = new MusicStoreWindow();
            this.Visibility = Visibility.Hidden;
            musicStoreWindow.Show();
        }

        private void Button_ShowAllArtist(object sender, RoutedEventArgs e)
        {
            ShowAllArtistsWindow showAllArtists = new ShowAllArtistsWindow();
            this.Visibility = Visibility.Hidden;
            showAllArtists.Show();
        }

        private void Button_ShowOneArtist(object sender, RoutedEventArgs e)
        {

        }

        private void Button_AddArtist(object sender, RoutedEventArgs e)
        {

        }

        private void Button_UpdateArtist(object sender, RoutedEventArgs e)
        {

        }

        private void Button_DeleteArtist(object sender, RoutedEventArgs e)
        {

        }
    }
}
