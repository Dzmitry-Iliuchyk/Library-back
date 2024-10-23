
using Library.Application.Implementations.AuthorUseCases;
using Library.Application.Implementations.BookUseCases;
using Library.Application.Implementations.UserUseCases;
using Library.Application.Interfaces.AuthorUseCases;
using Library.Application.Interfaces.BookUseCases;
using Library.Application.Interfaces.UserUseCases;

using Microsoft.Extensions.DependencyInjection;

namespace Library.Application {
    public static class DependencyInjection {
        public static IServiceCollection AddUseCases( this IServiceCollection services ) {

            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            services.AddScoped<IGetUserGroupsUseCase, GetUserGroupsUseCase>();
            services.AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>();
            services.AddScoped<IGetUserBooksUseCase, GetUserBooksUseCase>();
            services.AddScoped<IGetUsersUseCase, GetUsersUseCase>();
            services.AddScoped<IGetFilteredUserBooksUseCase, GetFilteredserBooksUseCase>();
            services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
            services.AddScoped<ILoginByRefreshUseCase, LoginByRefreshUseCase>();
            services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IGetUserWithBooksByIdUseCase, GetUserWithBooksByIdUseCase>();

            services.AddScoped<ICreateAuthorUseCase, CreateAuthorUseCase>();
            services.AddScoped<IUpdateAuthorUseCase, UpdateAuthorUseCase>();
            services.AddScoped<IDeleteAuthorUseCase, DeleteAuthorUseCase>();
            services.AddScoped<IGetAuthorsUseCase, GetAuthorsUseCase>();
            services.AddScoped<IGetAuthorWithBooksUseCase, GetAuthorWithBooksUseCase>();
            services.AddScoped<IGetBooksByAuthorUseCase, GetBooksByAuthorUseCase>();

            services.AddScoped<IUpdateBookUseCase, UpdateBookUseCase>();
            services.AddScoped<IGiveBookToClientUseCase, GiveBookToClientUseCase>();
            services.AddScoped<IGetFilteredBooksUseCase, GetFilteredBooksUseCase>();
            services.AddScoped<IGetBookWithAuthorByIdUseCase, GetBookWithAuthorByIdUseCase>();
            services.AddScoped<IGetBookWithAllUseCase, GetBookWithAllUseCase>();
            services.AddScoped<IGetBooksUseCase, GetBooksUseCase>();
            services.AddScoped<IGetBookByISBNUseCase, GetBookByISBNUseCase>();
            services.AddScoped<IFreeBookUseCase, FreeBookUseCase>();
            services.AddScoped<IDeleteBookUseCase, DeleteBookUseCase>();
            services.AddScoped<ICreateBookUseCase, CreateBookUseCase>();

            return services;
        }
    }
}
