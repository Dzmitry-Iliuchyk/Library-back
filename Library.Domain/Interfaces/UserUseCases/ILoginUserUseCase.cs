using Library.Domain.Interfaces.UserUseCases.DTO;

namespace Library.Domain.Interfaces.UserUseCases
{
    public interface ILoginUserUseCase {
        Task<AuthResponce> Execute( LoginModel loginModel );
    }
}
