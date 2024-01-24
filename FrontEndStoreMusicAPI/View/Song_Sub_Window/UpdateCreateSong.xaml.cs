using FrontEndStoreMusicAPI.Models.Create;
using FrontEndStoreMusicAPI.Models.Update;
using FrontEndStoreMusicAPI.Services;
using FrontEndStoreMusicAPI.Utilites;
using System.Windows;

namespace FrontEndStoreMusicAPI.View.Song_Sub_Window
{
    /// <summary>
    /// Interaction logic for UpdateCreateSong.xaml
    /// </summary>
    public partial class UpdateCreateSong : Window
    {
        public static UpdateCreateSong c;
        public int SongId { get; set; }
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        public UpdateCreateSong()
        {
            InitializeComponent();
            SongId = 0;
            ArtistId = 0;
            SongId = 0;
            c = this;
        }

        private void Button_ReturnToAllSongs(object sender, RoutedEventArgs e)
        {
            ShowAllSongsWindow showAllSongsWindow = new ShowAllSongsWindow();
            showAllSongsWindow.ArtistId = ArtistId;
            showAllSongsWindow.AlbumId = AlbumId;
            showAllSongsWindow.FillArraySongs();
            this.Visibility = Visibility.Hidden;
            showAllSongsWindow.Show();
        }

        private void Button_SaveSong(object sender, RoutedEventArgs e)
        {
            if (SongId == 0)
            {
                CreateSongDto createSongDto = new CreateSongDto();
               
                    Fill.FillValuesOfCreateUpdateSong(createSongDto);

                    ISongService songService = new SongService();

                    if (songService.Create(ArtistId, AlbumId, createSongDto))
                    {
                        Button_ReturnToAllSongs(sender, e);
                    }

            }
            else
            {
                UpdateSongDto updateSongDto = new UpdateSongDto();
                
                    Fill.FillValuesOfCreateUpdateSong(updateSongDto);

                    ISongService songService = new SongService();
                if (songService.Update(ArtistId, AlbumId, SongId, updateSongDto))
                    {
                        Button_ReturnToAllSongs(sender, e);
                    }
               
            }
        }

        private void Button_ClearValuesOfSong(object sender, RoutedEventArgs e)
        {
            Reset.ClearValuesOfUpdateCreateSong();
        }
    }
}
