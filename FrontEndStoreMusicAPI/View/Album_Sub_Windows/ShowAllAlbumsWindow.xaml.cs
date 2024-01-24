
using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Services;
using FrontEndStoreMusicAPI.Utilites;
using FrontEndStoreMusicAPI.View.Artist_Sub_Windows;
using FrontEndStoreMusicAPI.View.Song_Sub_Window;
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
        public bool IsGetAllArtists { get; set; }
        public int ArtistId { get; set; }
        public int AlbumId { get; set; }
        private ObservableCollection<AlbumDto> albums;
        private AlbumQuery query;
        public ShowAllAlbumsWindow()
        {
            ArtistId = -1;
            AlbumId = -1;
            InitializeComponent();
            albums = new ObservableCollection<AlbumDto>();
            query = new AlbumQuery();
            IsGetAllArtists = false;
        }

        public async void FillArrayAlbums()
        {
            query.SearchWord = SearchWord.Text;
            List<AlbumDto> allAlbums;
            albums.Clear();
            if (ArtistId == -1) { MessageBox.Show("Not Albums found"); return; }


            if (IsGetAllArtists)
            {
                IAllAlbumsService allAlbumService = new AllAlbumsService();
                allAlbums = await allAlbumService.GetAll(query); // all Albums
            }
            else
            {
                IAlbumService albumService = new AlbumService();
                allAlbums = await albumService.GetAll(ArtistId, query); // Albums from given Artist
            }

            if (allAlbums == null || allAlbums.Count == 0) return;
            allAlbums.ForEach(album => albums.Add(album));
            DataGridAlbums.DataContext = albums;
        }

        private void Button_ReturnToAllArtist(object sender, RoutedEventArgs e)
        {
            if (ArtistId == -2) 
            {
                MusicStoreWindow musicStoreWindow = new MusicStoreWindow();
                this.Visibility = Visibility.Hidden;
                musicStoreWindow.Show();
            }
            else
            {
                ShowAllArtistsWindow showAllArtistsWindow = new ShowAllArtistsWindow();
                this.Visibility = Visibility.Hidden;
                showAllArtistsWindow.GetAllArtistsAndSetDataGridArtistsAndSetDataGridArtistsResults();
                showAllArtistsWindow.Show();
            }
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
            createAlbum.AlbumId = 0;
            createAlbum.ArtistId = ArtistId;

            this.Visibility = Visibility.Hidden;
            createAlbum.Show();
        }

        private async void Button_UpdateAlbum(object sender, RoutedEventArgs e)
        {
            AlbumDto selectedAlbum;
            var indexItem = DataGridAlbums.SelectedIndex;
            if (indexItem == -1 || indexItem == albums.Count) { MessageBox.Show("Select any record to be updated!"); return; }
            else if (MusicStoreWindow.DetailsUser == null)
            {
                MessageBox.Show("You are not logged in, so you do not have access to this option : Update!");
                return;
            }
            else
            {
                AlbumId = albums[indexItem].Id;
                IAlbumService albumService = new AlbumService();
                selectedAlbum = await albumService.GetById(ArtistId, AlbumId);
                if (selectedAlbum == null) return;
            }

            UpdateCreateAlbum updateAlbum = new UpdateCreateAlbum();
            updateAlbum.DescriptionUpdateCreateAlbum.Text = "You are in the Update Album section";
            updateAlbum.AlbumId = AlbumId;
            updateAlbum.ArtistId = ArtistId;
            Fill.GetValuesToUpdateAlbum(selectedAlbum);

            this.Visibility = Visibility.Hidden;
            updateAlbum.Show();
        }

        private void Button_DeleteAlbumById(object sender, RoutedEventArgs e)
        {
            var indexItem = DataGridAlbums.SelectedIndex;
            if (indexItem == -1 || indexItem == albums.Count) { MessageBox.Show("Select any record to be deleted!"); return; }
            else if (MusicStoreWindow.DetailsUser == null) { MessageBox.Show("You are not logged in, so you do not have access to this option : Delete One!"); } // no user logged in
            else
            {
                AlbumId = albums[indexItem].Id;
                IAlbumService albumService = new AlbumService();
                albumService.DeleteById(ArtistId, AlbumId);
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
               if (albumService.DeleteAll(ArtistId))
                {
                    FillArrayAlbums();
                }
            }
        }

        private async void Button_DetailsAlbum(object sender, RoutedEventArgs e)
        {
            var indexItem = DataGridAlbums.SelectedIndex;
            if (indexItem == -1 || indexItem == albums.Count) { MessageBox.Show("Select any record to display Details of Album !"); return; }

            IAlbumService albumService = new AlbumService();
            AlbumId = albums[indexItem].Id;
            var detailsAlbum = await albumService.GetDetails(ArtistId, AlbumId);

            if (detailsAlbum == null) return;
            HelperHttpClient.GenerateSongsForAlbum(detailsAlbum);

            DetailsAlbum details = new DetailsAlbum();
            details.artistId = ArtistId;
            details.FillDetailsArray(detailsAlbum);

            this.Visibility = Visibility.Hidden;
            details.Show();
        }

        private void Button_ShowAllSongs(object sender, RoutedEventArgs e)
        {
            var indexItem = DataGridAlbums.SelectedIndex;
            if (indexItem == -1 || indexItem == albums.Count) { MessageBox.Show("Select any record to display All Songs of given Album!"); return; }
            
            var selectedSongs = albums[indexItem].Songs;
            if (selectedSongs != null && selectedSongs.Count > 0)
            {
                ShowAllSongsWindow showAllSongsWindow = new ShowAllSongsWindow();
                showAllSongsWindow.ArtistId = ArtistId;
                AlbumId = albums[indexItem].Id;
                showAllSongsWindow.AlbumId = AlbumId;
                showAllSongsWindow.FillArraySongs();
                this.Visibility = Visibility.Hidden;
                showAllSongsWindow.Show();
            }
            else
            {
                MessageBox.Show("There are no Songs! Select another record, which has a Song or Songs");
            }
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

        public void SetButtons()
        {
             Add.Visibility = Visibility.Hidden;
             Update.Visibility = Visibility.Hidden;
             Delete_One.Visibility = Visibility.Hidden;
             Delete_All.Visibility = Visibility.Hidden;
             Details_Album.Visibility = Visibility.Hidden;
             Show_Songs.Visibility = Visibility.Hidden;
             ReturnToAllArtist.ToolTip = "return to Music Store";
        }
    }
}
