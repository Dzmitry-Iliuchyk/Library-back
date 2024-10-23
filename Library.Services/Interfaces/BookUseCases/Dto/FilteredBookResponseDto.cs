namespace Library.Application.Interfaces.BookUseCases.Dto
{
    public record FilteredBookResponseDto(IList<BookResponce> Books, int Total);
}