using MusicStoreApi.Models;

namespace MusicStoreApi.Services
{
    public interface IUserService
    {
        UserDto GetUserByEmail(string email);
        List<ArtistDto> GetArtistsByUserId(int id);
        UserDto GetUserById(int id);
    }
}