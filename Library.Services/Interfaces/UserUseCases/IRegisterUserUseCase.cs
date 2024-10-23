using Library.Application.Interfaces.UserUseCases.DTO;

namespace Library.Application.Interfaces.UserUseCases
{
    public interface IRegisterUserUseCase {
        Task<AuthResponce> Execute( RegisterModel registerModel );
    }
}
