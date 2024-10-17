using Library.Domain.Interfaces.BookUseCases.Dto;

namespace Library.Domain.Interfaces.AuthorUseCases.Dto {
    public record AuthorWithBooksDTO(Guid Id, string FirstName, string LastName, DateTime Birthday, string Country, IList<BookDto> Books);

}
