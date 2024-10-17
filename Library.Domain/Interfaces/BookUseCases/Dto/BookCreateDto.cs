using Microsoft.AspNetCore.Http;

namespace Library.Domain.Interfaces.BookUseCases.Dto {
    public record BookCreateDto( string ISBN, string title, string genre, string description, Guid authorId, IFormFile image );
}