using Library.DataAccess.DataBase.Entities;
using Library.Domain.Interfaces;
using Library.WebAPI.Contracts.User;
using Microsoft.AspNetCore.Mvc;


namespace Library.WebAPI.Controllers {
    [Route( "api/[controller]" )]
    [ApiController]
    public class UserController: ControllerBase {
        private readonly IUserService _userService;
        public UserController( IUserService userService ) {
            _userService = userService;
        }

        [HttpGet( "[action]" )]
        public async Task<IResult> GetUsers( int skip, int take ) {
            var users = await _userService.GetUsers( skip, take);
            return Results.Ok( users );
        }
        [HttpGet( "{userId}/[action]" )]
        public async Task<IResult> Get( [FromRoute] Guid userId ) {
            var user = await _userService.Get( userId );
            return Results.Ok( user );
        }
        [HttpGet( "{userId}/getBooks" )]
        public async Task<IResult> GetBooks([FromRoute] Guid userId, int skip, int take ) {
            var books = await _userService.GetBooks( userId, skip, take );
            return Results.Ok( books );
        }
        [HttpPost( "[action]" )]
        public async Task<IResult> Register( RegisterUserRequest request ) {
            var (token, refreshToken) = await _userService.Register( request.UserName, request.Email, request.Password );
            base.Response.Cookies.Append( "Auth-Cookies", token );
            base.Response.Cookies.Append( "Refresh", refreshToken );
            return Results.Ok(new AuthResponce (token, refreshToken) );
        }
        [HttpPost( "[action]" )]
        public async Task<IResult> Login( LoginUserRequest request ) {
            var (token, refreshToken) = await _userService.Login( request.Email, request.Password );
            base.Response.Cookies.Append( "Auth-Cookies", token );
            base.Response.Cookies.Append( "Refresh", refreshToken );
            return Results.Ok( new AuthResponce( token, refreshToken ) );
        }
        [HttpPost( "[action]" )]
        public async Task<IResult> LoginByRefresh( LoginRefreshRequest request ) {
            var (token, refreshToken) = await _userService.LoginByRefresh( request.accessToken, request.refreshToken);
            base.Response.Cookies.Append( "Auth-Cookies", token );
            base.Response.Cookies.Append( "Refresh", refreshToken );
            return Results.Ok( new AuthResponce( token, refreshToken ) );
        }
        [HttpPut( "[action]" )]
        public async Task<IResult> Update( UpdateUserRequest request ) {
            await _userService.Update( request.UserId, request.UserName, request.Email, request.Password );
            return Results.Ok();
        }
        [HttpDelete( "{userId}/[action]" )]
        public async Task<IResult> Delete( Guid userId) {
            await _userService.Delete( userId );
            return Results.Ok();
        }

    }
}
