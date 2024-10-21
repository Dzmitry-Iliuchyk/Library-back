using AutoMapper;
using Library.Application.Implementations.BookUseCases;
using Library.Application.Interfaces.Services;
using Library.Domain.Interfaces;
using Library.Domain.Interfaces.BookUseCases;
using Library.Domain.Interfaces.BookUseCases.Dto;
using Library.Infrastracture;
using Library.WebAPI.Contracts.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers {

    [Route( "api/[controller]" )]
    [ApiController]
    public class BookController: ControllerBase {
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private readonly ICreateBookUseCase _createBookUseCase;
        private readonly IUpdateBookUseCase _updateBookUseCase;
        private readonly IDeleteBookUseCase _deleteBookUseCase;
        private readonly IFreeBookUseCase _freeBookUseCase;
        private readonly IGetBooksUseCase _getBooksUseCase;
        private readonly IGetFilteredBooksUseCase _getFilteredBooksUseCase;
        private readonly IGiveBookToClientUseCase _giveBookToClientUseCase;
        private readonly IGetBookWithAuthorByIdUseCase _getBookWithAuthorUseCase;
        private readonly IGetBookWithAllUseCase  _getBookWithAllUseCase;
        private readonly IGetBookByISBNUseCase _getBookByISBNUseCase;
        public BookController( 
            IImageService imageService,
            IMapper mapper,
            ICreateBookUseCase createBookUseCase,
            IUpdateBookUseCase updateBookUseCase,
            IDeleteBookUseCase deleteBookUseCase,
            IFreeBookUseCase freeBookUseCase,
            IGiveBookToClientUseCase giveBookToClientUseCase,
            IGetBookWithAuthorByIdUseCase getBookWithAuthorUseCase,
            IGetBookWithAllUseCase getBookWithAllUseCase,
            IGetBooksUseCase getBooksUseCase,
            IGetFilteredBooksUseCase getFilteredBooksUseCase,
            IGetBookByISBNUseCase getBookByISBNUseCase ) {
            _createBookUseCase = createBookUseCase;
            _updateBookUseCase = updateBookUseCase;
            _deleteBookUseCase = deleteBookUseCase;
            _freeBookUseCase = freeBookUseCase;
            _giveBookToClientUseCase = giveBookToClientUseCase;
            _getBookWithAuthorUseCase = getBookWithAuthorUseCase;
            _getBookWithAllUseCase = getBookWithAllUseCase;
            _getBookByISBNUseCase = getBookByISBNUseCase;
            _getBooksUseCase = getBooksUseCase;
            _getFilteredBooksUseCase = getFilteredBooksUseCase;
            this._imageService = imageService;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet( "[action]" )]
        public async Task<IResult> GetBooks( int skip, int take ) {
            var books = await _getBooksUseCase.Execute( skip, take );
            return Results.Ok( books );
        }
        [AllowAnonymous]
        [HttpPost( "[action]" )]
        public async Task<IResult> GetFilteredBooks( BooksRequest request ) {
            var result = await _getFilteredBooksUseCase.Execute( request.skip, request.take, request.authorFilter, request.titleFilter );
            
            return Results.Ok( result );
        }
        [Authorize( Policy = CustomPolicyNames.CanRead )]
        [HttpGet( "{ISBN}/[action]" )]
        public async Task<IResult> GetByISBN( [FromRoute] string ISBN ) {
            var books = await _getBookByISBNUseCase.Execute( ISBN );
            return Results.Ok( books );
        }
        [HttpGet( "{bookId}/[action]" )]
        public async Task<IResult> GetById( [FromRoute] Guid bookId ) {
            var book = await _getBookWithAuthorUseCase.Execute( bookId );
            return Results.Ok( book );
        }
        [HttpPost( "[action]" )]
        public async Task<IResult> Create( [FromForm] CreateBookRequest request ) {
            var id = await _createBookUseCase.Execute( _mapper.Map<BookCreateDto>(request));
            
            return Results.Ok();
        }
        [HttpPut( "[action]" )]
        public async Task<IResult> Update( [FromForm] UpdateBookRequest request ) {
            await _updateBookUseCase.Execute( _mapper.Map<BookUpdateDto>( request ) );
            return Results.Ok();
        }
        [Authorize]
        [HttpPost( "{bookId}/[action]" )]
        public async Task<IResult> FreeBook( [FromRoute] Guid bookId ) {
            var userId = Guid.Parse( HttpContext.User.Claims.First( u => u.Type == CustumClaimTypes.UserId ).Value );
            await _freeBookUseCase.Execute( bookId, userId );
            return Results.Ok();
        }
        [HttpPost( "[action]" )]
        public async Task<IResult> GiveBookToClientId( Guid bookId, Guid clientId, int hoursToUse ) {
            await _giveBookToClientUseCase.Execute( bookId, clientId, hoursToUse );
            return Results.Ok();
        }
        [Authorize]
        [HttpPost( "{bookId}/[action]" )]
        public async Task<IResult> TakeBook( [FromRoute] Guid bookId ) {
            var userId = Guid.Parse( HttpContext.User.Claims.First( x => x.Type == CustumClaimTypes.UserId ).Value );
            const int HOURS_TO_USE = 24;
            await _giveBookToClientUseCase.Execute( bookId, userId, HOURS_TO_USE );
            return Results.Ok();
        }
        [HttpDelete( "{bookId}/[action]" )]
        public async Task<IResult> Delete( Guid bookId ) {
            await _deleteBookUseCase.Execute( bookId );
            return Results.Ok();
        }
        [HttpPost( "[action]" )]
        public async Task<IResult> AttachImage( Guid bookId, IFormFile img ) {
            using (var stream = img.OpenReadStream()) {
                
                await _imageService.SaveImage( stream, bookId, img.ContentType.Split( "/" )?[1] );
            }
            return Results.Ok();
        }

        [HttpGet( "[action]" )]
        public async Task<IResult> GetImage( Guid bookId ) {

            var image = await _imageService.GetImage( bookId );

            return Results.Ok( image );
        }
        [HttpGet( "[action]" )]
        public async Task<IResult> ThrowException( Guid bookId ) {

            throw new Exception( "sfdsfssfsfd" );
        }
    }
}
