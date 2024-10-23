using Library.Application.Interfaces.UserUseCases.DTO;

namespace Library.Application.Interfaces.UserUseCases
{
    public interface ILoginUserUseCase {
        Task<AuthResponce> Execute( LoginModel loginModel );
    }
}
