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
        public async Task<IResult> GetAllAuthors( CancellationToken token = default ) {
            var authors = await _authorService.GetAllAuthors( token );
            return Results.Ok( authors );
        }
        [HttpGet( "{authorId}/getBooks" )]
        public async Task<IResult> GetAllBooksByAuthor( [FromRoute] Guid authorId, CancellationToken token = default ) {
            var books = await _authorService.GetAllBooks( authorId,token  );
            return Results.Ok( books );
        }
        [HttpGet( "{authorId}/get" )]
        public async Task<IResult> Get( [FromRoute] Guid authorId, CancellationToken token = default ) {
            await _authorService.GetAuthor( authorId,  token );
            return Results.Ok();
        }
        [HttpDelete( "{authorId}/delete" )]
        public async Task<IResult> DeleteAuthor( [FromRoute] Guid authorId, CancellationToken token = default ) {
            await _authorService.DeleteAuthor( authorId,  token  );
            return Results.Ok();
        }
        [HttpPut( "[action]" )]
        public async Task<IResult> UpdateAuthor( UpdateAuthorRequest request, CancellationToken token = default ) {
            await _authorService.UpdateAuthor( request.Id, request.FirstName, request.LastName, request.Birthday, request.Country,  token  );
            return Results.Ok();
        }
        [HttpPost( "[action]" )]
        public async Task<IResult> CreateAuthor( CreateAuthorRequest request, CancellationToken token = default ) {
            await _authorService.CreateAuthor( request.FirstName, request.LastName, request.Birthday, request.Country,  token  );
            return Results.Ok();
        }
    }
}
