namespace Library.WebAPI.Contracts.User {
    public record UserResponce(
        string guid,
    string name,
    string email,
    bool isAdmin);
}
