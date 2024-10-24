﻿using AutoMapper;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.Services;
using Library.Application.Interfaces.BookUseCases.Dto;
using Library.Application.Interfaces.UserUseCases;
using Library.Application.Interfaces.UserUseCases.DTO;

namespace Library.Application.Implementations.UserUseCases {

    public class GetFilteredserBooksUseCase: IGetFilteredUserBooksUseCase {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public GetFilteredserBooksUseCase( IUnitOfWork unit, IMapper mapper, IImageService image ) {
            _unit = unit;
            _mapper = mapper;
            _imageService = image;
        }

        public async Task<FilteredUserBookResponseDto> Execute( int skip, int take, string? authorFilter, string? titleFilter, Guid userId ) {
            var result = await _unit.userRepository.GetFilteredBooksAsync( skip, take, authorFilter, titleFilter, userId );
            var booksResponces = _mapper.Map<IList<BookResponce>>( result.Item1 );
            foreach (var item in booksResponces) {
                item.Image = await _imageService.GetImageAsBase64( item.Id );
            }
            return new( booksResponces, result.Item2);
        }
    }
}