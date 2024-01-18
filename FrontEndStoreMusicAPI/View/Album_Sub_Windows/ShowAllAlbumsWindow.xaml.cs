
using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Services;
using FrontEndStoreMusicAPI.Utilites;
using FrontEndStoreMusicAPI.View.Artist_Sub_Windows;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;

namespace FrontEndStoreMusicAPI.View.Album_Sub_Windows
{
    /// <summary>
    /// Interaction logic for ShowAllArtistsWindow.xaml
    /// </summary>
    public partial class ShowAllAlbumsWindow : Window
    {
        public int artistId { get; set; }
        private ObservableCollection<AlbumDto> albums;
        public ShowAllAlbumsWindow()
        {
            artistId = -1;
            InitializeComponent();
            albums = new ObservableCollection<AlbumDto>();
        }

        public async void FillArrayAlbums()
        {
            if (artistId == -1) { MessageBox.Show("Not Albums found"); return; }

            IAlbumService albumService = new AlbumService();
            var allAlbums = await albumService.GetAll(artistId);

            allAlbums.ForEach(album => albums.Add(album));
            DataGridAlbums.DataContext = albums;
        }

        private void Button_ReturnToAllArtist(object sender, RoutedEventArgs e)
        {
            ShowAllArtistsWindow showAllArtistsWindow = new ShowAllArtistsWindow();
            this.Visibility = Visibility.Hidden;
            showAllArtistsWindow.Show();
        }

        private void Button_SearchAlbums(object sender, RoutedEventArgs e)
        {

        }

        private void Button_CreateAlbum(object sender, RoutedEventArgs e)
        {
            if (MusicStoreWindow.DetailsUser == null)
            {
                MessageBox.Show("You are not logged in, so you do not have access to this option : Create!");
                return;
            }

            UpdateCreateAlbum createAlbum = new UpdateCreateAlbum();
            createAlbum.DescriptionUpdateCreateAlbum.Text = "You are in the Create Album section";
            createAlbum.albumId = 0;
            createAlbum.artistId = artistId;

            this.Visibility = Visibility.Hidden;
            createAlbum.Show();
        }

        private void Button_UpdateAlbum(object sender, RoutedEventArgs e)
        {
            var indexItem = DataGridAlbums.SelectedIndex;
            if (indexItem == -1) { MessageBox.Show("Select any record to be updated!"); return; }
            else if (MusicStoreWindow.DetailsUser == null)
            {
                MessageBox.Show("You are not logged in, so you do not have access to this option : Update!");
                return;
            }
            else
            {
                UpdateCreateAlbum updateAlbum = new UpdateCreateAlbum();
                updateAlbum.DescriptionUpdateCreateAlbum.Text = "You are in the Update Album section";
                updateAlbum.albumId = albums[indexItem].Id;
                updateAlbum.artistId = artistId;

                this.Visibility = Visibility.Hidden;
                updateAlbum.Show();
            }
        }

        private void Button_DeleteAlbumById(object sender, RoutedEventArgs e)
        {
            var indexItem = DataGridAlbums.SelectedIndex;
            if (indexItem == -1) { MessageBox.Show("Select any record to be deleted !"); return; }
            else if (MusicStoreWindow.DetailsUser == null) { MessageBox.Show("You are not logged in, so you do not have access to this option : Delete One!"); } // no user logged in
            else
            {
                var albumId = albums[indexItem].Id;
                IAlbumService albumService = new AlbumService();
                albumService.DeleteById(artistId, albumId);
                FillArrayAlbums();
            }
        }

        private void Button_DeleteAllAlbums(object sender, RoutedEventArgs e)
        {
            var indexItem = DataGridAlbums.SelectedIndex;
            if (MusicStoreWindow.DetailsUser == null) { MessageBox.Show("You are not logged in, so you do not have access to this option : Delete All!"); } // no user logged in
            else
            {
                IAlbumService albumService = new AlbumService();
                albumService.DeleteAll(artistId);
                FillArrayAlbums();
            }
        }

        private async void Button_DetailsAlbum(object sender, RoutedEventArgs e)
        {
            var indexItem = DataGridAlbums.SelectedIndex;
            if (indexItem == -1) { MessageBox.Show("Select any record to display Details of Album !"); return; }

            IAlbumService albumService = new AlbumService();
            int albumId = albums[indexItem].Id;
            DetailsAlbumDto detailsAlbum = await albumService.GetDetails(artistId, albumId);

            HelperHttpClient.GenerateSongsForAlbum(detailsAlbum);

            DetailsAlbum details = new DetailsAlbum();
            details.FillDetailsArray(detailsAlbum);

            this.Visibility = Visibility.Hidden;
            details.Show();
        }

    }
}
