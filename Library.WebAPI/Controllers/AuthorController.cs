using AutoMapper;
using Library.Application.Interfaces.AuthorUseCases;
using Library.Application.Interfaces.AuthorUseCases.Dto;
using Library.Infrastracture;
using Library.WebAPI.Contracts.Authors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers {
    [Authorize]
    
    [Route( "api/[controller]" )]
    [ApiController]
    public class AuthorController: ControllerBase {
        
        private readonly IMapper _mapper;
        private readonly ICreateAuthorUseCase _createAuthorUseCase;
        private readonly IUpdateAuthorUseCase _updateAuthorUseCase;
        private readonly IDeleteAuthorUseCase _deleteAuthorUseCase;
        private readonly IGetAuthorsUseCase _getAuthorsUseCase;
        private readonly IGetAuthorWithBooksUseCase _getAuthorWithBooksUseCase;
        private readonly IGetBooksByAuthorUseCase _getBooksByAuthorUseCase;
    
        
        public AuthorController(
            IMapper mapper,
            ICreateAuthorUseCase createAuthorUseCase,
            IUpdateAuthorUseCase updateAuthorUseCase,
            IDeleteAuthorUseCase deleteAuthorUseCase,
            IGetAuthorsUseCase getAuthorsUseCase,
            IGetAuthorWithBooksUseCase getAuthorUseCase,
            IGetBooksByAuthorUseCase getBooksByAuthorUseCase ) {
            _createAuthorUseCase = createAuthorUseCase;
            _updateAuthorUseCase = updateAuthorUseCase;
            _deleteAuthorUseCase = deleteAuthorUseCase;
            _getAuthorsUseCase = getAuthorsUseCase;
            _getAuthorWithBooksUseCase = getAuthorUseCase;
            _getBooksByAuthorUseCase = getBooksByAuthorUseCase;
            _mapper = mapper;   
        }

        [HttpGet( "[action]" )]
        public async Task<IResult> GetAuthors( int skip, int take ) {
            var authors = await _getAuthorsUseCase.Execute( skip, take );
            return Results.Ok( authors );
        }
        [Authorize( Policy = CustomPolicyNames.CanRead )]
        [HttpGet( "{authorId}/getBooks" )]
        public async Task<IResult> GetAllBooksByAuthor( [FromRoute] Guid authorId, int skip, int take ) {
            var books = await _getBooksByAuthorUseCase.Execute( authorId, skip, take  );
            return Results.Ok( books );
        }
        [Authorize( Policy = CustomPolicyNames.CanRead )]
        [HttpGet( "{authorId}/get" )]
        public async Task<IResult> Get( [FromRoute] Guid authorId ) {
            var author = await _getAuthorWithBooksUseCase.Execute( authorId);
            return Results.Ok(author);
        }
        [HttpDelete( "{authorId}/delete" )]
        public async Task<IResult> DeleteAuthor( [FromRoute] Guid authorId ) {
            await _deleteAuthorUseCase.Execute( authorId );
            return Results.Ok();
        }
        [HttpPut( "[action]" )]
        public async Task<IResult> UpdateAuthor( UpdateAuthorRequest request) {
            await _updateAuthorUseCase.Execute( _mapper.Map<AuthorDto>(request));
            return Results.Ok();
        }
        [HttpPost( "[action]" )]
        public async Task<IResult> CreateAuthor( CreateAuthorRequest request) {
            await _createAuthorUseCase.Execute( _mapper.Map<CreateAuthorDTO>( request ) );
            return Results.Ok();
        }
    }
}
