using MusicStoreApi.Models;

namespace MusicStoreApi.Services
{
    public interface IAllAlbumService
    {
        List<AlbumDto> GetAll(AlbumQuery searchQuery);
    }
}
