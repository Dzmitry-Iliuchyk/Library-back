using Library.Domain.Interfaces;
using Library.Infrastracture;
using Library.WebAPI.Contracts.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers {
    [Authorize( Policy = CustomPolicyNames.Admin )]
    [Route( "api/[controller]" )]
    [ApiController]
    public class BookController: ControllerBase {
        private readonly IBookService _bookService;
        private readonly IImageService _imageService;

        public BookController( IBookService bookService, IImageService imageService ) {
            this._bookService = bookService;
            this._imageService = imageService;
        }
        [AllowAnonymous]
        [HttpGet( "[action]" )]
        public async Task<IResult> GetBooks( int skip, int take ) {
            var books = await _bookService.GetBooksAsync( skip, take );
            return Results.Ok( books );
        }
        [Authorize( Policy = CustomPolicyNames.CanRead )]
        [HttpGet( "{ISBN}/[action]" )]
        public async Task<IResult> GetByISBN( [FromRoute] string ISBN ) {
            var books = await _bookService.GetBookAsync( ISBN );
            return Results.Ok( books );
        }
        [Authorize( Policy = CustomPolicyNames.CanRead )]
        [HttpGet( "{bookId}/[action]" )]
        public async Task<IResult> GetById( [FromRoute] Guid bookId ) {
            var books = await _bookService.GetBookAsync( bookId );
            return Results.Ok( books );
        }
        [HttpPost( "[action]" )]
        public async Task<IResult> Create( CreateBookRequest request ) {
            await _bookService.CreateBookAsync( request.ISBN, request.Title, request.Genre, request.Description, request.AuthorId );
            return Results.Ok();
        }
        [HttpPut( "[action]" )]
        public async Task<IResult> Update( UpdateBookRequest request ) {
            await _bookService.UpdateBookAsync( request.BookId, request.ISBN, request.Title, request.Genre, request.Description, request.AuthorId );
            return Results.Ok();
        }
        [Authorize]
        [HttpPost( "[action]" )]
        public async Task<IResult> Free( Guid bookId) {
            var userId = Guid.Parse(HttpContext.User.Claims.First(u=>u.Type == CustumClaimTypes.UserId).Value);
            await _bookService.FreeBookAsync( bookId, userId );
            return Results.Ok();
        }
        [HttpPost( "[action]" )]
        public async Task<IResult> GiveBook( Guid bookId, Guid clientId, int hoursToUse ) {
            await _bookService.GiveBookToClientAsync( bookId, clientId, hoursToUse );
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

            throw new Exception("sfdsfssfsfd");
        }
    }
}
