namespace Library.Domain.Interfaces.UserUseCases.DTO {
    public record UserResponceWithAdminDto(Guid Id, string UserName, string Email, bool IsAdmin);
}
