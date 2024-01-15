using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndStoreMusicAPI.Models
{
    class AlbumDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Length { get; set; }
        public int NumberOfSongs { get; set; }
        public double Price { get; set; }
        public List<SongDto> Songs { get; set; }
    }
}
