namespace Library.Application.Interfaces.AuthorUseCases.Dto {
    public record AuthorDto(Guid Id, string FirstName, string LastName, DateTime Birthday, string Country);

}
