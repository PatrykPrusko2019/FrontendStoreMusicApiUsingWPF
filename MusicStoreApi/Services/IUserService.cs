using MusicStoreApi.Models;

namespace MusicStoreApi.Services
{
    public interface IUserService
    {
        UserDto GetUserByEmail(string email);
    }
}