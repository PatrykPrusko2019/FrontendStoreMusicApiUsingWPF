using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Services;
using FrontEndStoreMusicAPI.Utilites;
using FrontEndStoreMusicAPI.View.Album_Sub_Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace FrontEndStoreMusicAPI.View.Song_Sub_Window
{
    /// <summary>
    /// Interaction logic for ShowAllSongsWindow.xaml
    /// </summary>
    public partial class ShowAllSongsWindow : Window
    {
        public bool IsGetAllSongs { get; set; }
        private SongQuery query;
        public int ArtistId { get; set; }
        public int AlbumId { get; set; }
        public int SongId { get; set; }
        private ObservableCollection<SongDto> songs;
        public ShowAllSongsWindow()
        {
            InitializeComponent();
            ArtistId = -1;
            AlbumId = -1;
            SongId = -1;
            songs = new ObservableCollection<SongDto>();
            query = new SongQuery();
            IsGetAllSongs = false;
        }

        public async void FillArraySongs()
        {
            query.SearchWord = SearchWord.Text;
            List<SongDto> allSongs;
            songs.Clear();
            if (ArtistId == -1 && AlbumId == -1) { Xceed.Wpf.Toolkit.MessageBox.Show("Not Songs found"); return; }

            if (IsGetAllSongs)
            {
                IAllSongsService allSongService = new AllSongsService();
                allSongs = await allSongService.GetAll(query);
            }
            else
            {
                ISongService songService = new SongService();
                allSongs = await songService.GetAll(ArtistId, AlbumId, query);
            }
            
            if (allSongs == null || allSongs.Count == 0) return;
            allSongs.ForEach(album => songs.Add(album));
            DataGridSongs.DataContext = songs;
        }

        private void Button_ReturnToAllAlbums(object sender, RoutedEventArgs e)
        {
            if (ArtistId == -2 && AlbumId == -2) 
            {
                MusicStoreWindow musicStoreWindow = new MusicStoreWindow();
                this.Visibility = Visibility.Hidden;
                musicStoreWindow.Show();
            }
            else
            {
                ShowAllAlbumsWindow showAllAlbumsWindow = new ShowAllAlbumsWindow();
                showAllAlbumsWindow.ArtistId = ArtistId;
                showAllAlbumsWindow.FillArrayAlbums();
                this.Visibility = Visibility.Hidden;
                showAllAlbumsWindow.Show();
            }
        }
        private void Button_SearchSongs(object sender, RoutedEventArgs e)
        {
            FillArraySongs();
        }

        private void Button_CreateSong(object sender, RoutedEventArgs e)
        {
            if (MusicStoreWindow.DetailsUser == null)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("You are not logged in, so you do not have access to this option : Create!");
                return;
            }

            UpdateCreateSong createSong = new UpdateCreateSong();
            createSong.DescriptionUpdateCreateSong.Text = "You are in the Create Song section";
            createSong.SongId = 0;
            createSong.ArtistId = ArtistId;
            createSong.AlbumId = AlbumId;

            this.Visibility = Visibility.Hidden;
            createSong.Show();
        }

        private async void Button_UpdateSong(object sender, RoutedEventArgs e)
        {
            SongDto selectedSong;
            var indexItem = DataGridSongs.SelectedIndex;
            if (indexItem == -1 || indexItem == songs.Count) { Xceed.Wpf.Toolkit.MessageBox.Show("Select any record to be updated!"); return; }
            else if (MusicStoreWindow.DetailsUser == null)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("You are not logged in, so you do not have access to this option : Update!");
                return;
            }
            else
            {
                SongId = songs[indexItem].Id;
                AlbumId = songs[indexItem].AlbumId;
                ISongService songService = new SongService();
                selectedSong = await songService.GetById(ArtistId, AlbumId, SongId);
                if (selectedSong == null) return;
            }

            UpdateCreateSong updateSong = new UpdateCreateSong();
            updateSong.DescriptionUpdateCreateSong.Text = "You are in the Update Song section";
            updateSong.SongId = SongId;
            updateSong.AlbumId = AlbumId;
            updateSong.ArtistId = ArtistId;
            Fill.GetValuesToUpdateSong(selectedSong);

            this.Visibility = Visibility.Hidden;
            updateSong.Show();
        }

        private void Button_DeleteSongById(object sender, RoutedEventArgs e)
        {
            var indexItem = DataGridSongs.SelectedIndex;
            if (indexItem == -1 || indexItem == songs.Count) { Xceed.Wpf.Toolkit.MessageBox.Show("Select any record to be deleted!"); return; }
            else if (MusicStoreWindow.DetailsUser == null) { Xceed.Wpf.Toolkit.MessageBox.Show("You are not logged in, so you do not have access to this option : Delete One!"); } // no user logged in
            else
            {
                SongId = songs[indexItem].Id;
                AlbumId = songs[indexItem].AlbumId;
                ISongService songService = new SongService();
                songService.DeleteById(ArtistId, AlbumId, SongId);
                FillArraySongs();
            }
        }

        private void Button_DeleteAllSongs(object sender, RoutedEventArgs e)
        {
            var indexItem = DataGridSongs.SelectedIndex;
            if (MusicStoreWindow.DetailsUser == null) { Xceed.Wpf.Toolkit.MessageBox.Show("You are not logged in, so you do not have access to this option : Delete All!"); } // no user logged in
            else
            {
                ISongService albumService = new SongService();
                if (albumService.DeleteAll(ArtistId, AlbumId))
                {
                    FillArraySongs();
                }
            }
        }

        private async void Button_DetailsSong(object sender, RoutedEventArgs e)
        {
            var indexItem = DataGridSongs.SelectedIndex;
            if (indexItem == -1 || indexItem == songs.Count) { Xceed.Wpf.Toolkit.MessageBox.Show("Select any record to display Details of Song !"); return; }

            ISongService songService = new SongService();
            SongId = songs[indexItem].Id;
            AlbumId= songs[indexItem].AlbumId;
            var detailsSong = await songService.GetDetails(ArtistId, AlbumId, SongId);

            if (detailsSong == null) return;

            DetailsSong details = new DetailsSong();
            details.artistId = ArtistId;
            details.albumId = AlbumId;
            details.FillDetailsArray(detailsSong);

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
            else if (SortBy.SelectedIndex == 1) query.SortBy = "Name";
        }

        public void SetButtons()
        {
            Add.Visibility = Visibility.Hidden;
            Update.Visibility = Visibility.Hidden;
            Delete_One.Visibility = Visibility.Hidden;
            Delete_All.Visibility = Visibility.Hidden;
            Details_Song.Visibility = Visibility.Hidden;
            ReturnToAllAlbums.ToolTip = "return to Music Store";
        }
    }
}
