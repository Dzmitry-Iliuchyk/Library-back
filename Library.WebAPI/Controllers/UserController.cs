using Library.Domain.Interfaces;
using Library.WebAPI.Contracts.User;
using Microsoft.AspNetCore.Mvc;


namespace Library.WebAPI.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class UserController: ControllerBase {
        private readonly IUserService _userService;
        public UserController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet( "[action]" )]
        public async Task<IResult> GetAllUsers() {
            var users = await _userService.GetAllUsers();
            return Results.Ok(users);
        }
        [HttpGet( "[action]/{userId}" )]
        public async Task<IResult> Get(Guid userId) {
            var user = await _userService.GetById(userId);
            return Results.Ok(user);
        }
        [HttpGet( "{userId}/getBooks" )]
        public async Task<IResult> GetBooks([FromRoute] Guid userId) {
            var books = await _userService.GetBooks(userId);
            return Results.Ok(books);
        }
        [HttpGet( "[action]" )]
        public async Task<IResult> Register(RegisterUserRequest request) {
            await _userService.CreateUser( request.UserName, request.Email, request.Password );
            return Results.Ok();
        }
    }
}
