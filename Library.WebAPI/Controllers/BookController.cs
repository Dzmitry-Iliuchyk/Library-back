using AutoMapper;
using Library.Application.Interfaces.Services;
using Library.Domain.Interfaces;
using Library.Infrastracture;
using Library.WebAPI.Contracts.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers {

    [Route( "api/[controller]" )]
    [ApiController]
    public class BookController: ControllerBase {
        private readonly IBookService _bookService;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public BookController( IBookService bookService, IImageService imageService, IMapper mapper ) {
            this._bookService = bookService;
            this._imageService = imageService;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet( "[action]" )]
        public async Task<IResult> GetBooks( int skip, int take ) {
            var books = await _bookService.GetBooksAsync( skip, take );
            return Results.Ok( books );
        }
        [AllowAnonymous]
        [HttpPost( "[action]" )]
        public async Task<IResult> GetFilteredBooks( BooksRequest request ) {
            var (books, booksCount) = await _bookService.GetFilteredBooksAsync( request.skip, request.take, request.authorFilter, request.titleFilter );
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
        [Authorize( Policy = CustomPolicyNames.CanRead )]
        [HttpGet( "{ISBN}/[action]" )]
        public async Task<IResult> GetByISBN( [FromRoute] string ISBN ) {
            var books = await _bookService.GetBookAsync( ISBN );
            return Results.Ok( books );
        }
        [HttpGet( "{bookId}/[action]" )]
        public async Task<IResult> GetById( [FromRoute] Guid bookId ) {
            var book = await _bookService.GetBookWithAllAsync( bookId );
            var response = _mapper.Map<BookResponce>( book );
            return Results.Ok( response );
        }
        [HttpPost( "[action]" )]
        public async Task<IResult> Create( [FromForm] CreateBookRequest request ) {
            var id = await _bookService.CreateBookAsync( request.ISBN, request.Title, request.Genre, request.Description, request.AuthorId );
            if (request.image != null) {

                using (var stream = request.image.OpenReadStream()) {
                    await _imageService.SaveImage( stream, id );
                }
            }
            return Results.Ok();
        }
        [HttpPut( "[action]" )]
        public async Task<IResult> Update([FromForm] UpdateBookRequest request ) {
            await _bookService.UpdateBookAsync( request.BookId, request.ISBN, request.Title, request.Genre, request.Description, request.AuthorId );
            if (request.image != null) {

                using (var stream = request.image.OpenReadStream()) {
                    await _imageService.SaveImage( stream, request.BookId );
                }
            }
            return Results.Ok();
        }
        [Authorize]
        [HttpPost( "{bookId}/[action]" )]
        public async Task<IResult> FreeBook( [FromRoute] Guid bookId ) {
            var userId = Guid.Parse( HttpContext.User.Claims.First( u => u.Type == CustumClaimTypes.UserId ).Value );
            await _bookService.FreeBookAsync( bookId, userId );
            return Results.Ok();
        }
        [HttpPost( "[action]" )]
        public async Task<IResult> GiveBookToClientId( Guid bookId, Guid clientId, int hoursToUse ) {
            await _bookService.GiveBookToClientAsync( bookId, clientId, hoursToUse );
            return Results.Ok();
        }
        [Authorize]
        [HttpPost( "{bookId}/[action]" )]
        public async Task<IResult> TakeBook( [FromRoute] Guid bookId ) {
            var userId = Guid.Parse( HttpContext.User.Claims.First( x => x.Type == CustumClaimTypes.UserId ).Value );
            const int HOURS_TO_USE = 24;
            await this._bookService.GiveBookToClientAsync( bookId, userId, HOURS_TO_USE );
            return Results.Ok();
        }
        [HttpDelete( "{bookId}/[action]" )]
        public async Task<IResult> Delete( Guid bookId ) {
            await _bookService.DeleteBookAsync( bookId );
            return Results.Ok();
        }
        [HttpPost( "[action]" )]
        public async Task<IResult> AttachImage( Guid bookId, IFormFile img ) {
            using (var stream = img.OpenReadStream()) {
                await _imageService.SaveImage( stream, bookId );
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
