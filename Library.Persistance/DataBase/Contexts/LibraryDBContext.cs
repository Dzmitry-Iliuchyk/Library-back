﻿using Bogus;
using Library.Application.Auth.Entities;
using Library.Application.Auth.Enums;
using Library.DataAccess.DataBase.Configuration;
using Library.DataAccess.DataBase.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Library.DataAccess.DataBase.Contexts {
    public class LibraryDBContext: DbContext {
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<AccessGroupEntity> Groups { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        private readonly AuthorizationOptions _authorizationOptions;
        public LibraryDBContext( DbContextOptions<LibraryDBContext> options, IOptions<AuthorizationOptions> authOptions ) : base( options ) {
            _authorizationOptions = authOptions.Value;
        }
        protected override void OnModelCreating( ModelBuilder modelBuilder ) {

            modelBuilder.Entity<BookEntity>().HasKey( x => x.Id );
            modelBuilder.Entity<AuthorEntity>().HasMany( x => x.Books ).WithOne( x => x.Author ).HasForeignKey( x => x.AuthorId ).OnDelete( DeleteBehavior.Restrict );
            modelBuilder.Entity<UserEntity>().HasMany( x => x.Books ).WithOne( x => x.User ).HasForeignKey( x => x.ClientId ).OnDelete( DeleteBehavior.Restrict );
            modelBuilder.Entity<UserEntity>().HasIndex( x => x.Email ).IsUnique( true );

            modelBuilder.Entity<RefreshToken>().HasOne<UserEntity>().WithMany().HasForeignKey( x => x.UserId ).OnDelete( DeleteBehavior.Cascade );

            AuthDbConfiguration.Configure( modelBuilder, _authorizationOptions );
            SeedUsers( modelBuilder );
            var authors = SeedAuthors( modelBuilder );
            SeedBooks( modelBuilder, authors );
            AddAdmin( modelBuilder );

        }

        private static void AddAdmin( ModelBuilder modelBuilder ) {
            var userId = Guid.NewGuid();
            var hasher = new PasswordHasher<UserEntity>();
            modelBuilder.Entity<UserEntity>().HasData( new UserEntity() {
                Id = userId,
                UserName = "admin",
                PasswordHash = hasher.HashPassword( null, "admin" ),
                Email = "admin@admin.com",

            }
            );
            var groups = Enum
            .GetValues<AccessGroupEnum>()
                .Select( x => new AccessGroupEntity {
                    Id = (int)x,
                    Name = x.ToString(),
                } );
            var userAccessGroups = groups.Select( g => new UserAccessGroup {
                UserId = userId,
                GroupId = g.Id
            } ).ToArray();
            modelBuilder.Entity<UserAccessGroup>().HasData( userAccessGroups );
        }

        private static void SeedBooks( ModelBuilder modelBuilder, List<AuthorEntity> authors ) {
            Faker faker;
            List<BookEntity> books = new List<BookEntity>();
            var rand = new Random();
            for (int i = 0; i < 50; i++) {
                faker = new Faker();
                var guid = Guid.NewGuid();
                var user = new BookEntity {
                    Id = guid,
                    AuthorId = authors[ rand.Next( 0, authors.Count() ) ].Id,
                    BookType = Enums.BookType.Free,
                    Description = faker.Lorem.Sentence(20,50),
                    ISBN = GenerateISBN13(),
                    Genre = faker.Lorem.Word(),
                    Title = faker.Lorem.Sentence(),
                };
                books.Add( user );
            }
            modelBuilder.Entity<BookEntity>().HasData( books );
        }

        private static List<AuthorEntity> SeedAuthors( ModelBuilder modelBuilder ) {
            Faker faker;
            List<AuthorEntity> authors = new List<AuthorEntity>();
            var rand = new Random();
            for (int i = 0; i < 5; i++) {
                faker = new Faker();
                var guid = Guid.NewGuid();
                var author = new AuthorEntity {
                    Id = guid,
                    Birthday = DateTime.UtcNow.AddYears( -rand.Next( 12, 120 ) ),
                    Country = faker.Address.Country(),
                    FirstName = faker.Person.FirstName,
                    LastName = faker.Person.LastName,
                };
                authors.Add( author );
            }
            modelBuilder.Entity<AuthorEntity>().HasData( authors );
            return authors;
        }

        private static void SeedUsers( ModelBuilder modelBuilder ) {
            Faker faker;
            List<UserEntity> users = new List<UserEntity>();
            var hasher = new PasswordHasher<UserEntity>();
            for (int i = 0; i < 5; i++) {
                faker = new Faker();
                var guid = Guid.NewGuid();
                var user = new UserEntity {
                    Id = guid,
                    Email = faker.Person.Email,
                    PasswordHash = hasher.HashPassword( null, guid.ToString() ),
                    UserName = faker.Person.UserName,
                    };
                users.Add( user );
            }
            var userGroup = new AccessGroupEntity() {
                Id = (int)AccessGroupEnum.User,
                Name = AccessGroupEnum.User.ToString(),
            };
            var groupUser = new List<UserAccessGroup>();
            users.ForEach( u => groupUser.Add( new() {
            GroupId = userGroup.Id,
            UserId = u.Id} ) );

            modelBuilder.Entity<UserEntity>().HasData( users );
            modelBuilder.Entity<UserAccessGroup>().HasData( groupUser );
        }
        public static string GenerateISBN13() {
            Random random = new Random();
            int[] isbn = new int[ 13 ];

            isbn[ 0 ] = 9;
            isbn[ 1 ] = 7;
            isbn[ 2 ] = random.Next( 8, 10 );

            for (int i = 3; i < 12; i++) {
                isbn[ i ] = random.Next( 0, 10 );
            }

            int sum = 0;
            for (int i = 0; i < 12; i++) {
                sum += isbn[ i ] * ( i % 2 == 0 ? 1 : 3 );
            }
            isbn[ 12 ] = ( 10 - ( sum % 10 ) ) % 10;

            return string.Join( "", isbn );
        }
    }
}
