using Library.Domain.Interfaces.UserUseCases.DTO;

namespace Library.Domain.Interfaces.UserUseCases
{
    public interface IGetUsersUseCase {
        Task<IList<UserResponceDto>> Execute( int skip, int take );
    }
}
