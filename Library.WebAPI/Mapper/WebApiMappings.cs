using AutoMapper;
using Library.WebAPI.Contracts.Book;
using Library.Application.Interfaces.BookUseCases.Dto;
using Library.WebAPI.Contracts.Authors;
using Library.Application.Interfaces.AuthorUseCases.Dto;
using Library.Application.Interfaces.UserUseCases.DTO;
using Library.WebAPI.Contracts.User;

namespace Library.WebAPI.Mapper {
    public class WebApiMappings: Profile {
        public WebApiMappings() {
            CreateMap<CreateAuthorRequest, CreateAuthorDTO>()
                .ConstructUsing( src => new CreateAuthorDTO( src.FirstName, src.LastName, src.Birthday, src.Country ) );
            CreateMap<UpdateAuthorRequest, AuthorDto>()
                 .ConstructUsing( src => new AuthorDto(src.Id ,src.FirstName, src.LastName, src.Birthday, src.Country ) );
            CreateMap<CreateBookRequest, BookCreateDto>()
                .ConstructUsing( src => new BookCreateDto( src.ISBN, src.Title, src.Genre, src.Description, src.AuthorId, src.image ) );
           
            CreateMap<UpdateBookRequest, BookUpdateDto>()
                .ConstructUsing( src => new BookUpdateDto( src.BookId, src.ISBN, src.Title, src.Genre, src.Description, src.AuthorId, src.Image ) );
            CreateMap<RegisterUserRequest, RegisterModel>()
                .ConstructUsing( src => new RegisterModel( src.UserName, src.Email, src.Password) );
            CreateMap<LoginUserRequest, LoginModel>()
                .ConstructUsing( src => new LoginModel( src.Email, src.Password ) );
            CreateMap<UpdateUserRequest, UserDto>()
                .ConstructUsing( src => new UserDto(src.UserId, src.UserName, src.Email, src.Password ) );
            
        }
    }
}
