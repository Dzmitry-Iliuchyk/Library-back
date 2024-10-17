namespace Library.Domain.Interfaces.BookUseCases.Dto
{
    public record FilteredBookResponseDto(IList<BookResponce> Books, int Total);
}