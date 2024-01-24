using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FrontEndStoreMusicAPI.Models.Details;

namespace FrontEndStoreMusicAPI.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public int RoleId { get; set; }
        public string TokenJWT { get; set; }
        public List<DetailsArtistDto> DetailsArtists { get; set; }
        public string Role { get; set; }
        public string RoleDescription { get; set; }

    }
}
