using Library.Domain.Interfaces.UserUseCases.DTO;

namespace Library.Domain.Interfaces.UserUseCases
{
    public interface ILoginByRefreshUseCase {
        Task<AuthResponce> Execute( string accessToken, string refreshToken );
    }
}
