using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Models.Details;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for DetailsUserWindow.xaml
    /// </summary>
    public partial class DetailsUserWindow : Window
    {
        ObservableCollection<UserDto> user;
        ObservableCollection<DetailsArtistDto> artists;
        public DetailsUserWindow()
        {
            InitializeComponent();
            artists = new ObservableCollection<DetailsArtistDto>();
            user = new ObservableCollection<UserDto>();
        }

         public void FillDetailsArray()
        {
            user.Add(MusicStoreWindow.DetailsUser);

            DataGridDetailsGivenUser.DataContext = user;

            foreach (DetailsArtistDto artist in MusicStoreWindow.DetailsUser.DetailsArtists) 
            {
                artists.Add(artist);
            }

            DataGridDetailsArtists.DataContext = artists;
        }

        private void Button_ReturnToMusicStore(object sender, RoutedEventArgs e)
        {
            MusicStoreWindow musicStoreWindow = new MusicStoreWindow();
            this.Visibility = Visibility.Hidden;
            musicStoreWindow.Show();
        }
    }
}
