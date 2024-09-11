using Library.DataAccess.DataBase.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder( args );
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<LibraryDBContext>(opt=> {
    opt.UseNpgsql(configuration.GetConnectionString(nameof( LibraryDBContext ) ), b=>b.MigrationsAssembly("Library.WebAPI"));
} );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
