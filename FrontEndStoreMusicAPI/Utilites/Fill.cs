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
            UpdateCreateArtist.c.ArtistUpdateName.Text = updateArtist.Name;
            UpdateCreateArtist.c.ArtistUpdateDescription.Text = updateArtist.Description;
            UpdateCreateArtist.c.ArtistUpdateKindOfMusic.Text = updateArtist.KindOfMusic;
            UpdateCreateArtist.c.ArtistUpdateConctactEmail.Text = updateArtist.ContactEmail;
            UpdateCreateArtist.c.ArtistUpdateContactPhone.Text = updateArtist.ContactNumber;
            UpdateCreateArtist.c.ArtistUpdateCountry.Text = updateArtist.Country;
            UpdateCreateArtist.c.ArtistUpdateCity.Text = updateArtist.City;
        }
    }
}
