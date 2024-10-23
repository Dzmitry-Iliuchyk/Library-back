using Library.Application.Interfaces.UserUseCases.DTO;

namespace Library.Application.Interfaces.UserUseCases
{
    public interface IUpdateUserUseCase {
        Task Execute( UserDto userDto );
    }
}
