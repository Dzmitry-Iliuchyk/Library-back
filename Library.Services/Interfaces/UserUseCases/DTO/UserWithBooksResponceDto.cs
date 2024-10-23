using Library.Application.Interfaces.BookUseCases.Dto;

namespace Library.Application.Interfaces.UserUseCases.DTO {
    public record UserWithBooksResponceDto(Guid Id, string UserName, string Email,bool IsAdmin, IList<TakenBookDto> Books);
}
