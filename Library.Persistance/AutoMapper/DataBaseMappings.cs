using AutoMapper;
using Library.DataAccess.DataBase.Entities;
using Library.DataAccess.DataBase.Enums;
using Library.Domain.Interfaces.UserUseCases.DTO;
using Library.Domain.Models;
using Library.Domain.Models.Book;
using System.Security.AccessControl;

namespace Library.DataAccess.AutoMapper {
    public class DataBaseMappings: Profile {
        public DataBaseMappings() {
            CreateMap<User, UserEntity>();
            CreateMap<UserEntity, User>()
                .ConvertUsing<UserEntityToUserConverter>();

            CreateMap<AuthorEntity, Author>().ReverseMap();
            CreateMap<AuthorEntity, Author>()
                .ConvertUsing<AuthorEntityToAuthorConverter>();
            CreateMap<BookEntity, Book>()
       .ConvertUsing<BookEntityToBookConverter>();
            CreateMap<Book, BookEntity>()
               //.ForMember( dest => dest.Author, opt => opt.MapFrom( src => src.Author ) )
               .ForMember( dest => dest.BookType, opt => opt.MapFrom( src => src is FreeBook ? BookType.Free : BookType.Taken ) )
               .ForMember( dest => dest.ClientId, opt => opt.MapFrom( src => src is TakenBook ? (Guid?)( src as TakenBook ).ClientId : null ) )
               .ForMember( dest => dest.TakenAt, opt => opt.MapFrom( src => src is TakenBook ? (DateTime?)( src as TakenBook ).TakenAt : null ) )
               .ForMember( dest => dest.ReturnTo, opt => opt.MapFrom( src => src is TakenBook ? (DateTime?)( src as TakenBook ).ReturnTo : null ) );
        }
    }
}
