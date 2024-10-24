using FluentValidation;
using Library.Application;
using Library.Application.Auth.Enums;
using Library.Application.Auth.Interfaces;
using Library.Application.Helpers;
using Library.Application.Implementations;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.Services;
using Library.Application.Mapper;
using Library.Application.Validator;
using Library.DataAccess.AutoMapper;
using Library.DataAccess.DataBase.Contexts;
using Library.DataAccess.Repository;
using Library.Domain.Models;
using Library.Domain.Models.Book;
using Library.Infrastracture;
using Library.Infrastracture.Auth;
using Library.Infrastracture.Jwt;
using Library.WebAPI.Mapper;
using Library.WebAPI.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Text;
using AuthorizationOptions = Library.DataAccess.DataBase.Configuration.AuthorizationOptions;

var builder = WebApplication.CreateBuilder( args );
var configuration = builder.Configuration;
var jwtOptions = configuration.GetRequiredSection( nameof( JwtOptions ) );
var authOptions = configuration.GetRequiredSection( nameof( AuthorizationOptions ) );
// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson( options => {
    options.SerializerSettings.Converters.Add( new StringEnumConverter() );
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
} );

builder.Services.Configure<ImageOptions>( configuration.GetSection( nameof( ImageOptions ) ) );
builder.Services.Configure<JwtOptions>( jwtOptions );
builder.Services.Configure<AuthorizationOptions>( authOptions );

builder.Services.AddDbContext<LibraryDBContext>( opt => {
    opt.UseNpgsql( configuration.GetConnectionString( nameof( LibraryDBContext ) ) );
} );

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddMemoryCache();

builder.Services.AddScoped<IValidator<User>, UserValidator>();
builder.Services.AddScoped<IValidator<Author>, AuthorValidator>();
builder.Services.AddScoped<IValidator<Book>, BookValidator>();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddUseCases();

builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddScoped<IAuthorizationHandler, GroupAuthorizationHandler>();
builder.Services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme )
                .AddJwtBearer( JwtBearerDefaults.AuthenticationScheme, options => {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters() {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        ClockSkew = TimeSpan.Zero,
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
    opt.AddPolicy( CustomPolicyNames.User, p => p.AddRequirements( new GroupRequirement( [ AccessGroupEnum.User ] ) ) );
} );
builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddCors( options => {
    options.AddPolicy( "AllowAngularOrigins",
    builder => {
        builder.WithOrigins( "http://localhost:4200" )
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
    } );
    options.AddPolicy( "AllowAll",
            builder => builder.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod());
} );
builder.Services.AddAutoMapper( typeof( DataBaseMappings ), typeof(WebApiMappings), typeof(DTOMapping) );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors( "AllowAngularOrigins" );
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
