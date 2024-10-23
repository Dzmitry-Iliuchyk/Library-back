using Library.Application.Interfaces.UserUseCases.DTO;

namespace Library.Application.Interfaces.UserUseCases
{
    public interface IGetUserByIdUseCase {
        Task<UserResponceWithAdminDto> Execute( Guid id );
    } 
    public interface IGetUserWithBooksByIdUseCase {
        Task<UserWithBooksResponceDto> Execute( Guid id, int skip, int take );
    }
}
