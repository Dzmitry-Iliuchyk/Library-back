using Library.Application.Interfaces.AuthorUseCases.Dto;
using Library.Application.Interfaces.UserUseCases.DTO;

namespace Library.Application.Interfaces.BookUseCases.Dto {
    public class BookDto
    {
        public Guid Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
        public AuthorDto Author { get; set; }
    }
    public class FreeBookDto: BookDto {
    }
    public class TakenBookDto: BookDto {
        public Guid ClientId { get; set; }
        public UserDto Client { get; set; }
        public DateTime TakenAt { get; set; }
        public DateTime ReturnTo { get; set; }
    }
}