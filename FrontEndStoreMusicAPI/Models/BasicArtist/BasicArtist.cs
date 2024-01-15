using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndStoreMusicAPI.Models
{
    abstract class BasicArtist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string KindOfMusic { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
