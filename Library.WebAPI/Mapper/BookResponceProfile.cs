using Library.DataAccess.AutoMapper;
using Library.DataAccess.DataBase.Entities;
using Library.Domain.Models.Book;
using Library.Domain.Models;
using AutoMapper;
using Library.WebAPI.Contracts.Book;

namespace Library.WebAPI.Mapper {
    public class BookResponceProfile: Profile {
        public BookResponceProfile() {
            CreateMap<Book, BooksResponce>()
               .ForMember( dest => dest.Id, opt => opt.MapFrom( src => src.Id ) )
               .ForMember( dest => dest.ISBN, opt => opt.MapFrom( src => src.ISBN ) )
               .ForMember( dest => dest.Title, opt => opt.MapFrom( src => src.Title ) )
               .ForMember( dest => dest.Genre, opt => opt.MapFrom( src => src.Genre ) )
               .ForMember( dest => dest.Description, opt => opt.MapFrom( src => src.Description ) )
               .ForMember( dest => dest.AuthorId, opt => opt.MapFrom( src => src.AuthorId ) )
               .ForMember( dest => dest.FirstName, opt => opt.MapFrom( src => src.Author.FirstName ) )
               .ForMember( dest => dest.LastName, opt => opt.MapFrom( src => src.Author.LastName ) )
               .ForMember( dest => dest.IsTaken, opt => opt.MapFrom( src => ( src is TakenBook ) ? true : false ) );
        }
    }
}
