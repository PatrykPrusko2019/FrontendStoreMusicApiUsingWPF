using FrontEndStoreMusicAPI.Models;
using FrontEndStoreMusicAPI.View.Artist_Sub_Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndStoreMusicAPI.Utilites
{
    static class Fill
    {
        public static void FillValuesOfUpdateArtist(DetailsArtistDto updateArtist)
        {
            UpdateArtist.c.ArtistUpdateName.Text = updateArtist.Name;
            UpdateArtist.c.ArtistUpdateDescription.Text = updateArtist.Description;
            UpdateArtist.c.ArtistUpdateKindOfMusic.Text = updateArtist.KindOfMusic;
            UpdateArtist.c.ArtistUpdateConctactEmail.Text = updateArtist.ContactEmail;
            UpdateArtist.c.ArtistUpdateContactPhone.Text = updateArtist.ContactNumber;
            UpdateArtist.c.ArtistUpdateCountry.Text = updateArtist.Country;
            UpdateArtist.c.ArtistUpdateCity.Text = updateArtist.City;
        }
    }
}
