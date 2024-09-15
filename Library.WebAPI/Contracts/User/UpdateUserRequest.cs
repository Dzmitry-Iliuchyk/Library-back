using System.ComponentModel.DataAnnotations;

namespace Library.WebAPI.Contracts.User {
    public record UpdateUserRequest(
    [Required] Guid UserId,
    [Required] string UserName,
    [Required] string Password,
    [Required] string Email);

}
