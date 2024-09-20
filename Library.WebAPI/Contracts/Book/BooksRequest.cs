using System.ComponentModel.DataAnnotations;

namespace Library.WebAPI.Contracts.Book {
    public record BooksRequest (
       [Required] int skip,
       [Required] int take,
        string authorFilter,
        string titleFilter
    );
}
