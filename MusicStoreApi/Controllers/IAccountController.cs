using Microsoft.AspNetCore.Mvc;
using MusicStoreApi.Models;

namespace MusicStoreApi.Controllers
{
    public interface IAccountController
    {
        ActionResult Login([FromBody] LoginDto loginDto);
        ActionResult RegisterUser([FromBody] RegisterUserDto registerUserDto);
    }
}