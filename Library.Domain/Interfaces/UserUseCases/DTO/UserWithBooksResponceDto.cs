using Library.Domain.Interfaces.BookUseCases.Dto;

namespace Library.Domain.Interfaces.UserUseCases.DTO {
    public record UserWithBooksResponceDto(Guid Id, string UserName, string Email,bool IsAdmin, IList<TakenBookDto> Books);
}
