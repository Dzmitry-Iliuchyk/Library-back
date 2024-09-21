using System.ComponentModel.DataAnnotations;

namespace Library.WebAPI.Contracts.User {
    public record RegisterUserRequest(
    string UserName,
    string Email,
    string Password);

}
