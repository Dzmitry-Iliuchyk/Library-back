using Library.Application.Interfaces.Repositories;
using Library.Domain.Interfaces.UserUseCases;

namespace Library.Application.Implementations.UserUseCases {
    public class DeleteUserUseCase: IDeleteUserUseCase {
        private readonly IUnitOfWork _unit;

        public DeleteUserUseCase( IUnitOfWork unit ) {
            _unit = unit;
        }

        public async Task Execute( Guid userId ) {
            var user = await _unit.userRepository.GetAsync( userId );
            await _unit.userRepository.DeleteAsync( user );
            await _unit.Save();
        }
    }
}