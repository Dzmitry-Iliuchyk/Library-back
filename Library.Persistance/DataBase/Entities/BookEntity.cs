using Library.DataAccess.DataBase.Enums;
using Library.Domain.Models;

namespace Library.DataAccess.DataBase.Entities {
    public class BookEntity {
        public Guid Id { get; set; }
        public string ISBN { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid AuthorId { get; set; }
        public AuthorEntity Author { get; set; } = null!;
        public BookType BookType { get; set; }
        public Guid? ClientId { get; set; }
        public UserEntity? User { get; set; }
        public DateTime? TakenAt { get; set; }
        public DateTime? ReturnTo { get; set; }

    }
}
