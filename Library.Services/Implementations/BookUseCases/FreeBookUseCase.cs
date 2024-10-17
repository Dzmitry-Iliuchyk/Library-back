﻿using Library.Application.Interfaces.Repositories;
using Library.Domain.Interfaces.BookUseCases;
using Library.Domain.Models.Book;

namespace Library.Application.Implementations.BookUseCases {
    public class FreeBookUseCase: IFreeBookUseCase {
        private readonly IUnitOfWork _unit;
        public FreeBookUseCase( IUnitOfWork unit) {
            _unit = unit;
        }

        public async Task Execute( Guid bookId, Guid clientId ) {
            var book = await _unit.bookRepository.GetBookWithAllAsync( bookId );
            if (book is TakenBook taken && taken.ClientId == clientId) {
                var freeBook = new FreeBook( book );
                await _unit.bookRepository.UpdateAsync( freeBook );
                await _unit.Save();
            }
        }
    }
}
