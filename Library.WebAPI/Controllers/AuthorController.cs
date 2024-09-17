using Library.Domain.Interfaces;
using Library.Infrastracture;
using Library.WebAPI.Contracts.Authors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers {
    [Authorize( Policy = CustomPolicyNames.Admin )]
    [Route( "api/[controller]" )]
    [ApiController]
    public class AuthorController: ControllerBase {
        private readonly IAuthorService _authorService;
        public AuthorController( IAuthorService authorService ) {
            _authorService = authorService;
           
        }
        [Authorize( Policy = CustomPolicyNames.CanRead )]
        [HttpGet( "[action]" )]
        public async Task<IResult> GetAuthors( int skip, int take ) {
            var authors = await _authorService.GetAuthorsAsync( skip, take );
            return Results.Ok( authors );
        }
        [Authorize( Policy = CustomPolicyNames.CanRead )]
        [HttpGet( "{authorId}/getBooks" )]
        public async Task<IResult> GetAllBooksByAuthor( [FromRoute] Guid authorId, int skip, int take ) {
            var books = await _authorService.GetBooksAsync( authorId, skip, take  );
            return Results.Ok( books );
        }
        [Authorize( Policy = CustomPolicyNames.CanRead )]
        [HttpGet( "{authorId}/get" )]
        public async Task<IResult> Get( [FromRoute] Guid authorId ) {
            var author = await _authorService.GetAuthorAsync( authorId);
            return Results.Ok(author);
        }
        [HttpDelete( "{authorId}/delete" )]
        public async Task<IResult> DeleteAuthor( [FromRoute] Guid authorId ) {
            await _authorService.DeleteAuthorAsync( authorId );
            return Results.Ok();
        }
        [HttpPut( "[action]" )]
        public async Task<IResult> UpdateAuthor( UpdateAuthorRequest request) {
            await _authorService.UpdateAuthorAsync( request.Id, request.FirstName, request.LastName, request.Birthday, request.Country );
            return Results.Ok();
        }
        [HttpPost( "[action]" )]
        public async Task<IResult> CreateAuthor( CreateAuthorRequest request) {
            await _authorService.CreateAuthorAsync( request.FirstName, request.LastName, request.Birthday, request.Country  );
            return Results.Ok();
        }
    }
}
