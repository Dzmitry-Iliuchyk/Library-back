using AutoMapper;
using Library.DataAccess.DataBase.Entities;
using Library.DataAccess.DataBase.Enums;
using Library.Domain.Models.Book;

namespace Library.DataAccess.AutoMapper {
    public class DataBaseMapping: Profile {
        public DataBaseMapping() {
            CreateMap<BookEntity, Book>()
       .ConvertUsing<BookEntityToBookConverter>();
            CreateMap<Book, BookEntity>()
               .ForMember( dest => dest.BookType, opt => opt.MapFrom( src => src is FreeBook ? BookType.Free : BookType.Taken ) )
               .ForMember( dest => dest.ClientId, opt => opt.MapFrom( src => src is TakenBook ? (int?)( src as TakenBook ).ClientId : null ) )
               .ForMember( dest => dest.TakenAt, opt => opt.MapFrom( src => src is TakenBook ? (DateTime?)( src as TakenBook ).TakenAt : null ) )
               .ForMember( dest => dest.ReturnTo, opt => opt.MapFrom( src => src is TakenBook ? (DateTime?)( src as TakenBook ).ReturnTo : null ) );
        }
    }
}
