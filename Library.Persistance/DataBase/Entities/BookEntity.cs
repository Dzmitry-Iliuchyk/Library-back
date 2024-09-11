using Library.DataAccess.DataBase.Enums;
using Library.Domain.Models;

namespace Library.DataAccess.DataBase.Entities {
    public class BookEntity {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public Author Author { get; set; }
        public BookType BookType { get; set; }
        public int? ClientId { get; set; }
        public DateTime? TakenAt { get; set; }
        public DateTime? ReturnTo { get; set; }

    }
}
