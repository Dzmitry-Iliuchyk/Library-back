using Library.Application.Interfaces.UserUseCases.DTO;

namespace Library.Application.Interfaces.UserUseCases
{
    public interface ILoginByRefreshUseCase {
        Task<AuthResponce> Execute( string accessToken, string refreshToken );
    }
}
