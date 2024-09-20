using Library.Application.Auth.Enums;
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
            var isAdmin = ( await _userService.GetGroups( user.Id ) ).Contains( AccessGroupEnum.Admin.ToString() );
            return Results.Ok( new UserResponce( user.Id.ToString(), user.UserName, user.Email, isAdmin ) );
        }
        [HttpGet( "{userId}/[action]" )]
        public async Task<IResult> GetUserWithBooks( [FromRoute] Guid userId, int skip, int take ) {
            var user = await _userService.Get( userId );
            var isAdmin = ( await _userService.GetGroups( user.Id ) ).Contains( AccessGroupEnum.Admin.ToString() );
            var books = await _userService.GetBooks(userId,skip, take ); 
            return Results.Ok( new UserWithBooksResponce( user.Id.ToString(), user.UserName, user.Email, isAdmin , books.ToArray()) );
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
            return Results.Ok();
        }
        [HttpPost( "[action]" )]
        public async Task<IResult> Login( LoginUserRequest request ) {
            var (token, refreshToken) = await _userService.Login( request.Email, request.Password );

            base.Response.Cookies.Append( "Auth-Cookies", token );
            base.Response.Cookies.Append( "Refresh", refreshToken );
            return Results.Ok( );
        }
        [HttpPost( "[action]" )]
        public IResult Logout( ) {
            base.Response.Cookies.Delete( "Auth-Cookies");
            base.Response.Cookies.Delete( "Refresh" );
            return Results.Ok( );
        }
        [HttpPost( "[action]" )]
        public async Task<IResult> LoginByRefresh( ) {
            var accessToken = base.Request.Cookies[ "Auth-Cookies" ]?.ToString();
            var refresh = base.Request.Cookies[ "Refresh" ]?.ToString();
            var (token, refreshToken) = await _userService.LoginByRefresh( accessToken, refresh );
            base.Response.Cookies.Append( "Auth-Cookies", token );
            base.Response.Cookies.Append( "Refresh", refreshToken );
            return Results.Ok();
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
