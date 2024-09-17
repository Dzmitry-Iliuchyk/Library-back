using FluentValidation;
using Library.Application.Auth.Enums;
using Library.Application.Auth.Interfaces;
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
using Library.Infrastracture;
using Library.Infrastracture.Auth;
using Library.Infrastracture.Jwt;
using Library.WebAPI.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AuthorizationOptions = Library.DataAccess.DataBase.Configuration.AuthorizationOptions;

var builder = WebApplication.CreateBuilder( args );
var configuration = builder.Configuration;
var jwtOptions = configuration.GetRequiredSection( nameof( JwtOptions ) );
var authOptions = configuration.GetRequiredSection( nameof( AuthorizationOptions ) );
// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<ImageOptions>( configuration.GetSection( nameof( ImageOptions ) ) );
builder.Services.Configure<JwtOptions>( jwtOptions );
builder.Services.Configure<AuthorizationOptions>( authOptions );

builder.Services.AddDbContext<LibraryDBContext>( opt => {
    opt.UseNpgsql( configuration.GetConnectionString( nameof( LibraryDBContext ) ) );
} );

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ITokenService, TokenService>();


builder.Services.AddScoped<IValidator<User>, UserValidator>();
builder.Services.AddScoped<IValidator<Author>, AuthorValidator>();
builder.Services.AddScoped<IValidator<Book>, BookValidator>();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddScoped<IAuthorizationHandler, GroupAuthorizationHandler>();
builder.Services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme )
                .AddJwtBearer( JwtBearerDefaults.AuthenticationScheme, options => {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters() {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(
                           jwtOptions.GetValue<string>( nameof( JwtOptions.Secret ) ) ) )
                    };

                    options.Events = new JwtBearerEvents {
                        OnMessageReceived = ( context ) => {
                            context.Token = context.Request.Cookies[ "Auth-Cookies" ];
                            return Task.CompletedTask;
                        }
                    };
                } );
builder.Services.AddAuthorization( opt => {
    opt.AddPolicy( CustomPolicyNames.CanRead, p => p.AddRequirements( new PermissionRequirement( [ PermissionEnum.Read ] ) ) );
    opt.AddPolicy( CustomPolicyNames.Admin, p => p.AddRequirements( new GroupRequirement( [ AccessGroupEnum.Admin ] ) ) );
   } );
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
