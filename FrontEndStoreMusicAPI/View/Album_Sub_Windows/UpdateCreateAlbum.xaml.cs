using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Services;
using FrontEndStoreMusicAPI.Utilites;
using FrontEndStoreMusicAPI.View.Artist_Sub_Windows;
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

namespace FrontEndStoreMusicAPI.View.Album_Sub_Windows
{
    /// <summary>
    /// Interaction logic for UpdateCreateAlbum.xaml
    /// </summary>
    public partial class UpdateCreateAlbum : Window
    {
        public static UpdateCreateAlbum c;
        public int albumId { get; set; }
        public int artistId { get; set; }
        public UpdateCreateAlbum()
        {
            InitializeComponent();
            c = this;
            albumId = 0;
        }

        private void Button_ReturnToAllAlbums(object sender, RoutedEventArgs e)
        {
            ShowAllAlbumsWindow showAllAlbumsWindow = new ShowAllAlbumsWindow();
            showAllAlbumsWindow.artistId = artistId;
            showAllAlbumsWindow.FillArrayAlbums();
            this.Visibility = Visibility.Hidden;
            showAllAlbumsWindow.Show();
        }

        private void Button_ClearValuesOfAlbum(object sender, RoutedEventArgs e)
        {
            Reset.ClearValuesOfUpdateCreateAlbum();
        }

        private void Button_SaveAlbum(object sender, RoutedEventArgs e)
        {
            if (albumId == 0) 
            {
                CreateAlbumDto createAlbumDto = new CreateAlbumDto();
                if (double.TryParse(AlbumUpdateCreateLength.Text, out double resultLength) && double.TryParse(AlbumUpdateCreatePrice.Text, out double resultPrice))
                {
                    Fill.FillValuesOfCreateUpdateAlbum(createAlbumDto);

                    IAlbumService albumService = new AlbumService();

                    if (albumService.Create(artistId, createAlbumDto))
                    {
                        Button_ReturnToAllAlbums(sender, e);
                    }

                }
                else
                {
                    MessageBox.Show("Invalid one of the values: Price or Length, should be a numbers!");
                    return;
                }

            } else
            {
                UpdateAlbumDto updateAlbumDto = new UpdateAlbumDto();
                if (double.TryParse(AlbumUpdateCreateLength.Text, out double resultLength) && double.TryParse(AlbumUpdateCreatePrice.Text, out double resultPrice))
                {
                    Fill.FillValuesOfCreateUpdateAlbum(updateAlbumDto);

                    IAlbumService albumService = new AlbumService();
                    if (albumService.Update(artistId, albumId, updateAlbumDto))
                    {
                        Button_ReturnToAllAlbums(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid one of the values: Price or Length, should be a numbers!");
                    return;
                }
            }
        }
    }
}
