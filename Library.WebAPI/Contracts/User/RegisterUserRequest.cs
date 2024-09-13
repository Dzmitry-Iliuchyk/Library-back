using System.ComponentModel.DataAnnotations;

namespace Library.WebAPI.Contracts.User
{
    public record RegisterUserRequest(
    [Required] string UserName,
    [Required] string Password,
    [Required] string Email);

}
