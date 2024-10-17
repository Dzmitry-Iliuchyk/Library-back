using Library.Domain.Interfaces.UserUseCases.DTO;

namespace Library.Domain.Interfaces.UserUseCases
{
    public interface IUpdateUserUseCase {
        Task Execute( UserDto userDto );
    }
}
