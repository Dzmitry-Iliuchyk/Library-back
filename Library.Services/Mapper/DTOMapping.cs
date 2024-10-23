using AutoMapper;
using Library.Application.Interfaces.AuthorUseCases.Dto;
using Library.Application.Interfaces.BookUseCases.Dto;
using Library.Application.Interfaces.UserUseCases.DTO;
using Library.Domain.Models;
using Library.Domain.Models.Book;

namespace Library.Application.Mapper {
    public class DTOMapping: Profile {
        public DTOMapping() {
            CreateMap<Book, BookDto>().Include<FreeBook, FreeBookDto>().Include<TakenBook, TakenBookDto>();
            CreateMap<FreeBook, FreeBookDto>();
            CreateMap<TakenBook, TakenBookDto>();
            CreateMap<BookCreateDto, FreeBook>()
                .ConstructUsing(src=> new FreeBook( Guid.NewGuid(),src.title,src.genre,src.description,src.ISBN,src.authorId, null));
            CreateMap<Author, AuthorDto>()
                .ConstructUsing(src=>new AuthorDto(src.Id, src.FirstName, src.LastName, src.Birthday, src.Country));
            CreateMap<Author, AuthorWithBooksDTO>()
                .ConvertUsing<AuthorToAuthorWithBooksConverter>();
            CreateMap<User, UserResponceDto>()
                .ConstructUsing(src=>new UserResponceDto(src.Id,src.UserName,src.Email));

            CreateMap<BookDto, Book>().Include<FreeBookDto, FreeBook>().Include<TakenBookDto, TakenBook>();

            CreateMap<FreeBookDto, FreeBook>()
                .ConvertUsing<FreeBookDtoToFreeBookConverter>();

            CreateMap<TakenBookDto, TakenBook>()
                .ConvertUsing<TakenBookDtoToTakenBookConverter>();
            CreateMap<AuthorDto, Author>()
                .ConvertUsing<AuthorDtoToAuthorConverter>();
            CreateMap<AuthorWithBooksDTO, Author>()
                .ConvertUsing<AuthorWithBooksDTOToAuthorConverter>();

            CreateMap<Book, BookResponce>()
               .ForMember( dest => dest.Id, opt => opt.MapFrom( src => src.Id ) )
               .ForMember( dest => dest.ISBN, opt => opt.MapFrom( src => src.ISBN ) )
               .ForMember( dest => dest.Title, opt => opt.MapFrom( src => src.Title ) )
               .ForMember( dest => dest.Genre, opt => opt.MapFrom( src => src.Genre ) )
               .ForMember( dest => dest.Description, opt => opt.MapFrom( src => src.Description ) )
               .ForMember( dest => dest.AuthorId, opt => opt.MapFrom( src => src.AuthorId ) )
               .ForMember( dest => dest.FirstName, opt => opt.MapFrom( src => src.Author.FirstName ) )
               .ForMember( dest => dest.LastName, opt => opt.MapFrom( src => src.Author.LastName ) )
               .ForMember( dest => dest.IsTaken, opt => opt.MapFrom( src => ( src is TakenBook ) ? true : false ) )
               .ForMember( dest => dest.ClientId, opt => opt.MapFrom( src => src is TakenBook ? (Guid?)( src as TakenBook ).ClientId : null ) )
               .ForMember( dest => dest.Username, opt => opt.MapFrom( src => src is TakenBook ? (string?)( src as TakenBook ).Client.UserName : null ) );
        }
    }
    public class FreeBookDtoToFreeBookConverter: ITypeConverter<FreeBookDto, FreeBook> {
        public FreeBook Convert( FreeBookDto src, FreeBook destination, ResolutionContext context ) {
            var author = context.Mapper.Map<Author>( src.Author );
            return new FreeBook( src.Id, src.Title, src.Genre, src.Description, src.ISBN, src.AuthorId, author );
        }
    }

    public class TakenBookDtoToTakenBookConverter: ITypeConverter<TakenBookDto, TakenBook> {

        public TakenBook Convert( TakenBookDto src, TakenBook destination, ResolutionContext context ) {
            var author = context.Mapper.Map<Author>( src.Author );

            return new TakenBook( src.Id, src.ClientId, src.Title, src.Genre, src.Description, src.ISBN, src.AuthorId, src.TakenAt, src.ReturnTo, author );
        }
    }
    public class AuthorDtoToAuthorConverter: ITypeConverter<AuthorDto, Author> {
        public Author Convert( AuthorDto src, Author destination, ResolutionContext context ) {
            return new Author( src.Id, src.FirstName, src.LastName, src.Birthday, src.Country );
        }
    }
    public class AuthorWithBooksDTOToAuthorConverter: ITypeConverter<AuthorWithBooksDTO, Author> {
        public Author Convert( AuthorWithBooksDTO src, Author destination, ResolutionContext context ) {
            var books = context.Mapper.Map<IList<Book>>( src.Books );
            return new Author( src.Id, src.FirstName, src.LastName, src.Birthday, src.Country, books );
        }
    }
    public class AuthorToAuthorWithBooksConverter: ITypeConverter<Author,AuthorWithBooksDTO > {
        public AuthorWithBooksDTO Convert( Author src, AuthorWithBooksDTO  destination, ResolutionContext context ) {
            var books = context.Mapper.Map<IList<BookDto>>( src.Books );
            return new AuthorWithBooksDTO( src.Id, src.FirstName, src.LastName, src.Birthday, src.Country, books );
        }

    }
}
