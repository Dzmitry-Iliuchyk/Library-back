using AutoMapper;
using Library.Application.Auth.Enums;
using Library.Application.Implementations;
using Library.Application.Interfaces.Services;
using Library.DataAccess.DataBase.Entities;
using Library.Domain.Interfaces;
using Library.Infrastracture;
using Library.WebAPI.Contracts.Book;
using Library.WebAPI.Contracts.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Library.WebAPI.Controllers {

    [Route( "api/[controller]" )]
    [ApiController]
    public class UserController: ControllerBase {
        private const string ACCESS_TOKEN_KEY = "Auth-Cookies";
        private const string REFRESH_TOKEN_KEY = "Refresh";
        private readonly IUserService _userService;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        public UserController( IUserService userService, IImageService imageService, IMapper mapper ) {
            _userService = userService;
            _imageService = imageService;
            _mapper = mapper;
        }
        [Authorize(Policy = CustomPolicyNames.Admin)]
        [HttpGet( "[action]" )]
        public async Task<IResult> GetUsers( int skip, int take ) {
            var users = await _userService.GetUsers( skip, take);
            return Results.Ok( users );
        }
        [Authorize]
        [HttpGet( "[action]" )]
        public async Task<IResult> Get( ) {
            var userId = Guid.Parse( HttpContext.User.Claims.First( x => x.Type == CustumClaimTypes.UserId ).Value );
            var user = await _userService.Get( userId );
            var isAdmin = ( await _userService.GetGroups( user.Id ) ).Contains( AccessGroupEnum.Admin.ToString() );
            return Results.Ok( new UserResponce( user.Id.ToString(), user.UserName, user.Email, isAdmin ) );
        }
        [Authorize(Policy = CustomPolicyNames.Admin)]
        [HttpGet( "{userId}/[action]" )]
        public async Task<IResult> GetUserWithBooks( [FromRoute] Guid userId, int skip, int take ) {
            var user = await _userService.Get( userId );
            var isAdmin = ( await _userService.GetGroups( user.Id ) ).Contains( AccessGroupEnum.Admin.ToString() );
            var books = await _userService.GetBooks(userId,skip, take ); 
            return Results.Ok( new UserWithBooksResponce( user.Id.ToString(), user.UserName, user.Email, isAdmin , books.ToArray()) );
        }
        [Authorize]
        [HttpGet( "[action]" )]
        public async Task<IResult> GetBooks(  int skip, int take ) {
            var userId = Guid.Parse( HttpContext.User.Claims.First( x => x.Type == CustumClaimTypes.UserId ).Value );
            var books = await _userService.GetBooks( userId, skip, take );
            return Results.Ok( books );
        }
        [Authorize]
        [HttpPost( "[action]" )]
        public async Task<IResult> GetFilteredBooks( BooksRequest request ) {
            var userId = Guid.Parse( HttpContext.User.Claims.First( x => x.Type == CustumClaimTypes.UserId ).Value );
            var (books, booksCount) = await _userService.GetFilteredBooksAsync( request.skip, request.take, request.authorFilter, request.titleFilter, userId );
            IList<BookResponce> booksResponces = _mapper.Map<IList<BookResponce>>( books );
            foreach (var item in booksResponces) {
                item.Image = await _imageService.GetImageAsBase64( item.Id );
            }
            BooksResponseWithCount response = new BooksResponseWithCount() {
                Books = booksResponces,
                Count = booksCount
            };
            return Results.Ok( response );
        }
        [AllowAnonymous]
        [HttpPost( "[action]" )]
        public async Task<IResult> Register( RegisterUserRequest request ) {
            var (token, refreshToken) = await _userService.Register( request.UserName, request.Email, request.Password );
     
            base.Response.Cookies.Append( ACCESS_TOKEN_KEY, token );
            base.Response.Cookies.Append( REFRESH_TOKEN_KEY, refreshToken );
            return Results.Ok();
        }
        [AllowAnonymous]
        [HttpPost( "[action]" )]
        public async Task<IResult> Login( LoginUserRequest request ) {
            var (token, refreshToken) = await _userService.Login( request.Email, request.Password );

            base.Response.Cookies.Append( ACCESS_TOKEN_KEY, token );
            base.Response.Cookies.Append( REFRESH_TOKEN_KEY, refreshToken );
            return Results.Ok( );
        }
        [HttpPost( "[action]" )]
        public IResult Logout( ) {
            base.Response.Cookies.Delete( ACCESS_TOKEN_KEY );
            base.Response.Cookies.Delete( REFRESH_TOKEN_KEY );
            return Results.Ok( );
        }
        [AllowAnonymous]
        [HttpPost( "[action]" )]
        public async Task<IResult> LoginByRefresh( ) {
            var accessToken = base.Request.Cookies[ ACCESS_TOKEN_KEY ]?.ToString();
            var refresh = base.Request.Cookies[ REFRESH_TOKEN_KEY ]?.ToString();
            var (token, refreshToken) = await _userService.LoginByRefresh( accessToken, refresh );
            base.Response.Cookies.Append( ACCESS_TOKEN_KEY, token );
            base.Response.Cookies.Append( REFRESH_TOKEN_KEY, refreshToken );
            return Results.Ok();
        }
        [Authorize(Policy = CustomPolicyNames.Admin)]
        [HttpPut( "[action]" )]
        public async Task<IResult> Update( UpdateUserRequest request ) {
            await _userService.Update( request.UserId, request.UserName, request.Email, request.Password );
            return Results.Ok();
        }
        [Authorize(Policy = CustomPolicyNames.Admin)]
        [HttpDelete( "{userId}/[action]" )]
        public async Task<IResult> Delete( Guid userId) {
            await _userService.Delete( userId );
            return Results.Ok();
        }

    }
}
