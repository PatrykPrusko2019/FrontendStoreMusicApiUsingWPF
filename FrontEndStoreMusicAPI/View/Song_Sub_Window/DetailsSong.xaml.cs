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

namespace FrontEndStoreMusicAPI.View.Song_Sub_Window
{
    /// <summary>
    /// Interaction logic for DetailsSong.xaml
    /// </summary>
    public partial class DetailsSong : Window
    {
        public int artistId;
        public int albumId;
        public int songId;
        ObservableCollection<DetailsSongDto> song;
        public DetailsSong()
        {
            InitializeComponent();
            song = new ObservableCollection<DetailsSongDto>();
        }

        public void FillDetailsArray(DetailsSongDto detailsSong)
        {
            song.Add(detailsSong);
            DataGridDetailsSong.DataContext = song;
        }

        private void Button_ReturnToAllSongs(object sender, RoutedEventArgs e)
        {
            ShowAllSongsWindow showAllSongsWindow = new ShowAllSongsWindow();
            showAllSongsWindow.ArtistId = artistId;
            showAllSongsWindow.AlbumId = albumId;
            showAllSongsWindow.FillArraySongs();
            this.Visibility = Visibility.Hidden;
            showAllSongsWindow.Show();
        }
    }
}
