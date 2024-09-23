using System.ComponentModel.DataAnnotations;

namespace Library.WebAPI.Contracts.Book {
    public record CreateBookRequest(
        string ISBN,
        string Title,
        string Genre,
        string Description,
        Guid AuthorId,
        IFormFile image );
}
