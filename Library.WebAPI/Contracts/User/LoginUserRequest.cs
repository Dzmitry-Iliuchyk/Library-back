using System.ComponentModel.DataAnnotations;

namespace Library.WebAPI.Contracts.User {
    public record LoginUserRequest(
    string Password,
    string Email);

}
