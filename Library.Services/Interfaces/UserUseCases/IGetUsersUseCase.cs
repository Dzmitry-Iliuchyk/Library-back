using Library.Application.Interfaces.UserUseCases.DTO;

namespace Library.Application.Interfaces.UserUseCases
{
    public interface IGetUsersUseCase {
        Task<IList<UserResponceDto>> Execute( int skip, int take );
    }
}
