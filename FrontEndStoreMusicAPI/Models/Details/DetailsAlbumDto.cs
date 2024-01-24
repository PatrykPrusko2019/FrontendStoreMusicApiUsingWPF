using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndStoreMusicAPI.Models.Details
{
    public class DetailsAlbumDto : AlbumDto
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
    }
}
