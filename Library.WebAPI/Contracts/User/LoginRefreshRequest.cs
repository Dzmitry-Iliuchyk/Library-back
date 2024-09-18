using System.ComponentModel.DataAnnotations;

namespace Library.WebAPI.Contracts.User {
    public record LoginRefreshRequest(
    [Required] string accessToken,
    [Required] string refreshToken);
}
