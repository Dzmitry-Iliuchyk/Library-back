using Microsoft.AspNetCore.Http;

namespace Library.Application.Interfaces.BookUseCases.Dto {
    public record BookUpdateDto(Guid BookId, string ISBN, string Title, string Genre, string Description, Guid AuthorId, IFormFile Image );
}