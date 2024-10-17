namespace Library.Domain.Interfaces.AuthorUseCases.Dto {
    public record AuthorDto(Guid Id, string FirstName, string LastName, DateTime Birthday, string Country);

}
