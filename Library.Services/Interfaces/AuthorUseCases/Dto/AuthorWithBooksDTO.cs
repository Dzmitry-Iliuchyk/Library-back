using Library.Application.Interfaces.BookUseCases.Dto;

namespace Library.Application.Interfaces.AuthorUseCases.Dto {
    public record AuthorWithBooksDTO(Guid Id, string FirstName, string LastName, DateTime Birthday, string Country, IList<BookDto> Books);

}
