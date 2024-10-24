﻿using AutoMapper;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.BookUseCases.Dto;
using Library.Application.Interfaces.UserUseCases;

namespace Library.Application.Implementations.UserUseCases {
    public class GetUserBooksUseCase: IGetUserBooksUseCase {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetUserBooksUseCase( IUnitOfWork unit, IMapper mapper ) {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<IList<TakenBookDto>> Execute( Guid userId, int skip, int take ) {
            return _mapper.Map<IList<TakenBookDto>>(await _unit.userRepository.GetBooksAsync( userId, skip, take ));
        }
    }
}