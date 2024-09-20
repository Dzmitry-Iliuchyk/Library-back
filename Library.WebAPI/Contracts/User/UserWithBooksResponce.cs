using Library.Domain.Models.Book;

namespace Library.WebAPI.Contracts.User {
    public record UserWithBooksResponce(
        string guid,
    string name,
    string email,
    bool isAdmin,
    TakenBook[] books);
}
