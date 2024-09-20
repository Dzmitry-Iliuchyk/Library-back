using System.ComponentModel.DataAnnotations;

namespace Library.WebAPI.Contracts.Book {
    public record BooksResponce (
        [Required] Guid id,
        [Required] string ISBN,
        [Required] string title,
        [Required] string genre,
        [Required] string description,
        [Required] Guid authorId,
        [Required] string firstName,
        [Required] string lastName,
        [Required] byte[] image,
        [Required] bool isTaken
    ); 
}
