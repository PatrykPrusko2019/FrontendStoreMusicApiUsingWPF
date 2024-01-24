using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndStoreMusicAPI.Models.Details
{
    public class DetailsSongDto : SongDto
    {
        public string AlbumTitle { get; set; }
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
    }
}
