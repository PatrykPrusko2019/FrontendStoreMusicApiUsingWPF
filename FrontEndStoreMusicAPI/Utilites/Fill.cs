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
            UpdateCreateArtist.c.ArtistUpdateCreateName.Text = updateArtist.Name;
            UpdateCreateArtist.c.ArtistUpdateCreateDescription.Text = updateArtist.Description;
            UpdateCreateArtist.c.ArtistUpdateCreateKindOfMusic.Text = updateArtist.KindOfMusic;
            UpdateCreateArtist.c.ArtistUpdateCreateConctactEmail.Text = updateArtist.ContactEmail;
            UpdateCreateArtist.c.ArtistUpdateCreateContactPhone.Text = updateArtist.ContactNumber;
            UpdateCreateArtist.c.ArtistUpdateCreateCountry.Text = updateArtist.Country;
            UpdateCreateArtist.c.ArtistUpdateCreateCity.Text = updateArtist.City;
        }

        // genereted new or updated artist 
        public static void FillValuesOfCreateUpdateArtist(BasicArtist newArtist)
        {
            newArtist.Name = UpdateCreateArtist.c.ArtistUpdateCreateName.Text;
            newArtist.Description = UpdateCreateArtist.c.ArtistUpdateCreateDescription.Text;
            newArtist.KindOfMusic = UpdateCreateArtist.c.ArtistUpdateCreateKindOfMusic.Text;
            newArtist.ContactEmail = UpdateCreateArtist.c.ArtistUpdateCreateConctactEmail.Text;
            newArtist.ContactNumber = UpdateCreateArtist.c.ArtistUpdateCreateContactPhone.Text;
            newArtist.Country = UpdateCreateArtist.c.ArtistUpdateCreateCountry.Text;
            newArtist.City = UpdateCreateArtist.c.ArtistUpdateCreateCity.Text;
        }
    }
}
