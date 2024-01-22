
using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Services;
using FrontEndStoreMusicAPI.Utilites;
using FrontEndStoreMusicAPI.View.Artist_Sub_Windows;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
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
        private AlbumQuery query;
        public ShowAllAlbumsWindow()
        {
            artistId = -1;
            InitializeComponent();
            albums = new ObservableCollection<AlbumDto>();
            query = new AlbumQuery();
        }

        public async void FillArrayAlbums()
        {
            query.SearchWord = SearchWord.Text;
            List<AlbumDto> allAlbums;
            albums.Clear();
            if (artistId == -1) { MessageBox.Show("Not Albums found"); return; }

            IAlbumService albumService = new AlbumService();
            if (query.SearchWord != "" || query.SortBy == "Title" || query.SortDirection == Models.SortDirection.ASC || query.SortDirection == Models.SortDirection.DESC)
            {

                allAlbums = await albumService.GetAll(artistId, query);
            }
            else
            {
                allAlbums = await albumService.GetAll(artistId);
            }

            if (allAlbums == null || allAlbums.Count == 0) return;
            allAlbums.ForEach(album => albums.Add(album));
            DataGridAlbums.DataContext = albums;
        }

        private void Button_ReturnToAllArtist(object sender, RoutedEventArgs e)
        {
            ShowAllArtistsWindow showAllArtistsWindow = new ShowAllArtistsWindow();
            this.Visibility = Visibility.Hidden;
            showAllArtistsWindow.GetAllArtistsAndSetDataGridArtistsAndSetDataGridArtistsResults();
            showAllArtistsWindow.Show();
        }

        private void Button_SearchAlbums(object sender, RoutedEventArgs e)
        {
            FillArrayAlbums();
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

        private async void Button_UpdateAlbum(object sender, RoutedEventArgs e)
        {
            AlbumDto selectedAlbum;
            int albumId;
            var indexItem = DataGridAlbums.SelectedIndex;
            if (indexItem == -1) { MessageBox.Show("Select any record to be updated!"); return; }
            else if (MusicStoreWindow.DetailsUser == null)
            {
                MessageBox.Show("You are not logged in, so you do not have access to this option : Update!");
                return;
            }
            else
            {
                albumId = albums[indexItem].Id;
                IAlbumService albumService = new AlbumService();
                selectedAlbum = await albumService.GetById(artistId, albumId);
                if (selectedAlbum == null) return;
            }

            UpdateCreateAlbum updateAlbum = new UpdateCreateAlbum();
            updateAlbum.DescriptionUpdateCreateAlbum.Text = "You are in the Update Album section";
            updateAlbum.albumId = albums[indexItem].Id;
            updateAlbum.artistId = artistId;
            Fill.GetValuesToUpdateAlbum(selectedAlbum);

            this.Visibility = Visibility.Hidden;
            updateAlbum.Show();
        }

        private void Button_DeleteAlbumById(object sender, RoutedEventArgs e)
        {
            var indexItem = DataGridAlbums.SelectedIndex;
            if (indexItem == -1) { MessageBox.Show("Select any record to be deleted!"); return; }
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
            if (indexItem == -1) { MessageBox.Show("Select any record to be deleted!"); return; }
            else if (MusicStoreWindow.DetailsUser == null) { MessageBox.Show("You are not logged in, so you do not have access to this option : Delete All!"); } // no user logged in
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
            var detailsAlbum = await albumService.GetDetails(artistId, albumId);

            if (detailsAlbum == null) return;
            HelperHttpClient.GenerateSongsForAlbum(detailsAlbum);

            DetailsAlbum details = new DetailsAlbum();
            details.artistId = artistId;
            details.FillDetailsArray(detailsAlbum);

            this.Visibility = Visibility.Hidden;
            details.Show();
        }

        private void ComboBox_ChangeSortDirection(object sender, SelectionChangedEventArgs e)
        {
            if (SortDirection.SelectedIndex == 0) query.SortDirection = Models.SortDirection.NULL;
            else if (SortDirection.SelectedIndex == 1) query.SortDirection = Models.SortDirection.ASC;
            else if (SortDirection.SelectedIndex == 2) query.SortDirection = Models.SortDirection.DESC;
        }

        private void ComboBox_ChangeSortBy(object sender, SelectionChangedEventArgs e)
        {
            if (SortBy.SelectedIndex == 0) query.SortBy = "";
            else if (SortBy.SelectedIndex == 1) query.SortBy = "Title";
        }
    }
}
