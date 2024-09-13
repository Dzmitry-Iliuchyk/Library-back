using System.ComponentModel.DataAnnotations;

namespace Library.WebAPI.Contracts.Authors {
    public record UpdateAuthorRequest([Required]Guid Id, [Required] string FirstName, [Required] string LastName, [Required] DateTime Birthday, [Required] string Country );
}
