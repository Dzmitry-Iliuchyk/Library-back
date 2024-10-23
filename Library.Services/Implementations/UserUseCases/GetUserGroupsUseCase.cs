using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.UserUseCases;

namespace Library.Application.Implementations.UserUseCases {
    public class GetUserGroupsUseCase: IGetUserGroupsUseCase {
        private readonly IUnitOfWork _unit;

        public GetUserGroupsUseCase( IUnitOfWork unit ) {
            _unit = unit;
        }

        public async Task<List<string>> Execute( Guid id ) {
            var groups = await _unit.authRepository.GetUserGroups( id );
            return groups.Select( x => x.ToString() ).ToList();
        }
    }
}