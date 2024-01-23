using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.Models.BasicAlbum;
using FrontEndStoreMusicAPI.Models.BasicSong;
using FrontEndStoreMusicAPI.View.Album_Sub_Windows;
using FrontEndStoreMusicAPI.View.Artist_Sub_Windows;
using FrontEndStoreMusicAPI.View.Song_Sub_Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndStoreMusicAPI.Utilites
{
    static class Fill
    {
        public static void GetValuesToUpdateArtist(DetailsArtistDto updateArtist)
        {
            UpdateCreateArtist.c.ArtistUpdateCreateName.Text = updateArtist.Name;
            UpdateCreateArtist.c.ArtistUpdateCreateDescription.Text = updateArtist.Description;
            UpdateCreateArtist.c.ArtistUpdateCreateKindOfMusic.Text = updateArtist.KindOfMusic;
            UpdateCreateArtist.c.ArtistUpdateCreateConctactEmail.Text = updateArtist.ContactEmail;
            UpdateCreateArtist.c.ArtistUpdateCreateContactPhone.Text = updateArtist.ContactNumber;
            UpdateCreateArtist.c.ArtistUpdateCreateCountry.Text = updateArtist.Country;
            UpdateCreateArtist.c.ArtistUpdateCreateCity.Text = updateArtist.City;
        }

        // genereted new or updated artist 
        public static void FillValuesOfCreateUpdateArtist(BasicArtistDto newArtist)
        {
            newArtist.Name = UpdateCreateArtist.c.ArtistUpdateCreateName.Text;
            newArtist.Description = UpdateCreateArtist.c.ArtistUpdateCreateDescription.Text;
            newArtist.KindOfMusic = UpdateCreateArtist.c.ArtistUpdateCreateKindOfMusic.Text;
            newArtist.ContactEmail = UpdateCreateArtist.c.ArtistUpdateCreateConctactEmail.Text;
            newArtist.ContactNumber = UpdateCreateArtist.c.ArtistUpdateCreateContactPhone.Text;
            newArtist.Country = UpdateCreateArtist.c.ArtistUpdateCreateCountry.Text;
            newArtist.City = UpdateCreateArtist.c.ArtistUpdateCreateCity.Text;
        }

        // genereated new or updated album
        public static void FillValuesOfCreateUpdateAlbum(BasicAlbumDto newAlbum)
        {
            newAlbum.Title = UpdateCreateAlbum.c.AlbumUpdateCreateTitle.Text;
            newAlbum.Length = double.Parse(UpdateCreateAlbum.c.AlbumUpdateCreateLength.Text);
            newAlbum.Price = double.Parse(UpdateCreateAlbum.c.AlbumUpdateCreatePrice.Text);
        }

        public static void GetValuesToUpdateAlbum(AlbumDto updateAlbum)
        {
            UpdateCreateAlbum.c.AlbumUpdateCreateTitle.Text = updateAlbum.Title;
            UpdateCreateAlbum.c.AlbumUpdateCreateLength.Text = updateAlbum.Length.ToString();
            UpdateCreateAlbum.c.AlbumUpdateCreatePrice.Text = updateAlbum.Price.ToString();
        }

        // genereated new or updated song
        public static void FillValuesOfCreateUpdateSong(BasicSongDto newSong)
        {
            newSong.Name = UpdateCreateSong.c.SongUpdateCreateName.Text;
        }

        public static void GetValuesToUpdateSong(SongDto updateSong)
        {
            UpdateCreateSong.c.SongUpdateCreateName.Text = updateSong.Name;
        }

    }
}
