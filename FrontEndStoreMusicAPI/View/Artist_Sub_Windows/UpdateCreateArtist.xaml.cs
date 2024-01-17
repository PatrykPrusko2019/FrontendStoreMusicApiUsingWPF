using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Services;
using FrontEndStoreMusicAPI.Utilites;
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
    public partial class UpdateCreateArtist : Window
    {
        public static UpdateCreateArtist c;
        public int artistId { get; set; }
        public UpdateCreateArtist()
        {
            InitializeComponent();
            c = this;
            artistId = 0;
        }

        private void Button_ReturnToShowAllArtists(object sender, RoutedEventArgs e)
        {
            ShowAllArtistsWindow showAllArtistsWindow = new ShowAllArtistsWindow();
            this.Visibility = Visibility.Hidden;
            showAllArtistsWindow.Show();
        }

        private void Button_SaveArtist(object sender, RoutedEventArgs e)
        {
            if (artistId == 0) 
            {
                CreateArtistDto createArtistDto = new CreateArtistDto();
                Fill.FillValuesOfCreateUpdateArtist(createArtistDto);
                
                IArtistService artistService = new ArtistService();

                if (artistService.Create(createArtistDto))
                {
                    Button_ReturnToShowAllArtists(sender, e);
                }
            }
            else
            {
                UpdateArtistDto updateArtist = new UpdateArtistDto();
                Fill.FillValuesOfCreateUpdateArtist(updateArtist);
                updateArtist.Id = artistId;

                IArtistService artistService = new ArtistService();
                
                if (artistService.Update(updateArtist))
                {
                    Button_ReturnToShowAllArtists(sender, e);
                }
            }
            
        }

        private void Button_ClearArtist(object sender, RoutedEventArgs e)
        {
            Reset.ClearValuesOfUpdateCreateArtist();
        }
    }
}
