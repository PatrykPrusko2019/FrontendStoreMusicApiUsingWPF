using FrontEndStoreMusicAPI.View.Artist_Sub_Windows;
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

        public static void ClearValuesOfUpdateArtist()
        {
            UpdateArtist.c.ArtistUpdateName.Clear();
            UpdateArtist.c.ArtistUpdateDescription.Clear();
            UpdateArtist.c.ArtistUpdateKindOfMusic.Clear();
            UpdateArtist.c.ArtistUpdateConctactEmail.Clear();
            UpdateArtist.c.ArtistUpdateContactPhone.Clear();
            UpdateArtist.c.ArtistUpdateCountry.Clear();
            UpdateArtist.c.ArtistUpdateCity.Clear();
        }


    }
}
