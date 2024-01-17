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
        public UpdateCreateAlbum()
        {
            InitializeComponent();
            c = this;
            albumId = 0;
        }

        private void Button_ReturnToAllAlbums(object sender, RoutedEventArgs e)
        {
            ShowAllAlbumsWindow showAllAlbumsWindow = new ShowAllAlbumsWindow();
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
                //to do !!! next
            }
        }
    }
}
