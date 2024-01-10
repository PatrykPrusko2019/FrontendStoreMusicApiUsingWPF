using AutoMapper;
using MusicStoreApi.Entities;
using MusicStoreApi.Models;

namespace MusicStoreApi.Services
{
    public class UserService : IUserService
    {
        private readonly ArtistDbContext dbContext;
        private readonly IMapper mapper;

        public UserService(ArtistDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public UserDto GetUserByEmail(string email)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Email == email);
            var userDto = mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}
