﻿using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Models.Details;
using FrontEndStoreMusicAPI.Services;
using FrontEndStoreMusicAPI.Utilites;
using FrontEndStoreMusicAPI.View.Album_Sub_Windows;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

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

        private void Button_ShowAllAlbums(object sender, RoutedEventArgs e)
        {
            var indexItem = DataGridArtists.SelectedIndex;
            if (indexItem == -1) { Xceed.Wpf.Toolkit.MessageBox.Show("Select any record to display All Albums of given Artist!"); return; }
            

            var selectedAlbums = artists[indexItem].Albums;
            if (selectedAlbums == null || selectedAlbums.Count == 0) Xceed.Wpf.Toolkit.MessageBox.Show("There are no Albums! Add new Album to given Artist");

            ShowAllAlbumsWindow showAllAlbumsWindow = new ShowAllAlbumsWindow();
            showAllAlbumsWindow.ArtistId = artists[indexItem].Id;
            showAllAlbumsWindow.FillArrayAlbums();
            this.Visibility = Visibility.Hidden;
            showAllAlbumsWindow.Show();
            
            

        }

        private void Button_ReturnToMusicStore(object sender, RoutedEventArgs e)
        {
            MusicStoreWindow musicStoreWindow = new MusicStoreWindow();
            this.Visibility = Visibility.Hidden;
            musicStoreWindow.Show();
        }


        private void Button_DeleteArtist(object sender, RoutedEventArgs e)
        {
            int artistId;
            var indexItem = DataGridArtists.SelectedIndex;

            if (indexItem == -1 || indexItem == artists.Count) { Xceed.Wpf.Toolkit.MessageBox.Show("Select any record to be deleted!"); return; }
            else if (MusicStoreWindow.DetailsUser == null) { Xceed.Wpf.Toolkit.MessageBox.Show("You are not logged in, so you do not have access to this option : Delete!"); } // no user logged in
            else
            {
                artistId = artists[indexItem].Id;
                IArtistService artistService = new ArtistService();
                artistService.Delete(artistId);

                GetAllArtistsAndSetDataGridArtistsAndSetDataGridArtistsResults();
            }
        }

        private void Button_ShowAllArtist(object sender, RoutedEventArgs e)
        {
            GetAllArtistsAndSetDataGridArtistsAndSetDataGridArtistsResults();
        }

        private async void Button_DetailsArtist(object sender, RoutedEventArgs e)
        {
            DetailsArtist detailsArtist = new DetailsArtist();

            int artistId;
            DetailsArtistDto selectedArtist;
            var indexItem = DataGridArtists.SelectedIndex;

            if (indexItem == -1 || indexItem == artists.Count) { Xceed.Wpf.Toolkit.MessageBox.Show("Select any record to display Details of Artist !"); return; }
            else if (MusicStoreWindow.DetailsUser != null && MusicStoreWindow.DetailsUser.Id == artists[indexItem].CreatedById) // user logged in and artist created by this artist
            {
                artistId = artists[indexItem].Id;
                IArtistService artistService = new ArtistService();
                selectedArtist = await artistService.GetDetails(artistId);
                if (selectedArtist == null) return;
                var resultAlbumsSongs = HelperHttpClient.GenerateAlbumsSongsForArtist(artists[indexItem]);
                selectedArtist.AlbumsSongs = resultAlbumsSongs.AlbumsSongs;

                detailsArtist.FillDetailsArray(selectedArtist); // plus 2 columns to display: ContactEmail, ContactNumber
            }
            else // for all users and non-loggers
            {
                ArtistDto artistDto = artists[indexItem];
                detailsArtist.FillArray(artistDto); // without 2 columns to display : ContactEmail, ContactNumber
            }
            
            this.Visibility = Visibility.Hidden;
            detailsArtist.Show();
        }


        private void Button_CreateArtist(object sender, RoutedEventArgs e)
        {
            if (MusicStoreWindow.DetailsUser == null)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("You are not logged in, so you do not have access to this option : Create!");
                return;
            }

            UpdateCreateArtist createArtist = new UpdateCreateArtist();
                createArtist.DescriptionUpdateCreateArtist.Text = "You are in the Create Artist section";
                this.Visibility = Visibility.Hidden;
                createArtist.Show();
        }

        private async void Button_UpdateArtist(object sender, RoutedEventArgs e)
        {
            int artistId;
            DetailsArtistDto selectedArtist;
            var indexItem = DataGridArtists.SelectedIndex;

            if (indexItem == -1 || indexItem == artists.Count) { Xceed.Wpf.Toolkit.MessageBox.Show("Select any record to be updated!"); return; }
            else if (MusicStoreWindow.DetailsUser == null) 
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("You are not logged in, so you do not have access to this option : Update!");
                return;
            }
            else
            { 
                artistId = artists[indexItem].Id;
                IArtistService artistService = new ArtistService();
                selectedArtist = await artistService.GetDetails(artistId);
                if (selectedArtist == null) return;
            }

            UpdateCreateArtist updateArtist = new UpdateCreateArtist();
            updateArtist.ArtistId = artistId;
            updateArtist.DescriptionUpdateCreateArtist.Text = "You are in the Update Artist section";
            Fill.GetValuesToUpdateArtist(selectedArtist);
            this.Visibility = Visibility.Hidden;
            updateArtist.Show();
        }

        //5, 10, 15 -> records 
        private void ComboBox_ChangeRecordsPerPage(object sender, SelectionChangedEventArgs e)
        {
            if (RecordsPerPage.SelectedIndex == 0) query.PageSize = 5;
            else if (RecordsPerPage.SelectedIndex == 1) query.PageSize = 10;
            else if (RecordsPerPage.SelectedIndex == 2) query.PageSize = 15;
        }

        private void ComboBox_ChangeSortBy(object sender, SelectionChangedEventArgs e)
        {
            if (SortBy.SelectedIndex == 0) query.SortBy = "";
            else if (SortBy.SelectedIndex == 1) query.SortBy = "Name";
            else if (SortBy.SelectedIndex == 2) query.SortBy = "Description";
            else if (SortBy.SelectedIndex == 3) query.SortBy = "KindOfMusic";

        }

        private void ComboBox_ChangeSortDirection(object sender, SelectionChangedEventArgs e)
        {
            if (SortDirection.SelectedIndex == 0) query.SortDirection = Models.SortDirection.NULL;
            else if (SortDirection.SelectedIndex == 1) query.SortDirection = Models.SortDirection.ASC;
            else if (SortDirection.SelectedIndex == 2) query.SortDirection = Models.SortDirection.DESC;
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

        private void Button_LastPage(object sender, RoutedEventArgs e)
        {
            string[] strings = CurrentPage.Text.Split(" ");

            int currentPage = int.Parse(strings[0]);
            int totalPage = int.Parse(strings[2]);

            if (currentPage < totalPage) currentPage = totalPage;

            strings[0] = currentPage.ToString();

            CurrentPage.Text = string.Join(" ", strings);

            query.PageNumber = currentPage;
            GetAllArtistsAndSetDataGridArtistsAndSetDataGridArtistsResults();
        }

        private void Button_FirstPage(object sender, RoutedEventArgs e)
        {
            string[] strings = CurrentPage.Text.Split(" ");

            int currentPage = int.Parse(strings[0]);

            if (currentPage > 1) currentPage = 1;
            strings[0] = currentPage.ToString();

            CurrentPage.Text = string.Join(" ", strings);

            query.PageNumber = currentPage;
            GetAllArtistsAndSetDataGridArtistsAndSetDataGridArtistsResults();
        }
        public async void GetAllArtistsAndSetDataGridArtistsAndSetDataGridArtistsResults()
        {
            artists.Clear();
            paginationResults.Clear();

            ArtistService artistService = new ArtistService();
            query.SearchWord = SearchWord.Text;
            if (query.PageNumber == 0) query.PageNumber = 1;
            if (query.PageSize == 0) query.PageSize = 5;

            var artistsResult = await artistService.GetAll(query);
            if (artistsResult == null || artistsResult.Items == null || artistsResult.Items.Count == 0) 
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("artists not found ! Please use the search button again !!!");
                query.PageNumber = 0;
                query.PageSize = 0;
                query.SearchWord = "";
                query.SortBy = "";
                query.SortDirection = Models.SortDirection.NULL;
                return;
            }
            paginationResults.Add(artistsResult);
            DataGridArtistsResults.DataContext = paginationResults;

            string[] strings = CurrentPage.Text.Split(" ");
            strings[2] = $"{artistsResult.TotalPages}";
            CurrentPage.Text = string.Join(" ", strings);
            RecordsPerPage.Text = query.PageSize.ToString();

            foreach (var artist in artistsResult.Items)
            {
                artists.Add(artist);
            }

            DataGridArtists.DataContext = artists;
        }
    }

}
