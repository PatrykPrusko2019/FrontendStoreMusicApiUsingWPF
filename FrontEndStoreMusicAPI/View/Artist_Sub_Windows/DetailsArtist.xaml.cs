using FrontEndStoreMusicAPI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for DetailsArtist.xaml
    /// </summary>
    public partial class DetailsArtist : Window
    {
        public int ArtistId { get; set; }
        ObservableCollection<ArtistDto> detailsArtist;
        public DetailsArtist()
        {
            InitializeComponent();
            detailsArtist = new ObservableCollection<ArtistDto>();
        }

        public void FillDetailsArray(DetailsArtistDto selectedArtist)
        {
            detailsArtist.Add(selectedArtist);
            DataGridDetailsArtist.DataContext = detailsArtist;
        }

        public void FillArray(ArtistDto selectedArtist)
        {
            detailsArtist.Add(selectedArtist);
            DataGridDetailsArtist.DataContext = detailsArtist;
        }

        private void Button_ReturnToAllArtists(object sender, RoutedEventArgs e)
        {
            ShowAllArtistsWindow showAllArtistsWindow = new ShowAllArtistsWindow();
            this.Visibility = Visibility.Hidden;
            showAllArtistsWindow.Show();
        }
    }
}
