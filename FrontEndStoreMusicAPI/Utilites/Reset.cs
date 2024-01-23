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
    public static class Reset
    {
        public static void ClearValuesOfUserRegisterWindow()
        {
            RegisterWindow.c.RegisterFirstName.Clear();
            RegisterWindow.c.RegisterLastName.Clear();
            RegisterWindow.c.RegisterEmail.Clear();
            RegisterWindow.c.RegisterPassword.Clear();
            RegisterWindow.c.RegisterConfirmPassword.Clear();
            RegisterWindow.c.RegisterNationality.Clear();
            RegisterWindow.c.RegisterDateOfBirth.Clear();
            RegisterWindow.c.RegisterRole.Clear();
        }

        public static void ClearValuesOfUpdateCreateArtist()
        {
            UpdateCreateArtist.c.ArtistUpdateCreateName.Clear();
            UpdateCreateArtist.c.ArtistUpdateCreateDescription.Clear();
            UpdateCreateArtist.c.ArtistUpdateCreateKindOfMusic.Clear();
            UpdateCreateArtist.c.ArtistUpdateCreateConctactEmail.Clear();
            UpdateCreateArtist.c.ArtistUpdateCreateContactPhone.Clear();
            UpdateCreateArtist.c.ArtistUpdateCreateCountry.Clear();
            UpdateCreateArtist.c.ArtistUpdateCreateCity.Clear();
        }

        public static void ClearValuesOfUpdateCreateAlbum()
        {
            UpdateCreateAlbum.c.AlbumUpdateCreateTitle.Clear();
            UpdateCreateAlbum.c.AlbumUpdateCreateLength.Clear();
            UpdateCreateAlbum.c.AlbumUpdateCreatePrice.Clear();
        }

        public static void ClearValuesOfUpdateCreateSong()
        {
            UpdateCreateSong.c.SongUpdateCreateName.Clear();
        }


    }
}
