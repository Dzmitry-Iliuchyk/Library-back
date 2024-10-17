using System.ComponentModel.DataAnnotations;

namespace Library.WebAPI.Contracts.Book {
    public record UpdateBookRequest(
        Guid BookId,
        string ISBN,
        string Title,
        string Genre,
        string Description,
        Guid AuthorId ,
        IFormFile? Image);
}
