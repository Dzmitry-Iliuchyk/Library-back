﻿using Library.Application.Auth.Enums;
using Library.DataAccess.DataBase.Configuration;
using Library.DataAccess.DataBase.Entities;
using Library.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace Library.DataAccess.DataBase.Contexts {
    public class LibraryDBContext: DbContext {
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<AccessGroupEntity> Groups { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }

        private readonly AuthorizationOptions _authorizationOptions;
        public LibraryDBContext( DbContextOptions<LibraryDBContext> options, IOptions<AuthorizationOptions> authOptions ) : base( options ) {
            _authorizationOptions = authOptions.Value;
        }
        protected override void OnModelCreating( ModelBuilder modelBuilder ) {

            modelBuilder.Entity<BookEntity>().HasKey( x => x.Id );
            modelBuilder.Entity<AuthorEntity>().HasMany( x => x.Books ).WithOne( x => x.Author ).HasForeignKey( x => x.AuthorId ).OnDelete( DeleteBehavior.Restrict );
            modelBuilder.Entity<UserEntity>().HasMany( x => x.Books ).WithOne( x => x.User ).HasForeignKey( x => x.ClientId ).OnDelete( DeleteBehavior.Restrict );
            modelBuilder.Entity<UserEntity>().HasIndex( x => x.Email ).IsUnique( true );
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
            List<BookEntity> books = new List<BookEntity>();
            var rand = new Random();
            for (int i = 0; i < 50; i++) {
                var guid = Guid.NewGuid();
                var user = new BookEntity {
                    Id = guid,
                    AuthorId = authors[ rand.Next( 0, authors.Count() ) ].Id,
                    BookType = Enums.BookType.Free,
                    Description = guid.ToString(),
                    ISBN = GenerateISBN13(),
                    Genre = $"{i % 10}",
                    Title = guid.ToString(),
                };
                books.Add( user );
            }
            modelBuilder.Entity<BookEntity>().HasData( books );
        }

        private static List<AuthorEntity> SeedAuthors( ModelBuilder modelBuilder ) {
            List<AuthorEntity> authors = new List<AuthorEntity>();
            var rand = new Random();
            for (int i = 0; i < 5; i++) {
                var guid = Guid.NewGuid();
                var author = new AuthorEntity {
                    Id = guid,
                    Birthday = DateTime.UtcNow.AddYears( -rand.Next( 12, 120 ) ),
                    Country = "Belarus",
                    FirstName = $"name{i}",
                    LastName = $"LastName{i}",
                };
                authors.Add( author );
            }
            modelBuilder.Entity<AuthorEntity>().HasData( authors );
            return authors;
        }

        private static void SeedUsers( ModelBuilder modelBuilder ) {
            List<UserEntity> users = new List<UserEntity>();
            var hasher = new PasswordHasher<UserEntity>();
            for (int i = 0; i < 5; i++) {
                var guid = Guid.NewGuid();
                var user = new UserEntity {
                    Id = guid,
                    Email = $"{guid}@test.test",
                    PasswordHash = hasher.HashPassword( null, guid.ToString() ),
                    UserName = $"user{i}",
                    Groups = new List<AccessGroupEntity>()
                };
                users.Add( user );
            }
            users.ForEach( x => x.Groups.Append( new AccessGroupEntity() {
                Id = (int)AccessGroupEnum.User,
                Name = AccessGroupEnum.User.ToString(),
            } ) );
            modelBuilder.Entity<UserEntity>().HasData( users );
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
