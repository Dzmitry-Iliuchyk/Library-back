using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.Services;
using Library.Domain.Interfaces.BookUseCases;

namespace Library.Application.Implementations.BookUseCases {
    
    public class DeleteBookUseCase: IDeleteBookUseCase {
        private readonly IUnitOfWork _unit;
        private readonly IImageService _imageService;

        public DeleteBookUseCase( IUnitOfWork unit, IImageService imageService ) {
            _unit = unit;
            _imageService = imageService;
        }

        public async Task Execute( Guid bookId ) {
            _unit.CreateTransaction();
            var book = await _unit.bookRepository.GetAsync( bookId );
            await _unit.bookRepository.DeleteAsync( book );
            _imageService.DeleteImage( bookId ); 
            await _unit.Save();
        }
    }
}
