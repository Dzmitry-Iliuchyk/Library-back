using FluentValidation;
using Library.Application.Helpers;
using Library.Application.Implementations;
using Library.Application.Interfaces;
using Library.Application.Validator;
using Library.DataAccess.AutoMapper;
using Library.DataAccess.DataBase.Contexts;
using Library.DataAccess.Repository;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Models.Book;
using Library.WebAPI.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder( args );
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<LibraryDBContext>(opt=> {
    opt.UseNpgsql(configuration.GetConnectionString(nameof( LibraryDBContext ) ));
} );
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddScoped<IValidator<User>, UserValidator>();
builder.Services.AddScoped<IValidator<Author>, AuthorValidator>();
builder.Services.AddScoped<IValidator<Book>, BookValidator>();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.Configure<ImageOptions>( configuration.GetSection( nameof( ImageOptions ) ) );
//builder.Services.Configure<BookValidationOptions>( configuration.GetSection( nameof( BookValidationOptions ) ) );

builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddAutoMapper( typeof( DataBaseMapping ) );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
