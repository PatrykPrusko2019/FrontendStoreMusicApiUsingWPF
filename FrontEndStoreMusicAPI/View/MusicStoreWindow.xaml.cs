using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Services;
using FrontEndStoreMusicAPI.Utilites;
using FrontEndStoreMusicAPI.View.Album_Sub_Windows;
using FrontEndStoreMusicAPI.View.Artist_Sub_Windows;
using FrontEndStoreMusicAPI.View.Song_Sub_Window;
using System.Windows;

namespace FrontEndStoreMusicAPI.View
{
    /// <summary>
    /// Interaction logic for MusicStoreWindow.xaml
    /// </summary>
    public partial class MusicStoreWindow : Window
    {
        public static UserDto? DetailsUser;


        public MusicStoreWindow()
        {
            InitializeComponent();

            DescriptionWindowMusicStore.Text += DetailsUser != null ? $" {DetailsUser.FirstName}" : "Guest";
        }


        private void Button_SingOut(object sender, RoutedEventArgs e)
        {
            DetailsUser = null;
            MainWindowLogin windowLogin = new MainWindowLogin();
            this.Visibility = Visibility.Hidden;
            windowLogin.Show();
        }

        private async void Button_UserDetails(object sender, RoutedEventArgs e)
        {
            DetailsUserWindow detailsUserWindow = new DetailsUserWindow();
            if (DetailsUser != null) 
            {
                IUserService userService = new UserService();
                var listArtists = await userService.GetDetailsArtistsByUserId(DetailsUser.Id);

                foreach (var detailsArtist in listArtists)
                {
                    ArtistDto artistDto = detailsArtist;
                    artistDto = HelperHttpClient.GenerateAlbumsSongsForArtist(artistDto);
                    detailsArtist.AlbumsSongs = artistDto.AlbumsSongs;
                }
                switch (DetailsUser.RoleId)
                {
                    case 1:
                        {
                            DetailsUser.Role = "User";
                            DetailsUser.RoleDescription = "You have the option to browse all artists, their albums and songs and\n view their details. You cannot create, update, delete artists, albums, songs.";
                            break;
                        }
                    case 2:
                        {
                            DetailsUser.Role = "PremiumUser";
                            DetailsUser.RoleDescription = "You have the option to create, browse all the artists, their albums\n and songs and view their details. You can update and delete one artist, album, song.\n" +
                                " You can delete a given artist all albums, or a given album all songs. You cannot update, delete\n artists, albums, songs that you have not created. You can not create new\n " +
                                "names of artists, albums, songs, so that there are no duplicates in the list of a given user.";
                            break;
                        }
                    case 3:
                        {
                            DetailsUser.Role = "Admin";
                            DetailsUser.RoleDescription = "You have the option to create, view, update, delete all artists,\n their albums, their songs. You cannot create new names of artists, albums, songs,\n" +
                                " so that there are no duplicates in the list of a given user.";
                            break;
                        }
                }
                

                DetailsUser.DetailsArtists = listArtists;
                detailsUserWindow.FillDetailsArray();

                this.Visibility = Visibility.Hidden;
                detailsUserWindow.Show();
            }
            else
            {
                MessageBox.Show("You are not logged in, so you do not have access to this option !");
            }
            


        }

        private void Button_ShowArtists(object sender, RoutedEventArgs e)
        {
            ShowAllArtistsWindow showAllArtistsWindow = new ShowAllArtistsWindow();
            showAllArtistsWindow.GetAllArtistsAndSetDataGridArtistsAndSetDataGridArtistsResults();
            this.Visibility= Visibility.Hidden;
            showAllArtistsWindow.Show();
        }

        private void Button_ShowAlbums(object sender, RoutedEventArgs e)
        {
            ShowAllAlbumsWindow showAllAlbumsWindow = new ShowAllAlbumsWindow();
            showAllAlbumsWindow.ArtistId = -2;
            showAllAlbumsWindow.IsGetAllArtists = true;
            showAllAlbumsWindow.FillArrayAlbums();
            this.Visibility= Visibility.Hidden;

            showAllAlbumsWindow.SetButtons();
            showAllAlbumsWindow.Show();
        }

        private void Button_ShowSongs(object sender, RoutedEventArgs e)
        {
            ShowAllSongsWindow showAllSongsWindow = new ShowAllSongsWindow();
            showAllSongsWindow.ArtistId = -2;
            showAllSongsWindow.AlbumId = -2;
            showAllSongsWindow.IsGetAllSongs = true;
            showAllSongsWindow.FillArraySongs();
            this.Visibility = Visibility.Hidden;

            showAllSongsWindow.SetButtons();
            showAllSongsWindow.Show();
        }
    }
}
