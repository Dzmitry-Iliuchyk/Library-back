using Library.Domain.Interfaces.UserUseCases.DTO;

namespace Library.Domain.Interfaces.UserUseCases
{
    public interface IRegisterUserUseCase {
        Task<AuthResponce> Execute( RegisterModel registerModel );
    }
}
