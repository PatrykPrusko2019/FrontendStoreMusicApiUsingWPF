using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndStoreMusicAPI.Models.BasicAlbum
{
    abstract class BasicAlbumDto
    {
        public string Title { get; set; }
        public double Length { get; set; }
        public double Price { get; set; }
    }
}
