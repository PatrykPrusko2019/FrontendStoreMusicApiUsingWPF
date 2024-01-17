using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Services;
using FrontEndStoreMusicAPI.Utilites;
using FrontEndStoreMusicAPI.View.Album_Sub_Windows;
using System.Collections.ObjectModel;
using System.Windows;

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
            if (indexItem == -1) { MessageBox.Show("Select any record to display Details of Artist!"); return; }

            var selectedAlbums = artists[indexItem].Albums;
            if (selectedAlbums != null && selectedAlbums.Count > 0) 
            {
                ShowAllAlbumsWindow showAllAlbumsWindow = new ShowAllAlbumsWindow();
                showAllAlbumsWindow.artistId = artists[indexItem].Id;
                showAllAlbumsWindow.FillArrayAlbums();
                this.Visibility = Visibility.Hidden;
                showAllAlbumsWindow.Show();
            }
            else
            {
                MessageBox.Show("There are no Albums! Select another record, which has a Album");
            }

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
            if (indexItem != -1 && MusicStoreWindow.DetailsUser != null) // user logged in
            {
                artistId = artists[indexItem].Id;
                IArtistService artistService = new ArtistService();
                artistService.Delete(artistId);

                GetAllArtistsAndSetDataGridArtistsAndSetDataGridArtistsResults();
            }
            else if (MusicStoreWindow.DetailsUser == null) // no user logged in
            {
                MessageBox.Show("You are not logged in, so you do not have access to this option : Delete!");
            }
            else { MessageBox.Show("Select any record to display Details of Artist !"); return; }

            
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
            if (indexItem != -1 && MusicStoreWindow.DetailsUser != null && MusicStoreWindow.DetailsUser.Id == artists[indexItem].CreatedById) // user logged in and artist created by this artist
            {
                artistId = artists[indexItem].Id;
                IArtistService artistService = new ArtistService();
                selectedArtist = await artistService.GetDetails(artistId);
                var resultAlbumsSongs = HelperHttpClient.GenerateAlbumsSongsForArtist(artists[indexItem]);
                selectedArtist.AlbumsSongs = resultAlbumsSongs.AlbumsSongs;

                detailsArtist.FillDetailsArray(selectedArtist); // plus 2 columns to display: ContactEmail, ContactNumber
            }
            else if (indexItem != -1) // for all users and non-loggers
            {
                ArtistDto artistDto = artists[indexItem];
                detailsArtist.FillArray(artistDto); // without 2 columns to display : ContactEmail, ContactNumber
            }
            else { MessageBox.Show("Select any record to display Details of Artist !"); return; }

            this.Visibility = Visibility.Hidden;
            detailsArtist.Show();
        }


        private void Button_CreateArtist(object sender, RoutedEventArgs e)
        {
            if (MusicStoreWindow.DetailsUser != null)
            {
                UpdateCreateArtist createArtist = new UpdateCreateArtist();
                createArtist.DescriptionUpdateCreateArtist.Text = "You are in the Create Artist section";
                this.Visibility = Visibility.Hidden;
                createArtist.Show();
            }
            else
            {
                MessageBox.Show("You are not logged in, so you do not have access to this option : Create!");
            }
            
        }

        private async void Button_UpdateArtist(object sender, RoutedEventArgs e)
        {
            int artistId;
            DetailsArtistDto selectedArtist;
            var indexItem = DataGridArtists.SelectedIndex;
            if (MusicStoreWindow.DetailsUser == null)
            {
                MessageBox.Show("You are not logged in, so you do not have access to this option : Update!");
                return;
            }
            else if (indexItem != -1) 
            { 
                artistId = artists[indexItem].Id;
                IArtistService artistService = new ArtistService();
                selectedArtist = await artistService.GetDetails(artistId);
            }
            else { MessageBox.Show("Select any record to be updated !"); return; }

            UpdateCreateArtist updateArtist = new UpdateCreateArtist();
            updateArtist.artistId = artistId;
            updateArtist.DescriptionUpdateCreateArtist.Text = "You are in the Update Artist section";
            Fill.FillValuesOfUpdateArtist(selectedArtist);
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
            if (artistsResult.Items == null || artistsResult.Items.Count == 0) { MessageBox.Show("artists not found !"); return; }
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
