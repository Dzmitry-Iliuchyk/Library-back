using Microsoft.AspNetCore.Http;

namespace Library.Application.Interfaces.BookUseCases.Dto {
    public record BookCreateDto( string ISBN, string title, string genre, string description, Guid authorId, IFormFile image );
}