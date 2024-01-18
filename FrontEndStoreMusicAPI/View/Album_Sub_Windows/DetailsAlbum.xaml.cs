using FrontEndStoreMusicAPI.Models;
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

namespace FrontEndStoreMusicAPI.View.Album_Sub_Windows
{
    /// <summary>
    /// Interaction logic for DetailsAlbum.xaml
    /// </summary>
    public partial class DetailsAlbum : Window
    {
        public int ArtistId;
        public int AlbumId;
        ObservableCollection<DetailsAlbumDto> album;
        public DetailsAlbum()
        {
            InitializeComponent();
            album = new ObservableCollection<DetailsAlbumDto>();
        }

        public void FillDetailsArray(DetailsAlbumDto detailsAlbum)
        {
            album.Add(detailsAlbum);
            DataGridDetailsAlbums.DataContext = album;
        }

        private void Button_ReturnToAllAlbums(object sender, RoutedEventArgs e)
        {
            ShowAllAlbumsWindow showAllAlbumsWindow = new ShowAllAlbumsWindow();
            this.Visibility = Visibility.Hidden;
            showAllAlbumsWindow.artistId = ArtistId;
            showAllAlbumsWindow.FillArrayAlbums();
            showAllAlbumsWindow.Show();
        }


    }
}
