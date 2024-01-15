using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FrontEndStoreMusicAPI.View.Artist_Sub_Windows
{
    /// <summary>
    /// Interaction logic for UpdateArtist.xaml
    /// </summary>
    public partial class UpdateArtist : Window
    {
        public int ArtistId { get; set; }
        public UpdateArtist()
        {
            InitializeComponent();
        }

        private void Button_ReturnToShowAllArtists(object sender, RoutedEventArgs e)
        {
            ShowAllArtistsWindow showAllArtistsWindow = new ShowAllArtistsWindow();
            this.Visibility = Visibility.Hidden;
            showAllArtistsWindow.Show();
        }

        private void Button_UpdateArtist(object sender, RoutedEventArgs e)
        {
            UpdateArtistDto updateArtist = new UpdateArtistDto()
            {
                Id = ArtistId,
                Name = ArtistUpdateName.Text,
                Description = ArtistUpdateDescription.Text,
                KindOfMusic = ArtistUpdateKindOfMusic.Text,
                ContactEmail = ArtistUpdateConctactEmail.Text,
                ContactNumber = ArtistUpdateContactPhone.Text,
                Country = ArtistUpdateCountry.Text,
                City = ArtistUpdateCity.Text
            };

            IArtistService artistService = new ArtistService();
            artistService.Update(updateArtist);
        }

        private void Button_ClearArtistUpdate(object sender, RoutedEventArgs e)
        {

        }
    }
}
