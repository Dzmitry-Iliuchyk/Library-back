namespace Library.Domain.Interfaces.UserUseCases {
    public interface IGetUserGroupsUseCase {
        Task<List<string>> Execute( Guid id );
    }
}
