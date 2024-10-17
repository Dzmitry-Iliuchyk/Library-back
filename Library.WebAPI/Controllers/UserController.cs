using AutoMapper;
using Library.Application.Auth.Enums;
using Library.Application.Interfaces.Services;
using Library.Domain.Interfaces;
using Library.Domain.Interfaces.UserUseCases;
using Library.Domain.Interfaces.UserUseCases.DTO;
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
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private readonly IUpdateUserUseCase _updateUserUseCase;
        private readonly IGetUserGroupsUseCase _getUserGroupsUseCase;
        private readonly IGetUserByIdUseCase _getUserByIdUseCase;
        private readonly IGetUserBooksUseCase _getUserBooksUseCase;
        private readonly IGetUsersUseCase _getUsersUseCase;
        private readonly IGetFilteredUserBooksUseCase _getFilteredserBooksUseCase;
        private readonly IDeleteUserUseCase _deleteUserUseCase;
        private readonly ILoginByRefreshUseCase _loginByRefreshUseCase;
        private readonly ILoginUserUseCase _loginUserUseCase;
        private readonly IRegisterUserUseCase _registerUserUseCase;
        private readonly IGetUserWithBooksByIdUseCase _getUserWithBooksByIdUseCase;

        public UserController(
            IImageService imageService,
            IMapper mapper,
            IUpdateUserUseCase updateUserUseCase,
            IGetUserGroupsUseCase getUserGroupsUseCase,
            IGetUserByIdUseCase getUserByIdUseCase,
            IGetUserBooksUseCase getUserBooksUseCase,
            IGetUsersUseCase getUsersUseCase,
            IGetFilteredUserBooksUseCase getFilteredserBooksUseCase,
            IDeleteUserUseCase deleteUserUseCase,
            ILoginByRefreshUseCase loginByRefreshUseCase,
            ILoginUserUseCase loginUserUseCase,
            IRegisterUserUseCase registerUserUseCase,
            IGetUserWithBooksByIdUseCase getUserWithBooksByIdUseCase) {
            _getUserWithBooksByIdUseCase = getUserWithBooksByIdUseCase;
            _deleteUserUseCase = deleteUserUseCase;
            _getFilteredserBooksUseCase = getFilteredserBooksUseCase;
            _getUserBooksUseCase = getUserBooksUseCase;
            _getUserByIdUseCase = getUserByIdUseCase;
            _getUserGroupsUseCase = getUserGroupsUseCase;
            _getUsersUseCase = getUsersUseCase;
            _loginByRefreshUseCase = loginByRefreshUseCase;
            _loginUserUseCase = loginUserUseCase;
            _registerUserUseCase = registerUserUseCase;
            _updateUserUseCase = updateUserUseCase;
        
            _imageService = imageService;
            _mapper = mapper;
        }
        [Authorize( Policy = CustomPolicyNames.Admin )]
        [HttpGet( "[action]" )]
        public async Task<IResult> GetUsers( int skip, int take ) {
            var users = await _getUsersUseCase.Execute( skip, take );
            return Results.Ok( users );
        }
        [Authorize]
        [HttpGet( "[action]" )]
        public async Task<IResult> Get() {
            var userId = Guid.Parse( HttpContext.User.Claims.First( x => x.Type == CustumClaimTypes.UserId ).Value );
            var user = await _getUserByIdUseCase.Execute(userId);
            return Results.Ok( user );
        }
        [Authorize( Policy = CustomPolicyNames.Admin )]
        [HttpGet( "{userId}/[action]" )]
        public async Task<IResult> GetUserWithBooks( [FromRoute] Guid userId, int skip, int take ) {
            var user = await _getUserWithBooksByIdUseCase.Execute(userId, skip, take);
            return Results.Ok( user );
        }
        [Authorize]
        [HttpGet( "[action]" )]
        public async Task<IResult> GetBooks( int skip, int take ) {
            var userId = Guid.Parse( HttpContext.User.Claims.First( x => x.Type == CustumClaimTypes.UserId ).Value );
            var books = await _getUserBooksUseCase.Execute(userId, skip, take);
            return Results.Ok( books );
        }
        [Authorize]
        [HttpPost( "[action]" )]
        public async Task<IResult> GetFilteredBooks( BooksRequest request ) {
            var userId = Guid.Parse( HttpContext.User.Claims.First( x => x.Type == CustumClaimTypes.UserId ).Value );
            var books = await _getFilteredserBooksUseCase.Execute(request.skip, request.take, request.authorFilter, request.titleFilter, userId);
            return Results.Ok( books );
        }
        [AllowAnonymous]
        [HttpPost( "[action]" )]
        public async Task<IResult> Register( RegisterUserRequest request ) {
            var result = await _registerUserUseCase.Execute( _mapper.Map<RegisterModel>(request) );

            base.Response.Cookies.Append( ACCESS_TOKEN_KEY, result.accessToken );
            base.Response.Cookies.Append( REFRESH_TOKEN_KEY, result.refreshToken );
            return Results.Ok();
        }
        [AllowAnonymous]
        [HttpPost( "[action]" )]
        public async Task<IResult> Login( LoginUserRequest request ) {
            var result = await _loginUserUseCase.Execute( _mapper.Map<LoginModel>(request) );
            
            base.Response.Cookies.Append( ACCESS_TOKEN_KEY, result.accessToken );
            base.Response.Cookies.Append( REFRESH_TOKEN_KEY, result.refreshToken );
            return Results.Ok();
        }
        [HttpPost( "[action]" )]
        public IResult Logout() {
            base.Response.Cookies.Delete( ACCESS_TOKEN_KEY );
            base.Response.Cookies.Delete( REFRESH_TOKEN_KEY );
            return Results.Ok();
        }
        [AllowAnonymous]
        [HttpPost( "[action]" )]
        public async Task<IResult> LoginByRefresh() {
            var accessToken = base.Request.Cookies[ ACCESS_TOKEN_KEY ]?.ToString();
            var refresh = base.Request.Cookies[ REFRESH_TOKEN_KEY ]?.ToString();

            var result = await _loginByRefreshUseCase.Execute( accessToken, refresh );

            base.Response.Cookies.Append( ACCESS_TOKEN_KEY, result.accessToken );
            base.Response.Cookies.Append( REFRESH_TOKEN_KEY, result.refreshToken );
            return Results.Ok();
        }
        [Authorize( Policy = CustomPolicyNames.Admin )]
        [HttpPut( "[action]" )]
        public async Task<IResult> Update( UpdateUserRequest request ) {
            await _updateUserUseCase.Execute( _mapper.Map<UserDto>(request) );
            return Results.Ok();
        }
        [Authorize( Policy = CustomPolicyNames.Admin )]
        [HttpDelete( "{userId}/[action]" )]
        public async Task<IResult> Delete( Guid userId ) {
            await _deleteUserUseCase.Execute( userId );
            return Results.Ok();
        }

    }
}
