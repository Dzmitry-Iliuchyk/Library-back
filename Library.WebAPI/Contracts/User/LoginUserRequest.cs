using System.ComponentModel.DataAnnotations;

namespace Library.WebAPI.Contracts.User {
    public record LoginUserRequest(
    [Required] string Password,
    [Required] string Email);

}
