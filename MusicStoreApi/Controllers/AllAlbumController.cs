using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStoreApi.Models;
using MusicStoreApi.Services;

namespace MusicStoreApi.Controllers
{
    [Route("api/album")]
    [ApiController]
    [Authorize]
    public class AllAlbumController : ControllerBase
    {
        private readonly IAllAlbumService allAlbumService;

        public AllAlbumController(IAllAlbumService allAlbumService)
        {
            this.allAlbumService = allAlbumService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<AlbumDto>> GetAll([FromQuery] AlbumQuery albumQuery)
        {
            var albums = allAlbumService.GetAll(albumQuery);

            return Ok(albums);
        }
    }
}
