using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Services;
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
        }

        public async void FillArraySongs()
        {
            query.SearchWord = SearchWord.Text;
            List<SongDto> allSongs;
            songs.Clear();
            if (ArtistId == -1 && AlbumId == -1) { MessageBox.Show("Not Songs found"); return; }

            ISongService songService = new SongService();

            allSongs = await songService.GetAll(ArtistId, AlbumId, query);

            if (allSongs == null || allSongs.Count == 0) return;
            allSongs.ForEach(album => songs.Add(album));
            DataGridSongs.DataContext = songs;
        }

        private void Button_ReturnToAllArtists(object sender, RoutedEventArgs e)
        {
            ShowAllAlbumsWindow showAllAlbumsWindow = new ShowAllAlbumsWindow();
            showAllAlbumsWindow.ArtistId = ArtistId;
            showAllAlbumsWindow.FillArrayAlbums();
            this.Visibility = Visibility.Hidden;
            showAllAlbumsWindow.Show();
        }
        private void Button_SearchSongs(object sender, RoutedEventArgs e)
        {
            FillArraySongs();
        }

        private void Button_CreateSong(object sender, RoutedEventArgs e)
        {

        }

        private void Button_UpdateSong(object sender, RoutedEventArgs e)
        {

        }

        private void Button_DeleteSongById(object sender, RoutedEventArgs e)
        {

        }

        private void Button_DeleteAllSongs(object sender, RoutedEventArgs e)
        {

        }

        private void Button_DetailsSong(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_ChangeSortDirection(object sender, SelectionChangedEventArgs e)
        {
            if (SortDirection.SelectedIndex == 0) query.SortDirection = Models.SortDirection.NULL;
            else if (SortDirection.SelectedIndex == 1) query.SortDirection = Models.SortDirection.ASC;
            else if (SortDirection.SelectedIndex == 2) query.SortDirection = Models.SortDirection.DESC;
            //FillArraySongs();
        }

        private void ComboBox_ChangeSortBy(object sender, SelectionChangedEventArgs e)
        {
            if (SortBy.SelectedIndex == 0) query.SortBy = "";
            else if (SortBy.SelectedIndex == 1) query.SortBy = "Name";
            //FillArraySongs();
        }
    }
}
