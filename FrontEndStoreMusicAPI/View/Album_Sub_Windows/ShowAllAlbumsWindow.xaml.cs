
using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Services;
using FrontEndStoreMusicAPI.View.Artist_Sub_Windows;
using System;
using System.Collections.ObjectModel;
using System.Windows;

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
            if (artistId == -1) return;

            

        }

        private void Button_UpdateAlbum(object sender, RoutedEventArgs e)
        {

        }

        private void Button_DeleteAlbum(object sender, RoutedEventArgs e)
        {

        }

        private void Button_DetailsAlbum(object sender, RoutedEventArgs e)
        {

        }
    }
}
