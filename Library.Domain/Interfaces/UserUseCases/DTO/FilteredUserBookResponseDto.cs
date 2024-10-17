using Library.Domain.Interfaces.BookUseCases.Dto;

namespace Library.Domain.Interfaces.UserUseCases.DTO
{
    public record FilteredUserBookResponseDto(IList<BookResponce> Books, int Total);
}
