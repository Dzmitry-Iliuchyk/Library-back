using System.ComponentModel.DataAnnotations;

namespace Library.WebAPI.Contracts.Book {
    public record CreateBookRequest(
        [Required] string ISBN,
        [Required] string Title,
        [Required] string Genre,
        [Required] string Description,
        [Required] Guid AuthorId );
}
