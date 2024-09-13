using Library.Domain.Interfaces;
using Library.WebAPI.Contracts.Authors;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers {
    [Route( "api/[controller]" )]
    [ApiController]
    public class AuthorController: ControllerBase {
        private readonly IAuthorService _authorService;
        public AuthorController( IAuthorService authorService ) {
            _authorService = authorService;

        }
        [HttpGet( "[action]" )]
        public async Task<IResult> GetAllAuthors() {
            var authors = await _authorService.GetAllAuthors();
            return Results.Ok( authors );
        }
        [HttpGet( "{authorId}/getBooks" )]
        public async Task<IResult> GetAllBooksByAuthor( [FromRoute] Guid authorId ) {
            var books = await _authorService.GetAllBooks( authorId );
            return Results.Ok( books );
        }
        [HttpGet( "{authorId}/delete" )]
        public async Task<IResult> DeleteAuthor( [FromRoute] Guid authorId ) {
            await _authorService.DeleteAuthor( authorId );
            return Results.Ok();
        }
        [HttpGet( "{authorId}/get" )]
        public async Task<IResult> Get( [FromRoute] Guid authorId ) {
            await _authorService.GetAuthor( authorId );
            return Results.Ok();
        }
        [HttpGet( "[action]" )]
        public async Task<IResult> UpdateAuthor( UpdateAuthorRequest request ) {
            await _authorService.UpdateAuthor( request.Id, request.FirstName, request.LastName, request.Birthday, request.Country );
            return Results.Ok();
        }
        [HttpGet( "[action]" )]
        public async Task<IResult> CreateAuthor( CreateAuthorRequest request ) {
            await _authorService.CreateAuthor( request.FirstName, request.LastName, request.Birthday, request.Country );
            return Results.Ok();
        }
    }
}
