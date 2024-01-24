using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndStoreMusicAPI.Models.Details
{
    public class DetailsArtistDto : ArtistDto
    {
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
    }
}
