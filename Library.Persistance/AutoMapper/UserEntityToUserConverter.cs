using AutoMapper;
using Library.DataAccess.DataBase.Entities;
using Library.Domain.Models;
using Library.Domain.Models.Book;

namespace Library.DataAccess.AutoMapper {
    public class UserEntityToUserConverter: ITypeConverter<UserEntity, User> {

        public User Convert( UserEntity src, User destination, ResolutionContext context ) {
                var books = context.Mapper.Map<IList<Book>>( src.Books );
                return new User( src.Id, src.UserName, src.PasswordHash, src.Email, books);
        }
    }
}
