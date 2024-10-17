using AutoMapper;
using Library.DataAccess.DataBase.Entities;
using Library.Domain.Models;
using Library.Domain.Models.Book;

namespace Library.DataAccess.AutoMapper {
    public class AuthorEntityToAuthorConverter: ITypeConverter<AuthorEntity, Author> {
        public Author Convert( AuthorEntity src, Author destination, ResolutionContext context ) {
            var books = context.Mapper.Map<IList<Book>>( src.Books );
            return new Author(src.Id, src.FirstName, src.LastName, src.Birthday, src.Country, books);
        }
    }
}
