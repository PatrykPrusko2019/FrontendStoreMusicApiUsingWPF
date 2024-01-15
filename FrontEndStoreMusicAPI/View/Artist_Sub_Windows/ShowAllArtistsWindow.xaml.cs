using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace FrontEndStoreMusicAPI.View.Artist_Sub_Windows
{
    /// <summary>
    /// Interaction logic for ShowAllArtistsWindow.xaml
    /// </summary>
    public partial class ShowAllArtistsWindow : Window
    {
        ObservableCollection<PageResult<ArtistDto>> paginationResults;
        ObservableCollection<ArtistDto> artists;
        private ArtistQuery query;
        public ShowAllArtistsWindow()
        {
            InitializeComponent();
            query = new ArtistQuery();
            artists = new ObservableCollection<ArtistDto>();
            paginationResults = new ObservableCollection<PageResult<ArtistDto>>();
        }

        private void Button_ReturnToArtists(object sender, RoutedEventArgs e)
        {
            ArtistWindow artistWindow = new ArtistWindow();
            this.Visibility = Visibility.Hidden;
            artistWindow.Show();
        }

        private void Button_ShowAllArtist(object sender, RoutedEventArgs e)
        {
            GetAllArtistsAndSetDataGridArtistsAndSetDataGridArtistsResults();
        }

        private void Button_UpdateArtist(object sender, RoutedEventArgs e)
        {
            int artistId;
            var indexItem = DataGridArtists.SelectedIndex;
            if (indexItem != -1) { artistId = artists[indexItem].Id; }
            else { MessageBox.Show("Select any record to be updated !"); return; }

            UpdateArtist updateArtist = new UpdateArtist();
            updateArtist.ArtistId = artistId;
            this.Visibility = Visibility.Hidden;
            updateArtist.Show();
        }

        //5, 10, 15 -> records 
        private void ComboBox_ChangeRecordsPerPage(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (RecordsPerPage.SelectedIndex == 0) query.PageSize = 5;
            else if (RecordsPerPage.SelectedIndex == 1) query.PageSize = 10;
            else if (RecordsPerPage.SelectedIndex == 2) query.PageSize = 15;
        }

        private void ComboBox_ChangeSortBy(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SortBy.SelectedIndex == 0) query.SortBy = "Name";
            else if (SortBy.SelectedIndex == 1) query.SortBy = "Description";
            else if (SortBy.SelectedIndex == 2) query.SortBy = "KindOfMusic";
            else if (SortBy.SelectedIndex == 3) query.SortBy = "";
        }

        private void ComboBox_ChangeSortDirection(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SortDirection.SelectedIndex == 0) query.SortDirection = Models.SortDirection.ASC;
            else if (SortDirection.SelectedIndex == 1) query.SortDirection = Models.SortDirection.DESC;
            else if (SortDirection.SelectedIndex == 2) query.SortDirection = default;
        }

        private void Button_PreviousPage(object sender, RoutedEventArgs e)
        {
            string[] strings = CurrentPage.Text.Split(" ");

            int currentPage = int.Parse(strings[0]);

            if (currentPage > 1) currentPage--;
            strings[0] = currentPage.ToString();

            CurrentPage.Text = string.Join(" ", strings);

            query.PageNumber = currentPage;
            GetAllArtistsAndSetDataGridArtistsAndSetDataGridArtistsResults();
        }

        private void Button_NextPage(object sender, RoutedEventArgs e)
        {
            string[] strings = CurrentPage.Text.Split(" ");

            int currentPage = int.Parse(strings[0]);
            int totalPage = int.Parse(strings[2]);

            if (currentPage < totalPage) currentPage++;

            strings[0] = currentPage.ToString();

            CurrentPage.Text = string.Join(" ", strings);

            query.PageNumber = currentPage;
            GetAllArtistsAndSetDataGridArtistsAndSetDataGridArtistsResults();
        }

        private async void GetAllArtistsAndSetDataGridArtistsAndSetDataGridArtistsResults()
        {
            artists.Clear();
            paginationResults.Clear();

            ArtistService artistService = new ArtistService();
            query.SearchWord = SearchWord.Text;
            if (query.PageNumber == 0) query.PageNumber = 1;
            if (query.PageSize == 0) query.PageSize = 5;

            var artistsResult = await artistService.GetAll(query);
            if (artistsResult.Items.Count == null || artistsResult.Items.Count == 0) { MessageBox.Show("artists not found !"); }
            paginationResults.Add(artistsResult);
            DataGridArtistsResults.DataContext = paginationResults;

            string[] strings = CurrentPage.Text.Split(" ");
            strings[2] = $"{artistsResult.TotalPages}";
            CurrentPage.Text = string.Join(" ", strings);
            RecordsPerPage.Text = query.PageSize.ToString();

            ObservableCollection<AlbumDto> Albums = new ObservableCollection<AlbumDto>();

            foreach (var artist in artistsResult.Items)
            {
                artists.Add(artist);
                foreach (var album in artist.Albums)
                {
                    Albums.Add(album);
                }
            }

            DataGridArtists.DataContext = artists;
        }
    }

}
