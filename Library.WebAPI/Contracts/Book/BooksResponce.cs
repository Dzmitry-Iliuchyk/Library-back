using System.ComponentModel.DataAnnotations;

namespace Library.WebAPI.Contracts.Book {
    public class BooksResponce {
        public Guid Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public bool IsTaken { get; set; }
    }
}
