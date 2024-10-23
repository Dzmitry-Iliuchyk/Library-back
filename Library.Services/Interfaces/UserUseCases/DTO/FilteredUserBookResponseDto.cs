using Library.Application.Interfaces.BookUseCases.Dto;

namespace Library.Application.Interfaces.UserUseCases.DTO
{
    public record FilteredUserBookResponseDto(IList<BookResponce> Books, int Total);
}
